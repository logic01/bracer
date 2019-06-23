﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using PR.Constants.Enums;
using PR.Constants.Extensions;
using PR.Export.Utility;
using PR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PR.Export
{

    public class DocumentGenerator : IDocumentGenerator
    {
        private const string ExamNote = "PR.Export.Documents.BLANK_EXAM_NOTE.docx";

        /// <summary>
        /// Take all of the intake forms and replace the properties on the word doc, then append all of the
        /// intake forms
        /// </summary>
        /// <param name="intakeForms"></param>
        /// <returns></returns>
        public byte[] GenerateIntakeDocuments(
            IntakeFormModel intakeForm,
            PatientModel patient,
            PhysicianModel physician,
            ICollection<SignatureModel> signatures)
        {
            MemoryStream examNoteMemoryStream = LoadMemoryStream();

            using (var doc = WordprocessingDocument.Open(examNoteMemoryStream, true))
            {
                Body docBody = doc.MainDocumentPart.Document.Body;

                // create and add the character style with the style id, style name, and
                // aliases specified.
                var answerFormatStyleId = CreateIntakeFormAnswersCharStyle(doc);

                SetSignatures(doc, signatures);

                // Create title and add the subsequent question answers for the questionaire
                AppendTitleForIntakeForm(doc, docBody, intakeForm);

                var questionCount = 1;
                foreach (QuestionModel question in intakeForm.Questions)
                {
                    questionCount = AppendQuestionAnswerPair(docBody, answerFormatStyleId, questionCount, question);
                }

                docBody.AppendChild(new Paragraph());

                // Manual mapping bits
                UpdateValuesInWordDocsCustomProperties(doc, intakeForm, patient, physician, signatures);
            }

            var result = examNoteMemoryStream.ToArray();
            examNoteMemoryStream.Flush();
            examNoteMemoryStream.Close();

            return result;
        }

        private void SetSignatures(WordprocessingDocument doc, ICollection<SignatureModel> signatures)
        {
            // replace the images with the Signature file. There are 2 signature images
            // that are replaced in order. If there are multiple signatures you can
            // place them below
            foreach (ImagePart imagePart in doc.MainDocumentPart.ImageParts)
            {

                byte[] signatureImage = null;

                if (imagePart.Uri.ToString().Contains("image1"))
                {
                    signatureImage = signatures.First(s => s.Type == SignatureType.PrescriptionDocument).ContentBytes;
                }
                else
                {
                    signatureImage = signatures.First(s => s.Type == SignatureType.IntakeDocument).ContentBytes;
                }
                using (var imageStream = imagePart.GetStream())
                using (var writer = new BinaryWriter(imageStream))
                {
                    writer.Write(signatureImage);                    
                }
            }
        }
        /// <summary>
        /// The field values in the word doc need to be update in the property settings object of the word doc. The questions
        /// have 'keys' which we are using with MappingsEnum to know how update with the value from the IntakeForms. Then we
        /// explicitly ask the user to verify the fields are to be updated when opening the word doc.
        /// </summary>
        /// <param name="intakeForms"></param>
        /// <param name="doc"></param>
        private void UpdateValuesInWordDocsCustomProperties(
            WordprocessingDocument doc,
            IntakeFormModel intakeForm,
            PatientModel patient,
            PhysicianModel physician,
            ICollection<SignatureModel> signatures)
        {
            //https://docs.microsoft.com/en-us/office/open-xml/how-to-set-a-custom-property-in-a-word-processing-document
            Properties properties = doc.CustomFilePropertiesPart.Properties;

            // Get all question's with a key, then gather the value as all answers comma delimited              
            var intakeFromKeys = intakeForm.Questions
                .Where(r => !string.IsNullOrEmpty(r.Key))
                .Select(y => new KeyValuePair<string, string>(y.Key, y.Answers.Select(z => z.Text)
                .Aggregate((c, n) => $"{c},{n}"))).ToList();

            intakeFromKeys.AddRange(GetPatientKeys(patient));
            intakeFromKeys.AddRange(GetAllCodes(intakeForm));
            intakeFromKeys.AddRange(GetPhysicanKeys(physician));
            intakeFromKeys.AddRange(GetSignature(signatures.First())); // just use the first signature for now since IP/Creation should be identicalish
            intakeFromKeys.AddRange(GetDrNotes(intakeForm.PhysicianNotes ?? ""));

            //This will update all of the custom properties that are used in the word doc.
            //Again, the fields are update in the document settings, but the downloading user
            //will need to approve the update for any fields.
            foreach (MappingEnums propertyEnum in Enum.GetValues(typeof(MappingEnums)))
            {
                var item = (CustomDocumentProperty)properties
                    .FirstOrDefault(x => ((CustomDocumentProperty)x).Name.Value.Equals(propertyEnum.ToString()));
                if (item != null)
                {
                    //If a key doesn't exist, you could see an empty value stuffed into the word doc
                    var val = intakeFromKeys.FirstOrDefault(x => x.Key == propertyEnum.ToString()).Value ?? "N/A";
                    item.VTLPWSTR = new VTLPWSTR(val);
                }
            }

            properties.Save();

            //The docx is using Custom Properties and above we are updating the custom property values,
            //however there is no way (that I have found) to programatically updated all of the fields
            //that are using the custom properties without requiring the downloader to 
            DocumentSettingsPart settingsPart = doc.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().First();
            var updateFields = new UpdateFieldsOnOpen
            {
                Val = new OnOffValue(true)
            };
            settingsPart.Settings.PrependChild(updateFields);
            settingsPart.Settings.Save();
            doc.Save();
        }

        /// <summary>
        /// The word doc has a different font color for the answers to questions
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static string CreateIntakeFormAnswersCharStyle(WordprocessingDocument doc)
        {
            var answerFormatStyleId = "AnswerStyleChar";
            var grayText = new Color() { Val = "888888" };
            Formatting.CreateAndAddCharacterStyle(doc,
                answerFormatStyleId,
                "Answer Style Char",
                "Answer Gray", "AnswerStylePar", new List<OpenXmlElement> { grayText });
            return answerFormatStyleId;
        }

        /// <summary>
        /// This will create the question answer pair and append it to the doc
        /// 
        /// It will write something like below
        /// What is your Height? 5'7
        /// </summary>
        /// <param name="docBody"></param>
        /// <param name="answerFormatStyleId"></param>
        /// <param name="count"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        private static int AppendQuestionAnswerPair(Body docBody, string answerFormatStyleId, int count, QuestionModel question)
        {
            // Get the answer text from the question, then create how they will be displayed and create the paragraphs
            var answerText = string.Join(',', question.Answers.Select(x => x.Text == "" ? "Not Applicable" : x.Text));
            var questionText = $"{count++}. {question.Text} ";
            var answerParagraph = new Paragraph(new Run(new Text(questionText) { Space = SpaceProcessingModeValues.Preserve }));
            Run answerRun = answerParagraph.AppendChild<Run>(new Run(new Text(answerText) { Space = SpaceProcessingModeValues.Preserve }));

            // If the Run has no RunProperties object, create one.
            if (answerRun.Elements<RunProperties>().Count() == 0)
            {
                answerRun.PrependChild<RunProperties>(new RunProperties());
            }

            // Get a reference to the RunProperties.
            RunProperties rPr = answerRun.RunProperties;

            // Set the character style of the run.
            if (rPr.RunStyle == null)
            {
                rPr.RunStyle = new RunStyle();
            }

            rPr.RunStyle.Val = answerFormatStyleId;
            docBody.AppendChild(answerParagraph);
            return count;
        }

        private void AppendTitleForIntakeForm(WordprocessingDocument doc, Body docBody, IntakeFormModel intakeForm)
        {
            docBody.AppendChild(new Paragraph());

            Paragraph titleParagraph = CreateIntakeFormTitle(intakeForm.IntakeFormType);
            if (titleParagraph != null)
            {
                Formatting.AddNewStyle(doc, "PRTitle1", "PRTitle", titleParagraph);
                ParagraphProperties paragraphProperties = titleParagraph.PrependChild(new ParagraphProperties());
                docBody.AppendChild(titleParagraph);
            }
            docBody.AppendChild(new Paragraph());
        }

        #region Manual Mappings
        /// <summary>
        /// Some of the values in the forms are populated directly from the patient object. That is where
        /// these mapping keys are being loaded from
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> GetPatientKeys(PatientModel patient)
        {
            var kvps = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(MappingEnums.DOB.ToString(), patient.DateOfBirth.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>(MappingEnums.Age.ToString(), patient.DateOfBirth.GetAge()),
                new KeyValuePair<string, string>(MappingEnums.PatientName.ToString(), $"{patient.FirstName} {patient.LastName}"),
                new KeyValuePair<string, string>(MappingEnums.Phone.ToString(), patient.PhoneNumber),
                new KeyValuePair<string, string>(MappingEnums.Gender.ToString(), patient.Sex.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Waist.ToString(), patient.Waist.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Weight.ToString(), patient.Weight.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Height.ToString(), patient.Height.ToString()),
                new KeyValuePair<string, string>(MappingEnums.ShoeSize.ToString(), patient.ShoeSize.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Allergies.ToString(), patient.Allergies.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Insurance.ToString(), patient.Insurance.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Address.ToString(), patient.Address.ToString()),
                new KeyValuePair<string, string>(MappingEnums.ServiceDate.ToString(), DateTime.Now.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>(MappingEnums.MedMemberId.ToString(), patient.Medicare?.MemberId ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.MedPatientGroup.ToString(), patient.Medicare?.PatientGroup ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.MedPCN.ToString(), patient.Medicare?.Pcn ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.MedSecondary.ToString(), patient.Medicare?.SecondaryCarrier ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.MedSecondarySubscriber.ToString(), patient.Medicare?.SecondarySubscriberNumber ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.MedSubscriber.ToString(), patient.Medicare?.SubscriberNumber  ?? "N/A")
            };

            return kvps;
        }

        private List<KeyValuePair<string, string>> GetPhysicanKeys(PhysicianModel physician)
        {
            var kvps = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(MappingEnums.PhyName.ToString(), $"Dr {physician?.FirstName} {physician?.LastName}"),
                new KeyValuePair<string, string>(MappingEnums.PhyNameNoDr.ToString(), $"{physician?.FirstName} {physician?.LastName}"),
                new KeyValuePair<string, string>(MappingEnums.PhyNpi.ToString(), physician?.NPI ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.PhyDea.ToString(), physician?.DEA ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.PhyAddress.ToString(), physician?.Address?.ToString() ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.PhyPhone.ToString(), physician?.PhoneNumber ?? "N/A"),
                new KeyValuePair<string, string>(MappingEnums.PhyFax.ToString(), physician?.FaxNumber ?? "N/A")
            };

            return kvps;
        }

        private List<KeyValuePair<string, string>> GetAllCodes(IntakeFormModel intake)
        {
            var kvps = new List<KeyValuePair<string, string>>
            {
                      new KeyValuePair<string, string>(MappingEnums.GeneralIntakeNotes.ToString(), ""), //Below the Pain Chart Image
                      new KeyValuePair<string, string>(MappingEnums.HCPCSCode.ToString(), GetOrthoPrescribedAfter255(intake.HCPCSCodes)),
                      new KeyValuePair<string, string>(MappingEnums.HCPCSProduct.ToString(), intake.Product ?? "N/A"),
                      new KeyValuePair<string, string>(MappingEnums.HCPCSDescription.ToString(), GetOrthoPrescribed(intake.HCPCSCodes)),
                      new KeyValuePair<string, string>(MappingEnums.HCPCSDuration.ToString(),"99 months/lifetime"),
                      new KeyValuePair<string, string>(MappingEnums.ICDCode.ToString(), GetDiagnosis(intake.ICD10Codes)),
                //  new KeyValuePair<string, string>(MappingEnums.ICDDescription.ToString(), intake.ICD10?.Description  ?? "N/A")
            };
            return kvps;
        }

        /// <summary>
        /// Not sure how the additional notes are being persisted, but calling it via this method will load 
        /// it into the doc
        /// </summary>
        /// <param name="additionalNotes"></param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> GetDrNotes(string additionalNotes)
        {
            var kvps = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>(MappingEnums.DrNotes1.ToString(), additionalNotes) };
            var secondPart =
            additionalNotes.Length > 255 ? additionalNotes.Substring(254, additionalNotes.Length - 254) : "";
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.DrNotes2.ToString(), secondPart));
            return kvps;
        }


        /// <summary>
        /// The diagnosis will be formed by creating comma delimited list of ICD10.Code and ICD10.description
        /// m24.221 disorder of ligament, right elbow, [Code]  [Description]
        /// </summary>
        /// <param name="icds"></param>
        /// <returns></returns>
        private string GetDiagnosis(ICollection<ICD10CodeModel> icds)
        {
            return string.Join(", ", icds.Select(x => x.Text)) ?? "N/A";
        }

        /// <summary>
        /// The plan and treatment will be formed by creating comma delimited list of HCPCS.Code and HCPCS.description
        /// L3761 – Elbow Orthosis, With Adjustable Position Locking Joint(s), Prefabricated, [Code]  [Description]
        /// </summary>
        /// <param name="icds"></param>
        /// <returns></returns>
        private string GetOrthoPrescribed(ICollection<HCPCSCodeModel> hCPCs)
        {
            return string.Join(" with ", hCPCs.Select(x => x.Text)) ?? "N/A";
        }

        /// <summary>
        /// So the Word Doc replacement for fields, are capped at 255 chars, so this will append the final 255 chars. The Orhtro
        /// prescription is longer than 255 chars so this is the only replacement that is an issue
        /// </summary>
        /// <param name="hCPCs"></param>
        /// <returns></returns>
        private string GetOrthoPrescribedAfter255(ICollection<HCPCSCodeModel> hCPCs)
        {
            var prescribed = string.Join(" with ", hCPCs.Select(x => x.Text)) ?? "N/A";
            if (prescribed.Length > 255)
            {
                return prescribed.Substring(254, prescribed.Length - 254);
            }
            return "";
        }

        /// <summary>
        /// The signature information IP and Signature date are signed
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> GetSignature(SignatureModel signature)
        {
            var kvps = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(MappingEnums.IP.ToString(), signature?.IpAddress  ?? "N/A"),
                //Format - Wednesday, April 03, 2019 11:58:52 PM
                new KeyValuePair<string, string>(MappingEnums.SignatureDate.ToString(), signature?.CreatedOn.ToString("dddd, MMMM dd, yyyy hh:mm:ss tt")  ?? "N/A")
            };
            return kvps;
        }
        #endregion


        /// <summary>
        /// The resource stream from the assembly doesn't work when opening
        /// the document with the OpenXML library so I had to load it into a memory
        /// stream. Also this will help to reduce the time the resource file itself is
        /// going to be locked.
        /// </summary>
        private MemoryStream LoadMemoryStream()
        {
            var assembly = Assembly.GetAssembly(typeof(DocumentGenerator));
            var memStream = new MemoryStream();
            using (Stream stream = assembly.GetManifestResourceStream(ExamNote))
            {
                var buffer = new byte[16 * 1024];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memStream.Write(buffer, 0, read);
                }
            }
            return memStream;
        }

        /// <summary>
        /// Since we are dynamically adding the sections each intake form needs a different
        /// title that isn't exactly the same as the enum name
        /// </summary>
        /// <param name="intakFormType"></param>
        /// <returns></returns>
        private Paragraph CreateIntakeFormTitle(IntakeFormType intakFormType)
        {
            var title = "";
            switch (intakFormType)
            {
                case IntakeFormType.GeneralDmeOnly:
                    title = "General (DME Only)";
                    break;
                case IntakeFormType.PainDmeOnly:
                    title = "Pain (DME Only)";
                    break;
                case IntakeFormType.PainRxOnly:
                    title = "Pain (Rx Only)";
                    break;
                case IntakeFormType.MigraineRxOnly:
                    title = "Migraine (Rx Only)";
                    break;
                case IntakeFormType.ScarRxOnly:
                    title = "Scar (Rx Only)";
                    break;
                case IntakeFormType.HeartburnAcidRxOnly:
                    title = "Heartburn / Acid Reflux (Rx Only)";
                    break;
                case IntakeFormType.RashSkinRxOnly:
                    title = "Rash / Skin Irritation (Rx Only)";
                    break;
                case IntakeFormType.AntiFungalRxOnly:
                    title = "Anti-Fungal (Rx Only)";
                    break;
                case IntakeFormType.DryMouthRxOnly:
                    title = "Dry Mouth (Rx Only)";
                    break;
                case IntakeFormType.GeneralRxOnly:
                    title = "General (Rx Only)";
                    break;
                case IntakeFormType.GeneralDmeAndRx:
                    title = "General (DME & Rx)";
                    break;
                case IntakeFormType.FootbathRxOnly:
                    title = "Footbath (Rx Only)";
                    break;
            }
            return new Paragraph(new Run(new Text(title)));
        }
    }
}

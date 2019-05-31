using DocumentFormat.OpenXml;
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

    public class IntakeFormExporter : IIntakeFormExporter
    {
        private const string ExamNote = "PR.Export.Documents.BLANK_EXAM_NOTE.docx";

        /// <summary>
        /// Take all of the intake forms and replace the properties on the word doc, then append all of the
        /// intake forms
        /// </summary>
        /// <param name="intakeForms"></param>
        /// <returns></returns>
        public byte[] CreateNewIntakeForm(IntakeFormModel intakeForm, PatientModel patient)
        {
            var examNoteMemoryStream = LoadMemoryStream();

            using (var doc = WordprocessingDocument.Open(examNoteMemoryStream, true))
            {
                var docBody = doc.MainDocumentPart.Document.Body;

                // Create and add the character style with the style id, style name, and
                // aliases specified.
                var answerFormatStyleId = CreateIntakeFormAnswersCharStyle(doc);

                //Create title and add the subsequent question answers

                AppendTitleForIntakeForm(doc, docBody, intakeForm);

                var count = 1;
                foreach (var question in intakeForm.Questions)
                {
                    count = AppendQuestionAnswerPair(docBody, answerFormatStyleId, count, question);
                }
                docBody.AppendChild(new Paragraph());

                UpdateValuesInWordDocsCustomProperties(intakeForm, patient, doc);
            }

            var result = examNoteMemoryStream.ToArray();
            examNoteMemoryStream.Flush();
            examNoteMemoryStream.Close();

            return result;
        }

        /// <summary>
        /// The field values in the word doc need to be update in the property settings object of the word doc. The questions
        /// have 'keys' which we are using with MappingsEnum to know how update with the value from the IntakeForms. Then we
        /// explicitly ask the user to verify the fields are to be updated when opening the word doc.
        /// </summary>
        /// <param name="intakeForms"></param>
        /// <param name="doc"></param>
        private void UpdateValuesInWordDocsCustomProperties(IntakeFormModel intakeForm, PatientModel patient, WordprocessingDocument doc)
        {
            //https://docs.microsoft.com/en-us/office/open-xml/how-to-set-a-custom-property-in-a-word-processing-document
            var properties = doc.CustomFilePropertiesPart.Properties;

            // Get all question's with a key, then gather the value as all answers comma delimited              
            var intakeFromKeys = intakeForm.Questions
                .Where(r => !string.IsNullOrEmpty(r.Key))
                .Select(y => new KeyValuePair<string, string>(y.Key, y.Answers.Select(z => z.Text)
                .Aggregate((c, n) => $"{c},{n}"))).ToList();

            intakeFromKeys.AddRange(GetPatientKeys(patient));

            //This will update all of the custom properties that are used in the word doc.
            //Again, the fields are update in the document settings, but the downloading user
            //will need to approve the update for any fields.
            foreach (var propertyEnum in Enum.GetValues(typeof(MappingEnums)))
            {
                CustomDocumentProperty item = (CustomDocumentProperty)properties
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
            UpdateFieldsOnOpen updateFields = new UpdateFieldsOnOpen
            {
                Val = new DocumentFormat.OpenXml.OnOffValue(true)
            };
            settingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(updateFields);
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
            Color grayText = new Color() { Val = "888888" };
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
            var answerRun = answerParagraph.AppendChild<Run>(new Run(new Text(answerText) { Space = SpaceProcessingModeValues.Preserve }));

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

            var titleParagraph = CreateIntakeFormTitle(intakeForm.IntakeFormType);
            if (titleParagraph != null)
            {
                Formatting.AddNewStyle(doc, "PRTitle1", "PRTitle", titleParagraph);
                var paragraphProperties = titleParagraph.PrependChild(new ParagraphProperties());
                docBody.AppendChild(titleParagraph);
            }
            docBody.AppendChild(new Paragraph());
        }

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
                new KeyValuePair<string, string>(MappingEnums.DOB.ToString(), patient.DateOfBirth.ToString("d")),
                new KeyValuePair<string, string>(MappingEnums.Age.ToString(), patient.DateOfBirth.GetAge()),
                new KeyValuePair<string, string>(MappingEnums.PatientName.ToString(), $"{patient.FirstName} {patient.LastName}"),
                new KeyValuePair<string, string>(MappingEnums.Phone.ToString(), patient.PhoneNumber),
                new KeyValuePair<string, string>(MappingEnums.Gender.ToString(), patient.Sex.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Insurance.ToString(), patient.Insurance.ToString()),
                new KeyValuePair<string, string>(MappingEnums.Address.ToString(), patient.Address.ToString()),
                new KeyValuePair<string, string>(MappingEnums.ServiceDate.ToString(), DateTime.Now.ToString("d"))
            };

            return kvps;
        }

        /// <summary>
        /// The resource stream from the assembly doesn't work when opening
        /// the document with the OpenXML library so I had to load it into a memory
        /// stream. Also this will help to reduce the time the resource file itself is
        /// going to be locked.
        /// </summary>
        private MemoryStream LoadMemoryStream()
        {
            var assembly = Assembly.GetAssembly(typeof(IntakeFormExporter));
            var memStream = new MemoryStream();
            using (var stream = assembly.GetManifestResourceStream(ExamNote))
            {
                byte[] buffer = new byte[16 * 1024];
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

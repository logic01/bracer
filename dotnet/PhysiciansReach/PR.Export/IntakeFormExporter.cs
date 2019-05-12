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
        private MemoryStream ExamNoteMemoryStream = new MemoryStream();

        public byte[] CreateNewIntakeForm(IList<IntakeFormFullModel> intakeForms)
        {

            return UpdateReport(intakeForms);
        }


        private byte[] UpdateReport(IList<IntakeFormFullModel> intakeForms)
        {
            LoadMemoryStream();

            using (var doc = WordprocessingDocument.Open(ExamNoteMemoryStream, true))
            {

                //TODO TEST, I am pretty sure loading the values into InnerXml isn't correct, also I have only set it up for one
                //intake form, there are a few more that need to be added
                //TODO take all of the IntakeForms and loop over each individual add it's title and add them directly to the doc.
                // I think that means I need to figure out how to create the elements I need. An example of how to handle the loop
                var docBody = doc.MainDocumentPart.Document.Body;

                // Create and add the character style with the style id, style name, and
                // aliases specified.
                var answerFormatStyleId = "AnswerStyleChar";
                Color grayText = new Color() { Val = "888888" };
                Formatting.CreateAndAddCharacterStyle(doc,
                    answerFormatStyleId,
                    "Answer Style Char",
                    "Answer Gray", "AnswerStylePar", new List<OpenXmlElement> { grayText });

                //Create title and add the subsequent question answers
                foreach (var intakeForm in intakeForms)
                {

                    if (intakeForm.IntakeFormType == IntakeFormType.PatientInfo)
                        continue;
                    docBody.AppendChild(new Paragraph());

                    var titleParagraph = CreateIntakeFormTitle(intakeForm.IntakeFormType);
                    if (titleParagraph != null)
                    {
                        Formatting.AddNewStyle(doc, "PRTitle1", "PRTitle", titleParagraph);
                        var paragraphProperties = titleParagraph.PrependChild(new ParagraphProperties());
                        docBody.AppendChild(titleParagraph);
                    }
                    docBody.AppendChild(new Paragraph());

                    var count = 1;
                    foreach (var question in intakeForm.Questions)
                    {
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
                            rPr.RunStyle = new RunStyle();
                        rPr.RunStyle.Val = answerFormatStyleId;
                        docBody.AppendChild(answerParagraph);
                    }
                    docBody.AppendChild(new Paragraph());
                }

                //https://docs.microsoft.com/en-us/office/open-xml/how-to-set-a-custom-property-in-a-word-processing-document
                var properties = doc.CustomFilePropertiesPart.Properties;
                               
                // Get all question's with a key, then gather the value as all answers comma delimited              
                var intakeFromKeys = intakeForms.SelectMany(x => x.Questions.Where(r => !string.IsNullOrEmpty(r.Key)))
                    .Select(y => new KeyValuePair<string, string>(y.Key, y.Answers.Select(z => z.Text).Aggregate((c, n) => $"{c},{n}"))).ToList();
                intakeFromKeys.AddRange(GetPatientKeys(intakeForms.First().Patient));
                
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
                        var val = intakeFromKeys.FirstOrDefault(x => x.Key == propertyEnum.ToString()).Value;                       
                        item.VTLPWSTR = new VTLPWSTR(val);
                    }
                }

                properties.Save();

                //The docx is using Custom Properties and above we are updating the custom property values,
                //however there is no way (that I have found) to programatically updated all of the fields
                //that are using the custom properties without requiring the downloader to 
                DocumentSettingsPart settingsPart = doc.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().First();
                UpdateFieldsOnOpen updateFields = new UpdateFieldsOnOpen();
                updateFields.Val = new DocumentFormat.OpenXml.OnOffValue(true);
                settingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(updateFields);
                settingsPart.Settings.Save();
                doc.Save();

            }
            var result = ExamNoteMemoryStream.ToArray();
            ExamNoteMemoryStream.Flush();
            ExamNoteMemoryStream.Close();

            return result;
        }

        private List<KeyValuePair<string, string>> GetPatientKeys(PatientModel patient)
        {
            var kvps = new List<KeyValuePair<string, string>>();
            kvps.Add(new KeyValuePair<string,string>(MappingEnums.DOB.ToString(), patient.DateOfBirth.ToString("d")));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.Age.ToString(), patient.DateOfBirth.GetAge()));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.PatientName.ToString(), $"{patient.FirstName} {patient.LastName}"));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.Phone.ToString(), patient.PhoneNumber));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.Gender.ToString(), patient.Sex.ToString()));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.Insurance.ToString(), patient.Insurance.ToString()));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.Address.ToString(), patient.Address.ToString()));
            kvps.Add(new KeyValuePair<string, string>(MappingEnums.ServiceDate.ToString(), DateTime.Now.ToString("d")));
           
            return kvps;
        }


        /// <summary>
        /// Again the resource stream from the assembly doesn't work when opening
        /// the document with the OpenXML library so I had to load it into a memory
        /// stream. Also this will help to reduce the time the resource file itself is
        /// going to be locked.
        /// </summary>
        private void LoadMemoryStream()
        {
            var assembly = Assembly.GetAssembly(typeof(IntakeFormExporter));
            using (var stream = assembly.GetManifestResourceStream(ExamNote))
            {
                CopyStream(stream);
            }
        }

        /// <summary>
        /// Copying the Stream from the assembly resource stream to a memory stream. There was an issue when
        /// trying to use the stream from the resource manifest.
        /// </summary>
        /// <param name="input"></param>
        private void CopyStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ExamNoteMemoryStream.Write(buffer, 0, read);
            }
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
                case IntakeFormType.PatientInfo:
                    return null;
            }
            return new Paragraph(new Run(new Text(title)));
        }


    }
}

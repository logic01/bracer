using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using PR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Doc = DocumentFormat.OpenXml;

namespace PR.Export
{

    public class IntakeFormExporter : IIntakeFormExporter
    {
        private const string ExamNote = "PR.Export.Documents.BLANK_EXAM_NOTE.docx";
        private MemoryStream ExamNoteMemoryStream = new MemoryStream();

        public byte[] CreateNewIntakeForm(IEnumerable<IntakeFormModel> intakeForms)
        {

            return UpdateReport(intakeForms);
        }

        private byte[] UpdateReport(IEnumerable<IntakeFormModel> intakeForms)
        {
            LoadMemoryStream();
            using (var doc = WordprocessingDocument.Open(ExamNoteMemoryStream, true))
            {
                //TODO TEST, I am pretty sure loading the values into InnerXml isn't correct, also I have only set it up for one
                //intake form, there are a few more that need to be added
                //TODO take all of the IntakeForms and loop over each individual add it's title and add them directly to the doc.
                // I think that means I need to figure out how to create the elements I need. An example of how to handle the loop
                var docBody = doc.MainDocumentPart.Document.Body;
                foreach (var intakeForm in intakeForms)
                {

                    if (intakeForm.IntakeFormType == Constants.Enums.IntakeFormType.GeneralDmeOnly)
                        docBody.AppendChild(new Paragraph() { InnerXml = "General DME Only" });
                    
                    foreach(var question in intakeForm.Questions)
                    {
                        var questionAnswer = question.Text + " " + string.Join(',', question.Answers.Select(x => x.Text == "" ? "Not Applicable":x.Text));
                        docBody.AppendChild(new Paragraph() { InnerXml = questionAnswer });
                    }
                }                

                //https://docs.microsoft.com/en-us/office/open-xml/how-to-set-a-custom-property-in-a-word-processing-document
                var properties = doc.CustomFilePropertiesPart.Properties;

                //TODO TEST
                // Get all question's with a key, then gather the value as all answers comma delimited
                var intakeFromKeys = intakeForms.SelectMany(x => x.Questions.Where(r => !string.IsNullOrEmpty(r.Key)).Select(y => new KeyValuePair<string, string>(y.Key, y.Answers.Select(z => z.Text).Aggregate((c, n) => $"{c},{n}"))));
                
                //This will update all of the custom properties that are used in the word doc.
                //Again, the fields are update in the document settings, but the downloading user
                //will need to approve the update for any fields.
                foreach (var propertyEnum in Enum.GetValues(typeof(MappingEnums)))
                {
                    CustomDocumentProperty item = (CustomDocumentProperty)properties
                        .FirstOrDefault(x => ((CustomDocumentProperty)x).Name.Value.Equals(propertyEnum.ToString()));
                    if (item != null)
                    {
                        //TODO TEST
                        //If a key doesn't exist, you could see an empty value stuffed into the word doc
                        var val = intakeFromKeys.FirstOrDefault(x => x.Key == propertyEnum.ToString()).Value;
                        //TODO you will need manually map the enums, to the various IntakeForms, and Patient info                        
                        //item.VTLPWSTR = new VTLPWSTR($"#{(propertyEnum).ToString()}#");
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

    }
}

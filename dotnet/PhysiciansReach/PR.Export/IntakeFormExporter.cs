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
        public string tempHeight = "5'7";
        private MemoryStream _memoryStream = new MemoryStream();

        public byte[] CreateNewIntakeForm(IEnumerable<IntakeFormModel> intakeForms)
        {
            if (intakeForms?.Any() == true)
            {
                tempHeight = intakeForms.First().IntakeFormId.ToString();
            }
            return UpdateReport();
        }

        private byte[] UpdateReport()
        {
            LoadMemoryStream();
            using (var doc = WordprocessingDocument.Open(_memoryStream, true))
            {
                //https://docs.microsoft.com/en-us/office/open-xml/how-to-set-a-custom-property-in-a-word-processing-document

                //var heightProperty = "Height";
                var properties = doc.CustomFilePropertiesPart.Properties;
               
               //TODO Copy the embedded DOC, and then create another one. Throw it into a
               //memory stream and add the object to the doc as an embedded object. That
               //will take care of your format issue.

                foreach (var propertyEnum in Enum.GetValues(typeof(MappingEnums)))
                {
                    CustomDocumentProperty item = (CustomDocumentProperty)properties
                        .First(x => ((CustomDocumentProperty)x).Name.Value.Equals(propertyEnum.ToString()));
                    item.VTLPWSTR = new VTLPWSTR(((int)propertyEnum).ToString());
                }
                //foreach (CustomDocumentProperty item in properties)
                //{
                //    if (item.Name.Value.Equals(heightProperty))
                //    {
                //        item.VTLPWSTR = new VTLPWSTR("5'6");

                //    }
                //}               

                properties.Save();
                DocumentSettingsPart settingsPart = doc.MainDocumentPart.GetPartsOfType<DocumentSettingsPart>().First();
                UpdateFieldsOnOpen updateFields = new UpdateFieldsOnOpen();
                updateFields.Val = new DocumentFormat.OpenXml.OnOffValue(true);
                settingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(updateFields);
                settingsPart.Settings.Save();
                doc.Save();
                //doc.Package.Flush();
                
            }
            var result = _memoryStream.ToArray();
            _memoryStream.Flush();

            return result;
        }

        private void LoadMemoryStream()
        {
            var assembly = Assembly.GetAssembly(typeof(IntakeFormExporter));
            using (var stream = assembly.GetManifestResourceStream(ExamNote))
            {
                CopyStream(stream);
            }
        }

        // Merged From linked CopyStream below and Jon Skeet's ReadFully example
        private void CopyStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                _memoryStream.Write(buffer, 0, read);
            }
        }      
       
    }
}

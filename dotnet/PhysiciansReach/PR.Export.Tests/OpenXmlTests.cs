using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Doc = DocumentFormat.OpenXml;
namespace PR.Export.Tests
{
    [TestClass]
    public class OpenXmlTests
    {
        [TestMethod]
        [TestCategory("Export")]
        public void Export_FindHeight_Success()
        {
            var intakeForm = new IntakeFormExporter();
            //intakeForm.UpdateReport();
        }       
    }
}

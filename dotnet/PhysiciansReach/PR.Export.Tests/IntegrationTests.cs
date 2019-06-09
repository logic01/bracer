using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR.Business.Business;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Export.Tests
{
    [TestClass]
    public class IntegrationTests : IntegrationTestsBase
    {
        [TestMethod]
        public void Create_Document_From_Scratch()
        {
            // Create agent with Vendor, UserProfile, and Agent
            var agent = CreateAgent();

            // Create Patient with Address, Medicare, and Private Insurance
            var patient = CreateAndPersistPatient(agent);

            IntakeForm intakeForm = CreateIntakeFormLocal(patient.PatientId);
            IntakeForm intakeForm2 = CreateIntakeFormLocal(patient.PatientId);

            // Create Document Byte Array
            var intakeModel = intakeForm.ToModel();
            var intakeModel2 = intakeForm2.ToModel();
            var exporter = new IntakeFormExporter();
            var documentContent = exporter.CreateNewIntakeForm(intakeModel, patient.ToModel(), intakeForm.Signature.ToModel(), intakeForm.Physician.ToModel(), new List<IntakeFormModel> { intakeModel, intakeModel2 });

            // Create Document
            var document = new Document
            {
                IntakeFormId = intakeForm.IntakeFormId,
                Content = documentContent
            };

            var doc = dbContext.Document.Add(document);
            var id = dbContext.SaveChanges();
            Console.WriteLine($"Run project and open your browser to https://localhost:44327/document/{doc.Entity.DocumentId}/download to see the word doc generated from this test");

        }

        
    }
}

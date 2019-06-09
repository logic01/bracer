using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR.Business;
using PR.Business.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PR.Export.Tests.BusinessTests
{
    [TestClass]
    public class IntakeFormTests : IntegrationTestsBase
    {
        private readonly IntakeFormBusiness intakeFormBiz;
        public IntakeFormTests()
        {
            intakeFormBiz = new IntakeFormBusiness(dbContext);
        }

        [TestMethod]
        public void Create_Intake_Form()
        {
            // Create agent with Vendor, UserProfile, and Agent
            var agent = CreateAgent();

            // Create Patient with Address, Medicare, and Private Insurance
            var patient = CreateAndPersistPatient(agent);

            // Create Intake Model
            var intakeForm = CreateIntakeForm(patient.PatientId);
            var intakeFormModel = intakeForm.ToModel();

            // Load with questions/answers
            var questions = CreateQuestions(default(int));
            intakeFormModel.Questions = questions.Select(x => x.ToModel()).ToList();

            var newIntakeFormModel = intakeFormBiz.Create(intakeFormModel);

            Assert.IsTrue(newIntakeFormModel.Questions?.Any() ?? false, "The questions are not being returned to the model when creating an intake form");

            // double checking against db
            questions = dbContext.Question.Include(x => x.Answers).Where(x => x.IntakeFormId == newIntakeFormModel.IntakeFormId).ToList();
            Assert.IsTrue(questions?.Any() ?? false, "The questions are not being persisted when creating an intake form");
        }

        [TestMethod]
        public void Complete_Intake_And_Create_Doc()
        {
            ////Create a patient and intake form then run this to create a document
            //var intakeForm = dbContext.IntakeForm.Find(11);

            //intakeForm.HCPCSs = CreateHCPCSs();
            //intakeForm.ICD10s = CreateICD10s();
            //intakeForm.Signature = CreateSignature();
            //intakeForm.Physician = CreatePhysician();

            //var tempIntakeForm = dbContext.IntakeForm.Update(intakeForm);
            //dbContext.SaveChanges();

            //var intakeFormModel = tempIntakeForm.Entity.ToModel();

            //var documentBiz = new DocumentBusiness(dbContext, intakeFormBiz, new IntakeFormExporter());
            //var doc = documentBiz.CreateIntakeFormDocument(intakeForm.PatientId, intakeForm.IntakeFormId);
            //Console.WriteLine($"Run project and open your browser to https://localhost:44327/document/{doc.DocumentId}/download to see the word doc generated from this test");

        }

    }
}

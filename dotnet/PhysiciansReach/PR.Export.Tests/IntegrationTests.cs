using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using PR.Business;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PR.Data.Models;
using PR.Business.Mappings;
using PR.Models;

namespace PR.Export.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly Data.Models.DataContext dataContext;
        public IntegrationTests()
        {
            var conn = "Data Source=DESKTOP-FD8N113\\sqlexpress;Initial Catalog=PhysicansReach;Integrated Security=True";
            //var conn = "Server=(localdb)\\mssqllocaldb;Database=PhysiciansReach;Trusted_Connection=True;ConnectRetryCount=0";
            dataContext = new Data.Models.DataContext(conn);
        }

        [TestMethod]
        public void Populate_Questions_And_Answers()
        {
            ////Run these to add the questions that will populate the word doc
            //  var intakeForm = dataContext.IntakeForm.FirstOrDefault(x => x.IntakeFormType == Constants.Enums.IntakeFormType.PainDmeOnly);
            //  Assert.IsNotNull(intakeForm, "A Pain DME ONLY intake form needs to be in the db in order for this test to be ran. Create one as an agent");


            //var questions = new List<Question>();
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Cause of Patients Pain?", "PainFeeling", "Pain Causer"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Location(s) of Pain?", "PainChart", "Lower Back"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Onset of pain (When did the pain begin?)", "PainBegan", "2 months ago"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "What Provokes Pain", "PainCause", "Pain Provoker"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "What currently relieves the pain", "PainSelfTreatment", "RICE"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]", "PainDescription", "Sharp"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Duration of Pain (Constant (Daily), Intermittent (from time to time)", "PainDuration", "Constant Pain"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)", "PreviousTreatment", "Brace Helped"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Affects Activities of Daily Living(ADL) (If so, what?)", "EffectsDaily", "All movement effected"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "If yes, what type of surgery?", "Surgies", "Back surgery twice"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Pain Rating", "PainLevel", "7"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Height", "Height", "5''7"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Weight", "Weight", "160"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Shoe size", "ShoeSize", "10.5"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Waist", "Waist", "32"));
            //  questions.Add(CreateQuestionAnswer(intakeForm.IntakeFormId, "Allergies current", "Allergies", "eggs, dairy"));
            //  intakeForm.Questions = questions;
            //  dataContext.IntakeForm.Update(intakeForm);
            //  dataContext.SaveChanges();
        }

        private Question CreateQuestionAnswer(int intakeFormId, string question, string key, string answer)
        {
            return new Question
            {
                IntakeFormId = intakeFormId,
                Text = question,
                Key = key,
                Answers = new List<Answer> { new Answer { Text = answer } }
            };
        }

        [TestMethod]
        public void Populate_Word_Doc()
        {
            // This is kind of important that a PainDmeOnly intake form exists
            var intakeForm = dataContext.IntakeForm
                .Include("Questions.Answers")
                .Include("ICD10")
                .Include("HCPCS")
                .Include("Signature")
                .Include("Physician").FirstOrDefault(x => x.IntakeFormType == Constants.Enums.IntakeFormType.PainDmeOnly);
            Assert.IsNotNull(intakeForm, "A Pain DME ONLY intake form needs to be in the db in order for this test to be ran. Create one as an agent");

            var patient = dataContext.Patient
                .Include(p => p.PhysiciansAddress)
                .Include(p => p.Address)
                .Include(p => p.PrivateInsurance)
                .Include(p => p.Medicare).FirstOrDefault(x => x.PatientId == intakeForm.PatientId);

            var intakeFormModel = intakeForm.ToModel();
            //TODO Delete this testing word 
            var patientModel = patient.ToModel();

            var signatureModel = new SignatureModel
            {
                IpAddress = "123.123.32.192",
                CreatedOn = DateTime.Now
            };

            var physicianModel = new PhysicianModel
            {
                FirstName = "Mantis",
                LastName = "Toboggan",
                PhoneNumber = "1234857447",
                NPI = "123123123",
                DEA = "57575755",
                Address = new AddressModel
                {
                    AddressLineOne = "123 main street",
                    State = "CO",
                    City = "Denver",
                    ZipCode = "802224"
                }
            };

            intakeFormModel.HCPCS = new HCPCSModel
            {
                Code = "L293",
                Product = "Back Brace",
                Description = "HCPCS Description",
                Duration = "69 Years"
            };

            intakeFormModel.ICD10 = new ICD10Model
            {
                Code = "Lower Back pain m54.5 low back pain, m53.2x7 spinal instabilities, lumbosacral region, g89.4 chronic pain,m51.36 lumbar disc degeneration"
                ,
                Description = "L0650(Lumbar - sacral orthosis.Sagittal control with rigid anterior and posterior panels, " +
                "posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, " +
                "with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required " +
                "and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self - adjusting.)"
            };
            var exporter = new IntakeFormExporter();

            var documentContent = exporter.CreateNewIntakeForm(intakeFormModel, patientModel, signatureModel, physicianModel);

            var document = new Document
            {
                IntakeFormId = intakeForm.IntakeFormId,
                Content = documentContent
            };

            var doc = dataContext.Document.Add(document);
            var id = dataContext.SaveChanges();
            Console.WriteLine(doc.Entity.DocumentId);            
        }
    }
}

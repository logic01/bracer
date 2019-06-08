using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR.Business.Business;
using PR.Business.Mappings;
using PR.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Export.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private readonly DataContext dbContext;
        public IntegrationTests()
        {
            var conn = "Data Source=DESKTOP-FD8N113\\sqlexpress;Initial Catalog=PhysicansReach;Integrated Security=True";
            //var conn = "Server=(localdb)\\mssqllocaldb;Database=PhysiciansReach;Trusted_Connection=True;ConnectRetryCount=0";
            dbContext = new Data.Models.DataContext(conn);
        }

        [TestMethod]
        public void Create_Document_From_Scratch()
        {
            // Crate Vendor
            var vendor = new Vendor
            {
                CompanyName = "Wolf Cola",
                DoingBusinessAs = "Franks Fluids",
                PhoneNumber = "2606027777",
                ContactFirstName = "Charlie",
                ContactLastName = "Kelley",
                Active = true,
            };
            var savedVendor = dbContext.Vendor.Add(vendor);
            dbContext.SaveChanges();
            var vendorId = savedVendor.Entity.VendorId;

            // Create Agent
            var agent = CreateAgent(vendorId);
            var savedAgent = dbContext.Agent.Add(agent);
            dbContext.SaveChanges();

            // Create Patient with Address, Medicare, and Private Insurance
            var patient = CreatePatient(savedAgent.Entity.UserAccountId);
            var savedPatient = dbContext.Patient.Add(patient);
            dbContext.SaveChanges();
            var patientId = savedPatient.Entity.PatientId;

            // Create IntakeForm with ICD, HCPCS, Phsycian, and Signature
            var intakeForm = CreateIntakeForm(patientId);
            var savedIntakeForm = dbContext.IntakeForm.Add(intakeForm);
            dbContext.SaveChanges();
            var intakeFormId = savedIntakeForm.Entity.IntakeFormId;

            // Add Questions
            intakeForm.Questions = CreateQuestions(intakeFormId);
            dbContext.IntakeForm.Update(intakeForm);
            dbContext.SaveChanges();

            // Create Document Byte Array
            var intakeModel = intakeForm.ToModel();
            var exporter = new IntakeFormExporter();
            var documentContent = exporter.CreateNewIntakeForm(intakeModel, patient.ToModel(), intakeForm.Signature.ToModel(), intakeForm.Physician.ToModel());

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

        //[TestMethod]
        //public void Populate_Word_Doc()
        //{
        //    // This is kind of important that a PainDmeOnly intake form exists
        //    var intakeForm = dataContext.IntakeForm
        //        .Include("Questions.Answers")
        //        .Include("ICD10")
        //        .Include("HCPCS")
        //        .Include("Signature")
        //        .Include("Physician").FirstOrDefault(x => x.IntakeFormType == Constants.Enums.IntakeFormType.PainDmeOnly);
        //    Assert.IsNotNull(intakeForm, "A Pain DME ONLY intake form needs to be in the db in order for this test to be ran. Create one as an agent");

        //    var patient = dataContext.Patient
        //        .Include(p => p.PhysiciansAddress)
        //        .Include(p => p.Address)
        //        .Include(p => p.PrivateInsurance)
        //        .Include(p => p.Medicare).FirstOrDefault(x => x.PatientId == intakeForm.PatientId);

        //    var intakeFormModel = intakeForm.ToModel();
        //    //TODO Delete this testing word 
        //    var patientModel = patient.ToModel();

        //    var signatureModel = new SignatureModel
        //    {
        //        IpAddress = "123.123.32.192",
        //        CreatedOn = DateTime.Now
        //    };

        //    var physicianModel = new PhysicianModel
        //    {
        //        FirstName = "Mantis",
        //        LastName = "Toboggan",
        //        PhoneNumber = "1234857447",
        //        NPI = "123123123",
        //        DEA = "57575755",
        //        Address = new AddressModel
        //        {
        //            AddressLineOne = "123 main street",
        //            State = "CO",
        //            City = "Denver",
        //            ZipCode = "802224"
        //        }
        //    };

        //    intakeFormModel.HCPCS = new HCPCSModel
        //    {
        //        Code = "L293",
        //        Product = "Back Brace",
        //        Description = "HCPCS Description",
        //        Duration = "69 Years"
        //    };

        //    intakeFormModel.ICD10 = new ICD10Model
        //    {
        //        Code = "Lower Back pain m54.5 low back pain, m53.2x7 spinal instabilities, lumbosacral region, g89.4 chronic pain,m51.36 lumbar disc degeneration"
        //        ,
        //        Description = "L0650(Lumbar - sacral orthosis.Sagittal control with rigid anterior and posterior panels, " +
        //        "posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, " +
        //        "with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required " +
        //        "and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self - adjusting.)"
        //    };
        //    var exporter = new IntakeFormExporter();

        //    var documentContent = exporter.CreateNewIntakeForm(intakeFormModel, patientModel, signatureModel, physicianModel);

        //    var document = new Document
        //    {
        //        IntakeFormId = intakeForm.IntakeFormId,
        //        Content = documentContent
        //    };

        //    var doc = dataContext.Document.Add(document);
        //    var id = dataContext.SaveChanges();
        //    Console.WriteLine(doc.Entity.DocumentId);
        //}

        private static Agent CreateAgent(int vendorId)
        {
            return new Agent
            {
                VendorId = vendorId,
                FirstName = "Frank",
                LastName = "Reynolds",
                UserAccount = new UserAccount
                {
                    Active = true,
                    Type = Constants.Enums.AccountType.Agent,
                    UserName = "User" + Guid.NewGuid().ToString("N"),
                    EmailAddress = "temp@test.com",
                    Password = new PasswordHash("Password1").ToArray()
                }
            };
        }

        private IntakeForm CreateIntakeForm(int patientId)
        {
            return new IntakeForm
            {
                PatientId = patientId,
                IntakeFormType = Constants.Enums.IntakeFormType.PainDmeOnly,
                ICD10 = new ICD10
                {
                    Code = "Lower Back pain m54.5 low back pain, m53.2x7 spinal instabilities, lumbosacral region, g89.4 chronic pain,m51.36 lumbar disc degeneration"
                            ,
                    Description = "L0650(Lumbar - sacral orthosis.Sagittal control with rigid anterior and posterior panels, " +
                            "posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, " +
                            "with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required " +
                            "and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self - adjusting.)"
                },
                HCPCS = new HCPCS
                {
                    Code = "L293",
                    Product = "Back Brace",
                    Description = "HCPCS Description",
                    Duration = "69 Years"
                },
                Physician = new Physician
                {
                    FirstName = "Mantis",
                    LastName = "Toboggan",
                    PhoneNumber = "1234857447",
                    NPI = "123123123",
                    DEA = "57575755",
                    Address = new Address
                    {
                        AddressLineOne = "123 main street",
                        State = "CO",
                        City = "Denver",
                        ZipCode = "802224"
                    },
                    UserAccount = new UserAccount
                    {
                        Active = true,
                        Type = Constants.Enums.AccountType.Physician,
                        UserName = "User" + Guid.NewGuid().ToString("N"),
                        EmailAddress = "temp@test.com",
                        Password = new PasswordHash("Password1").ToArray()
                    }
                },
                Signature = new Signature
                {
                    IpAddress = "123.123.32.192",
                    CreatedOn = DateTime.Now,
                    Content = Encoding.ASCII.GetBytes("random data")
                },
                Status = Constants.Enums.IntakeFormStatus.New
            };
        }

        private static Patient CreatePatient(int userAccountId)
        {
            return new Patient
            {
                AgentId = userAccountId,
                Language = Constants.Enums.LanguageType.English,
                Sex = Constants.Enums.SexType.Male,
                Therapy = Constants.Enums.TherapyType.BOTH,
                Insurance = Constants.Enums.InsuranceType.BOTH,
                Pharmacy = Constants.Enums.PharmacyType.TWA,
                FirstName = "Dennis",
                LastName = "Reynolds",
                DateOfBirth = DateTime.Now.AddYears(-30),
                PhoneNumber = "2606027777",
                CallBackImmediately = true,
                BestTimeToCallBack = Constants.Enums.CallbackTime.Afternoon,
                IsDme = true,
                Address = new Address
                {
                    AddressLineOne = "123 Main Street",
                    City = "denver",
                    State = "CO",
                    ZipCode = "80224",
                },
                PrivateInsurance = new PrivateInsurance
                {
                    Insurance = "Insurance",
                    InsuranceId = "12312",
                    Group = "Insur Group",
                    PCN = "PCN",
                    Bin = "bin",
                    Street = "Street",
                    City = "City",
                    State = "CO",
                    Zip = "80224",
                    Phone = "2606028989"
                },
                Medicare = new Medicare
                {
                    MemberId = "13213",
                    PatientGroup = "Patient Group",
                    Pcn = "PCN",
                    SubscriberNumber = "33333",
                    SecondaryCarrier = "Geico",
                    SecondarySubscriberNumber = "4444"
                }
            };
        }

        private List<Question> CreateQuestions(int intakeFormId)
        {
            var questions = new List<Question>();
            questions.Add(CreateQuestionAnswer(intakeFormId, "Cause of Patients Pain?", "PainFeeling", "Pain Causer"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Location(s) of Pain?", "PainChart", "Lower Back"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Onset of pain (When did the pain begin?)", "PainBegan", "2 months ago"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "What Provokes Pain", "PainCause", "Pain Provoker"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "What currently relieves the pain", "PainSelfTreatment", "RICE"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]", "PainDescription", "Sharp"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Duration of Pain (Constant (Daily), Intermittent (from time to time)", "PainDuration", "Constant Pain"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)", "PreviousTreatment", "Brace Helped"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Affects Activities of Daily Living(ADL) (If so, what?)", "EffectsDaily", "All movement effected"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "If yes, what type of surgery?", "Surgies", "Back surgery twice"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Pain Rating", "PainLevel", "7"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Height", "Height", "5''7"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Weight", "Weight", "160"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Shoe size", "ShoeSize", "10.5"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Waist", "Waist", "32"));
            questions.Add(CreateQuestionAnswer(intakeFormId, "Allergies current", "Allergies", "eggs, dairy"));
            return questions;
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
    }
}

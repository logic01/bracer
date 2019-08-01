using PR.Business.Business;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;

namespace PR.Export.Tests
{
    public class IntegrationTestsBase
    {
        protected readonly DataContext dbContext;
        public IntegrationTestsBase()
        {
            //var conn = "Data Source=DESKTOP-FD8N113\\sqlexpress;Initial Catalog=PhysicansReach;Integrated Security=True";
            var conn = "Server=(localdb)\\mssqllocaldb;Database=PhysiciansReach;Trusted_Connection=True;ConnectRetryCount=0";
            dbContext = new Data.Models.DataContext(conn);
        }

        protected static Agent CreateAgent(int vendorId)
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

        protected IntakeForm CreateIntakeFormLocal(int patientId)
        {
            // Create IntakeForm with ICD, HCPCSCode, Phsycian, and Signature
            IntakeForm intakeForm = CreateIntakeForm(patientId);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IntakeForm> savedIntakeForm = dbContext.IntakeForm.Add(intakeForm);
            dbContext.SaveChanges();
            var intakeFormId = savedIntakeForm.Entity.IntakeFormId;

            // Add Questions
            intakeForm.Questions = CreateQuestions(intakeFormId);
            dbContext.IntakeForm.Update(intakeForm);
            dbContext.SaveChanges();
            return intakeForm;
        }

        protected IntakeForm CreateIntakeForm(int patientId)
        {
            Signature signature = CreateSignature();
            return new IntakeForm
            {
                PatientId = patientId,
                IntakeFormType = Constants.Enums.IntakeFormType.PainDmeOnly,
                ICD10Codes = CreateICD10s(),
                HCPCSCode = CreateHCPCSCodes(),
                Physician = CreatePhysician(),
                Status = Constants.Enums.IntakeFormStatus.New,
                PhysicianNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi blandit cursus risus at ultrices. Enim nunc faucibus a pellentesque sit amet porttitor eget dolor. Tincidunt dui ut ornare lectus sit amet est. Laoreet sit amet cursus sit amet dictum. Dignissim cras tincidunt lobortis feugiat vivamus at. Ullamcorper dignissim cras tincidunt lobortis feugiat vivamus. Morbi quis commodo odio aenean sed adipiscing diam donec adipiscing"
            };
        }

        protected static Physician CreatePhysician()
        {
            return new Physician
            {
                FirstName = "Mantis",
                LastName = "Toboggan",
                PhoneNumber = "1234857447",
                NPI = "123123123",
                DEA = "57575755",
                FaxNumber = "332244559",
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
            };
        }

        protected static Signature CreateSignature(bool firstSignature = true)
        {
            var signatureModel = new SignatureModel
            {

                IpAddress = "123.123.32.192",
                CreatedOn = DateTime.Now,
                Content = firstSignature
                    ? "data:image/jpeg;base64,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"
                   : "data:image/jpeg;base64,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",
                ModifiedOn = DateTime.Now,
                Type = firstSignature
                ? Constants.Enums.SignatureType.PrescriptionDocument
                : Constants.Enums.SignatureType.IntakeDocument
            };
            var signature = new Signature();
            //Testing the mapper to ensure the data:image/jpeg;base64, gets stripped before being stored
            signature.MapFromModel(signatureModel);
            return signature;
        }

        protected static string CreateHCPCSCodes()
        {
            return "L0650 - (Lumbar - sacral orthosis.Sagittal control with rigid anterior and posterior panels, " +
                            "posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, " +
                            "with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required " +
                            "and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self - adjusting.)";
        }

        protected static List<ICD10Code> CreateICD10s()
        {
            return new List<ICD10Code> { new ICD10Code
                {
                    Text = "m54.5 - low back pain"
                },new ICD10Code
                {
                    Text = "m53.2x7 - spinal instabilities"
                },new ICD10Code
                {
                    Text = "g89.4 - chronic pain"
                },new ICD10Code
                {
                    Text = "m51.36 - lumbar disc degeneration"
                }};
        }

        protected static Patient CreatePatient(int userAccountId)
        {
            return new Patient
            {
                AgentId = userAccountId,
                Language = Constants.Enums.LanguageType.English,
                Sex = Constants.Enums.SexType.Male,
                Therapy = Constants.Enums.TherapyType.BOTH,
                Insurance = Constants.Enums.InsuranceType.BOTH,
                FirstName = "Dennis",
                LastName = "Reynolds",
                DateOfBirth = DateTime.Now.AddYears(-30),
                PhoneNumber = "2606027777",
                BestTimeToCallBack = Constants.Enums.CallbackTime.Afternoon,
                IsDme = true,
                Weight = "160",
                Height = "5'7",
                Waist = "32",
                ShoeSize = "10.5",
                Allergies = "Eggs, Shrimp, Fish",
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

        protected List<Question> CreateQuestions(int intakeFormId)
        {
            var questions = new List<Question>
            {
                CreateQuestionAnswer(intakeFormId, "Cause of Patients Pain?", "PainFeeling", "Pain Causer"),
                CreateQuestionAnswer(intakeFormId, "Location(s) of Pain?", "PainChart", "Lower Back"),
                CreateQuestionAnswer(intakeFormId, "Onset of pain (When did the pain begin?)", "PainBegan", "2 months ago"),
                CreateQuestionAnswer(intakeFormId, "What Provokes Pain", "PainCause", "Pain Provoker"),
                CreateQuestionAnswer(intakeFormId, "What currently relieves the pain", "PainSelfTreatment", "RICE"),
                CreateQuestionAnswer(intakeFormId, "Description of Pain [Sharp/Stabbing, Weak Feeling/Unstable]", "PainDescription", "Sharp"),
                CreateQuestionAnswer(intakeFormId, "Duration of Pain (Constant (Daily), Intermittent (from time to time)", "PainDuration", "Constant Pain"),
                CreateQuestionAnswer(intakeFormId, "Other or Previous Helpful Treatments(Brace, Physical Therapy, Meds)", "PreviousTreatment", "Brace Helped"),
                CreateQuestionAnswer(intakeFormId, "Affects Activities of Daily Living(ADL) (If so, what?)", "EffectsDaily", "All movement effected"),
                CreateQuestionAnswer(intakeFormId, "If yes, what type of surgery?", "Surgies", "Back surgery twice"),
                CreateQuestionAnswer(intakeFormId, "Pain Rating", "PainLevel", "7")
            };
            return questions;
        }

        protected Question CreateQuestionAnswer(int intakeFormId, string question, string key, string answer)
        {
            return new Question
            {
                IntakeFormId = intakeFormId,
                Text = question,
                Key = key,
                Answers = new List<Answer> { new Answer { Text = answer } }
            };
        }

        protected Patient CreateAndPersistPatient(Agent agent)
        {
            Patient patient = CreatePatient(agent.UserAccountId);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient> savedPatient = dbContext.Patient.Add(patient);
            dbContext.SaveChanges();
            return savedPatient.Entity;
        }

        protected Agent CreateAgent()
        {
            Vendor vendor = CreateAndPersistVendor();

            // Create Agent
            Agent agent = CreateAgent(vendor.VendorId);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Agent> savedAgent = dbContext.Agent.Add(agent);
            dbContext.SaveChanges();
            return savedAgent.Entity;
        }

        protected Vendor CreateAndPersistVendor()
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
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Vendor> savedVendor = dbContext.Vendor.Add(vendor);
            dbContext.SaveChanges();
            return savedVendor.Entity;
        }
    }
}

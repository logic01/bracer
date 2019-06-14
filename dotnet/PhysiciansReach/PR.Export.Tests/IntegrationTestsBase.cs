using PR.Business.Business;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            // Create IntakeForm with ICD, HCPCS, Phsycian, and Signature
            var intakeForm = CreateIntakeForm(patientId);
            var savedIntakeForm = dbContext.IntakeForm.Add(intakeForm);
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
            var signature = CreateSignature();
            return new IntakeForm
            {
                PatientId = patientId,
                IntakeFormType = Constants.Enums.IntakeFormType.PainDmeOnly,
                ICD10s = CreateICD10s(),
                HCPCSs = CreateHCPCSs(),
                Physician = CreatePhysician(),
                Signature = signature,
                Status = Constants.Enums.IntakeFormStatus.New
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

        protected static Signature CreateSignature()
        {
            var signatureModel = new SignatureModel
            {

                IpAddress = "123.123.32.192",
                CreatedOn = DateTime.Now,
                Content = "data:image/jpeg;base64,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",
                ModifiedOn = DateTime.Now,

            };
            var signature = new Signature();
            //Testing the mapper to ensure the data:image/jpeg;base64, gets stripped before being stored
            signature.MapFromModel(signatureModel);
            return signature;
        }

        protected static List<HCPCS> CreateHCPCSs()
        {
            return new List<HCPCS>{ new HCPCS
                {
                    Code = "L0650",
                    Product = "Back Brace",
                    Description = "(Lumbar - sacral orthosis.Sagittal control with rigid anterior and posterior panels, " +
                            "posterior panels, posterior extends from Sacrococcygeal junction to the T-9 vertebra, lateral strength, " +
                            "with rigid lateral panels, prefabricated and off the shelf. Custom fitting of the orthosis is not required " +
                            "and the patient or an assisting care giver can apply the prescribed orthotic device with minimal self - adjusting.)",
                    Duration = "99/lifetime"
                }, new HCPCS
                {
                    Code = "L111",
                    Product = "Back Brace",
                    Description = "The custom description for L111",
                    Duration = "99/lifetime"
                } };
        }

        protected static List<ICD10> CreateICD10s()
        {
            return new List<ICD10> { new ICD10
                {
                    Code = "m54.5",
                    Description = "low back pain"
                },new ICD10
                {
                    Code = "m53.2x7",
                    Description = "spinal instabilities"
                },new ICD10
                {
                    Code = "g89.4",
                    Description = "chronic pain"
                },new ICD10
                {
                    Code = "m51.36",
                    Description = "lumbar disc degeneration"
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
                Pharmacy = Constants.Enums.PharmacyType.TWA,
                FirstName = "Dennis",
                LastName = "Reynolds",
                DateOfBirth = DateTime.Now.AddYears(-30),
                PhoneNumber = "2606027777",
                CallBackImmediately = true,
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
            var patient = CreatePatient(agent.UserAccountId);
            var savedPatient = dbContext.Patient.Add(patient);
            dbContext.SaveChanges();
            return savedPatient.Entity;
        }

        protected Agent CreateAgent()
        {
            var vendor = CreateAndPersistVendor();

            // Create Agent
            var agent = CreateAgent(vendor.VendorId);
            var savedAgent = dbContext.Agent.Add(agent);
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
            var savedVendor = dbContext.Vendor.Add(vendor);
            dbContext.SaveChanges();
            return savedVendor.Entity;
        }
    }
}

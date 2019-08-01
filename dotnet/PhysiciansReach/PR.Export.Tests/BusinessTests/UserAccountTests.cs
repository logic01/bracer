using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR.Business;
using PR.Models;
using System;

namespace PR.Export.Tests.BusinessTests
{
    [TestClass]
    public class UserAccountTests : IntegrationTestsBase
    {
        private readonly AdminBusiness adminBiz;
        private readonly PhysicianBusiness physBiz;
        private readonly AgentBusiness agentBiz;
        private readonly VendorBusiness vendorBiz;
        private readonly PatientBusiness patientBiz;

        public UserAccountTests()
        {
            adminBiz = new AdminBusiness(dbContext);
            physBiz = new PhysicianBusiness(dbContext);
            agentBiz = new AgentBusiness(dbContext);
            vendorBiz = new VendorBusiness(dbContext);
            patientBiz = new PatientBusiness(dbContext);
        }

        [TestMethod]
        public void Create_Agent()
        {
            var vendor = CreateAndPersistVendor();
            var agentModel = new AgentModel
            {
                VendorId = vendor.VendorId,
                FirstName = "Luke",
                LastName = "Skywalker",
                UserAccount = new UserAccountModel
                {
                    Type = Constants.Enums.AccountType.Agent,
                    UserName = "agent01",
                    EmailAddress = "agent01@test.com",
                    Password = "P@ssword1",
                    ConfirmationPassword = "P@ssword1",
                    Active = true
                },

            };
            var agent = agentBiz.Create(agentModel);
            var accountId = agent.UserAccount?.UserAccountId ?? 0;
            Assert.IsTrue(accountId > 0);
        }

        [TestMethod]
        public void Create_Admin()
        {
            var adminModel = new AdminModel
            {
                FirstName = "Han",
                LastName = "Solo",
                UserAccount = new UserAccountModel
                {
                    Type = Constants.Enums.AccountType.Admin,
                    UserName = "admin09",
                    EmailAddress = "admin09@test.com",
                    Password = "P@ssword1",
                    ConfirmationPassword = "P@ssword1",
                    Active = true
                }
            };
            var admin = adminBiz.Create(adminModel);
            var accountId = admin.UserAccount?.UserAccountId ?? 0;
            Assert.IsTrue(accountId > 0);
        }

        [TestMethod]
        public void Create_Physican()
        {
            var physModel = new PhysicianModel
            {
                FirstName = "Mantis",
                LastName = "Toaboggan",
                FaxNumber = "1234567890",
                PhoneNumber = "1234567890",
                DEA = "dea000513",
                NPI = "npi008481",
                UserAccount = new UserAccountModel
                {
                    Type = Constants.Enums.AccountType.Physician,
                    EmailAddress = "phy09@test.com",
                    UserName = "phy09",
                    Password = "P@ssword1",
                    ConfirmationPassword = "P@ssword1",
                    Active = true
                },
                Address = new AddressModel
                {
                    AddressLineOne = "234 ads",
                    AddressLineTwo = "",
                    City = "Denver",
                    State = "co",
                    ZipCode = "1123"
                }

            };
            var phys = physBiz.Create(physModel);
            var accountId = phys.UserAccount?.UserAccountId ?? 0;
            Assert.IsTrue(accountId > 0);
        }

        [TestMethod]
        public void Create_Vendor()
        {
            // Crate Vendor
            var vendorModel = new VendorModel
            {
                CompanyName = "Wolf Cola",
                DoingBusinessAs = "Franks Fluids",
                PhoneNumber = "2606027777",
                ContactFirstName = "Charlie",
                ContactLastName = "Kelley",
                Active = true,
            };

            var newVendorModel = vendorBiz.Create(vendorModel);

            Assert.IsNotNull(newVendorModel);
            Assert.IsTrue(newVendorModel.VendorId > 0);
        }

        [TestMethod]
        public void Create_Patient()
        {
            var agent = CreateAgent();

            var patientModel = new PatientModel
            {
                AgentId = agent.UserAccountId,
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
                Address = new AddressModel
                {
                    AddressLineOne = "123 Main Street",
                    City = "denver",
                    State = "CO",
                    ZipCode = "80224",
                },
                PhysiciansAddress = new AddressModel
                {
                    AddressLineOne = "123 Main Street",
                    City = "denver",
                    State = "CO",
                    ZipCode = "80224",
                },
                PrivateInsurance = new PrivateInsuranceModel
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
                Medicare = new MedicareModel
                {
                    MemberId = "13213",
                    PatientGroup = "Patient Group",
                    Pcn = "PCN",
                    SubscriberNumber = "33333",
                    SecondaryCarrier = "Geico",
                    SecondarySubscriberNumber = "4444"
                }
            };

            var newPatientModelId = patientBiz.Create(patientModel);

            
            Assert.IsTrue(newPatientModelId > 0);
        }
    }
}

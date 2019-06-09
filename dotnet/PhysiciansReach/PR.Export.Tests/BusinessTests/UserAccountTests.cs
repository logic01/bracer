using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Export.Tests.BusinessTests
{
    [TestClass]
    public class UserAccountTests : IntegrationTestsBase
    {
        private readonly AgentBusiness agentBiz;
        public UserAccountTests()
        {
            agentBiz = new AgentBusiness(dbContext);
        }

        [TestMethod]
        public void Create_Agent()
        {
            var vendor = CreateAndPersistVendor();
            var agentModel = new Models.AgentModel
            {
                VendorId = vendor.VendorId,
                FirstName = "Luke",
                LastName = "Skywalker",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                UserAccount = new Models.UserAccountModel
                {
                    Type = Constants.Enums.AccountType.Agent,
                    UserName = "agent2",
                    Password = "Password1",
                    EmailAddress = "test@test.com",
                    ConfirmationPassword = "Password1",
                    Active = true
                },

            };
            var agent = agentBiz.Create(agentModel);
            var accountId = agent.UserAccount?.UserAccountId ?? 0;
            Assert.IsTrue(accountId > 0);
        }
    }
}

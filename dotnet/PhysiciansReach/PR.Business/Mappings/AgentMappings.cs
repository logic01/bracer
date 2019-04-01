using PR.Models;
using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class AgentMappings
    {
        public static AgentModel ToModel(this Agent entity)
        {
            AgentModel model = new AgentModel
            {
                UserAccount = entity.UserAccount.ToModel(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                VendorId = entity.VendorId,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Agent ToEntity(this AgentModel model)
        {
            Agent entity = new Agent
            {
                UserAccount = model.UserAccount.ToEntity(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                VendorId = model.VendorId,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            entity.UserAccount.Type = UserAccount.AccountType.Agent;

            return entity;
        }
    }
}

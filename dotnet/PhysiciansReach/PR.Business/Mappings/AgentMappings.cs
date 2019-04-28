using PR.Constants.Enums;
using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class AgentMappings
    {
        public static AgentModel ToModel(this Agent entity)
        {
            var model = new AgentModel
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
            var entity = new Agent
            {
                UserAccount = model.UserAccount.ToEntity(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                VendorId = model.VendorId,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            entity.UserAccount.Type = AccountType.Agent;

            return entity;
        }
    }
}

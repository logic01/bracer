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

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this Agent entity, AgentModel model)
        {
            // userAccountId primary key not mapped
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.VendorId = model.VendorId;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            if (entity.UserAccount == null)
            {
                entity.UserAccount = new UserAccount();
            }

            entity.UserAccount.MapFromModel(model.UserAccount);
        }
    }
}

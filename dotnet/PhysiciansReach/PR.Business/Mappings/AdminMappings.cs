using PR.Constants.Enums;
using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class AdminMappings
    {
        public static AdminModel ToModel(this Admin entity)
        {
            var model = new AdminModel
            {
                UserAccount = entity.UserAccount.ToModel(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
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
        public static void MapFromModel(this Admin entity, AdminModel model)
        {
            // userAccountId primary key not mapped
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
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

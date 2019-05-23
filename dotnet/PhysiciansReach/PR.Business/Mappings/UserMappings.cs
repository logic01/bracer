using PR.Business.Business;
using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class UserMappings
    {
        public static UserAccountModel ToModel(this UserAccount entity)
        {
            // password intentionally not mapped
            var model = new UserAccountModel
            {
                UserAccountId = entity.UserAccountId,
                UserName = entity.UserName,
                EmailAddress = entity.EmailAddress,
                Type = entity.Type,
                Active = entity.Active,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };


            return model;
        }

        public static UserAccount ToEntity(this UserAccountModel model)
        {
            var hash = new PasswordHash(model.Password);

            var hashBytes = hash.ToArray();

            var entity = new UserAccount
            {
                UserAccountId = model.UserAccountId,
                UserName = model.UserName,
                EmailAddress = model.EmailAddress,
                Password = hashBytes,
                Active = model.Active,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

    }
}

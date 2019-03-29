using PhysiciansReach.Models;
using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class UserMappings
    {
        public static UserAccountModel ToModel(this UserAccount entity)
        {
            var model = new UserAccountModel
            {
                UserAccountId = entity.UserAccountId,
                UserName = entity.UserName,
                // Password = entity.Password, Never let the password leave the API
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static UserAccount ToEntity(this UserAccountModel model)
        {
            var entity = new UserAccount
            {
                UserAccountId = model.UserAccountId,
                UserName = model.UserName,
                Password = model.Password,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }
    }
}

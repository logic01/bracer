using PR.Models;
using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class AdminMappings
    {
        public static AdminModel ToModel(this Admin entity)
        {
            AdminModel model = new AdminModel
            {
                UserAccount = entity.UserAccount.ToModel(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Admin ToEntity(this AdminModel model)
        {
            Admin entity = new Admin
            {
                UserAccount = model.UserAccount.ToEntity(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }
    }
}

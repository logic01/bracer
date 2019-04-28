using PR.Constants.Enums;
using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class PhysicianMappings
    {
        public static PhysicianModel ToModel(this Physician entity)
        {
            var model = new PhysicianModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                UserAccount = entity.UserAccount.ToModel(),
                Address = entity.Address.ToModel()
            };

            return model;
        }

        public static Physician ToEntity(this PhysicianModel model)
        {
            var entity = new Physician
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                UserAccount = model.UserAccount.ToEntity(),
                Address = model.Address.ToEntity()
            };

            entity.UserAccount.Type = AccountType.Physician;

            return entity;
        }
    }
}

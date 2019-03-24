using PhysiciansReach.Models;
using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class PhysicianMappings
    {
        public static PhysicianModel ToModel(this Physician entity)
        {
            PhysicianModel model = new PhysicianModel
            {
                UserAccount = entity.UserAccount.ToModel(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                ContactFirstName = entity.ContactFirstName,
                ContactLastName = entity.ContactLastName,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Physician ToEntity(this PhysicianModel model)
        {
            Physician entity = new Physician
            {
                UserAccount = model.UserAccount.ToEntity(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContactFirstName = model.ContactFirstName,
                ContactLastName = model.ContactLastName,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }
    }
}

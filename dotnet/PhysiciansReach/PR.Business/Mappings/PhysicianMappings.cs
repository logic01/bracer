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
                FaxNumber = entity.FaxNumber,
                NPI = entity.NPI,
                DEA = entity.DEA,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                UserAccount = entity.UserAccount?.ToModel(),
                Address = entity.Address.ToModel()
            };

            return model;
        }

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this Physician entity, PhysicianModel model)
        {
            // userAccountId primary key not mapped
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.FaxNumber = model.FaxNumber;
            entity.NPI = model.NPI;
            entity.DEA = model.DEA;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            if (entity.Address == null)
            {
                entity.Address = new Address();
            }

            entity.Address.MapFromModel(model.Address);

            if (entity.UserAccount == null)
            {
                entity.UserAccount = new UserAccount();
            }

            entity.UserAccount.MapFromModel(model.UserAccount);

        }
    }
}

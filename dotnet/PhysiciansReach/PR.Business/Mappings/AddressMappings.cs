using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class AddressMappings
    {
        public static AddressModel ToModel(this Address entity)
        {
            var model = new AddressModel
            {
                AddressId = entity.AddressId,
                AddressLineOne = entity.AddressLineOne,
                AddressLineTwo = entity.AddressLineTwo,
                City = entity.City,
                State = entity.State,
                ZipCode = entity.ZipCode,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Address ToEntity(this AddressModel model)
        {
            var entity = new Address
            {
                AddressId = model.AddressId,
                AddressLineOne = model.AddressLineOne,
                AddressLineTwo = model.AddressLineTwo,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this Address entity, AddressModel model)
        {
            //TODO Is this code needed?
            if (entity == null)
            {
                entity = new Address();
            }
            // addressId primary key not mapped
            entity.AddressLineOne = model.AddressLineOne;
            entity.AddressLineTwo = model.AddressLineTwo;
            entity.City = model.City;
            entity.State = model.State;
            entity.ZipCode = model.ZipCode;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;
        }
    }
}

using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class AddressMappings
    {
        public static AddressModel ToModel(this Address entity)
        {
            AddressModel model = new AddressModel
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
            Address entity = new Address
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
    }
}

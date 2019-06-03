using PR.Data.Models;
using PR.Models;
namespace PR.Business.Mappings
{
    public static class VendorMappings
    {
        public static VendorModel ToModel(this Vendor entity)
        {
            var model = new VendorModel
            {
                VendorId = entity.VendorId,
                CompanyName = entity.CompanyName,
                DoingBusinessAs = entity.DoingBusinessAs,
                PhoneNumber = entity.PhoneNumber,
                ContactFirstName = entity.ContactFirstName,
                ContactLastName = entity.ContactLastName,
                Active = entity.Active,
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
        public static void MapFromModel(this Vendor entity, VendorModel model)
        {
            // vendorId primary key not mapped
            entity.CompanyName = model.CompanyName;
            entity.DoingBusinessAs = model.DoingBusinessAs;
            entity.PhoneNumber = model.PhoneNumber;
            entity.ContactFirstName = model.ContactFirstName;
            entity.ContactLastName = model.ContactLastName;
            entity.Active = model.Active;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

        }
    }
}

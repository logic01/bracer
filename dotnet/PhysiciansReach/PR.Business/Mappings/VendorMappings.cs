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
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Vendor ToEntity(this VendorModel model)
        {
            var entity = new Vendor
            {
                VendorId = model.VendorId,
                CompanyName = model.CompanyName,
                DoingBusinessAs = model.DoingBusinessAs,
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

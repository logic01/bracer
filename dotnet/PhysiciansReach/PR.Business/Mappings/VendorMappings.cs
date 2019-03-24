using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class VendorMappings
    {
        public static VendorModel ToModel(this Vendor entity)
        {
            VendorModel model = new VendorModel
            {
                VendorId = entity.VendorId,
                CompanyName = entity.CompanyName,
                DoingBusinessAs = entity.DoingBusinessAs,
                PhoneNumber = entity.PhoneNumber,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Vendor ToEntity(this VendorModel model)
        {
            Vendor entity = new Vendor
            {
                VendorId = model.VendorId,
                CompanyName = model.CompanyName,
                DoingBusinessAs = model.DoingBusinessAs,
                PhoneNumber = model.PhoneNumber,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }
    }
}

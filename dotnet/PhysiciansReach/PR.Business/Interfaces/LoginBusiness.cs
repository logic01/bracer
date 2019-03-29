using PR.Data.Models;
using System.Collections.Generic;
using PR.Models;
namespace PR.Business.Interfaces
{
    public interface IVendorBusiness
    {
        List<VendorModel> Get();

        VendorModel Get(int id);

        VendorModel Create(VendorModel vendorModel);

        VendorModel Update(VendorModel vendorModel);
    }
}

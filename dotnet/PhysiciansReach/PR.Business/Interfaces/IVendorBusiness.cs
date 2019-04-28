using PR.Models;
using System.Collections.Generic;

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

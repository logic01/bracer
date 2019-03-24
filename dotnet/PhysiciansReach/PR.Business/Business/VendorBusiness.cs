using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class VendorBusiness : IVendorBusiness
    {
        private DataContext _context;

        public VendorBusiness(DataContext context)
        {
            _context = context;
        }

        public List<VendorModel> Get()
        {
            return _context.Vendor.Select(i => i.ToModel()).ToList();
        }

        public VendorModel Get(int vendorId)
        {
            Vendor vendor = _context.Vendor.FirstOrDefault(v => v.VendorId == vendorId);

            return vendor.ToModel();
        }

        public VendorModel Create(VendorModel vendorModel)
        {
            Vendor vendor = vendorModel.ToEntity();

            _context.Vendor.Add(vendor);
            _context.SaveChanges();

            return vendor.ToModel();
        }

        public VendorModel Update(VendorModel vendorModel)
        {
            Vendor vendor = _context.Vendor.FirstOrDefault(v => v.VendorId == vendorModel.VendorId);

            vendor = vendorModel.ToEntity();
            _context.Vendor.Add(vendor);
            _context.SaveChanges();

            return vendor.ToModel();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System;
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
            var vendor = new Vendor();
            vendor.MapFromModel(vendorModel);

            _context.Vendor.Add(vendor);
            _context.SaveChanges();

            return vendor.ToModel();
        }

        public VendorModel Update(VendorModel vendorModel)
        {
            Vendor vendor = _context.Vendor
                .Include("Agent.UserAccount")
                .FirstOrDefault(v => v.VendorId == vendorModel.VendorId);

            if (vendor.Active != vendorModel.Active)
            {
                InverseAgentsActive(vendor);
            }

            vendor.MapFromModel(vendorModel);
            vendor.ModifiedOn = DateTime.Now;

            _context.SaveChanges();


            return vendor.ToModel();
        }

        private void InverseAgentsActive(Vendor vendor)
        {
            foreach(var agent in vendor.Agent)
            {
                agent.UserAccount.Active = !agent.UserAccount.Active;
            }

            _context.SaveChanges();
        }
    }
}

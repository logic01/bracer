using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Exceptions;
using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private DataContext _context;

        public AdminBusiness(DataContext context)
        {
            _context = context;
        }

        public List<AdminModel> Get()
        {
            return _context.Admin
                    .Include(p => p.UserAccount)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public AdminModel Get(int userAccountId)
        {
            Admin admin = _context.Admin
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == userAccountId);

            return admin.ToModel();
        }

        public AdminModel Create(AdminModel adminModel)
        {
            var admin = new Admin();
            admin.MapFromModel(adminModel);

            _context.Admin.Add(admin);
            _context.SaveChanges();

            return admin.ToModel();
        }

        public int Update(AdminModel adminModel)
        {
            Admin admin = _context.Admin
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == adminModel.UserAccount.UserAccountId);

            admin.MapFromModel(adminModel);
            admin.UserAccount.ModifiedOn = DateTime.Now;
            admin.ModifiedOn = DateTime.Now;

            _context.SaveChanges();

            return admin.UserAccountId;
        }
    }
}

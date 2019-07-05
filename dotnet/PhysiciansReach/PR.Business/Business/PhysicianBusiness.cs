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
    public class PhysicianBusiness : IPhysicianBusiness
    {
        private DataContext _context;

        public PhysicianBusiness(DataContext context)
        {
            _context = context;
        }

        public PhysicianModel Get(int userAccountId)
        {
            Physician physician = _context.Physician
                .Include(p => p.UserAccount)
                .Include(p => p.Address)
                .FirstOrDefault(u => u.UserAccountId == userAccountId);

            return physician.ToModel();
        }

        public List<PhysicianModel> GetAll()
        {
            return _context.Physician
                    .Include(p => p.UserAccount)
                    .Include(p => p.Address)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public List<PhysicianModel> Get(int[] ids)
        {
            return _context.Physician
                    .Include(p => p.UserAccount)
                    .Include(p => p.Address)
                    .Where(p => ids.Contains(p.UserAccountId))
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public PhysicianModel Create(PhysicianModel physicianModel)
        {
            var physician = new Physician();
            physician.MapFromModel(physicianModel);

            _context.Physician.Add(physician);
            _context.SaveChanges();

            return physician.ToModel();
        }

        public PhysicianModel Update(PhysicianModel physicianModel)
        {
            Physician physician = _context.Physician
                .Include(p => p.UserAccount)
                .Include(p => p.Address)
                .FirstOrDefault(u => u.UserAccountId == physicianModel.UserAccount.UserAccountId);

            physician.MapFromModel(physicianModel);

            physician.UserAccount.ModifiedOn = DateTime.Now;
            physician.ModifiedOn = DateTime.Now;
            physician.Address.ModifiedOn = DateTime.Now;

            _context.SaveChanges();

            return physician.ToModel();
        }


    }
}

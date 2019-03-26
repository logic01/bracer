using Microsoft.EntityFrameworkCore;
using PhysiciansReach.Models;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
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

        public List<PhysicianModel> Get()
        {
            return _context.Physician
                    .Include(p => p.UserAccount)
                    .Include(p => p.Address)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public PhysicianModel Get(int userAccountId)
        {
            Physician physician = _context.Physician
                .Include(p => p.UserAccount)
                .Include(p => p.Address)
                .FirstOrDefault(u => u.UserAccountId == userAccountId);

            return physician.ToModel();
        }

        public PhysicianModel Create(PhysicianModel physicianModel)
        {
            Physician physician = physicianModel.ToEntity();

            _context.Physician.Add(physician);
            _context.SaveChanges();

            return physician.ToModel();
        }

        public PhysicianModel Update(PhysicianModel physicianModel)
        {
            Physician physician = _context.Physician.FirstOrDefault(u => u.UserAccountId == physicianModel.UserAccount.UserAccountId);

            physician = physicianModel.ToEntity();
            _context.Physician.Add(physician);
            _context.SaveChanges();

            return physician.ToModel();
        }


    }
}

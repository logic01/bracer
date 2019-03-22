using PhysiciansReach.Models;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class UserBusiness : IUserBusiness
    {
        private DataContext _context;

        public UserBusiness(DataContext context)
        {
            _context = context;
        }

        public List<UserAccountModel> Get(int[] userIds)
        {
            return new List<UserAccountModel> { new UserAccountModel() };
        }

        public UserAccountModel Get(int userId)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userId);
            return user.ToModel();
        }

        public UserAccountModel Create(UserAccountModel userAccount)
        {
            var user = userAccount.ToEntity();

            _context.User.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }

        public UserAccountModel Update(UserAccountModel userAccount)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userAccount.UserId);

            user = userAccount.ToEntity();

            _context.User.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }


    }
}

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

        public UserAccountModel Get(int userAccountId)
        {
            var user = _context.UserAccount.FirstOrDefault(u => u.UserAccountId == userAccountId);

            return user.ToModel();
        }

        public UserAccountModel Create(UserAccountModel userAccountModel)
        {
            var user = userAccountModel.ToEntity();

            _context.UserAccount.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }

        public UserAccountModel Update(UserAccountModel userAccountModel)
        {
            var user = _context.UserAccount.FirstOrDefault(u => u.UserAccountId == userAccountModel.UserId);

            user = userAccountModel.ToEntity();
            _context.UserAccount.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }


    }
}

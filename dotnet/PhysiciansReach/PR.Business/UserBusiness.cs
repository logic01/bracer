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

        public List<UserModel> Get(int[] userIds)
        {
            return new List<UserModel> { new UserModel() };
        }

        public UserModel Get(int userId)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userId);
            return user.ToModel();
        }

        public UserModel Create(UserModel userModel)
        {
            var user = userModel.ToEntity();

            _context.User.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }

        public UserModel Update(UserModel userModel)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userModel.UserId);

            user = userModel.ToEntity();

            _context.User.Add(user);
            _context.SaveChanges();

            return user.ToModel();
        }


    }
}

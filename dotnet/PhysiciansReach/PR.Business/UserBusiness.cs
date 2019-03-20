using PhysiciansReach.Models;
using PR.Business.Interfaces;
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
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return Map(user);
        }

        public UserModel Create(UserModel userModel)
        {
            var user = Map(userModel);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Map(user);
        }

        public UserModel Update(UserModel userModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userModel.UserId);
            user = Map(userModel);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Map(user);
        }

        private UserModel Map(User user)
        {
            return new UserModel
            {
                UserId = user.UserId,
                ConfirmationPassword = user.ConfirmationPassword,
                ContactFirstName = user.ConfirmationPassword,
                ContactLastName = user.ConfirmationPassword,
                FirstName = user.ConfirmationPassword,
                LastName = user.ConfirmationPassword,
                Password = user.ConfirmationPassword,
                PhoneNumber = user.ConfirmationPassword,
                UserName = user.UserName
            };
        }

        private User Map(UserModel userModel)
        {
            return new User
            {
                UserId = userModel.UserId,
                ConfirmationPassword = userModel.ConfirmationPassword,
                ContactFirstName = userModel.ConfirmationPassword,
                ContactLastName = userModel.ConfirmationPassword,
                FirstName = userModel.ConfirmationPassword,
                LastName = userModel.ConfirmationPassword,
                Password = userModel.ConfirmationPassword,
                PhoneNumber = userModel.ConfirmationPassword,
                UserName = userModel.UserName
            };
        }
    }
}

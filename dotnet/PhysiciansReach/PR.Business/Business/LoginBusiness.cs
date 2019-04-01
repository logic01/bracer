using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        private DataContext _context;

        public LoginBusiness(DataContext context)
        {
            _context = context;
        }

        public UserAccountModel Login(UserAccountModel userAccountModel)
        {
            var userAccount = userAccountModel.ToEntity();

            var user = _context.UserAccount.FirstOrDefault(u => u.UserName == userAccount.UserName);

            if (user != null)
            {
                var hash = new PasswordHash(user.Password);
                if (hash.Verify(userAccountModel.Password))
                {
                    return user.ToModel();
                }
            }

            return LoginFailed();
        }

        private UserAccountModel LoginFailed()
        {
            var model = new UserAccountModel
            {
                Type = Models.Enum.AccountType.None,
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Code = "001",
                        Message = "Login Failed"
                    }
                }
            };
            return model;
        }

    }
}

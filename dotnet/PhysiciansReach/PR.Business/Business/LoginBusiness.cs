using PR.Models;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
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

        public UserAccountModel Login(UserAccountModel userAccount)
        {
            UserAccount user = _context.UserAccount
                .FirstOrDefault(u => u.UserAccountId == userAccount.UserAccountId && u.Password == userAccount.Password);

            return user.ToModel();
        }

    }
}

using PR.Business.Interfaces;
using PR.Data.Models;
using System.Linq;

namespace PR.Business
{
    public class UserAccountBusiness : IUserAccountBusiness
    {
        private DataContext _context;

        public UserAccountBusiness(DataContext context)
        {
            _context = context;
        }

        public bool Exists(string userName)
        {
            UserAccount user = _context.UserAccount.FirstOrDefault(u => u.UserName == userName);

            return user != null;
        }

    }
}

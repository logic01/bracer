using PR.Business.Interfaces;
using PR.Data.Models;
namespace PR.Business
{
    public class UserBusiness : IUserBusiness
    {
        private DataContext _context;

        public UserBusiness(DataContext context) {
            _context = context;
        }

        public void SaveUser()
        {
            var user = new User
            {
                UserName = "bpohl"
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

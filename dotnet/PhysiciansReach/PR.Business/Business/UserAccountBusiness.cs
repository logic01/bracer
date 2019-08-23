using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PR.Business.Business;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Configurations;
using PR.Data.Models;
using PR.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PR.Business
{
    public class UserAccountBusiness : IUserAccountBusiness
    {
        private DataContext _context;
        private readonly SecuritySettings _securitySettings;

        public UserAccountBusiness(DataContext context, IOptions<SecuritySettings> securitySettings)
        {
            _context = context;
            _securitySettings = securitySettings.Value;
        }

        public bool Exists(string userName)
        {
            UserAccount user = _context.UserAccount.FirstOrDefault(u => u.UserName == userName);

            return user != null;
        }


    }
}

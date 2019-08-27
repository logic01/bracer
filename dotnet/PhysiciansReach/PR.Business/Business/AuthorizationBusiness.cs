using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Configurations;
using PR.Constants.Enums;
using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PR.Business.Business
{
    public class AuthorizationBusiness : IAuthorizationBusiness
    {
        private DataContext _context;
        private ILoggingBusiness _logging;
        private SecuritySettings _securitySettings;

        public AuthorizationBusiness(DataContext context, ILoggingBusiness logging, IOptions<SecuritySettings> securitySettings)
        {
            _context = context;
            _logging = logging;
            _securitySettings = securitySettings.Value;
        }

        public UserAccountModel Login(UserAccountModel userAccountModel)
        {
            _logging.Log(LogSeverity.Info, JsonConvert.SerializeObject(userAccountModel));

            UserAccount userAccount = userAccountModel.ToEntity();

            UserAccount user = _context.UserAccount.FirstOrDefault(u => u.UserName == userAccount.UserName && u.Active);

            if (user != null)
            {
                var hash = new PasswordHash(user.Password);

                _logging.Log(LogSeverity.Info, $"newhash: {hash}, oldhash: {userAccountModel.Password}");

                if (hash.Verify(userAccountModel.Password))
                {
                    var userModel = user.ToModel();
                    userModel.Token = GetToken(userModel.UserAccountId);

                    return userModel;
                }
            }

            _logging.Log(LogSeverity.Error, "Login Failed", JsonConvert.SerializeObject(userAccountModel));

            return Unauthorized();
        }

        private string GetToken(int userAccountId)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var expire = DateTime.UtcNow;

            var handler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                claims: new List<Claim>(),
                expires: expire.AddMinutes(_securitySettings.TimeoutInMinutes),
                signingCredentials: signinCredentials
            );

            return handler.WriteToken(token);

        }

        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }

        private UserAccountModel Unauthorized()
        {
            var model = new UserAccountModel
            {
                Type = AccountType.None,
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                        ErrorCode = "001",
                        Message = "Login Failed"
                    }
                }
            };
            return model;
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthorizationBusiness _authBusiness;
        private readonly ILoggingBusiness _logging;

        public LoginController(IAuthorizationBusiness authBusiness, ILoggingBusiness logging)
        {
            _authBusiness = authBusiness;
            _logging = logging;
        }

        [HttpPost]
        public ActionResult<UserAccountModel> Post([FromBody] UserAccountModel userAccountModel)
        {
            var user = _authBusiness.Login(userAccountModel);

            return user;
        }
    }
}

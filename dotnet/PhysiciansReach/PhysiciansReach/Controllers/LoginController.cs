using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;
        private readonly ILoggingBusiness _logging;

        public LoginController(ILoginBusiness loginBusiness, ILoggingBusiness logging)
        {
            _loginBusiness = loginBusiness;
            _logging = logging;
        }

        [HttpPost]
        public ActionResult<UserAccountModel> Post([FromBody] UserAccountModel userAccountModel)
        {
            return _loginBusiness.Login(userAccountModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PR.Business.Interfaces;
using PR.Models;
using static PR.Data.Models.Log;

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
            _logging.Log(LogSeverity.Info, "Login");
            return _loginBusiness.Login(userAccountModel);
        }
    }
}

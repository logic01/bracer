using Microsoft.AspNetCore.Mvc;
using PR.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        public ActionResult<UserAccountModel> Post([FromBody] UserAccountModel userAccountModel)
        {
            return _loginBusiness.Login(userAccountModel);
        }
    }
}

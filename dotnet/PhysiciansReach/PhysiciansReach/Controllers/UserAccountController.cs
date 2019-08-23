using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountBusiness _business;
        private readonly ILoggingBusiness _logging;

        public UserAccountController(IUserAccountBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet("{username}")]
        public ActionResult<bool> Exists(string username)
        {
            return _business.Exists(username);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public AdminController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<AdminModel> Get(int id)
        {
            var user = _userBusiness.Get(id);
            //var admin = _adminBusiness.Get(id);
            return new AdminModel();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<AdminModel> Post([FromBody] AdminModel admin)
        {
            var user = _userBusiness.Create(admin.UserAccount);
            //var admin = _adminBusiness.Create(admin);

            return new AdminModel();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<AdminModel> Put(int id, [FromBody] AdminModel admin)
        {
            var user = _userBusiness.Update(admin.UserAccount);
            //var admin = _adminBusiness.Update(admin);

            return new AdminModel();
        }
    }
}

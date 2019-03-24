using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<AdminModel> Get(int id)
        {
            return _adminBusiness.Get(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<AdminModel> Post([FromBody] AdminModel admin)
        {
            return _adminBusiness.Create(admin);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<AdminModel> Put(int id, [FromBody] AdminModel admin)
        {
            return _adminBusiness.Update(admin);
        }
    }
}

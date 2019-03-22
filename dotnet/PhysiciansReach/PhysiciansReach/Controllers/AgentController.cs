using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public AgentController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<AgentModel> Get(int id)
        {
            var user = _userBusiness.Get(id);
            //var admin = _adminBusiness.Get(id);
            return new AgentModel();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<AgentModel> Post([FromBody] AgentModel admin)
        {
            var user = _userBusiness.Create(admin.UserAccount);
            //var admin = _adminBusiness.Create(admin);

            return new AgentModel();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<AgentModel> Put(int id, [FromBody] AgentModel admin)
        {
            var user = _userBusiness.Update(admin.UserAccount);
            //var admin = _adminBusiness.Update(admin);

            return new AgentModel();
        }
    }
}

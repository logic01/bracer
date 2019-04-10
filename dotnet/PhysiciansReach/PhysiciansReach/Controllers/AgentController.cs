using Microsoft.AspNetCore.Mvc;
using PR.Models;
using PR.Business.Interfaces;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentBusiness _business;

        public AgentController(IAgentBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public ActionResult<List<AgentModel>> Get()
        {
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<AgentModel> Get(int id)
        {
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<AgentModel> Post([FromBody] AgentModel agent)
        {
            return _business.Create(agent);
        }

        [HttpPut("{id}")]
        public ActionResult<AgentModel> Put(int id, [FromBody] AgentModel agent)
        {
            return _business.Update(agent);
        }
    }
}

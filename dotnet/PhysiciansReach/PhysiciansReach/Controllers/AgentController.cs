using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentBusiness _business;
        private readonly ILoggingBusiness _logging;

        public AgentController(IAgentBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<AgentModel>> Get()
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Get All Agent");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<AgentModel> Get(int id)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Get Agent");
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<AgentModel> Post([FromBody] AgentModel agent)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Post Agent");
            return _business.Create(agent);
        }

        [HttpPut("{id}")]
        public ActionResult<AgentModel> Put(int id, [FromBody] AgentModel agent)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Put Agent");
            return _business.Update(agent);
        }
    }

}

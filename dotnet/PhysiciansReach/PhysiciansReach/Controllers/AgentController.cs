﻿using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentBusiness _business;

        public AgentController(IAgentBusiness business)
        {
            _business = business;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<AgentModel> Get(int id)
        {
            return _business.Get(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<AgentModel> Post([FromBody] AgentModel agent)
        {
            return _business.Create(agent);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<AgentModel> Put(int id, [FromBody] AgentModel agent)
        {
            return _business.Update(agent);
        }
    }
}

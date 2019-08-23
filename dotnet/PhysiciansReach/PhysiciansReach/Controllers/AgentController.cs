using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
        public ActionResult<List<AgentModel>> Get([FromQuery]int[] ids)
        {
            try
            {
                if (ids.Length == 0)
                {
                    return _business.GetAll();
                }
                else
                {
                    return _business.Get(ids);
                }
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AgentModel> Get(int id)
        {
            try
            {
                return _business.Get(id);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPost]
        public ActionResult<AgentModel> Post([FromBody] AgentModel agent)
        {
            try
            {
                agent.UserAccount.Type = AccountType.Agent;
                return _business.Create(agent);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPut("{id}")]
        public ActionResult<AgentModel> Put(int id, [FromBody] AgentModel agent)
        {
            try
            {
                agent.UserAccount.Type = AccountType.Agent;
                agent.UserAccount.UserAccountId = id;

                return _business.Update(agent);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }
    }

}

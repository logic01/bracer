using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhysicianController : ControllerBase
    {
        private readonly IPhysicianBusiness _business;
        private readonly ILoggingBusiness _logging;

        public PhysicianController(IPhysicianBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<PhysicianModel>> Get()
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Get All Physician");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<PhysicianModel> Get(int id)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Get Physician");
            return _business.Get(id);
        }
        [HttpPost]
        public ActionResult<PhysicianModel> Post([FromBody] PhysicianModel physician)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Post Physician");
            return _business.Create(physician);
        }

        [HttpPut("{id}")]
        public ActionResult<PhysicianModel> Put(int id, [FromBody] PhysicianModel physician)
        {
            _logging.Log(PR.Data.Models.Log.LogSeverity.Info, "Put Physician");
            return _business.Update(physician);
        }
    }
}

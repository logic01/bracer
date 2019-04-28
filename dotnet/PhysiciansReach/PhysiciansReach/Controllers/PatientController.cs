using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _business;
        private readonly ILoggingBusiness _logging;

        public PatientController(IPatientBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<PatientModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All Patient");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<PatientModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get Patient");
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<PatientModel> Post([FromBody] PatientModel patient)
        {
            _logging.Log(LogSeverity.Info, "Post Patient");
            return _business.Create(patient);
        }

        [HttpPut("{id}")]
        public ActionResult<PatientModel> Put(int id, [FromBody] PatientModel patient)
        {
            _logging.Log(LogSeverity.Info, "Put Patient");
            return _business.Update(patient);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PR.Api.Models;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
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

        [HttpGet("Patient")]
        public ActionResult<List<PatientModel>> Get([FromQuery]int[] ids)
        {
            _logging.Log(LogSeverity.Info, "Get All Patient");
            return _business.Get(ids);
        }

        [HttpGet("Patient")]
        public ActionResult<List<PatientModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All Patient");
            return _business.Get();
        }

        [HttpGet("Patient/{id}")]
        public ActionResult<PatientModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get Patient");
            return _business.Get(id);
        }

        [HttpGet("Agent/{agentId}/Patient")]
        public ActionResult<List<PatientModel>> GetByAgent(int agentId)
        {
            _logging.Log(LogSeverity.Info, "Get Patient By Agent");
            return _business.GetByAgent(agentId);
        }

        [HttpGet("Vendor/{vendorId}/Patient")]
        public ActionResult<List<PatientModel>> GetByVendor(int vendorId)
        {
            _logging.Log(LogSeverity.Info, "Get Patient By Vendor");
            return _business.GetByVendor(vendorId);
        }

        [HttpPost("Patient")]
        public ActionResult<PatientModel> Post([FromBody] PatientModel patient)
        {
            _logging.Log(LogSeverity.Info, "Post Patient");
            var patientId = _business.Create(patient);

            return CreatedAtAction("Post", new { patientId });
        }

        [HttpPut("Patient/{id}")]
        public ActionResult<PatientModel> Put(int id, [FromBody] PatientModel patient)
        {
            _logging.Log(LogSeverity.Info, "Put Patient");

            patient.PatientId = id;

            _business.Update(patient);

            return Ok();
        }
    }
}

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

        [HttpGet("Patient/{id}")]
        public ActionResult<PatientModel> Get(int id)
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

        [HttpGet("Agent/{agentId}/Patient")]
        public ActionResult<List<PatientModel>> GetByAgent(int agentId)
        {
            try
            {
                return _business.GetByAgent(agentId);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("Vendor/{vendorId}/Patient")]
        public ActionResult<List<PatientModel>> GetByVendor(int vendorId)
        {
            try
            {
                return _business.GetByVendor(vendorId);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPost("Patient")]
        public ActionResult<PatientModel> Post([FromBody] PatientModel patient)
        {
            try
            {
                var patientId = _business.Create(patient);

                return CreatedAtAction("Post", new { patientId });
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPut("Patient/{id}")]
        public ActionResult<PatientModel> Put(int id, [FromBody] PatientModel patient)
        {
            try
            {
                patient.PatientId = id;

                _business.Update(patient);

                return Ok();
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }

        }
    }
}

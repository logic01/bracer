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
    public class IntakeFormController : ControllerBase
    {
        private readonly IIntakeFormBusiness _intakeBusiness;
        private readonly IDocumentBusiness _documentBusiness;
        private readonly ILoggingBusiness _logging;

        public IntakeFormController(IIntakeFormBusiness intakeBusiness, IDocumentBusiness documentBusiness, ILoggingBusiness logging)
        {
            _intakeBusiness = intakeBusiness;
            _documentBusiness = documentBusiness;
            _logging = logging;
        }

        [HttpGet("IntakeForm")]
        public ActionResult<List<IntakeFormModel>> Get()
        {
            try
            {
                return _intakeBusiness.Get();
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("IntakeForm/{id}")]
        public ActionResult<IntakeFormModel> Get(int id)
        {
            try
            {
                return _intakeBusiness.Get(id);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("Patient/{patientId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByPatient(int patientId)
        {
            try
            {
                return _intakeBusiness.GetByPatient(patientId);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }


        [HttpGet("Physician/{physicianId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByPhysician(int physicianId)
        {
            try
            {
                return _intakeBusiness.GetByPhysician(physicianId);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("Vendor/{vendorId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByVendor(int vendorId)
        {
            try
            {
                return _intakeBusiness.GetByVendor(vendorId);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPost("IntakeForm")]
        public ActionResult<IntakeFormModel> Post([FromBody] IntakeFormModel intakeForm)
        {
            try
            {
                IntakeFormModel newIntakeForm = _intakeBusiness.Create(intakeForm);

                return newIntakeForm;
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPut("IntakeForm/{id}")]
        public ActionResult<IntakeFormModel> Put(int id, [FromBody] IntakeFormModel intakeForm)
        {
            try
            {
                intakeForm.IntakeFormId = id;

                return _intakeBusiness.Update(intakeForm);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }
    }
}

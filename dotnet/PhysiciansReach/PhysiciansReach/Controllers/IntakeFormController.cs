﻿using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
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
            _logging.Log(LogSeverity.Info, "Get All IntakeForm");
            return _intakeBusiness.Get();
        }

        [HttpGet("IntakeForm/{id}")]
        public ActionResult<IntakeFormModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get IntakeForm");
            return _intakeBusiness.Get(id);
        }

        [HttpGet("Patient/{patientId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByPatient(int patientId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _intakeBusiness.GetByPatient(patientId);
        }


        [HttpGet("Physician/{physicianId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByPhysician(int physicianId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _intakeBusiness.GetByPhysician(physicianId);
        }

        [HttpGet("Vendor/{vendorId}/IntakeForm")]
        public ActionResult<List<IntakeFormModel>> GetByVendor(int vendorId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _intakeBusiness.GetByVendor(vendorId);
        }

        [HttpPost("IntakeForm")]
        public ActionResult<IntakeFormModel> Post([FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Post IntakeForm");

            IntakeFormModel newIntakeForm = _intakeBusiness.Create(intakeForm);

            return newIntakeForm;
        }

        [HttpPut("IntakeForm/{id}")]
        public ActionResult<IntakeFormModel> Put(int id, [FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Put IntakeForm");

            intakeForm.IntakeFormId = id;

            return _intakeBusiness.Update(intakeForm);
        }
    }
}

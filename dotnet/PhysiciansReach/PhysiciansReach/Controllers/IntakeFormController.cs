using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
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

        [HttpGet]
        public ActionResult<List<IntakeFormModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All IntakeForm");
            return _intakeBusiness.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<IntakeFormModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get IntakeForm");
            return _intakeBusiness.Get(id);
        }

        [HttpPost]
        public ActionResult<IntakeFormModel> Post([FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Post IntakeForm");

            var newIntakeForm = _intakeBusiness.Create(intakeForm);

            _documentBusiness.CreateIntakeFormDocument(newIntakeForm.PatientId, newIntakeForm.IntakeFormId);

            return newIntakeForm;
        }

        [HttpPut("{id}")]
        public ActionResult<IntakeFormModel> Put(int id, [FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Put IntakeForm");
            return _intakeBusiness.Update(intakeForm);
        }
    }
}

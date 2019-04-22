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
        private readonly IIntakeFormBusiness _business;
        private readonly ILoggingBusiness _logging;

        public IntakeFormController(IIntakeFormBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<IntakeFormModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All IntakeForm");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<IntakeFormModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get IntakeForm");
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<IntakeFormModel> Post([FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Post IntakeForm");
            return _business.Create(intakeForm);
        }

        [HttpPut("{id}")]
        public ActionResult<IntakeFormModel> Put(int id, [FromBody] IntakeFormModel intakeForm)
        {
            _logging.Log(LogSeverity.Info, "Put IntakeForm");
            return _business.Update(intakeForm);
        }
    }
}

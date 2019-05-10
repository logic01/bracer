using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailBusiness _emailBusiness;
        private readonly ILoggingBusiness _logging;

        public EmailController(IEmailBusiness emailBusiness, ILoggingBusiness logging)
        {
            _emailBusiness = emailBusiness;
            _logging = logging;
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] SendEmailModel sendEmailModel)
        {
            _logging.Log(LogSeverity.Info, "Send Email");
            return _emailBusiness.SendEmail(sendEmailModel.DocumentId, sendEmailModel.EmailAddress);
        }
    }
}

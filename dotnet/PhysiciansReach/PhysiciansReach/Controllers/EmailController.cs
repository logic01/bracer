using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
            return _emailBusiness.SendEmail(sendEmailModel.DocumentId, sendEmailModel.EmailAddress);
        }
    }
}

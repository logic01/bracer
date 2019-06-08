using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureBusiness _business;
        private readonly ILoggingBusiness _logging;

        public SignatureController(ISignatureBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpPost("IntakeForm/{intakeFormId}/Signature")]
        public ActionResult Post(int intakeFormId, [FromBody] SignatureModel signature)
        {
            _logging.Log(LogSeverity.Info, "Sign Document");

            // get the client ip address
            signature.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            _business.Create(intakeFormId, signature);

            return Ok();
        }

    }
}

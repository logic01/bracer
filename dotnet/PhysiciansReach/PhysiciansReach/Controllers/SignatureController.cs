using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureBusiness _business;
        private readonly ILoggingBusiness _logging;
        private IHttpContextAccessor _accessor;

        public SignatureController(
            IHttpContextAccessor accessor,
            ISignatureBusiness business,
            ILoggingBusiness logging)
        {
            _accessor = accessor;
            _business = business;
            _logging = logging;
        }

        [HttpPost("IntakeForm/{intakeFormId}/Signature")]
        public ActionResult Post(int intakeFormId, [FromBody] SignatureModel signature)
        {
            try
            {
                // set the client ip address
                signature.IpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                var signatureId = _business.Create(intakeFormId, signature);

                return CreatedAtAction("Post", new { signatureId });

            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;

namespace PhysiciansReach.Controllers
{
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly IDocumentBusiness _business;
        private readonly ILoggingBusiness _logging;

        public SignatureController(IDocumentBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpPost("Document/{documentId}/Signature")]
        public ActionResult Post(int documentId, [FromBody] SignatureModel signature)
        {
            _logging.Log(LogSeverity.Info, "Sign Document");

            _business.SaveSignature(documentId, signature);

            return Ok();
        }
    }
}

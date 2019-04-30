using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentBusiness _business;
        private readonly ILoggingBusiness _logging;

        public DocumentController(IDocumentBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet("Physician/{physicianId}/Document")]
        public ActionResult<List<DocumentModel>> Get(int physicianId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _business.Get(physicianId);
        }

        [HttpGet("Physician/{physicianId}/Document/{documentId}")]
        public ActionResult<DocumentModel> Get(int physicianId, int documentId)
        {
            _logging.Log(LogSeverity.Info, "Get Document");
            return _business.Get(physicianId, documentId);
        }

        [HttpPost("Physician/{physicianId}/Document/")]
        public ActionResult<DocumentModel> Post(int physicianId, [FromBody] DocumentModel document)
        {
            _logging.Log(LogSeverity.Info, "Post Document");

            document.PhysicianId = physicianId;

            return _business.Create(document);
        }

        [HttpPut("Physician/{physicianId}/Document/{documentId}")]
        public ActionResult<DocumentModel> Put(int physicianId, int documentId, [FromBody] DocumentModel document)
        {
            _logging.Log(LogSeverity.Info, "Put Document");

            document.PhysicianId = physicianId;
            document.DocumentId = documentId;

            return _business.Update(document);
        }
    }
}

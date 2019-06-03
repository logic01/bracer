using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System;
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
        public ActionResult<List<DocumentModel>> GetByPhysician(int physicianId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _business.GetByPhysician(physicianId);
        }

        [HttpGet("Vendor/{vendorId}/Document")]
        public ActionResult<List<DocumentModel>> GetByVendor(int vendorId)
        {
            _logging.Log(LogSeverity.Info, "Get All Document");
            return _business.GetByVendor(vendorId);
        }

        [HttpGet("Document/{documentId}")]
        public ActionResult<DocumentModel> Get(int documentId)
        {
            _logging.Log(LogSeverity.Info, "Get Document");
            return _business.Get(documentId);
        }

        [HttpPut("Document/{documentId}")]
        public ActionResult<DocumentModel> Put(int documentId, [FromBody] DocumentModel document)
        {
            _logging.Log(LogSeverity.Info, "Put Document");

            if (document.DocumentId != documentId)
            {
                throw new Exception("Invalid Request.");
            }

            // this api is only used by the 'assignment' in the Admin.
            // if we need it for something else then we should re-consider seeting this status here.
            document.Status = DocumentStatus.Assigned;

            return _business.Update(document);
        }

        [HttpGet("Document/{documentId}/Download")]
        public FileResult Download(int documentId)
        {
            _logging.Log(LogSeverity.Info, $"Get Document {documentId}");

            DocumentModel document = _business.Get(documentId);

            if (document == null)
            {
                throw new Exception("Document Not Found");
            }

            var fr = new FileContentResult(document.Content, "application/vnd.ms-word")
            {
                FileDownloadName = string.Format("Exam_{0}_{1}.docx", DateTime.Now.ToString("yyMMdd"), "Doc")
            };

            return fr;
        }
    }
}

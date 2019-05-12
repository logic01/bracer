using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Export;
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
                throw new System.Exception("Invalid Request.");
            }

            return _business.Update(document);
        }

        [HttpGet("Document/{patientId}/TestCreateByPatientId")]
        public FileResult GetWordByPatient(int patientId)
        {
            //This method will be dying once I can figure out how the inserts, and updates are working for 
            //intake forms works. Doing this by patient allows me to gather all of the intake forms and
            //create one massive form. I am thinking there might actually be 3 seperate forms, but I don't quite understand that
            //
            //To use this method you will need to run the script that creates all of the intake forms, then run the patient id
            //through this end point. This will save the newly created document. Then you can call the other endpoint.
            _logging.Log(LogSeverity.Info, $"Temp Endpoint to save the doc by patient id {patientId}");

            var temp = new DocumentModel()
            {
                PhysicianId = 24,
                IntakeFormId = 1,
                Type = DocumentType.IntakeForm,
                Content = _business.GetDocByPatient(patientId)
            };
            var doc = _business.Create(temp);

            FileResult fr = new FileContentResult(doc.Content, "application/vnd.ms-word")
            {
                FileDownloadName = string.Format("Exam_{0}_{1}.docx", DateTime.Now.ToString("yyMMdd"), "Doc")
            };
            return fr;
        }

        /// <summary>
        /// This will pull the content of the Document specifically then formats it to a word document for
        /// downloading.
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("Document/{documentId}/Download")]
        public FileResult GetWordByDocumentId(int documentId)
        {
            _logging.Log(LogSeverity.Info, $"Get Document {documentId}");
            var document = _business.Get(documentId);
            if (document == null)
                throw new Exception("Document Not Found");
            FileResult fr = new FileContentResult(document.Content, "application/vnd.ms-word")
            {
                FileDownloadName = string.Format("Exam_{0}_{1}.docx", DateTime.Now.ToString("yyMMdd"), "Doc")
            };
            return fr;
        }
    }
}

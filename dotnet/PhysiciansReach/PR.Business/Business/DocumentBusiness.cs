using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Export;
using PR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class DocumentBusiness : IDocumentBusiness
    {
        private DataContext _context;
        private readonly IIntakeFormBusiness _intakeFormBusiness;
        private readonly IIntakeFormExporter _exporter;

        public DocumentBusiness(DataContext context, IIntakeFormBusiness intakeFormBusiness, IIntakeFormExporter exporter)
        {
            _context = context;
            _intakeFormBusiness = intakeFormBusiness;
            _exporter = exporter;
        }

        public List<DocumentModel> GetByPhysician(int physicianId)
        {
            return _context.Document
                    .Where(d => d.PhysicianId == physicianId)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public List<DocumentModel> GetByVendor(int vendorId)
        {
            var documents = from agent in _context.Agent
                            join patient in _context.Patient on agent.UserAccountId equals patient.AgentId
                            join intake in _context.IntakeForm on patient.PatientId equals intake.PatientId
                            join document in _context.Document on intake.IntakeFormId equals document.IntakeFormId
                            where agent.VendorId == vendorId
                            select document.ToModel();


            return documents.ToList();
        }

        public DocumentModel Get(int documentId)
        {
            var document = _context.Document.FirstOrDefault(u => u.DocumentId == documentId);

            return document.ToModel();
        }

        public DocumentModel Create(DocumentModel documentModel)
        {
            var document = documentModel.ToEntity();

            _context.Document.Add(document);
            _context.SaveChanges();

            return document.ToModel();
        }

        public DocumentModel Update(DocumentModel documentModel)
        {
            // get original
            var document = _context.Document.FirstOrDefault(u => u.DocumentId == documentModel.DocumentId);

            // populate with model data
            document = documentModel.MapToEntity(document);

            // save
            _context.SaveChanges();

            // return new
            return document.ToModel();
        }

        public byte[] GetDoc()
        {
            //TODO remove hard coding. I need to fix the script I created
            //to have all questions/answers from the pdf and then figure out
            //the relationship for a document to all intake form models
            var intakeForms = new List<IntakeFormModel>();
            intakeForms.Add(_intakeFormBusiness.Get(11));
            intakeForms.Add(_intakeFormBusiness.Get(12));
            intakeForms.Add(_intakeFormBusiness.Get(13));
            intakeForms.Add(_intakeFormBusiness.Get(14));

            // After the doc is created this needs to be persisted. I think
            // the update/create of Documents should avoid dealing with content
            // outside the usage of export
            var documentContent = _exporter.CreateNewIntakeForm(intakeForms);
            return documentContent;
        }
    }
}

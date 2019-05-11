using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class DocumentBusiness : IDocumentBusiness
    {
        private DataContext _context;

        public DocumentBusiness(DataContext context)
        {
            _context = context;
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
    }
}

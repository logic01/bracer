using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Enums;
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
            IQueryable<DocumentModel> documents = from agent in _context.Agent
                                                  join patient in _context.Patient on agent.UserAccountId equals patient.AgentId
                                                  join intake in _context.IntakeForm on patient.PatientId equals intake.PatientId
                                                  join document in _context.Document on intake.IntakeFormId equals document.IntakeFormId
                                                  where agent.VendorId == vendorId
                                                  select document.ToModel();


            return documents.ToList();
        }

        public DocumentModel Get(int documentId)
        {
            Document document = _context.Document.FirstOrDefault(u => u.DocumentId == documentId);

            return document.ToModel();
        }

        public DocumentModel Update(DocumentModel documentModel)
        {
            // get original
            Document document = _context.Document.FirstOrDefault(u => u.DocumentId == documentModel.DocumentId);

            // populate with model data
            document = documentModel.MapToEntity(document);

            // save
            _context.SaveChanges();

            // return new
            return document.ToModel();
        }

        public DocumentModel CreateIntakeFormDocument(int patientId, int intakeFormId)
        {
            Patient patient = _context.Patient
                .Include(p => p.PhysiciansAddress)
                .Include(p => p.Address)
                .First(p => p.PatientId == patientId);

            IntakeForm intakeForm = _context.IntakeForm
                .Include("Questions.Answers")
                .First(i => i.IntakeFormId == intakeFormId);

            var documentContent = _exporter.CreateNewIntakeForm(intakeForm.ToModel(), patient.ToModel());

            var document = new Document
            {
                IntakeFormId = intakeFormId,
                Status = DocumentStatus.New,
                Type = DocumentType.IntakeForm,
                Content = documentContent
            };

            _context.Document.Add(document);
            _context.SaveChanges();

            return document.ToModel();
        }
    }
}

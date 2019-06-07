using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Enums;
using PR.Data.Models;
using PR.Export;
using PR.Models;
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
                Content = documentContent
            };

            _context.Document.Add(document);
            _context.SaveChanges();

            return document.ToModel();
        }
    }
}

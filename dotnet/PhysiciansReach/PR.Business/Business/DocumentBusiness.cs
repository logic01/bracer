using Microsoft.EntityFrameworkCore;
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
        private readonly IDocumentGenerator _exporter;

        public DocumentBusiness(DataContext context, IIntakeFormBusiness intakeFormBusiness, IDocumentGenerator exporter)
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

        public void Update(DocumentModel documentModel)
        {
            // get original
            Document document = _context.Document.FirstOrDefault(u => u.DocumentId == documentModel.DocumentId);

            // populate with model data
            document = documentModel.MapToEntity(document);

            // save
            _context.SaveChanges();
        }

        public int Create(DocumentModel documentModel)
        {
            IntakeForm intakeForm = _context.IntakeForm
                    .Include("Questions.Answers")
                    .Include(i => i.ICD10Codes)
                    .Include(i => i.Physician.Address)
                    .Include(i => i.Signatures)
                    .First(i => i.IntakeFormId == documentModel.IntakeFormId);

            Patient patient = _context.Patient
                .Include(p => p.Address)
                .Include(p => p.PrivateInsurance)
                .Include(p => p.Medicare)
                .First(p => p.PatientId == intakeForm.PatientId);

            IntakeFormModel intakeFormModel = intakeForm.ToModel();
            PatientModel patientModel = patient.ToModel();
            PhysicianModel physicianModel = intakeForm.Physician.ToModel();
            ICollection<SignatureModel> signatureModels = intakeForm.Signatures.Select(s => s.ToModel()).ToList();

            var documentContent = _exporter.GenerateIntakeDocuments(
                intakeFormModel,
                patientModel,
                physicianModel,
                signatureModels);

            var document = new Document
            {
                IntakeFormId = documentModel.IntakeFormId,
                Content = documentContent
            };

            intakeForm.Document = document;

            _context.SaveChanges();

            return document.DocumentId;
        }
    }
}

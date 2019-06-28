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
    public class SignatureBusiness : ISignatureBusiness
    {
        private DataContext _context;
        private readonly IIntakeFormBusiness _intakeFormBusiness;
        private readonly IDocumentGenerator _exporter;

        public SignatureBusiness(DataContext context, IIntakeFormBusiness intakeFormBusiness, IDocumentGenerator exporter)
        {
            _context = context;
            _intakeFormBusiness = intakeFormBusiness;
            _exporter = exporter;
        }

        public SignatureModel Get(int signatureId)
        {
            Signature signature = _context.Signature.FirstOrDefault(u => u.SignatureId == signatureId);

            return signature.ToModel();
        }

        public int Create(int intakeFormId, SignatureModel signatureModel)
        {
            IntakeForm intakeForm = _context.IntakeForm
                .Include(i => i.Signatures)
                .First(u => u.IntakeFormId == intakeFormId);

            var sig = new Signature();
            sig.MapFromModel(signatureModel);

            if (intakeForm.Signatures == null)
            {
                intakeForm.Signatures = new List<Signature>();
            }

            intakeForm.Signatures.Add(sig);

           _context.SaveChanges();

            return sig.SignatureId;
        }
    }
}

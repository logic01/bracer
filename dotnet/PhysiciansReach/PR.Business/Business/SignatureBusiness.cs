using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Export;
using PR.Models;
using System.Linq;

namespace PR.Business
{
    public class SignatureBusiness : ISignatureBusiness
    {
        private DataContext _context;
        private readonly IIntakeFormBusiness _intakeFormBusiness;
        private readonly IIntakeFormExporter _exporter;

        public SignatureBusiness(DataContext context, IIntakeFormBusiness intakeFormBusiness, IIntakeFormExporter exporter)
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

        public SignatureModel Create(int intakeFormId, SignatureModel signatureModel)
        {
            IntakeForm intakeForm = _context.IntakeForm.FirstOrDefault(u => u.IntakeFormId == intakeFormId);

            var sig = new Signature();
            sig.MapFromModel(signatureModel);

            intakeForm.Signature = sig;

            _context.SaveChanges();

            return sig.ToModel();
        }
    }
}

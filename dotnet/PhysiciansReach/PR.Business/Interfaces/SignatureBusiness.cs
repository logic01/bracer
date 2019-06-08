using PR.Models;

namespace PR.Business.Interfaces
{
    public interface ISignatureBusiness
    {
        SignatureModel Get(int signatureId);

        SignatureModel Create(int intakeFormId, SignatureModel signatureModel);
    }
}

using PR.Models;

namespace PR.Business.Interfaces
{
    public interface ISignatureBusiness
    {
        SignatureModel Get(int signatureId);

        int Create(int intakeFormId, SignatureModel signatureModel);
    }
}

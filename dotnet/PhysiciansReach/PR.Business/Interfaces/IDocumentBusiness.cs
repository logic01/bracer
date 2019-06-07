using PR.Models;

namespace PR.Business.Interfaces
{
    public interface IDocumentBusiness
    {
        DocumentModel Get(int documentId);

        DocumentModel CreateIntakeFormDocument(int patientId, int intakeFormId);

        DocumentModel Update(DocumentModel documentModel);
    }
}

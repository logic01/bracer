using PR.Models;

namespace PR.Business.Interfaces
{
    public interface IDocumentBusiness
    {
        DocumentModel Get(int documentId);

        int Create(DocumentModel documentModel);

        void Update(DocumentModel documentModel);
    }
}

using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IDocumentBusiness
    {
        List<DocumentModel> Get(int physicianId);

        DocumentModel Get(int physicianId, int documentId);

        DocumentModel Create(DocumentModel adminModel);

        DocumentModel Update(DocumentModel adminModel);
    }
}

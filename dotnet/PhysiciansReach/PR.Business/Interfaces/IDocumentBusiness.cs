using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IDocumentBusiness
    {
        List<DocumentModel> GetByPhysician(int physicianId);

        List<DocumentModel> GetByVendor(int vendorId);

        DocumentModel Get(int documentId);

        DocumentModel Create(DocumentModel documentModel);

        DocumentModel Update(DocumentModel documentModel);
    }
}

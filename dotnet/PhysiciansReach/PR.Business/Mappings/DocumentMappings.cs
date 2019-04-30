using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class DocumentMappings
    {
        public static DocumentModel ToModel(this Document entity)
        {
            var model = new DocumentModel
            {
                DocumentId = entity.DocumentId,
                PhysicianId = entity.PhysicianId,
                IntakeFormId = entity.IntakeFormId,
                Content = entity.Content,
                Signature = entity.Signature,
                Type = entity.Type,
                Status = entity.Status,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Document ToEntity(this DocumentModel model)
        {
            var entity = new Document
            {
                DocumentId = model.DocumentId,
                PhysicianId = model.PhysicianId,
                IntakeFormId = model.IntakeFormId,
                Content = model.Content,
                Signature = model.Signature,
                Type = model.Type,
                Status = model.Status,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

    }
}

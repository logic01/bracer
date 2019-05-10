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
            return model.MapToEntity(new Document());
        }

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Document MapToEntity(this DocumentModel model, Document entity)
        {
            entity.DocumentId = model.DocumentId;
            entity.PhysicianId = model.PhysicianId;
            entity.IntakeFormId = model.IntakeFormId;
            entity.Content = model.Content;
            entity.Signature = model.Signature;
            entity.Type = model.Type;
            entity.Status = model.Status;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            return entity;
        }

    }
}

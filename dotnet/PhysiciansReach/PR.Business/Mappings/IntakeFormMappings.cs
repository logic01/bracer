using PR.Data.Models;
using PR.Models;
using System.Linq;

namespace PR.Business.Mappings
{
    public static class IntakeFormMappings
    {
        public static IntakeFormModel ToModel(this IntakeForm entity)
        {
            var model = new IntakeFormModel
            {
                IntakeFormId = entity.IntakeFormId,
                PatientId = entity.PatientId,
                PhysicianId = entity.PhysicianId,
                SignatureId = entity.SignatureId,
                ICD10 = entity.ICD10?.ToModel(),
                HCPCS = entity.HCPCS?.ToModel(),
                IntakeFormType = entity.IntakeFormType,
                Status = entity.Status,
                Questions = entity.Questions?.Select(q => q.ToModel()).ToList(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this IntakeForm entity, IntakeFormModel model)
        {
            //TODO Is this code needed?
            if (entity == null)
            {
                entity = new IntakeForm();
            }
            //IntakeFormId = model.IntakeFormId don't map primary key from the model
            entity.PatientId = model.PatientId;
            entity.PhysicianId = model.PhysicianId;
            entity.SignatureId = model.SignatureId;
            entity.Status = model.Status;
            entity.IntakeFormType = model.IntakeFormType;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            entity.ICD10.MapFromModel(model.ICD10);
            entity.HCPCS.MapFromModel(model.HCPCS);
        }

        public static ICD10Model ToModel(this ICD10 entity)
        {
            return new ICD10Model
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static HCPCSModel ToModel(this HCPCS entity)
        {
            return new HCPCSModel
            {
                Id = entity.Id,
                Code = entity.Code,
                Product = entity.Product,
                Duration = entity.Duration,
                Description = entity.Description,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static void MapFromModel(this ICD10 entity, ICD10Model model)
        {
            //TODO Is this code needed?
            if (model == null)
            {
                return;
            }
            if(entity == null)
            {
                entity = new ICD10();
            }
            model.Id = entity.Id;
            model.Code = entity.Code;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;
            model.ModifiedOn = entity.ModifiedOn;
        }

        public static void MapFromModel(this HCPCS entity, HCPCSModel model)
        {
            //TODO Is this code needed?
            if(model == null)
            {
                return;
            }
            if(entity == null)
            {
                entity = new HCPCS();
            }
            entity.Id = model.Id;
            entity.Code = model.Code;
            entity.Product = model.Product;
            entity.Duration = model.Duration;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;
        }

        public static QuestionModel ToModel(this Question entity)
        {
            var model = new QuestionModel
            {
                QuestionId = entity.QuestionId,
                Text = entity.Text,
                Key = entity.Key,
                Answers = entity.Answers.Select(a => a.ToModel()).ToList(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static AnswerModel ToModel(this Answer entity)
        {
            var model = new AnswerModel
            {
                AnswerId = entity.AnswerId,
                Text = entity.Text,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

    }
}

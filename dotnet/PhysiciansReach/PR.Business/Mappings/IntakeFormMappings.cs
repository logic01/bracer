using PR.Data.Models;
using PR.Models;
using System;
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
                ICD10 = entity.ICD10s?.Select(x => x.ToModel()).ToList(),
                HCPCS = entity.HCPCSs?.Select(x => x.ToModel()).ToList(),
                IntakeFormType = entity.IntakeFormType,
                Status = entity.Status,
                Questions = entity.Questions?.Select(q => q.ToModel()).ToList(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                AdditionalDrNotes = entity.AdditionalDrNotes
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
            entity.AdditionalDrNotes = model.AdditionalDrNotes;

            entity.ICD10s = model.ICD10?.Select(x => x.ToEntity()).ToList();
            entity.HCPCSs = model.HCPCS?.Select(x => x.ToEntity()).ToList();
        }

        /// <summary>
        /// When the intake form is being created the Questions need to be populated
        /// </summary>
        /// <param name="model"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static IntakeForm ToCreateEntity(this IntakeFormModel model, int? documentId = null)
        {
            var now = DateTime.Now;
            return new IntakeForm
            {
                PatientId = model.PatientId,
                PhysicianId = model.PhysicianId,
                SignatureId = model.SignatureId,
                DocumentId = documentId,
                IntakeFormType = model.IntakeFormType,
                CreatedOn = now,
                ModifiedOn = now,
                ICD10s = model.ICD10?.Select(x => x.ToEntity()).ToList(),
                HCPCSs = model.HCPCS?.Select(x => x.ToEntity()).ToList(),
                Questions = model.Questions?.Select(x => x.ToEntity(default(int))).ToList(),
                Status = model.Status,
                AdditionalDrNotes = model.AdditionalDrNotes
            };
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

        public static ICD10 ToEntity(this ICD10Model model)
        {
            return new ICD10
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };
        }



        public static void MapFromModel(this ICD10 entity, ICD10Model model)
        {
            //TODO Is this code needed?
            if (model == null)
            {
                return;
            }
            if (entity == null)
            {
                entity = new ICD10();
            }
            model.Id = entity.Id;
            model.Code = entity.Code;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;
            model.ModifiedOn = entity.ModifiedOn;
        }

        public static HCPCS ToEntity(this HCPCSModel entity)
        {
            return new HCPCS
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

        public static void MapFromModel(this HCPCS entity, HCPCSModel model)
        {
            //TODO Is this code needed?
            if (model == null)
            {
                return;
            }
            if (entity == null)
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

        public static Question ToEntity(this QuestionModel model, int intakeFormId)
        {
            return new Question
            {
                IntakeFormId = intakeFormId,
                QuestionId = model.QuestionId,
                Text = model.Text,
                Key = model.Key,
                Answers = model.Answers?.Select(x => x.ToEntity()).ToList(),
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };
        }

        public static QuestionModel ToModel(this Question entity)
        {
            return new QuestionModel
            {
                QuestionId = entity.QuestionId,
                Text = entity.Text,
                Key = entity.Key,
                Answers = entity.Answers?.Select(x => x.ToModel()).ToList(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static Answer ToEntity(this AnswerModel model)
        {
            return new Answer
            {

                AnswerId = model.AnswerId,
                Text = model.Text,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };
        }

        public static AnswerModel ToModel(this Answer entity)
        {
            return new AnswerModel
            {
                AnswerId = entity.AnswerId,
                Text = entity.Text,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

    }
}

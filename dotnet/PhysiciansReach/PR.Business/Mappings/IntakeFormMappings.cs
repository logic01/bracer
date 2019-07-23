using PR.Data.Models;
using PR.Models;
using System.Collections.Generic;
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
                DocumentId = entity.DocumentId,
                ICD10Codes = entity.ICD10Codes?.Select(x => x.ToModel()).ToList(),
                HCPCSCode = entity.HCPCSCode,
                Product = entity.Product,
                PhysicianNotes = entity.PhysicianNotes,
                Duration = entity.Duration,
                IntakeFormType = entity.IntakeFormType,
                Status = entity.Status,
                PhysicianPaid = entity.PhysicianPaid,
                VendorBilled = entity.VendorBilled,
                VendorPaid = entity.VendorPaid,
                DeniedReason = entity.DeniedReason,
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
            if (entity == null)
            {
                entity = new IntakeForm();
            }
            //IntakeFormId = model.IntakeFormId don't map primary key from the model
            entity.PatientId = model.PatientId;
            entity.PhysicianId = model.PhysicianId;
            entity.DocumentId = model.DocumentId;
            entity.Status = model.Status;
            entity.IntakeFormType = model.IntakeFormType;
            entity.Product = model.Product;
            entity.PhysicianNotes = model.PhysicianNotes;
            entity.Duration = model.Duration;
            entity.PhysicianPaid = model.PhysicianPaid;
            entity.VendorBilled = model.VendorBilled;
            entity.VendorPaid = model.VendorPaid;
            entity.DeniedReason = model.DeniedReason;
            entity.HCPCSCode = model.HCPCSCode;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            if (entity.Questions == null)
            {
                entity.Questions = new List<Question>();
            }

            entity.Questions = model.Questions?.Select(questionsModel =>
            {
                var question = new Question();

                question.MapFromModel(questionsModel, entity.IntakeFormId);

                return question;

            }).ToList();

            if (entity.ICD10Codes == null)
            {
                entity.ICD10Codes = new List<ICD10Code>();
            }

            entity.ICD10Codes = model.ICD10Codes?.Select(iCD10CodeModel =>
            {
                var icd10Code = new ICD10Code();

                icd10Code.MapFromModel(iCD10CodeModel);

                return icd10Code;

            }).ToList();

        }

        public static ICD10CodeModel ToModel(this ICD10Code entity)
        {
            return new ICD10CodeModel
            {
                ICD10CodeId = entity.ICD10CodeId,
                Text = entity.Text,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static void MapFromModel(this ICD10Code entity, ICD10CodeModel model)
        {
            entity.ICD10CodeId = model.ICD10CodeId;
            entity.Text = model.Text;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;
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

        public static void MapFromModel(this Question entity, QuestionModel model, int intakeFormId)
        {
            // entity.QuestionId = model.QuestionId; don't map primary key
            entity.IntakeFormId = intakeFormId;

            entity.Text = model.Text;
            entity.Key = model.Key;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            if (entity.Answers == null)
            {
                entity.Answers = new List<Answer>();
            }

            entity.Answers = model.Answers?.Select(answerModel =>
            {
                var answer = new Answer();

                answer.MapFromModel(answerModel);

                return answer;

            }).ToList();
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

        public static void MapFromModel(this Answer entity, AnswerModel model)
        {
            //entity.AnswerId = model.AnswerId; don't map primary key
            entity.Text = model.Text;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;
        }
    }
}

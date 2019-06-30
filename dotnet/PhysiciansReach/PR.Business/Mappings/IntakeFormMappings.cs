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
                DocumentId = entity.DocumentId,
                ICD10Codes = entity.ICD10Codes?.Select(x => x.ToModel()).ToList(),
                HCPCSCodes = entity.HCPCSCodes?.Select(x => x.ToModel()).ToList(),
                Product = entity.Product,
                PhysicianNotes = entity.PhysicianNotes,
                Duration = entity.Duration,
                IntakeFormType = entity.IntakeFormType,
                Status = entity.Status,
                PhysicianBilled = entity.PhysicianBilled,
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
            entity.PhysicianBilled = model.PhysicianBilled;
            entity.VendorBilled = model.VendorBilled;
            entity.VendorPaid = model.VendorPaid;
            entity.DeniedReason = model.DeniedReason;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            entity.ICD10Codes = model.ICD10Codes?.Select(x => x.ToEntity()).ToList();
            entity.HCPCSCodes = model.HCPCSCodes?.Select(x => x.ToEntity()).ToList();
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

        public static HCPCSCodeModel ToModel(this HCPCSCode entity)
        {
            return new HCPCSCodeModel
            {
                HCPCSCodeId = entity.HCPCSCodeId,
                Text = entity.Text,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static ICD10Code ToEntity(this ICD10CodeModel model)
        {
            return new ICD10Code
            {
                ICD10CodeId = model.ICD10CodeId,
                Text = model.Text,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };
        }

        public static void MapFromModel(this ICD10Code entity, ICD10CodeModel model)
        {
            model.ICD10CodeId = entity.ICD10CodeId;
            model.Text = entity.Text;
            model.CreatedOn = entity.CreatedOn;
            model.ModifiedOn = entity.ModifiedOn;
        }

        public static HCPCSCode ToEntity(this HCPCSCodeModel entity)
        {
            return new HCPCSCode
            {
                HCPCSCodeId = entity.HCPCSCodeId,
                Text = entity.Text,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static void MapFromModel(this HCPCSCode entity, HCPCSCodeModel model)
        {
            entity.HCPCSCodeId = model.HCPCSCodeId;
            entity.Text = model.Text;
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

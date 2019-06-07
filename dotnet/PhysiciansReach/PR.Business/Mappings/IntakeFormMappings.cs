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
                ICD10 = entity.ICD10,
                HCPCS = entity.HCPCS,
                IntakeFormType = entity.IntakeFormType,
                Status = entity.Status,
                Questions = entity.Questions?.Select(q => q.ToModel()).ToList(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static IntakeForm ToEntity(this IntakeFormModel model)
        {
            var entity = new IntakeForm
            {
                IntakeFormId = model.IntakeFormId,
                PatientId = model.PatientId,
                PhysicianId = model.PhysicianId,
                ICD10 = model.ICD10,
                HCPCS = model.HCPCS,
                Status = model.Status,
                IntakeFormType = model.IntakeFormType,
                Questions = model.Questions.Select(q => q.ToEntity()).ToList(),
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this IntakeForm entity, IntakeFormModel model)
        {
            //IntakeFormId = model.IntakeFormId don't map primary key from the model
            entity.PatientId = model.PatientId;
            entity.PhysicianId = model.PhysicianId;
            entity.ICD10 = model.ICD10;
            entity.HCPCS = model.HCPCS;
            entity.Status = model.Status;
            entity.IntakeFormType = model.IntakeFormType;
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

        public static Question ToEntity(this QuestionModel model)
        {
            var entity = new Question
            {
                QuestionId = model.QuestionId,
                Text = model.Text,
                Key = model.Key,
                Answers = model.Answers.Select(a => a.ToEntity()).ToList(),
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
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

        public static Answer ToEntity(this AnswerModel model)
        {
            var entity = new Answer
            {
                AnswerId = model.AnswerId,
                Text = model.Text,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

    }
}

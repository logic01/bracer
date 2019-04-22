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
                IntakeFormType = entity.IntakeFormType,
                Questions = entity.Questions.Select(q => q.ToModel()).ToList(),
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
                IntakeFormType = model.IntakeFormType,
                Questions = model.Questions.Select(q => q.ToEntity()).ToList(),
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

        public static QuestionModel ToModel(this Question entity)
        {
            var model = new QuestionModel
            {
                QuestionId = entity.QuestionId,
                Text = entity.Text,
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

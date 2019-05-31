using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class IntakeFormBusiness : IIntakeFormBusiness
    {
        private DataContext _context;

        public IntakeFormBusiness(DataContext context)
        {
            _context = context;
        }

        public List<IntakeFormModel> Get()
        {
            var intakeFormModels = _context.IntakeForm.Include("Questions.Answers");
            return intakeFormModels.Select(x => x.ToModel()).ToList();
        }

        public IntakeFormModel Get(int id)
        {
            var intakeFormModels = _context.IntakeForm.Where(x => x.IntakeFormId == id).Include("Questions.Answers");

            return intakeFormModels.Select(x => x.ToModel()).FirstOrDefault();
        }

        public IntakeFormModel Create(IntakeFormModel intakeFormModel)
        {
            var intakeForm = intakeFormModel.ToEntity();
            _context.IntakeForm.Add(intakeForm);

            _context.SaveChanges();

            return intakeForm.ToModel();
        }

        public IntakeFormModel Update(IntakeFormModel intakeFormModel)
        {
            IntakeForm intakeForm = _context.IntakeForm.FirstOrDefault(u => u.IntakeFormId == intakeFormModel.IntakeFormId);

            intakeForm = intakeFormModel.ToEntity();
            _context.IntakeForm.Add(intakeForm);
            _context.SaveChanges();

            return intakeForm.ToModel();
        }


    }
}

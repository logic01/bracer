using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Constants.Enums;
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

        public List<IntakeFormModel> GetByPatient(int patientId)
        {
            return _context.IntakeForm
                    .Include("Questions.Answers")
                    .Where(d => d.PatientId == patientId)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public List<IntakeFormModel> GetByPhysician(int physicianId)
        {
            return _context.IntakeForm
                    .Include("Questions.Answers")
                    .Where(d => d.PhysicianId == physicianId)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public List<IntakeFormModel> GetByVendor(int vendorId)
        {
            IQueryable<IntakeFormModel> forms = from agent in _context.Agent
                                                join patient in _context.Patient on agent.UserAccountId equals patient.AgentId
                                                join intake in _context.IntakeForm.Include("Questions.Answers") on patient.PatientId equals intake.PatientId
                                                where agent.VendorId == vendorId
                                                select intake.ToModel();

            return forms.ToList();
        }

        public List<IntakeFormModel> Get()
        {
            IQueryable<IntakeForm> intakeFormModels = _context.IntakeForm.Include("Questions.Answers");
            return intakeFormModels.Select(x => x.ToModel()).ToList();
        }

        public IntakeFormModel Get(int id)
        {
            IQueryable<IntakeForm> intakeFormModels = _context.IntakeForm.Where(x => x.IntakeFormId == id).Include("Questions.Answers");

            return intakeFormModels.Select(x => x.ToModel()).FirstOrDefault();
        }

        public IntakeFormModel Create(IntakeFormModel intakeFormModel)
        {
            var intakeForm = new IntakeForm();
            intakeForm.MapFromModel(intakeFormModel);
            intakeForm.Status = IntakeFormStatus.New;

            _context.IntakeForm.Add(intakeForm);
            _context.SaveChanges();

            return intakeForm.ToModel();
        }

        public IntakeFormModel Update(IntakeFormModel intakeFormModel)
        {
            IntakeForm intakeForm = _context.IntakeForm
                .Include(i => i.Signatures)
                .Include("Questions.Answers")
                .First(u => u.IntakeFormId == intakeFormModel.IntakeFormId);

            intakeForm.MapFromModel(intakeFormModel);

            _context.SaveChanges();

            return intakeForm.ToModel();
        }

    }
}

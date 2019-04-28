using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class PatientBusiness : IPatientBusiness
    {
        private DataContext _context;

        public PatientBusiness(DataContext context)
        {
            _context = context;
        }

        public List<PatientModel> Get()
        {
            return _context.Patient
                    .Include(p => p.Address)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public PatientModel Get(int patientId)
        {
            var patient = _context.Patient
                .Include(p => p.Address)
                .FirstOrDefault(u => u.PatientId == patientId);

            return patient.ToModel();
        }

        public PatientModel Create(PatientModel patientModel)
        {
            Patient patient = patientModel.ToEntity();

            _context.Patient.Add(patient);
            _context.SaveChanges();

            return patient.ToModel();
        }

        public PatientModel Update(PatientModel patientModel)
        {
            Patient patient = _context.Patient.FirstOrDefault(u => u.PatientId == patientModel.PatientId);

            patient = patientModel.ToEntity();
            _context.Patient.Add(patient);
            _context.SaveChanges();

            return patient.ToModel();
        }


    }
}

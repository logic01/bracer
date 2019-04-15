using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IPatientBusiness
    {
        List<PatientModel> Get();

        PatientModel Get(int id);

        PatientModel Create(PatientModel patientModel);

        PatientModel Update(PatientModel patientModel);
    }
}

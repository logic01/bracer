using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IPatientBusiness
    {
        PatientModel Get(int id);

        List<PatientModel> Get(int[] ids);

        List<PatientModel> GetAll();

        List<PatientModel> GetByAgent(int agentId);

        List<PatientModel> GetByVendor(int vendorId);

        int Create(PatientModel patientModel);

        void Update(PatientModel patientModel);
    }
}

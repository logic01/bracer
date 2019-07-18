using PR.Models;
using System.Collections.Generic;
namespace PR.Business.Interfaces
{
    public interface IIntakeFormBusiness
    {
        List<IntakeFormModel> Get();

        IntakeFormModel Get(int id);

        List<IntakeFormModel> GetByPatient(int patientId);

        List<IntakeFormModel> GetByPhysician(int physicianId);

        List<IntakeFormModel> GetByVendor(int vendorId);

        IntakeFormModel Create(IntakeFormModel intakeFormModel);

        IntakeFormModel Update(IntakeFormModel intakeFormModel);
        
    }
}

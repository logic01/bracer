using PR.Models;

namespace PR.Export
{
    public interface IIntakeFormExporter
    {
        byte[] CreateNewIntakeForm(IntakeFormModel intakeForm, PatientModel patient, SignatureModel signature, PhysicianModel physician);
    }
}

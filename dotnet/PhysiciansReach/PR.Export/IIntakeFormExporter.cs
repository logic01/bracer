using PR.Models;
using System.Collections.Generic;

namespace PR.Export
{
    public interface IIntakeFormExporter
    {
        byte[] CreateNewIntakeForm(IntakeFormModel intakeForm, PatientModel patient, SignatureModel signature, PhysicianModel physician, List<IntakeFormModel> allIntakeForms);
    }
}

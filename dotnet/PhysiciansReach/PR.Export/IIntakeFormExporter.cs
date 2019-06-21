using PR.Models;
using System.Collections.Generic;

namespace PR.Export
{
    public interface IDocumentGenerator
    {
        byte[] GenerateIntakeDocuments(
            IntakeFormModel intakeForm,
            PatientModel patient,
            PhysicianModel physician,
            ICollection<SignatureModel> signatures);
    }
}

using System;
using System.Collections.Generic;

namespace PR.Models
{
    public class IntakeFormModel
    {
        public int IntakeFormId { get; set; }

        public int PatientId { get; set; }

        public int? PhysicianId { get; set; }

        public IntakeFormType IntakeFormType { get; set; }

        public List<string> ICD10Codes { get; set; }

        public List<string> HCPCSCodes { get; set; }

        public string Duration { get; set; }

        public string PhysicianNotes { get; set; }

        public IntakeFormStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<QuestionModel> Questions { get; set; }

        public string AdditionalDrNotes { get; set; }

    }
}

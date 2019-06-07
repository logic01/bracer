using PR.Constants.Enums;
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

        public string ICD10 { get; set; }

        public string HCPCS { get; set; }

        public IntakeFormStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<QuestionModel> Questions { get; set; }

    }
}

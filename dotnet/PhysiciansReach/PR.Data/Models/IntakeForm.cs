using PR.Constants.Enums;
using System;
using System.Collections.Generic;

namespace PR.Data.Models
{
    public class IntakeForm
    {
        public int IntakeFormId { get; set; }

        public int PatientId { get; set; }

        public int? PhysicianId { get; set; }

        public IntakeFormType IntakeFormType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<Question> Questions { get; set; }

        public string ICD10 { get; set; }

        public string HCPCS { get; set; }

        public IntakeFormStatus Status { get; set; }

        public Document Document { get; set; }

        public Patient Patient { get; set; }

        public Physician Physician { get; set; }
    }
}

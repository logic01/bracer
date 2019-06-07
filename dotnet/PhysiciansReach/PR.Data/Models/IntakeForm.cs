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

        public int? SignatureId { get; set; }

        public int? ICD10Id { get; set; }

        public int? HCPCSId { get; set; }

        public IntakeFormType IntakeFormType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<Question> Questions { get; set; }

        public ICD10 ICD10 { get; set; }

        public HCPCS HCPCS { get; set; }

        public Signature Signature { get; set; }

        public IntakeFormStatus Status { get; set; }

        public Document Document { get; set; }

        public Patient Patient { get; set; }

        public Physician Physician { get; set; }
    }
}

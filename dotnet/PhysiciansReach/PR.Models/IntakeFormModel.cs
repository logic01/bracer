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

        public int? DocumentId { get; set; }

        public IntakeFormType IntakeFormType { get; set; }

        public IntakeFormStatus Status { get; set; }

        public List<ICD10CodeModel> ICD10Codes { get; set; }

        public string HCPCSCode { get; set; }

        public string Duration { get; set; }

        public string PhysicianNotes { get; set; }

        public string Product { get; set; }

        public bool PhysicianPaid { get; set; }

        public bool VendorBilled { get; set; }

        public bool VendorPaid { get; set; }

        public string DeniedReason { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<QuestionModel> Questions { get; set; }

    }
}

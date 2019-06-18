using System;

namespace PR.Data.Models
{
    public class ICD10Code
    {
        public int ICD10CodeId { get; set; }

        public int IntakeFormId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IntakeForm IntakeForm { get; set; }
    }
}

using System;

namespace PR.Data.Models
{
    public class HCPCSCode
    {
        public int HCPCSCodeId { get; set; }

        public int IntakeFormId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IntakeForm IntakeForm { get; set; }
    }
}

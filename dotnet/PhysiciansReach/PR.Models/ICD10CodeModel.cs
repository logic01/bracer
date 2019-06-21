using System;

namespace PR.Models
{
    public class ICD10CodeModel
    {
        public int ICD10CodeId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

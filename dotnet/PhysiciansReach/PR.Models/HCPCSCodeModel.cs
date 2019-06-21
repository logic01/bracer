using System;

namespace PR.Models
{
    public class HCPCSCodeModel
    {
        public int HCPCSCodeId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

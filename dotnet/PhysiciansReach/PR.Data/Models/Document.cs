using PR.Constants.Enums;
using System;

namespace PR.Data.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        public int IntakeFormId { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IntakeForm IntakeForm { get; set; }
    }
}

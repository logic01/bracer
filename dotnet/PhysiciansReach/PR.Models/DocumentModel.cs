using PR.Constants.Enums;
using System;

namespace PR.Models
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }

        public int IntakeFormId { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

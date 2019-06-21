using PR.Constants.Enums;
using System;

namespace PR.Data.Models
{
    public class Signature
    {
        public int SignatureId { get; set; }

        public SignatureType Type { get; set; }

        public byte[] Content { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IntakeForm IntakeForm { get; set; }

    }
}

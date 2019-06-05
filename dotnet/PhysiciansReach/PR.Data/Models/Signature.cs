using System;

namespace PR.Data.Models
{
    public class Signature
    {
        public int SignatureId { get; set; }

        public byte[] Content { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Document Document { get; set; }

    }
}

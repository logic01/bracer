using PR.Constants.Enums;
using System;

namespace PR.Models
{
    public class SignatureModel
    {
        public int SignatureId { get; set; }

        public string Content { get; set; }

        public byte[] ContentBytes { get; set; }

        public SignatureType Type { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

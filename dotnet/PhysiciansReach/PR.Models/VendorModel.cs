using System;

namespace PR.Data.Models
{
    public class VendorModel
    {
        public int VendorId { get; set; }

        public string CompanyName { get; set; }

        public string DoingBusinessAs { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

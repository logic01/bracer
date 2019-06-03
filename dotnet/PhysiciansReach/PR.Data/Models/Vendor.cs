using System;
using System.Collections.Generic;

namespace PR.Data.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }

        public string CompanyName { get; set; }

        public string DoingBusinessAs { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<Agent> Agent { get; set; }
    }
}

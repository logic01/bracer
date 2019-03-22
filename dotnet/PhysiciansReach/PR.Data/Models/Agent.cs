using System;

namespace PR.Data.Models
{
    public class Agent
    {
        public int AgentId { get; set; }

        public int UserAccountId { get; set; }

        public int VendorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public UserAccount UserAccount { get; set; }

        public Vendor Vendor { get; set; }
    }
}

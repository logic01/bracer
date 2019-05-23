using System;

namespace PR.Models
{
    public class AgentModel
    {
        public UserAccountModel UserAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int VendorId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

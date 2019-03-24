using System;

namespace PR.Data.Models
{
    public class Admin
    {
        public int UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public UserAccount UserAccount { get; set; }
    }
}

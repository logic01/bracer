using System;

namespace PR.Models
{
    public class AdminModel
    {
        public UserAccountModel UserAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

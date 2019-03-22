using System;

namespace PR.Data.Models
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Admin Admin { get; set; }

        public Agent Agent { get; set; }

        public Physician Physician { get; set; }
    }
}

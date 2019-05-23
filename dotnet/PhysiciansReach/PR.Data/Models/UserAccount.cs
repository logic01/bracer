using PR.Constants.Enums;
using System;

namespace PR.Data.Models
{
    public class UserAccount
    {
        public int UserAccountId { get; set; }

        public AccountType Type { get; set; }

        public string UserName { get; set; }

        public byte[] Password { get; set; }

        public string EmailAddress { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Admin Admin { get; set; }

        public Agent Agent { get; set; }

        public Physician Physician { get; set; }
    }
}

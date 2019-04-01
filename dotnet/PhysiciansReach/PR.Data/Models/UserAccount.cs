using System;

namespace PR.Data.Models
{
    public class UserAccount
    {
        public enum AccountType
        {
            Admin = 0,
            Agent = 1,
            Physician = 2,
            None = 10
        }

        public int UserAccountId { get; set; }

        public AccountType Type { get; set; }

        public string UserName { get; set; }

        public byte[] Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Admin Admin { get; set; }

        public Agent Agent { get; set; }

        public Physician Physician { get; set; }
    }
}

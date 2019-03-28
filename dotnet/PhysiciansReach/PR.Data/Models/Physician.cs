using System;

namespace PR.Data.Models
{
    public class Physician
    {
        public int UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public UserAccount UserAccount { get; set; }

        public Address Address { get; set; }
    }
}

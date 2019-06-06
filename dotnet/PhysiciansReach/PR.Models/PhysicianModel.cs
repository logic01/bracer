using System;

namespace PR.Models
{
    public class PhysicianModel
    {
        public UserAccountModel UserAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string NPI { get; set; }

        public string DEA { get; set; }

        public AddressModel Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

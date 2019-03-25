using PR.Models;
using System;

namespace PhysiciansReach.Models
{
    public class PhysicianModel
    {
        public UserAccountModel UserAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public AddressModel Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

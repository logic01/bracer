﻿using System;

namespace PR.Data.Models
{
    public class Physician
    {
        public int UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public UserAccount UserAccount { get; set; }
    }
}

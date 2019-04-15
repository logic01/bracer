using System;

namespace PR.Data.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public string AddressLineOne { get; set; }

        public string AddressLineTwo { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Physician Physician { get; set; }

        public Patient Patient { get; set; }

        public Patient PatientsPhysician { get; set; }
    }
}

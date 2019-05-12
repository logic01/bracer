using System;

namespace PR.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Will return the line 1 line 2, city, state, zipcode
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{AddressLineOne} {AddressLineTwo}, {City}, {State}, {ZipCode}";
        }
    }
}

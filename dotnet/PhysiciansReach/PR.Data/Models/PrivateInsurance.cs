using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PR.Data.Models
{
    public class PrivateInsurance
    {
        [Key]
        public int PrivateInsuranceId { get; set; }
        [MaxLength(100)]
        public string Insurance { get; set; }
        [MaxLength(100)]
        public string InsuranceId { get; set; }
        [MaxLength(100)]
        public string Group { get; set; }
        [MaxLength(100)]
        public string PCN { get; set; }
        [MaxLength(100)]
        public string Bin { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(10)]
        public string Zip { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        public Patient Patient { get; set; }
    }
}

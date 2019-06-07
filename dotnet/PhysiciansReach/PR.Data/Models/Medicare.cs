using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PR.Data.Models
{
    public class Medicare
    {
        [Key]
        public int MedicareId { get; set; } 
        [MaxLength(100)]
        public string MemberId { get; set; }
        [MaxLength(100)]
        public string PatientGroup { get; set; }
        [MaxLength(100)]
        public string Pcn { get; set; }
        [MaxLength(100)]
        public string SubscriberNumber { get; set; }
        [MaxLength(100)]
        public string SecondaryCarrier { get; set; }
        [MaxLength(100)]
        public string SecondarySubscriberNumber { get; set; }
        public Patient Patient { get; set; }
    }
}

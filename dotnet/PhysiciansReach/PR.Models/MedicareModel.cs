namespace PR.Models
{
    public class MedicareModel
    {
        public int MedicareId { get; set; }

        public string MemberId { get; set; }

        public string PatientGroup { get; set; }

        public string Pcn { get; set; }

        public string SubscriberNumber { get; set; }

        public string SecondaryCarrier { get; set; }

        public string SecondarySubscriberNumber { get; set; }
    }
}
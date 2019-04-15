using System;

namespace PR.Data.Models
{
    public class Patient
    {
        public enum CallbackTime
        {
            Morning,
            Afternoon,
            Evening
        }

        public enum LanguageType
        {
            English,
            Spanish
        }

        public enum SexType
        {
            Male,
            Female
        }

        public enum TherapyType
        {
            RX,
            DME,
            BOTH,
            LABS
        }

        public enum InsuranceType
        {
            PRIVATE,
            MEDICARE,
            BOTH
        }

        public enum PharmacyType
        {
            TWA,
            UNIVERSALRX
        }

        public int PatientId { get; set; }

        public LanguageType Language { get; set; }

        public SexType Sex { get; set; }

        public TherapyType Therapy { get; set; }

        public InsuranceType Insurance { get; set; }

        public PharmacyType Pharmacy { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public bool CallBackImmediately { get; set; }

        public CallbackTime BestTimeToCallBack { get; set; }

        public bool IsDme { get; set; }

        public string Medications { get; set; }

        public string Notes { get; set; }

        public string OtherProducts { get; set; }

        public string PhysiciansName { get; set; }

        public string PhysiciansPhoneNumber { get; set; }

        public int AddressId { get; set; }

        public int PhysiciansAddressId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public Address Address { get; set; }

        public Address PhysiciansAddress { get; set; }
    }

}

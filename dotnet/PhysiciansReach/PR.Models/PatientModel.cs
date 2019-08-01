using PR.Constants.Enums;
using System;

namespace PR.Models
{
    public class PatientModel
    {
        public int PatientId { get; set; }

        public int AgentId { get; set; }

        public LanguageType Language { get; set; }

        public SexType Sex { get; set; }

        public CallbackTime BestTimeToCallBack { get; set; }

        public TherapyType Therapy { get; set; }

        public InsuranceType Insurance { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public AddressModel Address { get; set; }

        public bool IsDme { get; set; }

        public string MainPainArea { get; set; }
        public string SecondPainArea { get; set; }
        public string HadBraceBefore { get; set; }
        public string PainCream { get; set; }

        public string Medications { get; set; }

        public string Notes { get; set; }

        public string OtherProducts { get; set; }

        public string PhysiciansName { get; set; }

        public string PhysiciansPhoneNumber { get; set; }

        public string Waist { get; set; }

        public string Height { get; set; }

        public string ShoeSize { get; set; }

        public string Allergies { get; set; }

        public string Weight { get; set; }

        public AddressModel PhysiciansAddress { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public PrivateInsuranceModel PrivateInsurance { get; set; }

        public MedicareModel Medicare { get; set; }


    }
}

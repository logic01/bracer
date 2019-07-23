namespace PR.Export
{
    /// <summary>
    /// The ids of these enums map the CustomPropertyName on the embedded word doc BLANK_EXAM_NOTE to
    /// the PDF provided by the customer that put numbers in the textboxes. The intake forms will have 
    /// to do some manual mapping to these, and the word doc will handle the rest.
    /// </summary>
    public enum MappingEnums
    {
        PatientName = 1,
        Address =2,
        Phone = 3,
        Height = 4,
        Weight = 5,
        Age = 6,
        DOB = 7,
        Gender = 8,
        Allergies = 9,
        MemberId = 10,
        ShoeSize = 11,
        Waist = 12,
        PainArea = 13,
        PainFeeling = 14,
        PainBegan = 15,
        PainCause = 16,
        PainSelfTreatment = 17,
        PainDescription = 18,
        PainDuration = 19,
        PreviousTreatment = 20,
        EffectsDaily = 21,
        Surgies = 22,
        PainLevel = 23,
        ServiceDate = 24,
        Insurance = 25,
        PhyName= 26, //Physician First Last w Dr prepended
        PhyNpi = 27,
        PhyDea = 28,
        PhyAddress = 29,
        PhyPhone = 30,
        PhyFax = 31, //Default this to 0000000000
        PhyNameNoDr = 32, //Physican First Last w/o Dr
        IP = 33,
        SignatureDate = 34,
        MedPatientGroup = 35,
        MedPCN = 36,
        MedMemberId = 37,
        MedSubscriber = 38,
        MedSecondary = 39,
        MedSecondarySubscriber = 40,
        GeneralIntakeNotes = 41,
        HCPCSCode = 42,
        HCPCSProduct = 43,
        HCPCSDescription = 44, //Not used in the doc?
        HCPCSDuration = 45,
        ICDCode = 46, //M, L codes
        ICDDescription = 47, //Diagnosis
        DrNotes1 = 48,
        DrNotes2 = 49

    }
}

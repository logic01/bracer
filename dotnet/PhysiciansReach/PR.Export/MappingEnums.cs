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
        PainChart = 13,
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
        Insurance = 25
    }
}

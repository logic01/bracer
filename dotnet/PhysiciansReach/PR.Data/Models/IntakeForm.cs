using PR.Constants.Enums;
using System;
using System.Collections.Generic;

namespace PR.Data.Models
{
    public class IntakeForm
    {
        public int IntakeFormId { get; set; }

        public string PatientId { get; set; }

        public IntakeFormType IntakeFormType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<Question> Questions { get; set; }

        public Patient Patient { get; set; }
    }
}

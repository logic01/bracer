using System;

namespace PR.Models
{
    public class ICD10Model
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }        
    }
}

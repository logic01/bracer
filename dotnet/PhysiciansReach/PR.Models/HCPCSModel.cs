using System;

namespace PR.Models
{
    public class HCPCSModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Product { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

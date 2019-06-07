﻿using System;

namespace PR.Data.Models
{
    public class HCPCS
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Product { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public IntakeForm IntakeForm { get; set; }
    }
}

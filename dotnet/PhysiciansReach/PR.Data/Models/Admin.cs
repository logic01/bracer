using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Data.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

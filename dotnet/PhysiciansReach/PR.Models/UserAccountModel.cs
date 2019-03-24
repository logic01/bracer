using System;

namespace PhysiciansReach.Models
{
    public class UserAccountModel
    {
        public int UserAccountId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}

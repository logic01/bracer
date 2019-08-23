using PR.Constants.Enums;
using System;
using System.Collections.Generic;

namespace PR.Models
{
    public class UserAccountModel
    {
        public int UserAccountId { get; set; }

        public AccountType Type { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string ConfirmationPassword { get; set; }

        public bool Active { get; set; }

        public string Token { get; set; }

        public List<ErrorModel> Errors { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}

namespace PhysiciansReach.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }
    }
}

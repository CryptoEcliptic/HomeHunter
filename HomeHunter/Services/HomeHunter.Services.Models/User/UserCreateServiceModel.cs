using System;

namespace HomeHunter.Services.Models.User
{
    public class UserCreateServiceModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Services.Models.User
{
    public class UserDetailsServiceModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IsActive { get; set; }

        public string CreatedOn { get; set; }

        public string EmailConfirmed { get; set; }

        public string LastLogin { get; set; }
    }
}

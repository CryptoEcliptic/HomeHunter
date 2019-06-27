using HomeHunter.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HomeHunter.Domain
{
    public class HomeHunterUser : IdentityUser, IAuditInfo
    {
        public HomeHunterUser()
        {
            this.UserRoles = new List<IdentityRole>();
            this.OffersCreated = new List<Offer>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<IdentityRole> UserRoles { get; set; }

        public ICollection<Offer> OffersCreated { get; set; }
    }
}

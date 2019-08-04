using HomeHunter.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class HomeHunterUser : IdentityUser, IAuditInfo
    {
        public HomeHunterUser()
        {
            this.OffersCreated = new List<Offer>();
        }

        [MaxLength(16)]
        public string FirstName { get; set; }

        [MaxLength(16)]
        public string LastName { get; set; }

        [MaxLength(32)]
        public string City { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Offer> OffersCreated { get; set; }
    }
}

using HomeHunter.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Domain
{
    public class Offer : IAuditInfo
    {
        public Offer()
        {
            this.Authors = new List<HomeHunterUser>();
        }
        //TODO Implement the rest of functionalities
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<HomeHunterUser> Authors { get; set; }
    }
}

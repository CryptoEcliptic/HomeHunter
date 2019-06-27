using HomeHunter.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeHunter.Domain
{
    public class Offer : IAuditInfo
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string Comments { get; set; }

        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(HomeHunterUser))]
        public string UserId { get; set; }
        public HomeHunterUser User { get; set; }
    }
}

using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class RealEstateType : BaseModel<int>
    {
        public RealEstateType()
        {
            this.RealEstates = new List<RealEstate>();
        }

        [Required]
        [MaxLength(32)]
        public string TypeName { get; set; }

        [Required]
        public string MinReferenceNumber { get; set; }

        [Required]
        public string MaxReferenceNumber { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }
    }
}

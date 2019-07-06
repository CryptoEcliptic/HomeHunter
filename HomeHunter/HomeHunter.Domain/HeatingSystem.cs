using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class HeatingSystem : BaseModel<int>
    {
        public HeatingSystem()
        {
            this.RealEstates = new List<RealEstate>();
        }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }
    }
}

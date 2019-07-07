using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class BuildingType : BaseModel<int>
    {
        public BuildingType()
        {
            this.RealEstates = new List<RealEstate>();
        }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }
    }
}

using HomeHunter.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class Address : BaseModel<int>
    {
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public string RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }

        public int? VillageId { get; set; }
        public Village Village { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }

        public int? NeighbourhoodId { get; set; }
        public Neighbourhood Neighbourhood { get; set; }
    }
}

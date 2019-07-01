using HomeHunter.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Domain
{
    public class RealEstate : BaseModel<string>
    {
        public RealEstate()
        {
            this.Offers = new List<Offer>();
        }

        public int? FloorNumber { get; set; }

        public int? BuildingTotalFloors { get; set; }

        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? Year { get; set; }

        public bool? ParkingPlace { get; set; }

        public bool? Yard { get; set; }

        public bool? MetroNearBy { get; set; }

        public bool? Balcony { get; set; }

        public bool? CellingOrBasement { get; set; }

        public Address Address { get; set; }

        public int? HeatingSystemId { get; set; }
        public HeatingSystem HeatingSystem { get; set; }

        public int RealEstateTypeId { get; set; }
        public RealEstateType RealEstateType { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}

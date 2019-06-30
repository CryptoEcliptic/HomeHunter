using HomeHunter.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeHunter.Domain
{
    public class RealEstate : BaseModel<string>
    {
        public int FloorNumber { get; set; }

        public int? BuildingTotalFloors { get; set; }

        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Year { get; set; }

        public bool? ParkingPlace { get; set; }

        public bool? Yard { get; set; }

        public bool? MetroNearBy { get; set; }

        public bool? Balcony { get; set; }

        public bool? CellingOrBasement { get; set; }

        public Address Address { get; set; }

        public int? HeatingSystemId { get; set; }
        public HeatingSystem HeatingSystem { get; set; }
    }
}

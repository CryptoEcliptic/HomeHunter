using HomeHunter.Domain;
using System.Collections.Generic;

namespace HomeHunter.Services.Models.RealEstate
{
    public class RealEstateDetailsServiceModel
    {
        public string Id { get; set; }

        public string FloorNumber { get; set; }

        public int? BuildingTotalFloors { get; set; }

        public double Area { get; set; }

        public decimal Price { get; set; }

        public decimal PricePerSquareMeter { get; set; }

        public int? Year { get; set; }

        public bool? ParkingPlace { get; set; }
                   
        public bool? Garage { get; set; }
                   
        public bool? Yard { get; set; }
                   
        public bool? Furnitures { get; set; }
                   
        public bool? Elevator { get; set; }
                   
        public bool? Celling { get; set; }

        public bool? Basement { get; set; }

        public string Address { get; set; }

        public string HeatingSystem { get; set; }

        public string RealEstateType { get; set; }

        public string BuildingType { get; set; }

        public string City { get; set; }

        public string Village { get; set; }

        public string Neighbourhood { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }
    }
}

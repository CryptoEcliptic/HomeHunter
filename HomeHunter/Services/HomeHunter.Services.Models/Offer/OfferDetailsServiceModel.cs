using System.Collections.Generic;

namespace HomeHunter.Services.Models.Offer
{
    public class OfferDetailsServiceModel
    {
        public string Id { get; set; }

        public string ReferenceNumber { get; set; }

        public string City { get; set; }

        public string Neighbourhood { get; set; }

        public string Village { get; set; }

        public string Address { get; set; }

        public string RealEstateType { get; set; }

        public string BuildingType { get; set; }

        public double Area { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }

        public string FloorNumber { get; set; }

        public int? BuildingTotalFloors { get; set; }

        public string HeatingSystem { get; set; }

        public bool ParkingPlace { get; set; }

        public bool Yard { get; set; }

        public bool MetroNearBy { get; set; }

        public bool Balcony { get; set; }

        public bool CellingOrBasement { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string OfferType { get; set; }

        public string Comments { get; set; }

        public string ContactNumber { get; set; }

        public string Author { get; set; }

        public decimal PricePerSquareMeter { get; set; }

        public List<string> Images { get; set; }
    }
}

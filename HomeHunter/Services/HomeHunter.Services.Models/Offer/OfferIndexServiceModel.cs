using HomeHunter.Services.Models.Image;
using System.Collections.Generic;

namespace HomeHunter.Services.Models.Offer
{
    public class OfferIndexServiceModel
    {
        public string Id { get; set; }

        public string ReferenceNumber { get; set; }

        public string OfferType { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string RealEstateType { get; set; }

        public string City { get; set; }

        public decimal Price { get; set; }

        public string FloorNumber { get; set; }

        public int? BuildingTotalFloors { get; set; }

        public string Neighbourhood { get; set; }

        public string Author { get; set; }

        public double Area { get; set; }

        public string BuildingType { get; set; }

        public string Comments { get; set; }

        public List<ImageIndexServiceModel> Images { get; set; }

    }
}

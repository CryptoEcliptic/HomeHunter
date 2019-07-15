using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Services.Models.RealEstate
{
    public class RealEstateIndexServiceModel
    {
        public string Id { get; set; }

        public string RealEstateType { get; set; }

        public string BuildingType { get; set; }

        public string City { get; set; }

        public string Village { get; set; }

        public string Neighbourhood { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

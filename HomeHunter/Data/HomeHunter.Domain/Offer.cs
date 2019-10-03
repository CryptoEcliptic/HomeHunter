using HomeHunter.Domain.Common;
using HomeHunter.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class Offer : BaseModel<string>
    {
        public OfferType OfferType { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }

        public string Comments { get; set; }

        public string ContactNumber { get; set; }

        public string AgentName { get; set; }

        public string OfferServiceInformation { get; set; }

        public bool IsOfferActive { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public HomeHunterUser Author { get; set; }

        [Required]
        public string RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}

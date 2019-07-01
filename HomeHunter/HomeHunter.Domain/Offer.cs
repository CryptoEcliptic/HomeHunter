using HomeHunter.Domain.Common;
using HomeHunter.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain
{
    public class Offer : BaseModel<int>
    {
        public OfferType OfferType { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }

        public string Comments { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public HomeHunterUser Author { get; set; }

        public int RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}

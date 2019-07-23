using HomeHunter.Domain.Common;

namespace HomeHunter.Domain
{
    public class Image : BaseModel<string>
    {
        public string Url { get; set; }

        public string RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}

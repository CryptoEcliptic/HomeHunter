namespace HomeHunter.Services.Models.Offer
{
    public class OfferPlainDetailsServiceModel
    {
        public string Id { get; set; }

        public bool IsOfferActive { get; set; }

        public string OfferType { get; set; }

        public string Comments { get; set; }

        public string ContactNumber { get; set; }

        public string AgentName { get; set; }

        public string OfferServiceInformation { get; set; }
    }
}

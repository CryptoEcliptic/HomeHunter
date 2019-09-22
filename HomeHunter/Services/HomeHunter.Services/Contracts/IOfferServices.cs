using HomeHunter.Domain.Enums;
using HomeHunter.Services.Models.Offer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IOfferServices
    {
        Task<bool> CreateOfferAsync(string authotId, string estateId, OfferCreateServiceModel model);

        Task<IEnumerable<OfferIndexServiceModel>> GetAllActiveOffersAsync(OfferType? condition = null);

        Task<IEnumerable<OfferIndexDeactivatedServiceModel>> GetAllDeactivatedOffersAsync();

        Task<OfferDetailsServiceModel> GetOfferDetailsAsync(string id);

        Task<OfferPlainDetailsServiceModel> GetOfferByIdAsync(string id);

        Task<bool> EditOfferAsync(OfferEditServiceModel model);

        string GetOfferIdByRealEstateIdAsync(string realEstateId);

        Task<bool> DeleteOfferAsync(string offerId);

    }
}

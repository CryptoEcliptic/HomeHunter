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

        Task<IEnumerable<OfferIndexDeletedServiceModel>> GetAllDeletedOffersAsync();

        Task<IEnumerable<OfferIndexServiceModel>> GetAllInactiveOffersAsync();

        Task<OfferDetailsServiceModel> GetOfferDetailsAsync(string id, bool isLogged);

        Task<OfferPlainDetailsServiceModel> GetOfferByIdAsync(string id);

        Task<bool> EditOfferAsync(OfferEditServiceModel model);

        string GetOfferIdByRealEstateIdAsync(string realEstateId);

        Task<bool> DeleteOfferAsync(string offerId);

        Task<bool> DeactivateOfferAsync(string offerId);

    }
}

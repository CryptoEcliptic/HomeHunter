using HomeHunter.Services.Models.Offer;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IOfferServices
    {
        Task<bool> CreateOffer(string authotId, string estateId, OfferCreateServiceModel model);
    }
}

using HomeHunter.Services.Models.RealEstate;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateServices
    {
        Task<bool> CreateRealEstate(RealEstateCreateServiceModel model);
    }
}

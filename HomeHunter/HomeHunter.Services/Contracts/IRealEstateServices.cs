using HomeHunter.Models.BindingModels.RealEstate;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateServices
    {
        Task<bool> CreateRealEstate(CreateRealEstateBindingModel model);
    }
}

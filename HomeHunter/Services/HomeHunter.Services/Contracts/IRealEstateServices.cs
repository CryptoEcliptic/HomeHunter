using HomeHunter.Services.Models.RealEstate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateServices
    {
        Task<bool> CreateRealEstate(RealEstateCreateServiceModel model);

        Task <IEnumerable<RealEstateIndexServiceModel>> GetAllRealEstates();
    }
}

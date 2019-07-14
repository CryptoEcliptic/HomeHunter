using HomeHunter.Domain;
using HomeHunter.Services.Models.RealEstateType;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateTypeServices
    {
        Task<IQueryable<RealEstateTypeServiceModel>> GetAllTypes();

       RealEstateType GetRealEstateTypeByName(string typeName);
    }
}

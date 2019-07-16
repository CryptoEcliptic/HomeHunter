using HomeHunter.Domain;
using HomeHunter.Services.Models.RealEstateType;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateTypeServices
    {
        Task<IQueryable<RealEstateTypeServiceModel>> GetAllTypesAsync();

        Task<RealEstateType> GetRealEstateTypeByNameAsync(string typeName);
    }
}

using HomeHunter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateTypeServices
    {
        List<RealEstateType> GetAllTypes();

        Task<RealEstateType> GetRealEstateTypeByName(string typeName);
    }
}

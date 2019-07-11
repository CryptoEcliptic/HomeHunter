using HomeHunter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IBuildingTypeServices
    {
        List<BuildingType> GetAllBuildingTypes();

        Task<BuildingType> GetBuildingType(string type);
    }
}

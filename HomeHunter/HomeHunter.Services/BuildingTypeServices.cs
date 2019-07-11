using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class BuildingTypeServices : IBuildingTypeServices
    {
        private readonly HomeHunterDbContext context;

        public BuildingTypeServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<BuildingType> GetAllBuildingTypes()
        {
            var buildingTypes = this.context.BuildingTypes.ToList();

            return buildingTypes;
        }

        public Task<BuildingType> GetBuildingType(string type)
        {
            var buildingType = Task.Run(() => this.context.BuildingTypes.FirstOrDefault(x => x.Name == type));

            return buildingType;
        }
    }
}

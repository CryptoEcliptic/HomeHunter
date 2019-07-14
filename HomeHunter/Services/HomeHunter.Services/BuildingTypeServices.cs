using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.BuildingType;
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

        public async Task<IQueryable<BuildingTypeServiceModel>> GetAllBuildingTypes()
        {
            var buildingTypes = this.context.BuildingTypes
                .Select(x => new BuildingTypeServiceModel
                {
                    Name = x.Name,
                });
              

            return buildingTypes;
        }

        public BuildingType GetBuildingType(string type)
        {
            var buildingType = this.context.BuildingTypes.FirstOrDefault(x => x.Name == type);

            return buildingType;
        }
    }
}

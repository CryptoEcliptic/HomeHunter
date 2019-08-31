using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.BuildingType;
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

        public async Task<IQueryable<BuildingTypeServiceModel>> GetAllBuildingTypesAsync()
        {
            var buildingTypes = Task.Run(() =>this.context.BuildingTypes
                .Select(x => new BuildingTypeServiceModel
                {
                    Name = x.Name,
                }));
              
            return await buildingTypes;
        }

        public async Task<BuildingType> GetBuildingTypeAsync(string type)
        {
            var buildingType = Task.Run(() => this.context.BuildingTypes.FirstOrDefault(x => x.Name == type));

            return await buildingType;
        }
    }
}

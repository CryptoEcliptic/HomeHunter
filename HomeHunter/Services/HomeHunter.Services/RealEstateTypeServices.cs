using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstateType;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class RealEstateTypeServices : IRealEstateTypeServices
    {
        private readonly HomeHunterDbContext context;

        public RealEstateTypeServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<RealEstateTypeServiceModel>> GetAllTypesAsync()
        {
           var types = Task.Run(() => this.context.RealEstateTypes
                .OrderBy(x => x.CreatedOn)
                .Select(x => new RealEstateTypeServiceModel
                {
                    TypeName = x.TypeName,
                }));

            return await types;
        }

        public async Task<RealEstateType> GetRealEstateTypeByNameAsync(string typeName)
        {
            var realEstateType = Task.Run(() => this.context.RealEstateTypes.FirstOrDefault(x => x.TypeName == typeName));

            return await realEstateType;
        }
    }
}

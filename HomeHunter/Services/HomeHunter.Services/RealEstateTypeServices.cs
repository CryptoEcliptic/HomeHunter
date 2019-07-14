using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstateType;
using System.Collections.Generic;
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

        public async Task<IQueryable<RealEstateTypeServiceModel>> GetAllTypes()
        {
           var types = this.context.RealEstateTypes
                .Select(x => new RealEstateTypeServiceModel
                {
                    TypeName = x.TypeName,
                });

            return types;
        }

        public RealEstateType GetRealEstateTypeByName(string typeName)
        {
            var realEstateType = this.context.RealEstateTypes.FirstOrDefault(x => x.TypeName == typeName);

            return realEstateType;
        }
    }
}

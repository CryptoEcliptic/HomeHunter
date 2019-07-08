using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace HomeHunter.Services
{
    public class RealEstateTypeService : IRealEstateTypeService
    {
        private readonly HomeHunterDbContext context;

        public RealEstateTypeService(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<RealEstateType> GetAllTypes()
        {
           var types = this.context.RealEstateTypes.ToList();

            return types;
        }
    }
}

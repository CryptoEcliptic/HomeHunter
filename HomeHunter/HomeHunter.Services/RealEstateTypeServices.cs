using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace HomeHunter.Services
{
    public class RealEstateTypeServices : IRealEstateTypeServices
    {
        private readonly HomeHunterDbContext context;

        public RealEstateTypeServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<RealEstateType> GetAllTypes()
        {
           var types = this.context.RealEstateTypes.ToList();

            return types;
        }

        public RealEstateType GetRealEstateTypeByName(string typeName)
        {
            var realEstateType = this.context.RealEstateTypes.FirstOrDefault(x => x.TypeName == typeName);

            return realEstateType;
        }
    }
}

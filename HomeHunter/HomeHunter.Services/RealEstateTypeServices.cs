using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public List<RealEstateType> GetAllTypes()
        {
           var types = this.context.RealEstateTypes.ToList();

            return types;
        }

        public async Task<RealEstateType> GetRealEstateTypeByName(string typeName)
        {
            var realEstateType = Task.Run(() => this.context.RealEstateTypes.FirstOrDefault(x => x.TypeName == typeName));

            return await realEstateType;
        }
    }
}

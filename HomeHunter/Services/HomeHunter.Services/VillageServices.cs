using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class VillageServices : IVillageServices
    {
        private readonly HomeHunterDbContext context;

        public VillageServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<Village> CreateVillageAsync(string name)
        {
            if (name == null)
            {
                return null;
            }

            if (!IsVillageExists(name))
            {
                var village =  new Village
                {
                    Name = name
                };

                await this.context.Villages.AddAsync(village);

                return village;

            }

            return null;
        }

        private bool IsVillageExists(string name)
        {
            return this.context.Villages.Any(x => x.Name == name);
        }
    }
}

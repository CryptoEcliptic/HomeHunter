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

        public Village CreateVillage(string name)
        {
            if (name == null)
            {
                return null;
            }

            if (!IsVillageEhists(name))
            {
                var village =  new Village
                {
                    Name = name
                };

                return village;
            }

            return null;
        }

        public bool IsVillageEhists(string name)
        {
            return this.context.Villages.Any(x => x.Name == name);
        }
    }
}

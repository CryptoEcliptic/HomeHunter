using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Neighbourhood;

namespace HomeHunter.Services
{
    public class NeighbourhoodServices : INeighbourhoodServices
    {
        private readonly HomeHunterDbContext context;

        public NeighbourhoodServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<Neighbourhood> GetAllNeighbourhoods()
        {
            return this.context.Neighbourhoods.ToList();
        }

        public Neighbourhood GetNeighbourhoodByName(string name)
        {
            return this.context.Neighbourhoods.FirstOrDefault(x => x.Name == name);
        }

        public async Task<IQueryable<NeighbourhoodServiceModel>> GetNeighbourhoodsByCity(string cityName)
        {
            var neighbourhoodsFromDb =  this.context.Neighbourhoods
                .Where(x => x.City.Name == cityName)
                .Select(x => new NeighbourhoodServiceModel
                {
                    Name = x.Name,
                });

            return neighbourhoodsFromDb;
        }
    }
}

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

        public async Task<List<Neighbourhood>> GetAllNeighbourhoodsAsync()
        {
            return await Task.Run(() => this.context.Neighbourhoods.ToList());
        }

        public async Task<Neighbourhood> GetNeighbourhoodByNameAsync(string name)
        {
            return await Task.Run(() => this.context.Neighbourhoods.FirstOrDefault(x => x.Name == name));
        }

        public async Task<IQueryable<NeighbourhoodServiceModel>> GetNeighbourhoodsByCityAsync(string cityName)
        {
            var neighbourhoodsFromDb = Task.Run(() =>  this.context.Neighbourhoods
                .Where(x => x.City.Name == cityName)
                .Select(x => new NeighbourhoodServiceModel
                {
                    Name = x.Name,
                }));

            return await neighbourhoodsFromDb;
        }
    }
}

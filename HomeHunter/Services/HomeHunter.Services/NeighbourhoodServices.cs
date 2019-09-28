using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Neighbourhood;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class NeighbourhoodServices : INeighbourhoodServices
    {
        private readonly HomeHunterDbContext context;

        public NeighbourhoodServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<Neighbourhood> GetNeighbourhoodByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await Task.Run(() => this.context.Neighbourhoods.FirstOrDefault(x => x.Name == name));
        }

        public async Task<IQueryable<NeighbourhoodServiceModel>> GetNeighbourhoodsByCityAsync(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                return null;
            }

            var neighbourhoodsFromDb = Task.Run(() =>  this.context.Neighbourhoods
                .Where(x => x.City.Name == cityName)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new NeighbourhoodServiceModel
                {
                    Name = x.Name,
                }));

            return await neighbourhoodsFromDb;
        }


    }
}

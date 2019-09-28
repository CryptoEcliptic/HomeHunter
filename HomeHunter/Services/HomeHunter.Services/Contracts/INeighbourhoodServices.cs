using HomeHunter.Domain;
using HomeHunter.Services.Models.Neighbourhood;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface INeighbourhoodServices
    {
        Task<Neighbourhood> GetNeighbourhoodByNameAsync(string name);

        Task<IQueryable<NeighbourhoodServiceModel>> GetNeighbourhoodsByCityAsync(string cityName);
    }
}

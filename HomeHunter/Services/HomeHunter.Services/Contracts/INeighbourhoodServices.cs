using HomeHunter.Domain;
using HomeHunter.Services.Models.Neighbourhood;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface INeighbourhoodServices
    {
        List<Neighbourhood> GetAllNeighbourhoods();

        Task<IQueryable<NeighbourhoodServiceModel>> GetNeighbourhoodsByCity(string cityName);

        Neighbourhood GetNeighbourhoodByName(string name);
    }
}

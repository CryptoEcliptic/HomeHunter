using HomeHunter.Domain;
using HomeHunter.Services.Models.City;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface ICitiesServices
    {
       Task<IQueryable<CityServiceModel>> GetAllCitiesAsync();

       Task<City> GetByNameAsync(string name);
    }
}

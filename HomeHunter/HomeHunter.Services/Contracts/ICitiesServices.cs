using HomeHunter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface ICitiesServices
    {
        List<City> GetAllCities();

        Task<City> GetByName(string name);
    }
}

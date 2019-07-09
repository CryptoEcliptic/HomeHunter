using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class CitiesServices : ICitiesServices
    {
        private readonly HomeHunterDbContext context;

        public CitiesServices(HomeHunterDbContext context)
        {
            this.context = context;
        }
        public List<City> GetAllCities()
        {
            var cities = this.context.Cities.ToList();
            return cities;

        }

        public async Task<City> GetByName(string name)
        {
            var city = Task.Run(() => this.context.Cities.FirstOrDefault(x => x.Name == name));

            return await city;
        }
    }
}

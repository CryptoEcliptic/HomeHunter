using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.City;
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
        public async Task<IQueryable<CityServiceModel>> GetAllCities()
        {
            var cities = this.context.Cities
                .Select(x => new CityServiceModel
                {
                    Name = x.Name,
                });

            return cities;
        }

        public City GetByName(string name)
        {
            var city = this.context.Cities.FirstOrDefault(x => x.Name == name);

            return city;
        }
    }
}

using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.City;
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
        public async Task<IQueryable<CityServiceModel>> GetAllCitiesAsync()
        {
            var cities = Task.Run(() => this.context.Cities
                .OrderBy(x => x.CreatedOn)
                .Select(x => new CityServiceModel
                {
                    Name = x.Name,
                }))
               ;
               
            return await cities;
        }

        public async Task<City> GetByNameAsync(string name)
        {
            if (name == null)
            {
                return null;
            }

            var city = Task.Run(() => this.context.Cities.FirstOrDefault(x => x.Name == name));

            return await city;
        }
    }
}

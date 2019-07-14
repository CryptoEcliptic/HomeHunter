using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.HeatingSystem;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class HeatingSystemServices : IHeatingSystemServices
    {
        private readonly HomeHunterDbContext context;

        public HeatingSystemServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<HeatingSystemServiceModel>> GetAllHeatingSystems()
        {
            var heatingSystems = this.context.HeatingSystems
                .Select(x => new HeatingSystemServiceModel
                {
                    Name = x.Name,
                });

            return heatingSystems;

        }

        public HeatingSystem GetHeatingSystem(string systemName)
        {
            var heatingSystem =  this.context.HeatingSystems
            .FirstOrDefault(x => x.Name == systemName)
                ;

            return heatingSystem;
        }
    }
}

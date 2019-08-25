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

        public async Task<IQueryable<HeatingSystemServiceModel>> GetAllHeatingSystemsAsync()
        {
            var heatingSystems = Task.Run(() => this.context.HeatingSystems
                .Select(x => new HeatingSystemServiceModel
                {
                    Name = x.Name,
                }));

            return await heatingSystems;
        }

        public async Task<HeatingSystem> GetHeatingSystemAsync(string systemName)
        {
            var heatingSystem = Task.Run(() =>  this.context.HeatingSystems
            .FirstOrDefault(x => x.Name == systemName));

            return await heatingSystem;
        }
    }
}

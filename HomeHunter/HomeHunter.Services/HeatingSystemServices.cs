using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace HomeHunter.Services
{
    public class HeatingSystemServices : IHeatingSystemServices
    {
        private readonly HomeHunterDbContext context;

        public HeatingSystemServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<HeatingSystem> GetAllHeatingSystems()
        {
            var heatingSystems = this.context.HeatingSystems.ToList();

            return heatingSystems;

        }
    }
}

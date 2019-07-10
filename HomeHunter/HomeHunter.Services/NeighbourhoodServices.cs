using System.Collections.Generic;
using System.Linq;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Services.Contracts;

namespace HomeHunter.Services
{
    public class NeighbourhoodServices : INeighbourhoodServices
    {
        private readonly HomeHunterDbContext context;

        public NeighbourhoodServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public List<Neighbourhood> GetAllNeighbourhoods()
        {
            return this.context.Neighbourhoods.ToList();
        }
    }
}

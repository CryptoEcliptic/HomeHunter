using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Models.BindingModels.RealEstate;
using HomeHunter.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IRealEstateTypeServices realEstateTypeServices;
        private readonly ICitiesServices citiesServices;

        public RealEstateServices(HomeHunterDbContext context, 
            IRealEstateTypeServices realEstateTypeServices,
            ICitiesServices citiesServices)
        {
            this.context = context;
            this.realEstateTypeServices = realEstateTypeServices;
            this.citiesServices = citiesServices;
        }

        public async Task<bool> CreateRealEstate(CreateRealEstateBindingModel model)
        {
            var realEstateType = Task.Run(() => this.realEstateTypeServices.GetRealEstateTypeByName(model.RealEstateType.TypeName));

            if (realEstateType == null)
            {
                return false;
            }

            var city = Task.Run(() => this.citiesServices.GetByName(model.City.Name));


            var realEstate = new RealEstate
            {
                RealEstateType = await realEstateType,
                Area = model.Area,
                Price = model.Price,
                CreatedOn = DateTime.UtcNow,
                


            };

            return false;
        }
    }
}

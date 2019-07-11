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
        private readonly INeighbourhoodServices neighbourhoodServices;
        private readonly IAddressServices addressServices;
        private readonly IVillageServices villageServices;
        private readonly IBuildingTypeServices buildingTypeServices;
        private readonly IHeatingSystemServices heatingSystemServices;

        public RealEstateServices(HomeHunterDbContext context, 
            IRealEstateTypeServices realEstateTypeServices,
            ICitiesServices citiesServices,
            INeighbourhoodServices neighbourhoodServices,
            IAddressServices addressServices,
            IVillageServices villageServices,
            IBuildingTypeServices buildingTypeServices,
            IHeatingSystemServices heatingSystemServices)
        {
            this.context = context;
            this.realEstateTypeServices = realEstateTypeServices;
            this.citiesServices = citiesServices;
            this.neighbourhoodServices = neighbourhoodServices;
            this.addressServices = addressServices;
            this.villageServices = villageServices;
            this.buildingTypeServices = buildingTypeServices;
            this.heatingSystemServices = heatingSystemServices;
        }

        public async Task<bool> CreateRealEstate(CreateRealEstateBindingModel model)
        {
            var realEstateType = Task.Run(() => this.realEstateTypeServices.GetRealEstateTypeByName(model.RealEstateType));

            if (realEstateType == null || model.Area <=0 || model.Price <= 0 || model.Address == null)
            {
                return false;
            }

            var city = Task.Run(() => this.citiesServices.GetByName(model.City));
            var village = this.villageServices.CreateVillage(model.Village);

            var neighbourhood = this.neighbourhoodServices.GetNeighbourhoodByName(model.Neighbourhood);
            var address = this.addressServices.CreateAddress(await city, model.Address, await village, neighbourhood);

            var buildingType = this.buildingTypeServices.GetBuildingType(model.BuildingType);
            var heatingSystem = this.heatingSystemServices.GetHeatingSystem(model.HeatingSystem);

            var realEstate = new RealEstate
            {
                Area = model.Area,
                Price = model.Price,
                Yard = model.Yard,
                Year = model.Year,
                FloorNumber = model.FloorNumber,
                ParkingPlace = model.ParkingPlace,
                Balcony = model.Balcony,
                MetroNearBy = model.MetroNearBy,
                BuildingTotalFloors = model.BuildingTotalFloors,
                CellingOrBasement = model.CellingOrBasement,

                RealEstateType = await realEstateType,
                Address = await address,
                BuildingType = await buildingType,
                HeatingSystem = await heatingSystem,

                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
            };

            await this.context.RealEstates.AddAsync(realEstate);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}

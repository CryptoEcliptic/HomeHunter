using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper mapper;

        public RealEstateServices(HomeHunterDbContext context, 
            IRealEstateTypeServices realEstateTypeServices,
            ICitiesServices citiesServices,
            INeighbourhoodServices neighbourhoodServices,
            IAddressServices addressServices,
            IVillageServices villageServices,
            IBuildingTypeServices buildingTypeServices,
            IHeatingSystemServices heatingSystemServices,
            IMapper mapper)
        {
            this.context = context;
            this.realEstateTypeServices = realEstateTypeServices;
            this.citiesServices = citiesServices;
            this.neighbourhoodServices = neighbourhoodServices;
            this.addressServices = addressServices;
            this.villageServices = villageServices;
            this.buildingTypeServices = buildingTypeServices;
            this.heatingSystemServices = heatingSystemServices;
            this.mapper = mapper;
        }

        public async Task<bool> CreateRealEstate(RealEstateCreateServiceModel model)
        {
            var realEstateType = await Task.Run(() => this.realEstateTypeServices.GetRealEstateTypeByName(model.RealEstateType));

            if (realEstateType == null || model.Area <=0 || model.Price <= 0 || model.Address == null)
            {
                return false;
            }

            var city = await Task.Run(() => this.citiesServices.GetByName(model.City));
            var village = await Task.Run(() => this.villageServices.CreateVillage(model.Village));

            var neighbourhood = await Task.Run(() => this.neighbourhoodServices.GetNeighbourhoodByName(model.Neighbourhood));
            var address = await Task.Run(() => this.addressServices.CreateAddress(city, model.Address, village, neighbourhood));

            var buildingType = await Task.Run(() => this.buildingTypeServices.GetBuildingType(model.BuildingType));
            var heatingSystem = await Task.Run(() => this.heatingSystemServices.GetHeatingSystem(model.HeatingSystem));

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

                RealEstateType = realEstateType,
                Address = address,
                BuildingType = buildingType,
                HeatingSystem = heatingSystem,

                IsDeleted = false,
            };

            await this.context.RealEstates.AddAsync(realEstate);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RealEstateIndexServiceModel>> GetAllRealEstates()
        {
            var realEstates = context.RealEstates
                .Include(r => r.BuildingType)
                .Include(r => r.HeatingSystem)
                .Include(r => r.RealEstateType)
                .Include(r => r.Address.City)
                .Include(r => r.Address.Village)
                .Include(r => r.Address.Neighbourhood)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();


            var realEstatesServiceModel = this.mapper.Map<IEnumerable<RealEstateIndexServiceModel>>(realEstates);

            return realEstatesServiceModel;
        }
    }
}

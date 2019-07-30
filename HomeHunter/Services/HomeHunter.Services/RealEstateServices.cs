using AutoMapper;
using HomeHunter.Common;
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

        public async Task<string> CreateRealEstateAsync(RealEstateCreateServiceModel model)
        {
            var realEstateType = await this.realEstateTypeServices.GetRealEstateTypeByNameAsync(model.RealEstateType);

            if (realEstateType == null || model.Area <=0 || model.Price <= 0 || model.Address == null)
            {
                return null;
            }

            var city = await this.citiesServices.GetByNameAsync(model.City);
            var village = await this.villageServices.CreateVillageAsync(model.Village);

            var neighbourhood = await this.neighbourhoodServices.GetNeighbourhoodByNameAsync(model.Neighbourhood);
            var address = await this.addressServices.CreateAddressAsync(city, model.Address, village, neighbourhood);

            var buildingType = await this.buildingTypeServices.GetBuildingTypeAsync(model.BuildingType);
            var heatingSystem = await this.heatingSystemServices.GetHeatingSystemAsync(model.HeatingSystem);

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

            return realEstate.Id;
        }

        public async Task<IEnumerable<RealEstateIndexServiceModel>> GetAllRealEstatesAsync()
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


            var realEstatesServiceModel =  this.mapper.Map<IEnumerable<RealEstateIndexServiceModel>>(realEstates);

            return realEstatesServiceModel;
        }

        public async Task<RealEstateDetailsServiceModel> GetDetailsAsync(string id)
        {
            var realEstate = await this.context.RealEstates
                .Include(r => r.BuildingType)
                .Include(r => r.HeatingSystem)
                .Include(r => r.RealEstateType)
                .Include(r => r.Address.City)
                .Include(r => r.Address.Village)
                .Include(r => r.Address.Neighbourhood)
                .Include(r => r.Images)
                .FirstOrDefaultAsync(x => x.Id == id);
            ;

            var realEstateServiceModel = this.mapper.Map<RealEstateDetailsServiceModel>(realEstate);

            return realEstateServiceModel;
        }

        public async Task<bool>EditRealEstateAsync(RealEstateEditServiceModel model)
        {
            if (!this.context.RealEstates.Any(x => x.Id == model.Id))
            {
                return false;
            }

            var realEstateToEdit = await this.context.RealEstates
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            var city = await this.citiesServices.GetByNameAsync(model.City);
            var neighbourhood = await this.neighbourhoodServices.GetNeighbourhoodByNameAsync(model.Neighbourhood);
            var village = await this.villageServices.CreateVillageAsync(model.Village);
            var addressId = realEstateToEdit.Address.Id;
            var address = await this.addressServices.EditAddressAsync(addressId, city, model.Address, village, neighbourhood);

            var realEstateType = await this.realEstateTypeServices.GetRealEstateTypeByNameAsync(model.RealEstateType);
            var buildingType = await this.buildingTypeServices.GetBuildingTypeAsync(model.BuildingType);
            var heatingSystem = await this.heatingSystemServices.GetHeatingSystemAsync(model.HeatingSystem);

            realEstateToEdit.Address = address;
            realEstateToEdit.BuildingType = buildingType;
            realEstateToEdit.HeatingSystem = heatingSystem;
            realEstateToEdit.RealEstateType = realEstateType;
            realEstateToEdit.Year = model.Year;
            realEstateToEdit.ModifiedOn = DateTime.UtcNow;

            this.mapper.Map<RealEstateEditServiceModel, RealEstate>(model, realEstateToEdit);

            try
            {
                this.context.Update(realEstateToEdit);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRealEstateAsync(string id)
        {
            var realEstate = await this.context.RealEstates
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (realEstate == null)
            {
                throw new ArgumentNullException($"Real estate with id {id} not found!");
            }

            await this.addressServices.DeleteAddressAsync(realEstate.Address.Id);

            realEstate.IsDeleted = true;
            realEstate.DeletedOn = DateTime.UtcNow;
            try
            {
                this.context.Update(realEstate);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return false;
            }
           
            return true;
        }
        public async Task<string> GetRealEstateIdByOfferId(string offerId)
        {
            if (offerId == null)
            {
                return null;
            }

            var id = this.context
                .Offers
                .FirstOrDefault(x => x.Id == offerId)
                .RealEstateId;

            return id;     
        }

    }
}

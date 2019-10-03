using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private const string InvalidMethodParametersMessage = "Some of the input parameters are not valid!";
        private const string UnsuccessfullyCreatedRealEstateMessage = "No real estate created!";
        private const string UnexistingRealEstateMessage = "No real estate with such Id!";
        private const string InvalidOfferIdMessage = "Invalid offer Id!";
        private const string InvalidRealEstateIdMessage = "Invalid Real Estate Id!";

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
            if (model.Area <= 0 || model.Price <= 0)
            {
                throw new ArgumentNullException(InvalidMethodParametersMessage);
            }

            var city = await this.citiesServices.GetByNameAsync(model.City);
            var village = await this.villageServices.CreateVillageAsync(model.Village);
            var neighbourhood = await this.neighbourhoodServices.GetNeighbourhoodByNameAsync(model.Neighbourhood);

            var realEstate = new RealEstate
            {
                RealEstateType = await this.realEstateTypeServices.GetRealEstateTypeByNameAsync(model.RealEstateType),
                Address = await this.addressServices.CreateAddressAsync(city, model.Address, village, neighbourhood),
                BuildingType = await this.buildingTypeServices.GetBuildingTypeAsync(model.BuildingType),
                HeatingSystem = await this.heatingSystemServices.GetHeatingSystemAsync(model.HeatingSystem),
                PricePerSquareMeter = model.Price / (decimal)model.Area
            };
            this.mapper.Map<RealEstateCreateServiceModel, RealEstate>(model, realEstate);

            var realEstateIdResult = await AddRealEstateToTheDb(realEstate);
            return realEstateIdResult;
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
                .FirstOrDefaultAsync(x => x.Id == id);

            if (realEstate == null)
            {
                throw new ArgumentNullException(UnexistingRealEstateMessage);
            }

            var realEstateServiceModel = this.mapper.Map<RealEstateDetailsServiceModel>(realEstate);
            return realEstateServiceModel;
        }

        public async Task<bool>EditRealEstateAsync(RealEstateEditServiceModel model)
        {
            if (!this.context.RealEstates.Any(x => x.Id == model.Id))
            {
                throw new ArgumentNullException(UnexistingRealEstateMessage);
            }

            var realEstateToEdit = await this.context.RealEstates
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            //Edit Address nav properties
            var city = await this.citiesServices.GetByNameAsync(model.City);
            var neighbourhood = await this.neighbourhoodServices.GetNeighbourhoodByNameAsync(model.Neighbourhood);
            var village = await this.villageServices.CreateVillageAsync(model.Village);
            var addressId = realEstateToEdit.Address.Id;

            //Edit the address with already edited nav properties
            realEstateToEdit.Address = await this.addressServices.EditAddressAsync(addressId, city, model.Address, village, neighbourhood);

            realEstateToEdit.RealEstateType = await this.realEstateTypeServices.GetRealEstateTypeByNameAsync(model.RealEstateType);
            realEstateToEdit.BuildingType = await this.buildingTypeServices.GetBuildingTypeAsync(model.BuildingType);
            realEstateToEdit.HeatingSystem = await this.heatingSystemServices.GetHeatingSystemAsync(model.HeatingSystem);
            realEstateToEdit.PricePerSquareMeter = model.Price / (decimal)model.Area;
            realEstateToEdit.ModifiedOn = DateTime.UtcNow;

            this.mapper.Map<RealEstateEditServiceModel, RealEstate>(model, realEstateToEdit);
            return await UpdateRealEstateInTheDb(realEstateToEdit);
        }

        public async Task<string> GetRealEstateIdByOfferId(string offerId)
        {
            if (string.IsNullOrEmpty(offerId))
            {
                throw new ArgumentNullException(InvalidOfferIdMessage);
            }

            var realEstate = await this.context.RealEstates
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Offer.Id == offerId);

            if (realEstate == null)
            {
                throw new ArgumentNullException(InvalidRealEstateIdMessage);
            }

            return realEstate.Id;     
        }

        private async Task<string> AddRealEstateToTheDb(RealEstate realEstate)
        {
            await this.context.RealEstates.AddAsync(realEstate);
            int affectedRows = await this.context.SaveChangesAsync();

            if (affectedRows == 0)
            {
                throw new InvalidOperationException(UnsuccessfullyCreatedRealEstateMessage);
            }
            return realEstate.Id;
        }

        private async Task<bool> UpdateRealEstateInTheDb(RealEstate realEstateToEdit)
        {
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
    }
}

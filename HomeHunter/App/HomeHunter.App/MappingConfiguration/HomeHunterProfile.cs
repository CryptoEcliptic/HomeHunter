using AutoMapper;
using HomeHunter.Models.BindingModels.RealEstate;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Services.Models.BuildingType;
using HomeHunter.Services.Models.City;
using HomeHunter.Services.Models.HeatingSystem;
using HomeHunter.Services.Models.Neighbourhood;
using HomeHunter.Services.Models.RealEstate;
using HomeHunter.Services.Models.RealEstateType;

namespace HomeHunter.App.MappingConfiguration
{
    public class HomeHunterProfile : Profile
    {
        public HomeHunterProfile()
        {
            this.CreateMap<HeatingSystemServiceModel, HeatingSystemViewModel>();
            this.CreateMap<BuildingTypeServiceModel, BuildingTypeViewModel>();
            this.CreateMap<RealEstateTypeServiceModel, RealEstateTypeViewModel>();
            this.CreateMap<CityServiceModel, CityViewModel>();
            this.CreateMap<NeighbourhoodServiceModel, NeighbourhoodViewModel>();
            this.CreateMap<CreateRealEstateBindingModel, RealEstateCreateServiceModel>();
        }
    }
}

using AutoMapper;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Models.ViewModels.RealEstateType;

namespace HomeHunter.App.MappingConfiguration
{
    public class HomeHunterProfile : Profile
    {
        public HomeHunterProfile()
        {
            this.CreateMap<HeatingSystem, HeatingSystemViewModel>();
            this.CreateMap<BuildingType, BuildingTypeViewModel>();
            this.CreateMap<RealEstateType, RealEstateTypeViewModel>();
            this.CreateMap<City, CityViewModel>();
            this.CreateMap<Neighbourhood, NeighbourhoodViewModel>();
        }
    }
}

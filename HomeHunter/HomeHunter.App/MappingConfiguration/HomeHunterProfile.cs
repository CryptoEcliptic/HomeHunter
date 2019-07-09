using AutoMapper;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        }
    }
}

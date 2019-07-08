using AutoMapper;
using HomeHunter.App.Models.BuildingType;
using HomeHunter.App.Models.HeatingSystem;
using HomeHunter.App.Models.RealEstateType;
using HomeHunter.Domain;

namespace HomeHunter.App.MappingConfiguration
{
    public class HomeHunterProfile : Profile
    {
        public HomeHunterProfile()
        {
            this.CreateMap<HeatingSystem, HeatingSystemViewModel>();
            this.CreateMap<BuildingType, BuildingTypeVewModel>();
            this.CreateMap<RealEstateType, RealEstateTypeVewModel>();
        }
    }
}

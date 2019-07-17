using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Domain;
using HomeHunter.Models.BindingModels.RealEstate;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Models.ViewModels.RealEstate;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Services.Models.BuildingType;
using HomeHunter.Services.Models.City;
using HomeHunter.Services.Models.HeatingSystem;
using HomeHunter.Services.Models.Neighbourhood;
using HomeHunter.Services.Models.RealEstate;
using HomeHunter.Services.Models.RealEstateType;

namespace HomeHunter.Infrastructure
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
            this.CreateMap<RealEstateDetailsServiceModel, RealEstateDetailsViewModel>();

            this.CreateMap<RealEstateIndexServiceModel, RealEstateIndexViewModel>()
               .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)));

            this.CreateMap<RealEstate, RealEstateIndexServiceModel>()
                .ForMember(x => x.RealEstateType, y => y.MapFrom(z => z.RealEstateType.TypeName))
                .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.BuildingType.Name))
                .ForMember(x => x.Village, y => y.MapFrom(z => z.Address.Village.Name))
                .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City.Name))
                .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.Address.Neighbourhood.Name));

            this.CreateMap<RealEstate, RealEstateDetailsServiceModel>()
               .ForMember(x => x.RealEstateType, y => y.MapFrom(z => z.RealEstateType.TypeName))
               .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.BuildingType.Name))
               .ForMember(x => x.Village, y => y.MapFrom(z => z.Address.Village.Name))
               .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City.Name))
               .ForMember(x => x.Address, y => y.MapFrom(z => z.Address.Description))
               .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.Address.Neighbourhood.Name))
               .ForMember(x => x.HeatingSystem, y => y.MapFrom(z => z.HeatingSystem.Name));
        }      
    }
}

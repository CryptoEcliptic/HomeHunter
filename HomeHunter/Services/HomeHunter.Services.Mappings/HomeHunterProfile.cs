﻿using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Models.BindingModels.Home;
using HomeHunter.Models.BindingModels.Image;
using HomeHunter.Models.BindingModels.Offer;
using HomeHunter.Models.BindingModels.RealEstate;
using HomeHunter.Models.BindingModels.User;
using HomeHunter.Models.MLModels;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.Image;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Models.ViewModels.RealEstate;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Models.ViewModels.Statistics;
using HomeHunter.Models.ViewModels.User;
using HomeHunter.Services.Models;
using HomeHunter.Services.Models.BuildingType;
using HomeHunter.Services.Models.City;
using HomeHunter.Services.Models.HeatingSystem;
using HomeHunter.Services.Models.Image;
using HomeHunter.Services.Models.Neighbourhood;
using HomeHunter.Services.Models.Offer;
using HomeHunter.Services.Models.RealEstate;
using HomeHunter.Services.Models.RealEstateType;
using HomeHunter.Services.Models.User;
using System;
using System.Linq;

namespace HomeHunter.Infrastructure
{
    public class HomeHunterProfile : Profile
    {
        private const string SaleNameConst = "Sale";
        private const int StartIndexDateFormat = 0;
        private const int EndIndexYearFormat = 10;
        private const string DeactivatedUserAccountLabel = "деактивиран";
        private const string ActiveUserAccountLabel = "активен";
        private const string ConfirmedUserAccountLabel = "потвърден";
        private const string UnconfirmedUserAccountLabel = "непотвърден";

        public HomeHunterProfile()
        {
            this.CreateMap<HeatingSystemServiceModel, HeatingSystemViewModel>();
            this.CreateMap<BuildingTypeServiceModel, BuildingTypeViewModel>();
            this.CreateMap<RealEstateTypeServiceModel, RealEstateTypeViewModel>();
            this.CreateMap<CityServiceModel, CityViewModel>();
            this.CreateMap<NeighbourhoodServiceModel, NeighbourhoodViewModel>();
            this.CreateMap<PricePredictionBindingModel, ModelInput>();
            this.CreateMap<StatisticsServiceModel, StatisticsViewModel>();

            #region Image Mappings

            this.CreateMap<Image, ImageChangeableServiceModel>();
            this.CreateMap<ImageLoadServiceModel, ImageChangeableBindingModel>();
            this.CreateMap<ImageUploadEditServiceModel, ImageUploadEditBindingModel>();
            this.CreateMap<ImageIndexServiceModel, ImageIndexViewModel>();
            this.CreateMap<Image, ImageIndexServiceModel>();

            #endregion

            #region Offer Mappings

            this.CreateMap<OfferCreateBindingModel, OfferCreateServiceModel>();
            this.CreateMap<OfferPlainDetailsServiceModel, OfferEditBindingModel>();
            this.CreateMap<OfferEditBindingModel, OfferEditServiceModel>();


            this.CreateMap<OfferIndexDeletedServiceModel, OfferIndexViewModel>()
                 .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType == SaleNameConst ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName));

            this.CreateMap<OfferDetailsServiceModel, OfferDetailsViewModel>()
                .ForMember(x => x.ParkingPlace, y => y.MapFrom(z => z.ParkingPlace == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Celling, y => y.MapFrom(z => z.Celling == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Yard, y => y.MapFrom(z => z.Yard == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Basement, y => y.MapFrom(z => z.Basement == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Elevator, y => y.MapFrom(z => z.Elevator == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Furnitures, y => y.MapFrom(z => z.Furnitures == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Garage, y => y.MapFrom(z => z.Garage == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                ;
               

            this.CreateMap<OfferDetailsServiceModel, OfferDetailsDeletedViewModel>()
                .ForMember(x => x.ParkingPlace, y => y.MapFrom(z => z.ParkingPlace == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Celling, y => y.MapFrom(z => z.Celling == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Yard, y => y.MapFrom(z => z.Yard == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Basement, y => y.MapFrom(z => z.Basement == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Elevator, y => y.MapFrom(z => z.Elevator == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Furnitures, y => y.MapFrom(z => z.Furnitures == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Garage, y => y.MapFrom(z => z.Garage == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                ;

            this.CreateMap<OfferDetailsServiceModel, OfferDetailsGuestViewModel>()
                  .ForMember(x => x.ParkingPlace, y => y.MapFrom(z => z.ParkingPlace == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Celling, y => y.MapFrom(z => z.Celling == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Yard, y => y.MapFrom(z => z.Yard == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Basement, y => y.MapFrom(z => z.Basement == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Elevator, y => y.MapFrom(z => z.Elevator == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Furnitures, y => y.MapFrom(z => z.Furnitures == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                .ForMember(x => x.Garage, y => y.MapFrom(z => z.Garage == true ? GlobalConstants.BoolTrueStringValue : GlobalConstants.BoolFalseStringValue))
                ;

            this.CreateMap<OfferIndexServiceModel, OfferIndexGuestViewModel>()
                .ForMember(x => x.Images, y => y.MapFrom(z => z.Images))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.Substring(StartIndexDateFormat, EndIndexYearFormat)))
                ;

            this.CreateMap<OfferIndexServiceModel, OfferIndexViewModel>()
                .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType == SaleNameConst ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName));

            this.CreateMap<Offer, OfferIndexServiceModel>()
               .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType.ToString()))
               .ForMember(x => x.Area, y => y.MapFrom(z => z.RealEstate.Area))
               .ForMember(x => x.FloorNumber, y => y.MapFrom(z => z.RealEstate.FloorNumber))
               .ForMember(x => x.BuildingTotalFloors, y => y.MapFrom(z => z.RealEstate.BuildingTotalFloors))
               .ForMember(x => x.Images, y => y.MapFrom(z => z.RealEstate.Images))
               .ForMember(x => x.Author, y => y.MapFrom(z => z.Author.FirstName))
               .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.RealEstate.BuildingType.Name))
               .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.ModifiedOn, y => y.MapFrom(z => z.ModifiedOn == null ? GlobalConstants.NotAvailableMessage : z.ModifiedOn.Value.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.RealEstateType, y => y.MapFrom(z => z.RealEstate.RealEstateType.TypeName))
               .ForMember(x => x.City, y => y.MapFrom(z => z.RealEstate.Address.City.Name))
               .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.RealEstate.Address.Neighbourhood.Name))
               .ForMember(x => x.Price, y => y.MapFrom(z => z.RealEstate.Price))
              ;

            this.CreateMap<Offer, OfferDetailsServiceModel>()
                .ForMember(x => x.City, y => y.MapFrom(z => z.RealEstate.Address.City.Name))
                .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.RealEstate.Address.Neighbourhood.Name))
                .ForMember(x => x.Village, y => y.MapFrom(z => z.RealEstate.Address.Village.Name == null ? GlobalConstants.NotAvailableMessage : z.RealEstate.Address.Village.Name))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.RealEstate.Address.Description))
                .ForMember(x => x.RealEstateType, y => y.MapFrom(z => z.RealEstate.RealEstateType.TypeName))
                .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.RealEstate.BuildingType.Name))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.RealEstate.Price))
                .ForMember(x => x.PricePerSquareMeter, y => y.MapFrom(z => z.RealEstate.PricePerSquareMeter))
                .ForMember(x => x.Area, y => y.MapFrom(z => z.RealEstate.Area))
                .ForMember(x => x.Year, y => y.MapFrom(z => z.RealEstate.Year))
                .ForMember(x => x.FloorNumber, y => y.MapFrom(z => z.RealEstate.FloorNumber))
                .ForMember(x => x.BuildingTotalFloors, y => y.MapFrom(z => z.RealEstate.BuildingTotalFloors))
                .ForMember(x => x.HeatingSystem, y => y.MapFrom(z => z.RealEstate.HeatingSystem.Name))
                .ForMember(x => x.ParkingPlace, y => y.MapFrom(z => z.RealEstate.ParkingPlace))
                .ForMember(x => x.Yard, y => y.MapFrom(z => z.RealEstate.Yard))
                .ForMember(x => x.Basement, y => y.MapFrom(z => z.RealEstate.Basement))
                .ForMember(x => x.Elevator, y => y.MapFrom(z => z.RealEstate.Elevator))
                .ForMember(x => x.Garage, y => y.MapFrom(z => z.RealEstate.Garage))
                .ForMember(x => x.Celling, y => y.MapFrom(z => z.RealEstate.Celling))
                .ForMember(x => x.Furnitures, y => y.MapFrom(z => z.RealEstate.Furnitures))
                .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType == OfferType.Sale ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                 .ForMember(x => x.DeletedOn, y => y.MapFrom(z => z.DeletedOn == null ? GlobalConstants.NotAvailableMessage : z.DeletedOn.Value.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(z => z.ModifiedOn == null ? GlobalConstants.NotAvailableMessage : z.ModifiedOn.Value.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                .ForMember(x => x.Images, y => y.MapFrom(z => z.RealEstate.Images.Select(u => u.Url)))
                .ForMember(x => x.Author, y => y.MapFrom(z => z.Author.FirstName));

            this.CreateMap<Offer, OfferIndexDeletedServiceModel>()
               .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType.ToString()))
               .ForMember(x => x.Author, y => y.MapFrom(z => z.Author.FirstName))
               .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.DeletedOn, y => y.MapFrom(z => z.DeletedOn.Value.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.RealEstateType, y => y.MapFrom(z => z.RealEstate.RealEstateType.TypeName))
               .ForMember(x => x.City, y => y.MapFrom(z => z.RealEstate.Address.City.Name))
               .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.RealEstate.Address.Neighbourhood.Name))
               .ForMember(x => x.Price, y => y.MapFrom(z => z.RealEstate.Price));

    
            this.CreateMap<Offer, OfferPlainDetailsServiceModel>()
               .ForMember(x => x.OfferType, y => y.MapFrom(z => z.OfferType == OfferType.Sale ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName));

            this.CreateMap<OfferEditServiceModel, Offer>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.OfferType, y => y.Ignore());

            this.CreateMap<OfferCreateServiceModel, Offer>()
               .ForMember(x => x.OfferType, y => y.Ignore())
               .ForMember(x => x.Author, y => y.Ignore())
               .ForMember(x => x.ReferenceNumber, y => y.Ignore())
               .ForMember(x => x.RealEstate, y => y.Ignore())
               .ForMember(x => x.RealEstateId, y => y.Ignore())
               .ForMember(x => x.Author, y => y.Ignore())
               .ForMember(x => x.AuthorId, y => y.Ignore())
               ;

            #endregion

            #region RealEstate Mappings
            this.CreateMap<CreateRealEstateBindingModel, RealEstateCreateServiceModel>();
            this.CreateMap<RealEstateEditBindingModel, RealEstateEditServiceModel>();
            this.CreateMap<RealEstateDetailsServiceModel, RealEstateEditBindingModel>();

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
               .ForMember(x => x.ModifiedOn, y => y.MapFrom(z => z.ModifiedOn == null ? GlobalConstants.NotAvailableMessage : z.ModifiedOn.Value.ToString(GlobalConstants.DateTimeVisualizationFormat)))
               .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.BuildingType.Name))
               .ForMember(x => x.Village, y => y.MapFrom(z => z.Address.Village.Name))
               .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City.Name))
               .ForMember(x => x.Address, y => y.MapFrom(z => z.Address.Description))
               .ForMember(x => x.Neighbourhood, y => y.MapFrom(z => z.Address.Neighbourhood.Name))
               .ForMember(x => x.HeatingSystem, y => y.MapFrom(z => z.HeatingSystem.Name))
               .ForMember(x => x.Year, y => y.MapFrom(z => z.Year));


            this.CreateMap<RealEstateEditServiceModel, RealEstate>()
                .ForMember(x => x.Address, y => y.Ignore())
                .ForMember(x => x.BuildingType, y => y.Ignore())
                .ForMember(x => x.RealEstateType, y => y.Ignore())
                .ForMember(x => x.HeatingSystem, y => y.Ignore())
                ;

            this.CreateMap<RealEstateDetailsServiceModel, RealEstateDetailsViewModel>()
                .ForMember(x => x.Year, y => y.MapFrom(z => z.Year.ToString() == "0" ? GlobalConstants.NotAvailableMessage : z.Year.ToString()));

            this.CreateMap<RealEstateCreateServiceModel, RealEstate>()
                .ForMember(x => x.Address, y => y.Ignore())
                .ForMember(x => x.BuildingType, y => y.Ignore())
                .ForMember(x => x.RealEstateType, y => y.Ignore())
                .ForMember(x => x.HeatingSystem, y => y.Ignore())
                .ForMember(x => x.PricePerSquareMeter, y => y.Ignore());

            #endregion

            #region User Mappings

            this.CreateMap<UserCreateBindingModel, UserCreateServiceModel>();
            this.CreateMap<UserDetailsServiceModel, UserDetailsViewModel>();
            this.CreateMap<UserIndexServiceModel, UserIndexViewModel>();

            this.CreateMap<UserCreateServiceModel, HomeHunterUser>()
               .ForMember(x => x.UserName, y => y.MapFrom(z => z.Email));

            this.CreateMap<HomeHunterUser, UserIndexServiceModel>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                .ForMember(x => x.LastLogin, y => y.MapFrom(z => z.LastLogin == DateTime.Parse(GlobalConstants.DefaultDateTimeDbValue) ? GlobalConstants.NotAvailableMessage : z.LastLogin.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsDeleted == true ? DeactivatedUserAccountLabel : ActiveUserAccountLabel))
                .ForMember(x => x.EmailConfirmed, y => y.MapFrom(z => z.EmailConfirmed == true ? ConfirmedUserAccountLabel : UnconfirmedUserAccountLabel));
           
            this.CreateMap<HomeHunterUser, UserDetailsServiceModel>()
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsDeleted == true ? DeactivatedUserAccountLabel : ActiveUserAccountLabel))
                .ForMember(x => x.EmailConfirmed, y => y.MapFrom(z => z.EmailConfirmed == true ? ConfirmedUserAccountLabel : UnconfirmedUserAccountLabel))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOn.ToString(GlobalConstants.DateTimeVisualizationFormat)))
                .ForMember(x => x.LastLogin, y => y.MapFrom(z => z.LastLogin == DateTime.Parse(GlobalConstants.DefaultDateTimeDbValue) ? GlobalConstants.NotAvailableMessage : z.LastLogin.ToString(GlobalConstants.DateTimeVisualizationFormat)));

            #endregion
        }
    }
}

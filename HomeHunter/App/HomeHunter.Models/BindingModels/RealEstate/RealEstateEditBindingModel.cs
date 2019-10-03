using HomeHunter.Common;
using HomeHunter.Infrastructure.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Models.BindingModels.RealEstate
{
    public class RealEstateEditBindingModel
    {
        private const string VillageNameRequirementsErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.";
        private const string FieldIsRequiredErrorMessage = "Полето {0} е задължително";
        private const string AddressRequirementsErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.";
        private const string PriceRequirementsErrorMessage = "Цената не трябва да бъде по-малка от {1}.";
        private const string AreaRequirementsErrorMessage = "Площта не трябва да бъде по-малка от {1} и да надвишава {2}!";
        private const string BuildingFloorsRequirementsErrorMessage = "Броят на етажите не трябва да бъде по-малък от {1}.";
        private const string FloorRequirementsErrorMessage = "Полето {0} не трябва да надвишава {1} символа.";

        public string Id { get; set; }

        [Display(Name = "Етаж")]
        [StringLength(7, ErrorMessage = FloorRequirementsErrorMessage)]
        public string FloorNumber { get; set; }


        [Display(Name = "Брой етажи")]
        [Range(1, 50, ErrorMessage = BuildingFloorsRequirementsErrorMessage)]
        public int? BuildingTotalFloors { get; set; }

        [Display(Name = "Площ *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [Range(1, 1000000, ErrorMessage = AreaRequirementsErrorMessage)]
        public double Area { get; set; }

        [Display(Name = "Цена *")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [Range(1, 9000000, ErrorMessage = PriceRequirementsErrorMessage)]
        public decimal Price { get; set; }

        [Display(Name = "Година")]
        [BeforeCurrentYear(1900)]
        public int? Year { get; set; }

        [Display(Name = "Паркомясто")]
        public bool? ParkingPlace { get; set; }

        [Display(Name = "Гараж")]
        public bool? Garage { get; set; }

        [Display(Name = "Двор")]
        public bool? Yard { get; set; }

        [Display(Name = "Обзаведен")]
        public bool? Furnitures { get; set; }

        [Display(Name = "Асансьор")]
        public bool? Elevator { get; set; }

        [Display(Name = "Таван")]
        public bool? Celling { get; set; }

        [Display(Name = "Мазе")]
        public bool? Basement { get; set; }


        [Display(Name = "Адрес/Местоположение")]
        [StringLength(256, ErrorMessage = AddressRequirementsErrorMessage, MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "Вид отопление")]
        [MaxLength(32)]
        public string HeatingSystem { get; set; }

        [Display(Name = "Тип на имота *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        public string RealEstateType { get; set; }

        [Display(Name = "Тип строителство")]
        [MaxLength(32)]
        public string BuildingType { get; set; }

        [Display(Name = "Град")]
        [MaxLength(32)]
        public string City { get; set; }

        [Display(Name = "Село")]
        [StringLength(32, ErrorMessage = VillageNameRequirementsErrorMessage, MinimumLength = 3)]
        public string Village { get; set; }

        [Display(Name = "Квартал")]
        [MaxLength(64)]
        public string Neighbourhood { get; set; }

    }
}

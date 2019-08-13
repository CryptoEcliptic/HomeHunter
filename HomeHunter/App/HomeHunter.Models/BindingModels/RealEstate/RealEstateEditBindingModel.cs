using HomeHunter.Common;
using HomeHunter.Models.CustomValidationAttributse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Models.BindingModels.RealEstate
{
    public class RealEstateEditBindingModel
    {
        public string Id { get; set; }

        [Display(Name = "Етаж")]
        [StringLength(7, ErrorMessage = "Полето {0} не трябва да надвишава {1} символа.")]
        public string FloorNumber { get; set; }


        [Display(Name = "Брой етажи")]
        [Range(1, 50, ErrorMessage = "Броят на етажите не трябва да бъде по-малък от {1}.")]
        public int? BuildingTotalFloors { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Площ*")]
        [Range(1, 1000000, ErrorMessage = "Площта не трябва да бъде по-малка от {1} и да надвишава {2}!")]
        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Полето Цена е задължително")]
        [Display(Name = "Цена*")]
        [Range(1, 9000000, ErrorMessage = "Цената не трябва да бъде по-малка от {1}.")]
        public decimal Price { get; set; }

        [Display(Name = "Година")]
        [BeforeCurrentYear(1900)]
        public int? Year { get; set; }

        [Display(Name = "Паркомясто/Гараж")]
        public bool ParkingPlace { get; set; }

        [Display(Name = "Двор")]
        public bool Yard { get; set; }

        [Display(Name = "Достъп до метро")]
        public bool MetroNearBy { get; set; }

        [Display(Name = "Тераса")]
        public bool Balcony { get; set; }

        [Display(Name = "Мазе/Таван")]
        public bool CellingOrBasement { get; set; }


        [Display(Name = "Адрес/Местоположение*")]
        [Required(ErrorMessage = "Полето Адрес/Местоположение е задължително")]
        [StringLength(256, ErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
        public string Address { get; set; }

        [MaxLength(32)]
        [Display(Name = "Вид отопление")]
        public string HeatingSystem { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Тип на имота *")]
        public string RealEstateType { get; set; }

        [MaxLength(32)]
        [Display(Name = "Тип строителство")]
        public string BuildingType { get; set; }

        [MaxLength(32)]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Село")]
        [StringLength(32, ErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
        public string Village { get; set; }

        [MaxLength(64)]
        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

    }
}

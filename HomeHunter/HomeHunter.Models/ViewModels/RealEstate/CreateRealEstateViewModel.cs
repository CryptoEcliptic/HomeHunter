using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.RealEstateType;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Models.ViewModels.RealEstate
{
    public class CreateRealEstateViewModel
    {
        [Display(Name = "Етаж")]
        [StringLength(10, ErrorMessage = "Полето {0} не трябва да надвишава {1} символа.")]
        public string FloorNumber { get; set; }

        [Display(Name = "Брой етажи")]
        [Range(1, 50, ErrorMessage = "Броят на етажите не трябва да бъде по-малък от {1}.")]
        public int? BuildingTotalFloors { get; set; }

        [Required(ErrorMessage = "Полето Площ е задължително")]
        [Display(Name = "Площ*")]
        [Range(1, 1000000, ErrorMessage = "Площта не трябва да бъде по-малка от {1}.")]
        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Полето Цена е задължително")]
        [Display(Name = "Цена*")]
        [Range(1, 100000000, ErrorMessage = "Цената не трябва да бъде по-малка от {1}.")]
        public decimal Price { get; set; }

        [Display(Name = "Година")]
        [Range(1800, 2030)]
        public int Year { get; set; }

        [Display(Name = "Паркомясто/Гараж")]
        public bool? ParkingPlace { get; set; }

        [Display(Name = "Двор")]
        public bool? Yard { get; set; }

        [Display(Name = "Достъп до метро")]
        public bool? MetroNearBy { get; set; }

        [Display(Name = "Тераса")]
        public bool? Balcony { get; set; }

        [Display(Name = "Мазе/Таван")]
        public bool? CellingOrBasement { get; set; }

        [Required(ErrorMessage = "Полето Адрес/Местоположение е задължително")]
        [Display(Name = "Адрес/Местоположение*")]
        public string Address { get; set; }


        [Display(Name = "Отопление")]
        public IList<HeatingSystemViewModel> HeatingSystems { get; set; }


        [Required(ErrorMessage = "Полето Вид на имота е задължително")]
        [Display(Name = "Вид на имота * ")]
        public IList<RealEstateTypeViewModel> RealEstateTypes { get; set; }


        [Display(Name = "Тип на сградата")]
        public IList<BuildingTypeViewModel> BuildingTypes { get; set; }


        [Display(Name = "Град")]
        public IList<CityViewModel> Cities { get; set; }
    }
}

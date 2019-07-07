using HomeHunter.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.App.Models.RealEstates
{
    public class CreateBindingModel
    {
        [Display(Name = "Етаж")]
        [StringLength(10, ErrorMessage = "Полето {0} не трябва да надвишава {1} символа.")]
        public string FloorNumber { get; set; }

        [Display(Name = "Брой етажи")]
        [Range(1, 50, ErrorMessage = "Броят на етажите не трябва да бъде по-малък от {1}.")]
        public int? BuildingTotalFloors { get; set; }

        [Required(ErrorMessage = "Полето Площ е задължително")]
        [Display(Name = "Площ")]
        [Range(1, 1000000, ErrorMessage = "Площта не трябва да бъде по-малка от {1}.")]
        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Полето Цена е задължително")]
        [Display(Name = "Цена")]
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
        [Display(Name = "Адрес/Местоположение")]
        public string Address { get; set; }

        [Display(Name = "Отопление")]
        public int? HeatingSystemId { get; set; }
        public HeatingSystem HeatingSystem { get; set; }

        [Required(ErrorMessage = "Полето Вид на имота е задължително")]
        [Display(Name = "Вид на имота")]
        public int RealEstateTypeId { get; set; }
        public RealEstateType RealEstateType { get; set; }

        [Display(Name = "Тип на сградата")]
        public int? BuildingTypeId { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}

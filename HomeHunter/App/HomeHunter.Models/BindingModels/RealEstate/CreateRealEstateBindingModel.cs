using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Models.BindingModels.RealEstate
{
    public class CreateRealEstateBindingModel
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

        [Required(ErrorMessage = "Полето Адрес/Местоположение е задължително")]
        [Display(Name = "Адрес/Местоположение*")]
        [MinLength(5, ErrorMessage = "Полето Адрес/Местоположение трябва да съдържа поне 5 символа")]
        public string Address { get; set; }

        [MaxLength(32)]
        public string  HeatingSystem { get; set; }


        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [MaxLength(32)]
        public string BuildingType { get; set; }

        [MaxLength(32)]
        public string City { get; set; }

        [MaxLength(32)]
        public string Village { get; set; } //Todo Add village field in the view

        [MaxLength(64)]
        public string Neighbourhood { get; set; }
    }
}

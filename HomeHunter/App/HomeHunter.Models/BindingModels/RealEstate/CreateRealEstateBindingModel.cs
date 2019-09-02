using HomeHunter.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHunter.Models.BindingModels.RealEstate
{
    public class CreateRealEstateBindingModel
    {
        private const string RealEstateAreaRequirementMessage = "Площта не трябва да бъде по-малка от {1} и да надвишава {2}!";

        private const string FieldIsRequiredMessage = "Полето {0} е задължително";
        private const string PriceFieldRequirementMessage = "Цената не трябва да бъде по-малка от {1}.";

        private const string AddressIsRequiredErrorMessage = "Полето Адрес/Местоположение е задължително";
        private const string FieldLengthRequirementMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.";
        private const string FloorDataRequirementsMessage = "Полето {0} не трябва да надвишава {1} символа.";

        private const string TotalFloorsRequirementMessage = "Броят на етажите не трябва да бъде по-малък от {1} и да надвишава {2}.";



        [Display(Name = "Етаж")]
        [StringLength(7, ErrorMessage = FloorDataRequirementsMessage)]
        public string FloorNumber { get; set; }


        [Display(Name = "Брой етажи")]
        [Range(1, 50, ErrorMessage = TotalFloorsRequirementMessage)]
        public int? BuildingTotalFloors { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Площ *")]
        [Range(1, 1000000, ErrorMessage = RealEstateAreaRequirementMessage)]
        public double Area { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Цена *")]
        [Range(1, 9000000, ErrorMessage = PriceFieldRequirementMessage)]
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


        [Display(Name = "Адрес/Местоположение *")]
        [Required(ErrorMessage = AddressIsRequiredErrorMessage)]
        [StringLength(256, ErrorMessage = FieldLengthRequirementMessage, MinimumLength = 3)]
        public string Address { get; set; }

        [MaxLength(32)]
        [Display(Name = "Вид отопление")]
        public string  HeatingSystem { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Тип на имота *")]
        public string RealEstateType { get; set; }

        [MaxLength(32)]
        [Display(Name = "Тип строителство")]
        public string BuildingType { get; set; }

        [MaxLength(32)]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Село")]
        [StringLength(32, ErrorMessage = FieldLengthRequirementMessage, MinimumLength = 3)]
        public string Village { get; set; }

        [MaxLength(64)]
        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }
    }
}

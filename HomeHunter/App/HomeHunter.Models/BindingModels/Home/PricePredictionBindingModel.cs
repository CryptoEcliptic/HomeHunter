using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Home
{
    public class PricePredictionBindingModel
    {
        private const string FieldIsRequiredMessage = "Полето {0} е задължително";

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Площ")]
        public float Size { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Етаж")]
        public float Floor { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Брой етажи")]
        public float TotalFloors { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Квартал")]
        public string District { get; set; }

        [Display(Name = "Година")]
        public float? Year { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Вид имот")]
        public string Type { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Тип сграда")]
        public string BuildingType { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "ТЕЦ")]
        public string CentralHeating { get; set; }

        public float Price { get; set; }

    }
}

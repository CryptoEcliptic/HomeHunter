using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Home
{
    public class PricePredictionBindingModel
    {
        [Required]
        [Display(Name = "Площ")]
        public float Size { get; set; }

        [Required]
        [Display(Name = "Етаж")]
        public float Floor { get; set; }

        [Required]
        [Display(Name = "Брой етажи")]
        public float TotalFloors { get; set; }

        [Required]
        [Display(Name = "Квартал")]
        public string District { get; set; }

        [Display(Name = "Година")]
        public float Year { get; set; }

        [Required]
        [Display(Name = "Вид имот")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Тип сграда")]
        public string BuildingType { get; set; }

    }
}

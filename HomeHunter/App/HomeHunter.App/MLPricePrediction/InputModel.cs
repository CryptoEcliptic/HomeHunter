using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.App.MLPricePrediction
{
    public class InputModel
    {
        private const string FieldIsRequiredMessage = "Полето {0} е задължително";

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Площ")]
        [ColumnName("Size"), LoadColumn(0)]
        public float Size { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Етаж")]
        [ColumnName("Floor"), LoadColumn(1)]
        public float Floor { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Брой етажи")]
        [ColumnName("TotalFloors"), LoadColumn(2)]
        public float TotalFloors { get; set; }


        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "ТЕЦ")]
        [ColumnName("CentralHeating"), LoadColumn(3)]
        public string CentralHeating { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Квартал")]
        [ColumnName("District"), LoadColumn(4)]
        public string District { get; set; }

        [Display(Name = "Година")]
        [ColumnName("Year"), LoadColumn(5)]
        public float Year { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Вид имот")]
        [ColumnName("Type"), LoadColumn(6)]
        public string Type { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Тип сграда")]
        [ColumnName("BuildingType"), LoadColumn(7)]
        public string BuildingType { get; set; }


        [ColumnName("Price"), LoadColumn(8)]
        public float Price { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.RealEstate
{
    public class RealEstateIndexViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [Display(Name = "Тип на сградата")]
        public string BuildingType { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Село")]
        public string Village { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Добавен")]
        public string CreatedOn { get; set; }

        [Display(Name = "Последна промяна")]
        public DateTime? ModifiedOn { get; set; }
    }
}

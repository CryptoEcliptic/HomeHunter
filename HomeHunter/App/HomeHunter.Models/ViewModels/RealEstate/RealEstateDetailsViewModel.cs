using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.RealEstate
{
    public class RealEstateDetailsViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [Display(Name = "Тип строителство")]
        public string BuildingType { get; set; }

        [Display(Name = "Площ")]
        public double Area { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Село")]
        public string Village { get; set; }

        [Display(Name = "Етаж")]
        public string FloorNumber { get; set; }

        [Display(Name = "Брой етажи")]
        public int? BuildingTotalFloors { get; set; }

        [Display(Name = "Година")]
        public string Year { get; set; }

        [Display(Name = "Паркомясто")]
        public bool ParkingPlace { get; set; }

        [Display(Name = "Двор")]
        public bool Yard { get; set; }

        [Display(Name = "Достъп до метро")]
        public bool MetroNearBy { get; set; }

        [Display(Name = "Тераса")]
        public bool Balcony { get; set; }

        [Display(Name = "Мазе/Таванско помещение")]
        public bool CellingOrBasement { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Отопление")]
        public string HeatingSystem { get; set; }

        [Display(Name = "Дата на добавяне")]
        public string CreatedOn { get; set; }

        [Display(Name = "Последна промяна")]
        public string ModifiedOn { get; set; }

        public List<string> Images { get; set; }
    }
}

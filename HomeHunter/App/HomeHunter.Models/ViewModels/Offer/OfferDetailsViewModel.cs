using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeHunter.Models.ViewModels.Offer
{
    public class OfferDetailsViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Референтен номер")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Село")]
        public string Village { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Тип имот")]
        public string RealEstateType { get; set; }

        [Display(Name = "Тип сграда")]
        public string BuildingType { get; set; }

        [Display(Name = "Площ")]
        public double Area { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Година")]
        public int Year { get; set; }

        [Display(Name = "Етаж")]
        public string FloorNumber { get; set; }

        [Display(Name = "Брой етажи")]
        public int? BuildingTotalFloors { get; set; }

        [Display(Name = "Тип отопление")]
        public string HeatingSystem { get; set; }

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

        [Display(Name = "Дата на публикуване")]
        public string CreatedOn { get; set; }

        [Display(Name = "Дата на последна промята")]
        public string ModifiedOn { get; set; }

        [Display(Name = "Тип на обявата")]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        public string ContactNumber { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Цена на квадратен метър")]
        public decimal PricePerSquareMeter { get; set; }

        [Display(Name = "Снимки")]
        public List<string> Images { get; set; }
    }
}

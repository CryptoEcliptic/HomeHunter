using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeHunter.Models.ViewModels.Offer
{
    public class OfferIndexDeletedViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Референтен номер")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Тип на обявата")]
        public string OfferType { get; set; }

        [Display(Name = "Публикувана")]
        public string CreatedOn { get; set; }

        [Display(Name = "Свалена")]
        public string DeletedOn { get; set; }

        [Display(Name = "Тип на имота")]
        public string RealEstateType { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Квартал")]
        public string Neighbourhood { get; set; }

        [Display(Name = "Създал")]
        public string Author { get; set; }
    }
}

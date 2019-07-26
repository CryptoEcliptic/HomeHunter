using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeHunter.Services.Models.Offer
{
    public class OfferEditServiceModel
    {
        [Display(Name = "Тип на обявата *")]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителни коментари")]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        public string ContactNumber { get; set; }
    }
}

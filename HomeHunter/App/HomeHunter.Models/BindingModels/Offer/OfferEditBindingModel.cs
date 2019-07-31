using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Offer
{
    public class OfferEditBindingModel
    {
        public string Id { get; set; }

        [Display(Name = "Тип на обявата *")]
        [Required]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        public string ContactNumber { get; set; }
    }
}

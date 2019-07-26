using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Offer
{
    public class OfferEditBindingModel
    {
        [Display(Name = "Тип на обявата *")]
        [Required]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителни коментари")]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        public string ContactNumber { get; set; }
    }
}

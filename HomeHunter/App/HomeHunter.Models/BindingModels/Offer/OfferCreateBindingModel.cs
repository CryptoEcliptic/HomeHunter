using HomeHunter.Common;
using System.ComponentModel.DataAnnotations;
namespace HomeHunter.Models.BindingModels.Offer
{
    public class OfferCreateBindingModel
    {
        [Display(Name = "Тип на обявата *")]
        [Required(ErrorMessage = "Полето {0} e задължително!")]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителни коментари")]
        [MaxLength(2000, ErrorMessage = "Полето \"{0}\" не може да бъде повече от {1} символа")]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(GlobalConstants.PhoneValidationRegex, ErrorMessage = "Моля, въведете валиден {0}!")]
        public string ContactNumber { get; set; }

    }
}

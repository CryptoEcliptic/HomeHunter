using HomeHunter.Common;
using System.ComponentModel.DataAnnotations;
namespace HomeHunter.Models.BindingModels.Offer
{
    public class OfferCreateBindingModel
    {
        private const string FieldLengthRequirementMessage = "Полето \"{0}\" не може да бъде повече от {1} символа";
        private const string ValidPhoneNumberErrorMessage = "Моля, въведете валиден {0}!";

        [Display(Name = "Тип на обявата *")]
        [Required(ErrorMessage = GlobalConstants.RequiredFieldMessage)]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителни коментари")]
        [MaxLength(2000, ErrorMessage = FieldLengthRequirementMessage)]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(GlobalConstants.PhoneValidationRegex, ErrorMessage = ValidPhoneNumberErrorMessage)]
        public string ContactNumber { get; set; }

    }
}

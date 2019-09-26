using HomeHunter.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Offer
{
    public class OfferEditBindingModel
    {
        private const string FieldLengthRequirementMessage = "Полето \"{0}\" не може да бъде повече от {1} символа";
        private const string ValidPhoneNumberErrorMessage = "Моля, въведете валиден {0}!";

        private const string FieldIsRequiredMessage = "Полето {0} е задължително";
        public string Id { get; set; }

        [Display(Name = "Тип на обявата *")]
        [Required(ErrorMessage = FieldIsRequiredMessage)]
        public string OfferType { get; set; }

        [Display(Name = "Допълнителна информация")]
        [MaxLength(2000, ErrorMessage = FieldLengthRequirementMessage)]
        public string Comments { get; set; }

        [Display(Name = "Телефон за контакт")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(GlobalConstants.PhoneValidationRegex, ErrorMessage = ValidPhoneNumberErrorMessage)]
        public string ContactNumber { get; set; }

        [Display(Name = "Водещ брокер")]
        [MaxLength(64, ErrorMessage = FieldLengthRequirementMessage)]
        public string AgentName { get; set; }

        [Display(Name = "Служебна информация")]
        [MaxLength(1024, ErrorMessage = FieldLengthRequirementMessage)]
        public string OfferServiceInformation { get; set; }
    }
}

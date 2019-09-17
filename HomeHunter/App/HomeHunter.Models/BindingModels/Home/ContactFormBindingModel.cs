using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Home
{
    public class ContactFormBindingModel
    {
        private const string MessageLengthRequirementMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.";

        private const string FieldIsRequiredMessage = "Полето {0} е задължително";

        private const string EmailErrorMessage = "Моля, въведете валиден Email!";

        public string OfferId { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [EmailAddress(ErrorMessage = EmailErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        [Display(Name = "Съобщение")]
        [StringLength(2500, ErrorMessage = MessageLengthRequirementMessage, MinimumLength = 16)]
        public string Message { get; set; }

        [Required(ErrorMessage = FieldIsRequiredMessage)]
        public string Name { get; set; }

        public string ReferenceNumber { get; set; }
    }
}

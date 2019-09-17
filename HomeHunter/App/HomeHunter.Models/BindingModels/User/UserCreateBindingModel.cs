using HomeHunter.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.User
{
    public class UserCreateBindingModel
    {
        private const string FieldIsRequiredErrorMessage = "Полето {0} e задължително!";
        private const string ValidEmailErrorMessage = "Моля, въведете валиден {0}!";
        private const string EmailLengthRequirementsErrorMessage = "{0} не трябва да надвишава {1} символа.";
        private const string PhoneNumberErrorMessage = "Моля, въведете валиден {0}!";
        private const string NamesLengthRequirementsErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.";
        private const string PasswordRequirementsErrorMessage = "Паролатата трябва да бъде от поне {2} и да не надвишава {1} символа.";
        private const string ConfirmPasswordErrorMessage = "Данните в полета \"Парола\" и \"Потвърдете паролата\" трябва да съвпадат.";

        [Display(Name = "Email *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [EmailAddress(ErrorMessage = ValidEmailErrorMessage)]
        [StringLength(40, ErrorMessage = EmailLengthRequirementsErrorMessage)]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        [Phone]
        [RegularExpression(GlobalConstants.PhoneValidationRegex, ErrorMessage = PhoneNumberErrorMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Име *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [StringLength(16, ErrorMessage = NamesLengthRequirementsErrorMessage, MinimumLength = 3)] 
        public string FirstName { get; set; }

        [Display(Name = "Фамилия *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [StringLength(16, ErrorMessage = NamesLengthRequirementsErrorMessage, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Парола *")]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [StringLength(50, ErrorMessage = PasswordRequirementsErrorMessage, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Потвърдете паролата *")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = FieldIsRequiredErrorMessage)]
        [Compare("Password", ErrorMessage = ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}

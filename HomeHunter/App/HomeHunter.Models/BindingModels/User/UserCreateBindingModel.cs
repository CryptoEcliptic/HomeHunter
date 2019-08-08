using HomeHunter.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.User
{
    public class UserCreateBindingModel
    {
        [Required(ErrorMessage ="Полето {0} e задължително!")]
        [EmailAddress(ErrorMessage ="Моля, въведете валиден {0}!")]
        [Display(Name = "Email*")]
        [StringLength(40, ErrorMessage = "{0} не трябва да надвишава {1} символа.")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Телефон")]
        [RegularExpression(GlobalConstants.PhoneValidationRegex, ErrorMessage = "Моля, въведете валиден {0}!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително!")]
        [StringLength(16, ErrorMessage = "Полето \"Име\" трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
        [Display(Name = "Име*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително!")]
        [StringLength(16, ErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
        [Display(Name = "Фамилия*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето {0} e задължително!")]
        [StringLength(50, ErrorMessage = "Паролатата трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола*")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърдете паролата*")]
        [Required]
        [Compare("Password", ErrorMessage = "Данните в полета \"Парола\" и \"Потвърдете паролата\" трябва да съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}

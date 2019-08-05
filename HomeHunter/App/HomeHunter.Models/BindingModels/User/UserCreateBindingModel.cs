using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.User
{
    public class UserCreateBindingModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        [StringLength(40, ErrorMessage = "{0} не трябва да надвишава {1} символа.")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Телефон")]
        [StringLength(16, ErrorMessage = "{0}ът не трябва да надвишава {1} символа.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Полето \"Име\" трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
        [Display(Name = "Име*")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Фамилията не трябва да надвишава {1} символа.")]
        [Display(Name = "Фамилия*")]
        public string LastName { get; set; }

        [Required]
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

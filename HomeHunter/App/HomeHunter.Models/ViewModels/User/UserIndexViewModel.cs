using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.User
{
    public class UserIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Профил")]
        public string IsActive { get; set; }

        [Display(Name = "Дата на създаване")]
        public string CreatedOn { get; set; }

        [Display(Name = "Потвърден Email")]
        public string EmailConfirmed { get; set; }

        [Display(Name = "Последно влизане")]
        public string LastLogin { get; set; }
    }
}

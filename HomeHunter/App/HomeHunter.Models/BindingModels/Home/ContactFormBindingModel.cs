using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Home
{
    public class ContactFormBindingModel
    {
        public string OfferId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Съобщение")]
        [StringLength(2408, ErrorMessage = "Полето {0} трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 16)]
        public string Message { get; set; }

        public string Name { get; set; }

        public string ReferenceNumber { get; set; }
    }
}

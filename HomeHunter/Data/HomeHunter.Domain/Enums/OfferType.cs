using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Domain.Enums
{
    public enum OfferType
    {
        [Display(Name = "Продажба")]
        Sale = 1,

        [Display(Name = "Наем")]
        Rental = 2
    }
}

using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.City
{
    public class CityViewModel
    {
        [MaxLength(32)]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.ViewModels.Statistics
{
    public class StatisticsViewModel
    {
        [Display(Name = "Общ брой активни обяви")]
        public int ActiveOffersCount { get; set; }

        [Display(Name = "Брой обяви (наеми)")]
        public int ActiveRentalsCount { get; set; }

        [Display(Name = "Брой обяви (продажби)")]
        public int ActiveSalesCount { get; set; }

        [Display(Name = "Средна цена на продажбите в Евро")]
        public decimal AverageSaleTotalPrice { get; set; }

        [Display(Name = "Средна цена на кв. м. (продажби) в Евро")]
        public decimal AverageSalePricePerSqMeter { get; set; }

        [Display(Name = "Средна цена на наемите в Евро")]
        public decimal AverageRentTotalPrice { get; set; }

        [Display(Name = "Средна цена на кв. м. (наеми) в Евро")]
        public decimal AverageRentPricePerSqMeter { get; set; }

        [Display(Name = "Общ брой неактивни обяви")]
        public int DeactivatedOffersCount { get; set; }

        [Display(Name = "Брой регистрирани потребители")]
        public int UsersCount { get; set; }

        [Display(Name = "Брой уникални посещения")]
        public long UniqueVisitorsCount { get; set; }
    }
}

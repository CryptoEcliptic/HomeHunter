using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Services.Models
{
    public class StatisticsServiceModel
    {
        public int ActiveOffersCount { get; set; }

        public int ActiveRentalsCount { get; set; }

        public int ActiveSalesCount { get; set; }

        public decimal AverageSaleTotalPrice { get; set; }

        public decimal AverageSalePricePerSqMeter { get; set; }

        public decimal AverageRentTotalPrice { get; set; }

        public decimal AverageRentPricePerSqMeter { get; set; }

        public int DeactivatedOffersCount { get; set; }

        public int UsersCount { get; set; }

        public long UniqueVisitorsCount { get; set; }
    }
}

using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class StatisticServices : IStatisticServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IUserServices userServices;
        private readonly IVisitorSessionServices visitorSessionServices;

        public StatisticServices(HomeHunterDbContext context, 
            IUserServices userServices,
            IVisitorSessionServices visitorSessionServices)
        {
            this.context = context;
            this.userServices = userServices;
            this.visitorSessionServices = visitorSessionServices;
        }

        public async Task<StatisticsServiceModel> GetAdministrationStatistics()
        {
            var offers = this.context.Offers.
                Include(x => x.RealEstate)
                .ToList();

            int usersCount = await GetUsersCount();

            decimal averageSaleTotalPrice = GetAverageSaleTotalPrice(offers);
            decimal averageSalePricePerSqMeter = GetAverageSalePricePerSquareMeter(offers);

            decimal averageRentTotalPrice = GetAverageRentTotalPrice(offers);
            decimal averageRentPricePerSqMeter = GetAverageRentPricePerSquareMeter(offers);

            var statisticsServiceModel = new StatisticsServiceModel
            {
                ActiveOffersCount = offers.Where(x => x.IsDeleted == false).Count(),
                ActiveRentalsCount = offers.Where(x => x.IsDeleted == false && x.OfferType == OfferType.Rental).Count(),
                ActiveSalesCount = offers.Where(x => x.IsDeleted == false && x.OfferType == OfferType.Sale).Count(),
                DeactivatedOffersCount = offers.Where(x => x.IsDeleted == true).Count(),

                AverageSaleTotalPrice = averageSaleTotalPrice,
                AverageSalePricePerSqMeter = averageSalePricePerSqMeter,
                AverageRentTotalPrice = averageRentTotalPrice,
                AverageRentPricePerSqMeter = averageRentPricePerSqMeter,

                UsersCount = usersCount,
                UniqueVisitorsCount = await this.visitorSessionServices.UniqueVisitorsCount(),
            };

            return statisticsServiceModel;
        }

        private static decimal GetAverageRentPricePerSquareMeter(List<Offer> offers)
        {
            return offers
                    .Where(x => x.OfferType == OfferType.Rental).Count() == 0 ? 0 :
              offers
                    .Where(x => x.OfferType == OfferType.Rental)
                    .Select(x => x.RealEstate.PricePerSquareMeter)
                    .Average();
        }

        private static decimal GetAverageRentTotalPrice(List<Offer> offers)
        {
            return offers
                    .Where(x => x.OfferType == OfferType.Rental).Count() == 0 ? 0 :
              offers
                    .Where(x => x.OfferType == OfferType.Rental)
                    .Select(x => x.RealEstate.Price)
                    .Average();
        }

        private static decimal GetAverageSalePricePerSquareMeter(List<Offer> offers)
        {
            return offers
                    .Where(x => x.OfferType == OfferType.Sale).Count() == 0 ? 0 :
              offers
                    .Where(x => x.OfferType == OfferType.Sale)
                    .Select(x => x.RealEstate.PricePerSquareMeter)
                    .Average();
        }

        private static decimal GetAverageSaleTotalPrice(List<Offer> offers)
        {
            return offers
                    .Where(x => x.OfferType == OfferType.Sale).Count() == 0 ? 0 :
               offers
                    .Where(x => x.OfferType == OfferType.Sale)
                    .Select(x => x.RealEstate.Price)
                    .Average();
        }

        private async Task<int> GetUsersCount()
        {
            var users = await this.userServices.GetAllUsersAsync();
            var usersCount = users.Count();
            return usersCount;
        }
    }
}

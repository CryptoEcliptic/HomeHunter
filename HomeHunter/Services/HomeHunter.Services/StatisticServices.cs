using HomeHunter.Data;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class StatisticServices : IStatisticServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IUserServices userServices;

        public StatisticServices(HomeHunterDbContext context, IUserServices userServices)
        {
            this.context = context;
            this.userServices = userServices;
        }

        public async Task<StatisticsServiceModel> GetAdministrationStatistics()
        {
            var offers = this.context.Offers.
                Include(x => x.RealEstate)
                .ToList();
            var users = await this.userServices.GetAllUsersAsync();
            var usersCount = users.Count();

            var statisticsServiceModel = new StatisticsServiceModel
            {
                ActiveOffersCount = offers.Where(x => x.IsDeleted == false).Count(),
                ActiveRentalsCount = offers.Where(x => x.IsDeleted == false && x.OfferType == OfferType.Rental).Count(),
                ActiveSalesCount = offers.Where(x => x.IsDeleted == false && x.OfferType == OfferType.Sale).Count(),
                DeactivatedOffersCount = offers.Where(x => x.IsDeleted == true).Count(),
                AverageSaleTotalPrice = offers
                    .Where(x => x.OfferType == OfferType.Sale)
                    .Select(x => x.RealEstate.Price)
                    .Average(),

                AverageSalePricePerSqMeter = offers
                    .Where(x => x.OfferType == OfferType.Sale)
                    .Select(x => x.RealEstate.PricePerSquareMeter)
                    .Average(),

                AverageRentTotalPrice = offers
                    .Where(x => x.OfferType == OfferType.Rental)
                    .Select(x => x.RealEstate.Price)
                    .Average(),

                AverageRentPricePerSqMeter = offers
                    .Where(x => x.OfferType == OfferType.Rental)
                    .Select(x => x.RealEstate.PricePerSquareMeter)
                    .Average(),

                UsersCount = usersCount,
            };

            return statisticsServiceModel;
        }
    }
}

using HomeHunter.Common;
using HomeHunter.Data;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Helpers
{
    public class ReferenceNumberGenerator : IReferenceNumberGenerator
    {
        private const string StartSaleRefNumberDigits = "30";

        Dictionary<string, string> StartingCodes = new Dictionary<string, string>
        {
            { "Едностаен апартамент", "0001" },
            { "Двустаен апартамент", "0200" },
            { "Тристаен апартамент", "0300" },
            { "Четиристаен апартамент", "0400" },
            { "Многостаен апартамент", "0500" },
            { "Мезонет", "0600" },
            { "Ателие, Таван", "0700" },
            { "Офис", "0800" },
            { "Магазин", "0900" },
            { "Заведение", "1000" },
            { "Склад", "1100" },
            { "Промишлено помещение", "1200" },
            { "Етаж от къща", "1300" },
            { "Къща", "1400" },
            { "Гараж", "1500" },
            { "Парцел", "1600" },
            { "Земеделска земя", "1700" },
            { "Хотел", "1800" },
        };
        private const string SingleRoomAppartmentRefNumberMaxValue = "300199";
        private readonly HomeHunterDbContext context;
        private readonly IRealEstateServices realEstateServices;

        public ReferenceNumberGenerator(
            IRealEstateServices realEstateServices,
            HomeHunterDbContext context)
        {
            this.context = context;
            this.realEstateServices = realEstateServices;
        }

        public async Task<string> GenerateOfferId(string offerType, string estateId)
        {
            var realEstate = await this.realEstateServices.GetDetailsAsync(estateId);
            var estateType = realEstate.RealEstateType;

            string referenceNumber = null;

            if (offerType == GlobalConstants.OfferTypeSaleName)
            {
                referenceNumber = StartSaleRefNumberDigits;

                var previousOfferRefNumber = await this.GetRefNumberOfLastOfferByEstateType(offerType, estateType);

                if (previousOfferRefNumber == null)
                {
                    referenceNumber += StartingCodes[estateType];
                }

                else if (previousOfferRefNumber != null && previousOfferRefNumber != SingleRoomAppartmentRefNumberMaxValue)
                {
                    int currentRefNumberAsInt = int.Parse(previousOfferRefNumber) + 1;
                    referenceNumber = currentRefNumberAsInt.ToString();
                }

            }
            return referenceNumber;
        }

        private async Task<string> GetRefNumberOfLastOfferByEstateType(string offerType, string estateType)
        {
            OfferType parsedEnum = offerType == GlobalConstants.OfferTypeSaleName ? OfferType.Sale : OfferType.Rental;

            var offer = await this.context.Offers
           .Include(x => x.RealEstate)
           .OrderBy(x => x.CreatedOn)
           .LastOrDefaultAsync(x => x.RealEstate.RealEstateType.TypeName == estateType
                    && x.OfferType == parsedEnum);

            var lastRefNumber = offer != null? offer.ReferenceNumber : null;

            return lastRefNumber;
        }
    }
}

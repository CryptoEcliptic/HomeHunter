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
        private const string StartSaleRefNumberDigit = "30";
        private const string StartRentRefNumberDigit = "10";
        private const int SymbolsToSkip = 2;
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

        Dictionary<string, string> MaxCodeValues = new Dictionary<string, string>
        {
            { "Едностаен апартамент", "0199" },
            { "Двустаен апартамент", "0299" },
            { "Тристаен апартамент", "0399" },
            { "Четиристаен апартамент", "0499" },
            { "Многостаен апартамент", "0599" },
            { "Мезонет", "0699" },
            { "Ателие, Таван", "0799" },
            { "Офис", "0899" },
            { "Магазин", "0999" },
            { "Заведение", "1099" },
            { "Склад", "1199" },
            { "Промишлено помещение", "1299" },
            { "Етаж от къща", "1399" },
            { "Къща", "1499" },
            { "Гараж", "1599" },
            { "Парцел", "1699" },
            { "Земеделска земя", "1799" },
            { "Хотел", "1899" },
        };

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

            string referenceNumber = offerType == GlobalConstants.OfferTypeSaleName ? StartSaleRefNumberDigit : StartRentRefNumberDigit;

            var previousOfferRefNumber = await this.GetRefNumberOfLastOfferByEstateType(offerType, estateType);
            string lastDigitsOfPreviousNumber = previousOfferRefNumber != null ? previousOfferRefNumber.Skip(SymbolsToSkip).ToString() : null;

            if (previousOfferRefNumber == null || MaxCodeValues[estateType] == lastDigitsOfPreviousNumber)
            {
                referenceNumber += StartingCodes[estateType];
                return referenceNumber;
            }
            
            else
            {
                int currentRefNumberAsInt = int.Parse(previousOfferRefNumber) + 1;
                referenceNumber = currentRefNumberAsInt.ToString();
                return referenceNumber;
            }

        }

        private async Task<string> GetRefNumberOfLastOfferByEstateType(string offerType, string estateType)
        {
            OfferType parsedEnum = offerType == GlobalConstants.OfferTypeSaleName ? OfferType.Sale : OfferType.Rental;

            var offer = await this.context.Offers
           .Include(x => x.RealEstate)
           .OrderBy(x => x.CreatedOn)
           .LastOrDefaultAsync(x => x.RealEstate.RealEstateType.TypeName == estateType
                    && x.OfferType == parsedEnum);

            var lastRefNumber = offer != null ? offer.ReferenceNumber : null;

            return lastRefNumber;
        }
    }
}

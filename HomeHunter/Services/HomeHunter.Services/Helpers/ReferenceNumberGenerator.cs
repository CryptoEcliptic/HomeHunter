using HomeHunter.Common;
using HomeHunter.Data;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Helpers
{
    public class ReferenceNumberGenerator : IReferenceNumberGenerator
    {
        private const string StartSaleRefNumberDigit = "30";
        private const string StartRentRefNumberDigit = "10";
        private const int SymbolsToTake = 4;
        private const int RefNumberIncrementationStep = 1;

        private readonly HomeHunterDbContext context;

        public ReferenceNumberGenerator(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<string> GenerateOfferReferenceNumber(string offerType, string estateId)
        {
            var realEstate = await this.context.RealEstates
                .Include(x => x.RealEstateType)
                .FirstOrDefaultAsync(x => x.Id == estateId);

            var realEstateType = realEstate.RealEstateType;

            var currentReferenceNumber = offerType == GlobalConstants.OfferTypeSaleName ? StartSaleRefNumberDigit : StartRentRefNumberDigit;
            var previousReferenceNumber = await this.GetLastReferenceNumberDigits(offerType, realEstateType.TypeName);

            string lastDigitsOfPreviousReferenceNumber = previousReferenceNumber != null ? previousReferenceNumber.Substring(previousReferenceNumber.Length - SymbolsToTake).ToString() : null;

            if (lastDigitsOfPreviousReferenceNumber == null || realEstateType.MaxReferenceNumber == lastDigitsOfPreviousReferenceNumber)
            {
                currentReferenceNumber += realEstateType.MinReferenceNumber;
                return currentReferenceNumber;
            }
            else
            {
                int currentRefNumberAsInt = int.Parse(previousReferenceNumber) + RefNumberIncrementationStep;
                currentReferenceNumber = currentRefNumberAsInt.ToString();
                return currentReferenceNumber;
            }
        }

        private async Task<string> GetLastReferenceNumberDigits(string offerType, string estateType)
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

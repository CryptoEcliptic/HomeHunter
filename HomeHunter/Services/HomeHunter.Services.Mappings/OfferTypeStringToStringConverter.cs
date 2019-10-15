using AutoMapper;
using HomeHunter.Common;

namespace HomeHunter.Services.Mappings
{
    public class OfferTypeStringToStringConverter : IValueConverter<string, string>
    {
        private const string SaleNameConst = "Sale";

        public string Convert(string sourceMember, ResolutionContext context)
        {
            return sourceMember == SaleNameConst ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName;
        }
    }
}

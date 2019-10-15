using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Domain.Enums;

namespace HomeHunter.Services.Mappings
{
    public class OfferTypeToStringConverter : IValueConverter<OfferType, string>
    {
        public string Convert(OfferType sourceMember, ResolutionContext context)
        {
            return sourceMember == OfferType.Sale ? GlobalConstants.OfferTypeSaleName : GlobalConstants.OfferTypeRentName;
        }
    }
}

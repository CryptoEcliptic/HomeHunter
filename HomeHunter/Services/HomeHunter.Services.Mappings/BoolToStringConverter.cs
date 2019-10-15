using AutoMapper;
using HomeHunter.Common;

namespace HomeHunter.Services.Mappings
{
    public class BoolToStringConverter : IValueConverter<bool?, string>
    {
        public string Convert(bool? sourceMember, ResolutionContext context)
        {
            return sourceMember == false ? GlobalConstants.BoolFalseStringValue
                : GlobalConstants.BoolTrueStringValue;
        }
    }


}

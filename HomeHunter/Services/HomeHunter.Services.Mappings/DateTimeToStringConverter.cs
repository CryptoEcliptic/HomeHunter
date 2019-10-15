using AutoMapper;
using HomeHunter.Common;
using System;

namespace HomeHunter.Services.Mappings
{
    public class DateTimeToStringConverter : IValueConverter<DateTime?, string>
    {
        public string Convert(DateTime? sourceMember, ResolutionContext context)
        {
            if (sourceMember != null)
            {
                return sourceMember.Value.ToString(GlobalConstants.DateTimeVisualizationFormat);
            }
            return GlobalConstants.NotAvailableMessage;
        }
    }
}

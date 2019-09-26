using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunter.Services.Helpers
{
    public interface IReferenceNumberGenerator
    {
        Task<string> GenerateOfferReferenceNumber(string offerType, string estateId);
    }
}

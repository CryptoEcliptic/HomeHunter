using HomeHunter.Domain;
using System.Collections.Generic;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateTypeServices
    {
        List<RealEstateType> GetAllTypes();
    }
}

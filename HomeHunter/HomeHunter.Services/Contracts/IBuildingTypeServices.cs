using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Services.Contracts
{
    public interface IBuildingTypeServices
    {
        List<BuildingType> GetAllBuildingTypes();
    }
}

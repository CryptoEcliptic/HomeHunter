using HomeHunter.Domain;
using System.Collections.Generic;

namespace HomeHunter.Services.Contracts
{
    public interface IHeatingSystemServices
    {
        List<HeatingSystem> GetAllHeatingSystems();
    }
}

using HomeHunter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IHeatingSystemServices
    {
        List<HeatingSystem> GetAllHeatingSystems();

        Task<HeatingSystem> GetHeatingSystem(string systemName);
    }
}

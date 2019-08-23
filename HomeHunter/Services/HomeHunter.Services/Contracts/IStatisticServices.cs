using HomeHunter.Services.Models;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IStatisticServices
    {
        Task<StatisticsServiceModel> GetAdministrationStatistics();
    }
}

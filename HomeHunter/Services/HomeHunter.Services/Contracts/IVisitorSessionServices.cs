using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IVisitorSessionServices
    {
        Task<bool> AddSessionInTheDb(string ipAddress, string visitorId);

        Task<long> UniqueVisitorsCount();
    }
}

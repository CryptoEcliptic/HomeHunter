using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IVisitorSessionServices
    {
        Task AddSessionInTheDb(string ipAddress, string visitorId);
    }
}

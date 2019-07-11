using HomeHunter.Domain;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IVillageServices
    {
        Task<Village> CreateVillage(string name);

        bool IsVillageEhists(string name);
    }
}

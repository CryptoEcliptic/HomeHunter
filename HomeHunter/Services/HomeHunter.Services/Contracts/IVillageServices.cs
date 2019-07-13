using HomeHunter.Domain;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IVillageServices
    {
        Village CreateVillage(string name);

        bool IsVillageEhists(string name);
    }
}

using HomeHunter.Domain;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IAddressServices
    {
        Task<bool> CreateAddress(City city);
        Task<bool> CreateAddress(Village village);
    }
}

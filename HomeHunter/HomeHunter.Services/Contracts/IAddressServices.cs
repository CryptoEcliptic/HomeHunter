using HomeHunter.Domain;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IAddressServices
    {
        Task<Address> CreateAddress(City city, string description, Village village, Neighbourhood neighbourhood);
        //Task<bool> CreateAddress(Village village); //TODO Is necessary?
    }
}

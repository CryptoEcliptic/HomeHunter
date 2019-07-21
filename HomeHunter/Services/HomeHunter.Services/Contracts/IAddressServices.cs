using HomeHunter.Domain;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IAddressServices
    {
        Task<Address> CreateAddressAsync(City city, string description, Village village, Neighbourhood neighbourhood);
        Task<Address> EditAddress(int addressId, City city, string description, Village village, Neighbourhood neighbourhood);

        Task<bool> DeleteAddress(int id);
    }
}

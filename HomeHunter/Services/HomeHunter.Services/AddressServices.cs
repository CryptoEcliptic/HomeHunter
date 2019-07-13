using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class AddressServices : IAddressServices
    {
        public async Task<Address> CreateAddress(City city, string description, Village village, Neighbourhood neighbourhood)
        {
            var address = Task.Run(() => new Address
            {
                City = city,
                Description = description,
                Village = village,
                Neighbourhood = neighbourhood
            });
            return await address;
        }
    }
}

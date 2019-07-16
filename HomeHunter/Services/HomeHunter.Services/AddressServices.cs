using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly HomeHunterDbContext context;

        public AddressServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<Address> CreateAddressAsync(City city, string description, Village village, Neighbourhood neighbourhood)
        {
            var address = new Address
            {
                City = city,
                Description = description,
                Village = village,
                Neighbourhood = neighbourhood
            };

            await this.context.Addresses.AddAsync(address);

            return address;
        }
    }
}

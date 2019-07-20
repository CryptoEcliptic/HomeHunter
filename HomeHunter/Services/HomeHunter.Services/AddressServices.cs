using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            //await this.context.Addresses.AddAsync(address);

            return address;
        }

        public async Task<Address> EditAddress(int addressId, City city, string description, Village village, Neighbourhood neighbourhood)
        {
            var address = this.context.Addresses
                .Include(x => x.Village)
                .FirstOrDefault(x => x.Id == addressId)
                ;

            if (address == null)
            {
                throw new ArgumentNullException("No such address in the database!");
            }

            address.City = city;
            address.Village = village;
            address.Neighbourhood = neighbourhood;

            if (city == null ||city.Name != "София")
            {
                address.Neighbourhood = null;
                address.NeighbourhoodId = null;
            }

           
            address.Description = description;
            address.ModifiedOn = DateTime.UtcNow;

            this.context.Update(address);
            await this.context.SaveChangesAsync();

            return address;
        }

    }
}

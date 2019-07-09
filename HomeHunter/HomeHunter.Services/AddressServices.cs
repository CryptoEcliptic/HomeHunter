using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    class AddressServices : IAddressServices
    {
        public Task<bool> CreateAddress(City city)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAddress(Village village)
        {
            throw new NotImplementedException();
        }
    }
}

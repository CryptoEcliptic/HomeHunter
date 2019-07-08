using HomeHunter.Data;
using HomeHunter.Services.Contracts;
using System;

namespace HomeHunter.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly HomeHunterDbContext context;

        public RealEstateServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public bool CreateRealEstate(string estateType,
            double area, 
            decimal price,
            string city,
            string address, 
            string typeBuilding, 
            string heatingSystem, 
            string floor, 
            int numberOfFloors, 
            int year, 
            bool parking, 
            bool yard,
            bool metroNearby,
            bool balcony, 
            bool basement)
        {
            throw new NotImplementedException();
        }
    }
}

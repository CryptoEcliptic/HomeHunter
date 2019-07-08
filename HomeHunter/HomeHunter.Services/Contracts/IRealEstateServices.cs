using HomeHunter.Domain;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateServices
    {
        bool CreateRealEstate(string estateType, 
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
            bool basement);
    }
}

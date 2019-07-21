﻿using HomeHunter.Services.Models.RealEstate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IRealEstateServices
    {
        Task<bool> CreateRealEstateAsync(RealEstateCreateServiceModel model);

        Task <IEnumerable<RealEstateIndexServiceModel>> GetAllRealEstatesAsync();

        Task<RealEstateDetailsServiceModel> GetDetailsAsync(string id);

        Task<bool> EditRealEstate(RealEstateEditServiceModel model);

        Task<bool> DeleteRealEstate(string id);
    }
}

﻿using HomeHunter.Services.Models.Offer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IOfferServices
    {
        Task<bool> CreateOfferAsync(string authotId, string estateId, OfferCreateServiceModel model);
        Task<IEnumerable<OfferIndexServiceModel>> GetAllActiveOffersAsync();
    }
}
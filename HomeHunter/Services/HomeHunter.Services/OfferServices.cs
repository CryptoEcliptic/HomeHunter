using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Offer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class OfferServices : IOfferServices
    {
        private readonly HomeHunterDbContext context;

        public OfferServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateOfferAsync(string authorId, string estateId, OfferCreateServiceModel model)
        {
            if (model.OfferType == null || authorId == null || estateId == null)
            {
                throw new ArgumentNullException("Invalid data provided");
            }

            OfferType parsedEnum = model.OfferType == "Продажба" ? OfferType.Sale : OfferType.Rental;

            string refereenceNumber = "A" + DateTime.UtcNow.ToString();

            var offer = new Offer
            {
                AuthorId = authorId,
                RealEstateId = estateId,
                Comments = model.Comments,
                ContactNumber = model.ContactNumber,
                OfferType = parsedEnum,
                ReferenceNumber = refereenceNumber,
            };

            await this.context.Offers.AddAsync(offer);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OfferIndexServiceModel>> GetAllActiveOffersAsync()
        {
            var activeOffers = await context.Offers
               .Where(z => z.IsDeleted == false)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.RealEstateType)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.City)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.Neighbourhood)
               .OrderByDescending(x => x.CreatedOn)
               .ToListAsync();

            ;


            //var realEstatesServiceModel = this.mapper.Map<IEnumerable<RealEstateIndexServiceModel>>(realEstates);

            return null;
        }
    }
}

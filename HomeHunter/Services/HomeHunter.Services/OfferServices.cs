using AutoMapper;
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
        private readonly IMapper mapper;

        public OfferServices(HomeHunterDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> CreateOfferAsync(string authorId, string estateId, OfferCreateServiceModel model)
        {
            if (model.OfferType == null || authorId == null || estateId == null)
            {
                throw new ArgumentNullException("Invalid data provided");
            }

            OfferType parsedEnum = model.OfferType == "Продажба" ? OfferType.Sale : OfferType.Rental;

            string refereenceNumber = "A" + DateTime.UtcNow.ToString();

            var author = await this.context.HomeHunterUsers.FirstOrDefaultAsync(x => x.Id == authorId);

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
               .Include(x => x.Author)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.RealEstateType)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.City)
               .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.Neighbourhood)
               .OrderByDescending(x => x.CreatedOn)
               .ToListAsync();

            var offerIndexServiceModel = this.mapper.Map<IEnumerable<OfferIndexServiceModel>>(activeOffers);

            return offerIndexServiceModel;
        }

        public async Task<OfferDetailsServiceModel> GetOfferDetailsAsync(string id)
        {
            var offer = await this.context.Offers
                .Include(x => x.Author)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.RealEstateType)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.BuildingType)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.HeatingSystem)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Images)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.City)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.Neighbourhood)
                .Include(r => r.RealEstate)
                    .ThenInclude(r => r.Address.Village)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (offer == null)
            {
                return null;
            }

            var offerDetailsServiceModel = this.mapper.Map<OfferDetailsServiceModel>(offer);

            return offerDetailsServiceModel;
        }

        public async Task<OfferPlainDetailsServiceModel> GetOfferByIdAsync(string id)
        {
            var offerToEdit = await this.context.Offers
                .FirstOrDefaultAsync(x => x.Id == id);

            var offerEditServiceModel = this.mapper.Map<OfferPlainDetailsServiceModel>(offerToEdit);

            return offerEditServiceModel;
        }

        public async Task<bool> EditOfferAsync(OfferEditServiceModel model)
        {
            var offer = await this.context.Offers
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (offer == null)
            {
                return false;
            }

            offer.ModifiedOn = DateTime.UtcNow;
            offer.OfferType = model.OfferType == "Продажба" ? offer.OfferType = OfferType.Sale : OfferType.Rental;

            this.mapper.Map<OfferEditServiceModel, Offer>(model, offer);

            try
            {
                this.context.Update(offer);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<string> GetOfferIdByRealEstateIdAsync(string realEstateId)
        {
            if (realEstateId == null)
            {
                throw new ArgumentNullException("Invalid parameter RealEstateId!");
            }

            var offerId = this.context.Offers
                .Include(x => x.RealEstate)
                .SingleOrDefault(x => x.RealEstate.Id == realEstateId)
                .Id;

            return offerId;
        }

        public async Task<bool> DeleteOfferAsync(string offerId)
        {
            var offer = await this.context.Offers
                 .Include(x => x.Author)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.RealEstateType)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.BuildingType)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.HeatingSystem)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Images)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address.City)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address.Neighbourhood)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address.Village)
                 .FirstOrDefaultAsync(x => x.Id == offerId);

            if (offer == null)
            {
                throw new ArgumentNullException("No offer with such Id!");
            }

            return true;

            
        }

        //private async Task<int> SoftDeleteEntity(Offer offer)
        //{
        //    offer.IsDeleted = true;
        //    offer.RealEstate.IsDeleted = true;
        //    offer.ModifiedOn = DateTime.UtcNow;
        //    offer.RealEstate.Images.Clear();



        //    this.context.Update(offer);
        //    await this.context.SaveChangesAsync();

        //}
    }
}

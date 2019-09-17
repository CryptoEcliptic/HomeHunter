using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.CloudinaryServices;
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
        private const string InvlidMethodParameterMessage = "Invalid data provided";
        private const string UnsuccessfullOfferCreationMessage = "Offer not created!";
        private const string OfferNotFoundMessage = "No offer found!";
        private const string UnsuccessfullyDeletedOfferMessage = "Offer not deleted!";
        private const string UnsuccessfullyUpdatedOfferMessage = "Offer not updated!";

        private readonly HomeHunterDbContext context;
        private readonly IImageServices imageServices;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public OfferServices(HomeHunterDbContext context,
            IImageServices imageServices,
            ICloudinaryService cloudinaryService,
           IUserServices userServices,
           IMapper mapper)
        {
            this.context = context;
            this.imageServices = imageServices;
            this.cloudinaryService = cloudinaryService;
            this.userServices = userServices;
            this.mapper = mapper;
        }

        public async Task<bool> CreateOfferAsync(string authorId, string estateId, OfferCreateServiceModel model)
        {
            if (model.OfferType == null || string.IsNullOrEmpty(authorId) || string.IsNullOrEmpty(estateId))
            {
                throw new ArgumentNullException(InvlidMethodParameterMessage);
            }

            OfferType parsedEnum = model.OfferType == GlobalConstants.OfferTypeSaleName ? OfferType.Sale : OfferType.Rental;

            string referenceNumber = this.GenerateOfferId(model.OfferType);
            var author = await this.userServices.GetUserById(authorId);

            var offer = new Offer
            {
                AuthorId = authorId,
                Author = author,
                RealEstateId = estateId,
                Comments = model.Comments,
                ContactNumber = model.ContactNumber,
                OfferType = parsedEnum,
                ReferenceNumber = referenceNumber,
            };

            await this.context.Offers.AddAsync(offer);
            var changedRows = await this.context.SaveChangesAsync();

            if (changedRows == 0)
            {
                throw new InvalidOperationException(UnsuccessfullOfferCreationMessage);
            }

            return true;
        }

        public async Task<IEnumerable<OfferIndexServiceModel>> GetAllActiveOffersAsync(OfferType? condition = null)
        {
            if (condition != null)
            {
                var salesOffers = await context.Offers
                        .Where(z => z.IsDeleted == false && z.OfferType == condition)
                        .Include(x => x.Author)
                        .Include(r => r.RealEstate)
                              .ThenInclude(r => r.RealEstateType)
                        .Include(x => x.RealEstate)
                              .ThenInclude(x => x.BuildingType)
                        .Include(r => r.RealEstate)
                              .ThenInclude(r => r.Address.City)
                        .Include(r => r.RealEstate)
                              .ThenInclude(r => r.Address.Neighbourhood)
                        .Include(x => x.RealEstate)
                             .ThenInclude(x => x.Images)
                        .OrderByDescending(x => x.CreatedOn)
                        .ToListAsync();
                
                var offerIndexServiceModel = this.mapper.Map<IEnumerable<OfferIndexServiceModel>>(salesOffers);
                return offerIndexServiceModel;
            }

            else
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
        }

        public async Task<IEnumerable<OfferIndexDeactivatedServiceModel>> GetAllDeactivatedOffersAsync()
        {
            var activeOffers = await context.Offers
                .Where(z => z.IsDeleted == true)
                .Include(x => x.Author)
                .Include(r => r.RealEstate)
                     .ThenInclude(r => r.RealEstateType)
                .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address.City)
                .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address.Neighbourhood)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            var offerIndexDeactivatedServiceModel = this.mapper.Map<IEnumerable<OfferIndexDeactivatedServiceModel>>(activeOffers);

            return offerIndexDeactivatedServiceModel;
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
                throw new ArgumentNullException(OfferNotFoundMessage);
            }

            var offerDetailsServiceModel = this.mapper.Map<OfferDetailsServiceModel>(offer);

            return offerDetailsServiceModel;
        }

        public async Task<OfferPlainDetailsServiceModel> GetOfferByIdAsync(string id)
        {
            var offerToEdit = await this.context.Offers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (offerToEdit == null)
            {
                throw new ArgumentNullException(OfferNotFoundMessage);
            }

            var offerEditServiceModel = this.mapper.Map<OfferPlainDetailsServiceModel>(offerToEdit);

            return offerEditServiceModel;
        }

        public async Task<bool> EditOfferAsync(OfferEditServiceModel model)
        {
            var offer = await this.context.Offers
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (offer == null)
            {
                throw new ArgumentNullException(OfferNotFoundMessage);
            }

            offer.ModifiedOn = DateTime.UtcNow;
            offer.OfferType = model.OfferType == GlobalConstants.OfferTypeSaleName ? offer.OfferType = OfferType.Sale : OfferType.Rental;

            this.mapper.Map<OfferEditServiceModel, Offer>(model, offer);

            this.context.Update(offer);
            int chandedRows = await this.context.SaveChangesAsync();
            if (chandedRows == 0)
            {
                throw new InvalidOperationException(UnsuccessfullyUpdatedOfferMessage);
            }

            return true;
        }

        public string GetOfferIdByRealEstateIdAsync(string realEstateId)
        {
            if (string.IsNullOrEmpty(realEstateId))
            {
                throw new ArgumentNullException(InvlidMethodParameterMessage);
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
                     .ThenInclude(r => r.Images)
                 .Include(r => r.RealEstate)
                     .ThenInclude(r => r.Address)
                 .FirstOrDefaultAsync(x => x.Id == offerId);

            if (offer == null)
            {
                throw new ArgumentNullException(OfferNotFoundMessage);
            }

            int deletionResult = await this.SoftDeleteEntity(offer);

            if (deletionResult == 0)
            {
                throw new InvalidOperationException(UnsuccessfullyDeletedOfferMessage);
            }

            return true;
        }

        private async Task<int> SoftDeleteEntity(Offer offer)
        {
            if (offer == null)
            {
                throw new ArgumentNullException(OfferNotFoundMessage);
            }

            offer.IsDeleted = true;
            offer.RealEstate.IsDeleted = true;
            offer.RealEstate.Address.IsDeleted = true;
            offer.DeletedOn = DateTime.UtcNow;
            offer.RealEstate.DeletedOn = DateTime.UtcNow;
            offer.RealEstate.Address.DeletedOn = DateTime.UtcNow;

            var imageIdsToDeleteFromCloudinary = await this.imageServices.GetImageIds(offer.RealEstate.Id);
            this.cloudinaryService.DeleteCloudinaryImages(imageIdsToDeleteFromCloudinary);

            await this.imageServices.RemoveImages(offer.RealEstate.Id);
            int changedRows = 0;

            try
            {
                this.context.Update(offer);
                changedRows = await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return changedRows;
            }

            return changedRows;
        }

        private string GenerateOfferId(string offerType)
        {
            string id = null;
            string saleIndexLetter = "П";
            string rentIndexLetter = "Н";
            Random rnd = new Random();

            if (offerType == GlobalConstants.OfferTypeSaleName)
            {
                id = saleIndexLetter + rnd.Next(1, 1000);
                return id;
            }
            else
            {
                id = rentIndexLetter + rnd.Next(1, 1000);
                return id;
            }
        }
    }
}

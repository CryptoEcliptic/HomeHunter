using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Offer;
using System;
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

        public async Task<bool> CreateOffer(string authorId, string estateId, OfferCreateServiceModel model)
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
    }
}

using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.ServiceOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class ServiceOfferService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        public ServiceOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool ServiceOfferCreate(ServiceOfferCreate model)
        {
            var entity =
                new ServiceOffer
                {
                    DaysTillComplete = model.DaysToComplete,
                    ServiceName = model.ServiceName,
                    ServiceDescription = model.ServiceDescription,
                    OwnerID = _userId,
                    CreatedUtc = DateTime.Now,
                    GameID = model.GameId,
                    Title = model.Title,
                    Body = model.Body,
                    Price = model.Price,
                    IsActive = true
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ServiceOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<ServiceOfferListItem> GetActiveServiceOffers()
        {
            var query = _ctx.ServiceOffers.Where(m => m.IsActive == true).Select(e => new ServiceOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                Price = e.Price,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<ServiceOfferListItem> GetInactiveServiceOffers()
        {
            var query = _ctx.ServiceOffers.Where(m => m.IsActive == false).Select(e => new ServiceOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                Price = e.Price,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<ServiceOfferListItem> GetAllServiceOffers()
        {
            var query = _ctx.ServiceOffers.Select(e => new ServiceOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                Price = e.Price,
                Title = e.Title,
                Body = e.Body,
                IsActive = e.IsActive
            });

            return query.ToList();
        }

        public List<ServiceOfferDetails> GetServiceOfferByOfferId(int offerId)
        {

            var query = _ctx.ServiceOffers.Where(e => e.OfferID == offerId).Select(e => new ServiceOfferDetails
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                Price = e.Price,
                Title = e.Title,
                Body = e.Body,
                IsActive = e.IsActive
            });

            return query.ToList();
        }

        public List<ServiceOfferDetails> GetServiceOfferByGameId(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = _ctx.ServiceOffers.Where(e => e.GameID == gameId).Select(e => new ServiceOfferDetails
                {
                    OfferId = e.OfferID,
                    CreatedUtc = e.CreatedUtc,
                    ModifiedUtc = e.ModifiedUtc,
                    Price = e.Price,
                    Title = e.Title,
                    Body = e.Body,
                    IsActive = e.IsActive
                });

                return query.ToList();
            }
        }

        public bool ServiceOfferUpdate(ServiceOfferEdit model)
        {
            var entity = _ctx.ServiceOffers.Single(j => j.OfferID == model.OfferId && j.OwnerID.ToString() == _userId.ToString());
            entity.ModifiedUtc = DateTime.Now;
            entity.OfferID = model.OfferId;
            entity.OwnerID = _userId;
            entity.IsActive = model.IsActive;
            entity.GameID = model.GameId;
            entity.Body = model.Body;
            entity.Title = model.Title;
            entity.Price = model.Price;

            return _ctx.SaveChanges() == 1;
        }

        public bool ServiceOfferDelete(int offerId)
        {
            var entity = _ctx.ServiceOffers.Single(e => e.OfferID == offerId);
            entity.IsActive = false;
            return _ctx.SaveChanges() == 1;
        }
    }
}
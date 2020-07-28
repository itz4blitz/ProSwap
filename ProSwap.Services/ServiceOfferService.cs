using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.CurrencyOffer;
using ProSwap.Models.Offer.OfferType.ServiceOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProSwap.Services
{
    public class ServiceOfferService
    {
        private readonly Guid _userId;

        public ServiceOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateServiceOffer(ServiceOfferCreate model)
        {
            var entity =
                new ServiceOffer()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Body = model.Body,
                    IsActive = true,
                    GameID = model.GameId,
                    ServiceName = model.ServiceName,
                    DaysTillComplete = model.DaysToComplete,
                    ServiceDescription = model.ServiceDescription,
                    Price = model.Price,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ServiceOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ServiceOfferListItem> GetServiceOffers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ServiceOffers
                        .Select(
                            e =>
                                new ServiceOfferListItem
                                {
                                    Owner = ctx.Users.FirstOrDefault(u => u.Id == e.OwnerID.ToString()).UserName,
                                    OfferId = e.OfferID,
                                    Title = e.Title,
                                    Body = e.Body,
                                    GameName = ctx.Games.FirstOrDefault(model => model.ID == e.GameID).Name,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    IsActive = e.IsActive,
                                    ServiceName = e.ServiceName,
                                    DaysToComplete = e.DaysTillComplete,
                                    ServiceDescription = e.ServiceDescription
                                }
                        );

                return query.ToArray();
            }
        }

        public ServiceOfferDetails GetServiceOfferById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ServiceOffers
                        .Single(e => e.OfferID == id);
                return
                    new ServiceOfferDetails
                    {
                        OfferId = entity.OfferID,
                        Title = entity.Title,
                        Body = entity.Body,
                        GameName = ctx.Games.Single(model => model.ID == entity.GameID).Name,
                        OwnerName = ctx.Users.FirstOrDefault(u => u.Id == entity.OwnerID.ToString()).UserName,
                        IsActive = entity.IsActive,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ServiceName = entity.ServiceName,
                        DaysToComplete = entity.DaysTillComplete,
                        ServiceDescription = entity.ServiceDescription,
                    };
            }
        }

        public bool UpdateServiceOffer(ServiceOfferEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ServiceOffers
                        .Single(e => e.OwnerID == _userId && e.OfferID == model.OfferId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.IsActive = model.IsActive;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.ServiceName = entity.ServiceName;
                entity.DaysTillComplete = entity.DaysTillComplete;
                entity.ServiceDescription = entity.ServiceDescription;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteServiceOffer(int offerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ServiceOffers
                        .Single(e => e.OfferID == offerId && e.OwnerID == _userId);

                ctx.ServiceOffers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

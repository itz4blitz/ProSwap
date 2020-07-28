using ProSwap.Data;
using ProSwap.Models.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class OfferService
    {
        private readonly Guid _userId;

        public OfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOffer(OfferCreate model)
        {
            var entity =
                new Offer()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Body = model.Body,
                    IsActive = true,
                    GameID = model.GameId,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Offers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<OfferListItem> GetOffers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Offers
                        .Select(
                            e =>
                                new OfferListItem
                                {
                                    Owner = ctx.Users.FirstOrDefault(u => u.Id == e.OwnerID.ToString()).UserName,
                                    OfferId = e.OfferID,
                                    Title = e.Title,
                                    Body = e.Body,
                                    GameName = ctx.Games.FirstOrDefault(model => model.ID == e.GameID).Name,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    IsActive = e.IsActive
                                }
                        );

                return query.ToArray();
            }
        }

        public OfferDetails GetOfferById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.OfferID == id);
                return
                    new OfferDetails
                    {
                        OfferId = entity.OfferID,
                        Title = entity.Title,
                        Body = entity.Body,
                        GameName = ctx.Games.Single(model => model.ID == entity.GameID).Name,
                        OwnerName = ctx.Users.FirstOrDefault(u => u.Id == entity.OwnerID.ToString()).UserName,
                        IsActive = entity.IsActive,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateOffer(OfferEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.OwnerID == _userId && e.OfferID == model.OfferId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.IsActive = model.IsActive;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteOffer(int offerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.OfferID == offerId && e.OwnerID == _userId);

                ctx.Offers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

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

        public bool OfferCreate(OfferCreate model)
        {
            var entity =
                new Offer()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Body = model.Body,
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
                        .Offers.Where(x => x.IsActive == true)
                        .Select(
                            e =>
                                new OfferListItem
                                {
                                    OfferId = e.OfferID,
                                    IsActive = e.IsActive,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
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
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public OfferDetails GetOfferByGameId(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.GameID == gameId);
                return
                    new OfferDetails
                    {
                        OfferId = entity.OfferID,
                        Title = entity.Title,
                        Body = entity.Body,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool OfferUpdate(OfferUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.OfferID == model.OfferId && e.OwnerID == _userId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }

        }

        public bool OfferDelete(int offerId)
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
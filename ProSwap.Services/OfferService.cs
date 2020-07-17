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
                    CreatedUtc = DateTimeOffset.Now,
                    UnitPrice = model.UnitPrice,
                    TotalUnitsAvailable = model.TotalUnitsAvailable
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
                                    ID = e.ID,
                                    IsActive = e.IsActive,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public OfferDetail GetOfferById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.ID == id);
                return
                    new OfferDetail
                    {
                        OfferId = entity.ID,
                        Title = entity.Title,
                        Body = entity.Body,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public OfferDetail GetOfferByGameId(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.GameID == gameId);
                return
                    new OfferDetail
                    {
                        OfferId = entity.ID,
                        Title = entity.Title,
                        Body = entity.Body,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool OfferUpdate(OfferEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Offers
                        .Single(e => e.ID == model.OfferId && e.OwnerID == _userId);

                entity.Title = model.Title;
                entity.Body = model.Content;
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
                        .Single(e => e.ID == offerId && e.OwnerID == _userId);

                ctx.Offers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
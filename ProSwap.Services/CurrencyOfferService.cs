using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.CurrencyOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class CurrencyOfferService
    {
        private readonly Guid _userId;

        public CurrencyOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCurrencyOffer(CurrencyOfferCreate model)
        {
            var entity =
                new CurrencyOffer()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Body = model.Body,
                    IsActive = true,
                    GameID = model.GameId,
                    UnitsAvailable = model.UnitsAvailable,
                    PricePerUnit = model.PricePerUnit,
                    CurrencyName = model.CurrencyName,
                    WasFulfilled = false,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CurrencyOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CurrencyOfferListItem> GetCurrencyoffers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .CurrencyOffers
                        .Select(
                            e =>
                                new CurrencyOfferListItem
                                {
                                    OfferId = e.OfferID,
                                    Title = e.Title,
                                    Body = e.Body,
                                    //GameID = e.GameID,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    IsActive = e.IsActive,
                                    UnitsAvailable = e.UnitsAvailable,
                                    PricePerUnit = e.PricePerUnit,
                                    CurrencyName = e.CurrencyName
                                }
                        );

                return query.ToArray();
            }
        }


        public CurrencyOfferDetails GetCurrencyOfferById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CurrencyOffers
                        .Single(e => e.OfferID == id);
                return
                    new CurrencyOfferDetails
                    {
                        OfferId = entity.OfferID,
                        Title = entity.Title,
                        Body = entity.Body,
                        //GameId = entity.GameID,
                        //OwnerID = entity.OwnerID,
                        IsActive = entity.IsActive,
                        UnitsAvailable = entity.UnitsAvailable,
                        PricePerUnit = entity.PricePerUnit,
                        CurrencyName = entity.CurrencyName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateCurrencyOffer(CurrencyOfferEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CurrencyOffers
                        .Single(e => e.OwnerID == _userId && e.OfferID == model.OfferId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.IsActive = model.IsActive;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.CurrencyName = entity.CurrencyName;
                entity.PricePerUnit = model.PricePerUnit;
                entity.UnitsAvailable = model.UnitsAvailable;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCurrencyOffer(int offerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CurrencyOffers
                        .Single(e => e.OfferID == offerId && e.OwnerID == _userId);

                ctx.CurrencyOffers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

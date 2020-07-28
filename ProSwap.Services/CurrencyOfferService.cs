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
                    CurrencyName = model.CurrencyName,
                    UnitsAvailable = model.UnitsAvailable,
                    PricePerUnit = model.PricePerUnit,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CurrencyOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CurrencyOfferListItem> GetCurrencyOffers()
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
                                    Owner = ctx.Users.FirstOrDefault(u => u.Id == e.OwnerID.ToString()).UserName,
                                    OfferId = e.OfferID,
                                    Title = e.Title,
                                    Body = e.Body,
                                    GameName = ctx.Games.FirstOrDefault(model => model.ID == e.GameID).Name,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    IsActive = e.IsActive,
                                    CurrencyName = e.CurrencyName,
                                    UnitsAvailable = e.UnitsAvailable,
                                    PricePerUnit = e.PricePerUnit
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
                        GameName = ctx.Games.Single(model => model.ID == entity.GameID).Name,
                        OwnerName = ctx.Users.FirstOrDefault(u => u.Id == entity.OwnerID.ToString()).UserName,
                        IsActive = entity.IsActive,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        CurrencyName = entity.CurrencyName,
                        UnitsAvailable = entity.UnitsAvailable,
                        PricePerUnit = entity.PricePerUnit
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
                entity.CurrencyName = model.CurrencyName;
                entity.UnitsAvailable = model.UnitsAvailable;
                entity.PricePerUnit = model.PricePerUnit;
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

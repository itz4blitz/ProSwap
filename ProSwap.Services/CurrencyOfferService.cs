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
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        public CurrencyOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool CurrencyOfferCreate(CurrencyOfferCreate model)
        {
            var entity =
                new CurrencyOffer
                {
                    OwnerID = _userId,
                    CreatedUtc = DateTime.Now,
                    GameID = model.GameId,
                    Title = model.Title,
                    Body = model.Body,
                    PricePerUnit = model.PricePerUnit,
                    UnitsAvailable = model.UnitsAvailable,
                    CurrencyName = model.CurrencyName,
                    IsActive = true,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CurrencyOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<CurrencyOfferListItem> GetActiveCurrencyOffers()
        {
            var query = _ctx.CurrencyOffers.Where(m => m.IsActive == true).Select(e => new CurrencyOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                PricePerUnit = e.PricePerUnit,
                CurrencyName = e.CurrencyName,
                UnitsAvailable = e.UnitsAvailable,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<CurrencyOfferListItem> GetInactiveCurrencyOffers()
        {
            var query = _ctx.CurrencyOffers.Where(m => m.IsActive == false).Select(e => new CurrencyOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                PricePerUnit = e.PricePerUnit,
                CurrencyName = e.CurrencyName,
                UnitsAvailable = e.UnitsAvailable,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<CurrencyOfferListItem> GetAllCurrencyOffers()
        {
            var query = _ctx.CurrencyOffers.Select(e => new CurrencyOfferListItem
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                PricePerUnit = e.PricePerUnit,
                CurrencyName = e.CurrencyName,
                UnitsAvailable = e.UnitsAvailable,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<CurrencyOfferDetails> GetCurrencyOfferByOfferId(int offerId)
        {

            var query = _ctx.CurrencyOffers.Where(e => e.OfferID == offerId).Select(e => new CurrencyOfferDetails
            {
                OfferId = e.OfferID,
                CreatedUtc = e.CreatedUtc,
                ModifiedUtc = e.ModifiedUtc,
                PricePerUnit = e.PricePerUnit,
                CurrencyName = e.CurrencyName,
                UnitsAvailable = e.UnitsAvailable,
                Title = e.Title,
                Body = e.Body
            });

            return query.ToList();
        }

        public List<CurrencyOfferDetails> GetCurrencyOfferByGameId(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = _ctx.CurrencyOffers.Where(e => e.GameID == gameId).Select(e => new CurrencyOfferDetails
                {
                    OfferId = e.OfferID,
                    CreatedUtc = e.CreatedUtc,
                    ModifiedUtc = e.ModifiedUtc,
                    PricePerUnit = e.PricePerUnit,
                    CurrencyName = e.CurrencyName,
                    UnitsAvailable = e.UnitsAvailable,
                    Title = e.Title,
                    Body = e.Body
                });

                return query.ToList();
            }
        }

        public bool CurrencyOfferUpdate(CurrencyOfferEdit model)
        {
            var entity = _ctx.CurrencyOffers.Single(j => j.OfferID == model.OfferId && j.OwnerID.ToString() == _userId.ToString());
            entity.OfferID = model.OfferId;
            entity.ModifiedUtc = DateTime.Now;
            entity.PricePerUnit = model.PricePerUnit;
            entity.CurrencyName = model.CurrencyName;
            entity.UnitsAvailable = model.UnitsAvailable;
            entity.Title = model.Title;
            entity.Body = model.Body;

            return _ctx.SaveChanges() == 1;
        }

        public bool OfferDelete(int offerId)
        {
            var entity = _ctx.AccountOffers.Single(e => e.OfferID == offerId);
            entity.IsActive = false;
            return _ctx.SaveChanges() == 1;
        }
    }
}

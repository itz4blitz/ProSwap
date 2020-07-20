using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.AccountOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Services
{
    public class AccountOfferService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        public AccountOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool AccountOfferCreate(AccountOfferCreate model)
        {
            var entity =
                new AccountOffer
                {
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
                ctx.AccountOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<AccountOfferListItem> GetActiveAccountOffers()
        {
            var query = _ctx.AccountOffers.Where(m => m.IsActive == true).Select(e => new AccountOfferListItem
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

        public List<AccountOfferListItem> GetInactiveAccountOffers()
        {
            var query = _ctx.AccountOffers.Where(m => m.IsActive == false).Select(e => new AccountOfferListItem
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

        public List<AccountOfferListItem> GetAllAccountOffers()
        {
            var query = _ctx.AccountOffers.Select(e => new AccountOfferListItem
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

        public List<AccountOfferDetails> GetAccountOfferByOfferId(int offerId)
        {

            var query = _ctx.AccountOffers.Where(e => e.OfferID == offerId).Select(e => new AccountOfferDetails
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

        public List<AccountOfferDetails> GetAccountOfferByGameId(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = _ctx.AccountOffers.Where(e => e.GameID == gameId).Select(e => new AccountOfferDetails
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

        public bool AccountOfferUpdate(AccountOfferUpdate model)
        {
            var entity = _ctx.AccountOffers.Single(j => j.OfferID == model.OfferId && j.OwnerID.ToString() == _userId.ToString());
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

        public bool OfferDelete(int offerId)
        {
            var entity = _ctx.AccountOffers.Single(e => e.OfferID == offerId);
            entity.IsActive = false;
            return _ctx.SaveChanges() == 1;
        }
    }
}
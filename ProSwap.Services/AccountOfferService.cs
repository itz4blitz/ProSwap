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

        public AccountOfferService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAccountOffer(AccountOfferCreate model)
        {
            var entity =
                new AccountOffer()
                {
                    OwnerID = _userId,
                    Title = model.Title,
                    Body = model.Body,
                    IsActive = true,
                    GameID = model.GameId,
                    Price = model.Price,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.AccountOffers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AccountOfferListItem> GetAccountOffers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .AccountOffers
                        .Select(
                            e =>
                                new AccountOfferListItem
                                {
                                    Owner = ctx.Users.FirstOrDefault(u => u.Id == e.OwnerID.ToString()).UserName,
                                    OfferId = e.OfferID,
                                    Title = e.Title,
                                    Body = e.Body,
                                    GameName = ctx.Games.FirstOrDefault(model => model.ID == e.GameID).Name,
                                    CreatedUtc = e.CreatedUtc,
                                    ModifiedUtc = e.ModifiedUtc,
                                    IsActive = e.IsActive,
                                    Price = e.Price
                                }
                        );

                return query.ToArray();
            }
        }

        public AccountOfferDetails GetAccountOfferById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AccountOffers
                        .Single(e => e.OfferID == id);
                return
                    new AccountOfferDetails
                    {
                        OfferId = entity.OfferID,
                        Title = entity.Title,
                        Body = entity.Body,
                        GameName = ctx.Games.Single(model => model.ID == entity.GameID).Name,
                        OwnerName = ctx.Users.FirstOrDefault(u => u.Id == entity.OwnerID.ToString()).UserName,
                        IsActive = entity.IsActive,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        Price = entity.Price
                    };
            }
        }

        public bool UpdateAccountOffer(AccountOfferEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AccountOffers
                        .Single(e => e.OwnerID == _userId && e.OfferID == model.OfferId);

                entity.Title = model.Title;
                entity.Body = model.Body;
                entity.IsActive = model.IsActive;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.Price = model.Price;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAccountOffer(int offerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AccountOffers
                        .Single(e => e.OfferID == offerId && e.OwnerID == _userId);

                ctx.AccountOffers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.CurrencyOffer;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class CurrencyOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: CurrencyOffers
        public ActionResult Index()
        {
            CurrencyOfferService _currencyOfferService = CreateCurrencyOfferService();
            var model = _currencyOfferService.GetCurrencyOffers();
            return View(model.ToList());
        }


        // GET: CurrencyOffers/Create
        public ActionResult Create()
        {
            var games = db.Games.ToList();
            var gameList = new SelectList(games.Select(e => new SelectListItem()
            {
                Value = e.ID.ToString(),
                Text = e.Name
            }).ToList(), "Value", "Text");

            var model = new CurrencyOfferCreate()
            {
                ListOfGames = gameList
            };

            return View(model);
        }

        // POST: CurrencyOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CurrencyOfferCreate offer)
        {
            if (ModelState.IsValid)
            {
                var svc = CreateCurrencyOfferService();
                svc.CreateCurrencyOffer(offer);
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", offer.GameId);
            return View(offer);
        }

        // GET: CurrencyOffers/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateCurrencyOfferService();
            var model = svc.GetCurrencyOfferById(id);
            return View(model);
        }

        // GET: CurrencyOffers/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateCurrencyOfferService();
            var detail = service.GetCurrencyOfferById(id);
            var model =
                new CurrencyOfferEdit
                {
                    OfferId = detail.OfferId,
                    Title = detail.Title,
                    Body = detail.Body,
                    IsActive = detail.IsActive
                };
            return View(model);
        }

        // POST: CurrencyOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CurrencyOfferEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.OfferId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCurrencyOfferService();

            if (service.UpdateCurrencyOffer(model))
            {
                TempData["SaveResult"] = "Your offer was modified.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your offer could not be updated.");
            return View(model);
        }

        // GET: CurrencyOffers/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = CreateCurrencyOfferService();
            var model = svc.GetCurrencyOfferById(id);
            return View(model);
        }

        // POST: CurrencyOffers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCurrencyOfferService();

            service.DeleteCurrencyOffer(id);

            TempData["SaveResult"] = "Your offer was deleted";

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private CurrencyOfferService CreateCurrencyOfferService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _currencyOfferService = new CurrencyOfferService(userId);
            return _currencyOfferService;
        }
    }
}

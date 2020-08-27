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
using ProSwap.Models.Game;
using ProSwap.Models.Offer;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class OffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offers
        public ActionResult Index()
        {
            OfferService _offerService = CreateOfferService();
            var model = _offerService.GetOffers();
            return View(model.ToList());
        }


        // GET: Offers/Create
        public ActionResult Create()
        {
            var games = db.Games.ToList();
            var gameList = new SelectList(games.Select(e => new SelectListItem()
            {
                Value = e.ID.ToString(), Text = e.Name
            }).ToList(), "Value", "Text");

            var model = new OfferCreate()
            {
                ListOfGames = gameList
            };

            return View(model);
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferCreate offer)
        {
            if (ModelState.IsValid)
            {
                var svc = CreateOfferService();
                svc.CreateOffer(offer);
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", offer.GameId);
            return View(offer);
        }

        // GET: Offers/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateOfferService();
            var model = svc.GetOfferById(id);
            return View(model);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateOfferService();
            var detail = service.GetOfferById(id);
            var model =
                new OfferEdit
                {
                    OfferId = detail.OfferId,
                    Title = detail.Title,
                    Body = detail.Body,
                    IsActive = detail.IsActive
                };
            return View(model);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OfferEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.OfferId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateOfferService();

            if (service.UpdateOffer(model))
            {
                TempData["SaveResult"] = "Your offer was modified.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your offer could not be updated.");
            return View(model);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = CreateOfferService();
            var model = svc.GetOfferById(id);
            return View(model);
        }

        // POST: Offers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateOfferService();

            service.DeleteOffer(id);

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

        private OfferService CreateOfferService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _offerService = new OfferService(userId);
            return _offerService;
        }
    }
}

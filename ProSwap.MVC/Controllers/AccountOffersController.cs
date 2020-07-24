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
using ProSwap.Models.Offer.OfferType.AccountOffer;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class AccountOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private AccountOfferService _accountOfferService;
        private Guid _userId;

        // GET: AccountOffers
        [AllowAnonymous]
        public ActionResult Index()
        {
            var offers = db.AccountOffers.Where(a => a.IsActive == true);
            return View(db.AccountOffers.ToList());
        }

        [Authorize]
        // GET: AccountOffers/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountOffer accountOffer = db.AccountOffers.Find(id);
            if (accountOffer == null)
            {
                return HttpNotFound();
            }
            return View(accountOffer);
        }

        [Authorize]
        // GET: AccountOffers/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.AccountOffers, "ID", "Name");
            return View();
        }

        [Authorize]
        // POST: AccountOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountOfferCreate accountOffer)
        {
            if (ModelState.IsValid)
            {
                _userId = Guid.Parse(User.Identity.GetUserId());
                _accountOfferService = new AccountOfferService(_userId);
                _accountOfferService.AccountOfferCreate(accountOffer);
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", accountOffer.GameID);
            return View(accountOffer);
        }

        [Authorize]
        // GET: AccountOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountOffer accountOffer = db.AccountOffers.Find(id);
            if (accountOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", accountOffer.GameID);
            return View(accountOffer);
        }

        [Authorize]
        // POST: AccountOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountOffer accountOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", accountOffer.GameID);
            return View(accountOffer);
        }

        // GET: AccountOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountOffer accountOffer = db.AccountOffers.Find(id);
            if (accountOffer == null)
            {
                return HttpNotFound();
            }
            return View(accountOffer);
        }

        // POST: AccountOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountOffer accountOffer = db.AccountOffers.Find(id);
            db.Offers.Remove(accountOffer);
            db.SaveChanges();
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
    }
}

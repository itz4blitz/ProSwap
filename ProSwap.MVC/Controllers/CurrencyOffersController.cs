using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class CurrencyOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [AllowAnonymous]
        // GET: CurrencyOffers
        public ActionResult Index()
        {
            var offers = db.Offers.Include(c => c.Game);
            return View(offers.ToList());
        }

        // GET: CurrencyOffers/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyOffer currencyOffer = db.CurrencyOffers.Find(id);
            if (currencyOffer == null)
            {
                return HttpNotFound();
            }
            return View(currencyOffer);
        }

        // GET: CurrencyOffers/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name");
            return View();
        }

        // POST: CurrencyOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CurrencyOffer currencyOffer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(currencyOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", currencyOffer.GameID);
            return View(currencyOffer);
        }

        // GET: CurrencyOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyOffer currencyOffer = db.CurrencyOffers.Find(id);
            if (currencyOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", currencyOffer.GameID);
            return View(currencyOffer);
        }

        // POST: CurrencyOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CurrencyOffer currencyOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", currencyOffer.GameID);
            return View(currencyOffer);
        }

        // GET: CurrencyOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyOffer currencyOffer = db.CurrencyOffers.Find(id);
            if (currencyOffer == null)
            {
                return HttpNotFound();
            }
            return View(currencyOffer);
        }

        // POST: CurrencyOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyOffer currencyOffer = db.CurrencyOffers.Find(id);
            db.Offers.Remove(currencyOffer);
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

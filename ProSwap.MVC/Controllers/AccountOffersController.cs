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
    public class AccountOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly AccountOfferService _accountOfferService;
        private readonly string _userId;

        // GET: AccountOffers
        public ActionResult Index()
        {
            var offers = db.AccountOffers.Where(a => a.IsActive == true);
            return View(db.AccountOffers.ToList());
        }

        // GET: AccountOffers/Details/5
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

        // GET: AccountOffers/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name");
            return View();
        }

        // POST: AccountOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountOffer accountOffer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(accountOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", accountOffer.GameID);
            return View(accountOffer);
        }

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

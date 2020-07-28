using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProSwap.Data;
using ProSwap.Data.OfferTypes;
using ProSwap.Models.Offer.OfferType.ServiceOffer;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class ServiceOffersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ServiceOfferService _serviceOfferService;
        private string _userId;


        // GET: ServiceOffers
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Index()
        {
            var offers = db.ServiceOffers.Include(s => s.Game);
            return View(offers.ToList());
        }

        [System.Web.Mvc.AllowAnonymous]
        // GET: ServiceOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceOffer serviceOffer = db.ServiceOffers.Find(id);
            if (serviceOffer == null)
            {
                return HttpNotFound();
            }
            return View(serviceOffer);
        }

        // GET: ServiceOffers/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name");
            return View();
        }

        // POST: ServiceOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceOfferCreate serviceOffer)
        {
            if (!ModelState.IsValid)
            {
                _userId = User.Identity.GetUserId();
                //_serviceOfferService = new ServiceOfferService(_userId);
                _serviceOfferService.CreateServiceOffer(serviceOffer);
                return RedirectToAction("Index");
            }
            return View(serviceOffer);
        }

        //private ServiceOfferService CreateServiceOfferService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    //var service = new ServiceOfferService(userId);
        //    //return service;
        //}

        // GET: ServiceOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceOffer serviceOffer = db.ServiceOffers.Find(id);
            if (serviceOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", serviceOffer.Game.Name);
            return View(serviceOffer);
        }

        // POST: ServiceOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceOfferEdit serviceOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.Games, "ID", "Name", serviceOffer.ServiceName);
            return View(serviceOffer);
        }

        // GET: ServiceOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceOffer serviceOffer = db.ServiceOffers.Find(id);
            if (serviceOffer == null)
            {
                return HttpNotFound();
            }
            return View(serviceOffer);
        }

        // POST: ServiceOffers/Delete/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceOffer serviceOffer = db.ServiceOffers.Find(id);
            db.Offers.Remove(serviceOffer);
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

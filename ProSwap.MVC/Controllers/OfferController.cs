using ProSwap.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProSwap.MVC.Controllers
{
    public class OfferController : Controller
    {
    // Add the application DB Context (link to the database)
    private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Offer
        // GET: Product
        public ActionResult Index()
        {
            List<Offer> productList = _db.Offers.ToList();
            List<Offer> orderedList = productList.OrderBy(prod => prod.Game).ToList();
            return View(orderedList);
        }

        // Get: Offer
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offer
        [HttpPost]
        public ActionResult Create(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _db.Offers.Add(offer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offer);
        }

        // GET : Delete
        // Offer/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Offer offer = _db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET : Edit
        // Offer/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = _db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST : Offer/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(offer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offer);
        }

        // GET : Details
        // Offer/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = _db.Offers.Find(id);

            if (offer == null)
            {
                return HttpNotFound();
            }

            return View(offer);
        }
    }
}
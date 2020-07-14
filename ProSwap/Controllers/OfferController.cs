using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProSwap.Data;
using ProSwap.Models;

namespace ProSwap.Controllers
{
    public class OfferController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Offer
        public ActionResult Index()
        {
            return View(_db.Offers.ToList());
        }
    }
}
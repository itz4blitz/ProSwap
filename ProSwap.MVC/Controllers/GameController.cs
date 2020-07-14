using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProSwap.Data;
using ProSwap.Models;
using ProSwap.Services;

namespace ProSwap.MVC.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private GameService _gameService;
        private string _userId;

        // GET: Games
        public ActionResult Index()
        {
            return View(_db.Games.ToList());
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(_db.Games, "GameId", "FirstName");
            return View();
        }

        // POST : Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Add(game);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET : Game/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // Post : Game/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Game game = _db.Games.Find(id);
            _db.Games.Remove(game);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get : Game/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // Get : Game/Edit/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

    }
}
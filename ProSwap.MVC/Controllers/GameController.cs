using System;
using System.Collections.Generic;
using System.Linq;
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

        // POST : Game/Create
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



    }
}
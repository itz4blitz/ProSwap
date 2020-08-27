using Microsoft.AspNet.Identity;
using ProSwap.Data;
using ProSwap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSwap.MVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var _userId = Guid.NewGuid();
            if (!User.Identity.IsAuthenticated)
            {
                _userId = Guid.Parse(User.Identity.GetUserId());
            }

            PostService _postService = CreatePostService(_userId);
            var model = _postService.GetPosts();
            return View(model.ToList());
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private PostService CreatePostService(Guid? _userId)
        {
            var _postService = new PostService(_userId);

            return _postService;
        }
    }
}
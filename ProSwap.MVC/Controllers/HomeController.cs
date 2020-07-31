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
            var userId = Guid.NewGuid();
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(User.Identity.GetUserId());
            }

            PostService _postService = CreatePostService();
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

        private PostService CreatePostService()
        {
            var _postService = new PostService();
            return _postService;
        }
    }
}
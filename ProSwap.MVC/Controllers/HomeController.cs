using Microsoft.AspNet.Identity;
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            PostService _postService = CreatePostService();
            var model = _postService.GetPosts();
            return View(model.ToList());
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "ProSwap - Online Games Marketplace.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Welcome to the contact page. We're still in the process of getting this page updated. Please check back soon.";

            return View();
        }
        private PostService CreatePostService()
        {
            var userId = Guid.Parse("89e6f76c-46f6-47f3-8f4e-ab8df9f5226e");
            var _postService = new PostService(userId);
            return _postService;
        }
    }
}
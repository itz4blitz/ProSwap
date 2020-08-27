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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
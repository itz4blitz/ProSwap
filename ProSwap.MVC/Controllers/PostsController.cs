using ProSwap.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProSwap.MVC.Controllers
{
    public class PostsController : Controller
    {
        // GET: Post/Random

        public ActionResult Random()
        {
            var post = new Post() { Title = "This is a test", Body = "This is some filler data for a test post." };

            var viewResult = new ViewResult();

            return View(post);
        }

        public ActionResult Edit(int id)
        {
            return Content("id" + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));

        }

        [Route("post/created//{month:regex(\\d{2}:range(1,12)}/{day:regex(\\d{2}:range(1,31)}/{year:regex(\\d{4}:range(2020)}")]
        public ActionResult ByCreatedDate(int month, int day, int year)
        {
            return Content(month + "/" + day + "/" + year);
        }
    }
}
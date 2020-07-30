using Microsoft.AspNet.Identity;
using ProSwap.Data;
using ProSwap.Models.Offer;
using ProSwap.Models.Post;
using ProSwap.Services;
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
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Offers
        public ActionResult Index()
        {
            PostService _postService = CreatePostService();
            var model = _postService.GetPosts();
            return View(model.ToList());
        }


        // GET: Offers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate post)
        {
            if (ModelState.IsValid)
            {
                var svc = CreatePostService();
                svc.CreatePost(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Offers/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreatePostService();
            var model = svc.GetPostById(id);
            return View(model);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreatePostService();
            var detail = service.GetPostById(id);
            var model =
                new PostEdit
                {
                    PostId = detail.PostId,
                    Title = detail.Title,
                    Body = detail.Body,
                };
            return View(model);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PostId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePostService();

            if (service.UpdatePost(model))
            {
                TempData["SaveResult"] = "Your post was modified.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your post could not be updated.");
            return View(model);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = CreatePostService();
            var model = svc.GetPostById(id);
            return View(model);
        }

        // POST: Offers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePostService();

            service.DeletePost(id);

            TempData["SaveResult"] = "Your post was deleted";

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

        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var _postService = new PostService(userId);
            return _postService;
        }
    }
}
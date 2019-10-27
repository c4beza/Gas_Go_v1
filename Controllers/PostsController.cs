using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gas_Go_v1.Models;
using Microsoft.AspNet.Identity;

namespace Gas_Go_v1.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Posts
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var post = db.Post.Where(b => b.ThreadID == id);
                return View(post.ToList());
            }
            else
                return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AdminIndex(int? id)
        {
            if (id != null)
            {
                var post = db.Post.Where(b => b.ThreadID == id);
                return View(post.ToList());
            }
            else
                return View();
        }

        public ActionResult ViewPosts(int? id)
        {
            if (id != null)
            {
                var post = db.Post.Where(b => b.ThreadID == id);
                return View(post.ToList());
            }
            else
                return View();
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {

            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create(Nullable<int> id)
        {
            var posts = new PostsViewModel();
            
            var threadId = id;
            if (id != null)
            ViewBag.ThreadID = threadId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(posts);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostsViewModel model) 
        {
            
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    ThreadID = model.ThreadID,
                    Content = model.Content,
                    CreatedTime = DateTime.Now,
                    UserID = User.Identity.GetUserId(),
                };
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("ViewPosts", new{ id = model.ThreadID});
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Content,CreatedTime,UserID,ThreadID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", post.UserID);
            ViewBag.ThreadID = new SelectList(db.Thread, "ThreadId", "Subject", post.ThreadID);
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

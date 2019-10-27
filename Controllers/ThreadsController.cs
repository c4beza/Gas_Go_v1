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
    public class ThreadsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Threads
        public ActionResult Index()
        {
            var thread = db.Thread.Include(t => t.AspNetUsers);
            return View(thread.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            var thread = db.Thread.Include(t => t.AspNetUsers);
            return View(thread.ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // GET: Threads/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThreadsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thread = new Thread
                {
                    UserID = User.Identity.GetUserId(),
                    Subject = model.Subject,
                    CreatedTime = DateTime.Now,
                    
                };
                db.Thread.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            
            return View(thread);
        }

        [Authorize(Roles = "Admin")]
        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadId,Subject,CreatedTime,UserID")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", thread.UserID);
            return View(thread);
        }

        [Authorize(Roles = "Admin")]
        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        [Authorize(Roles = "Admin")]
        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Thread.Find(id);
            db.Thread.Remove(thread);
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

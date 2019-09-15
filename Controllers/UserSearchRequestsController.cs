using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gas_Go_v1.Models;

namespace Gas_Go_v1.Controllers
{
    public class UserSearchRequestsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: UserSearchRequests
        public ActionResult Index()
        {
            return View(db.UserSearchRequest.ToList());
        }

        // GET: UserSearchRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearchRequest userSearchRequest = db.UserSearchRequest.Find(id);
            if (userSearchRequest == null)
            {
                return HttpNotFound();
            }
            return View(userSearchRequest);
        }

        // GET: UserSearchRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserSearchRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestId,RequestDateTime,RequestKeyword,UserID")] UserSearchRequest userSearchRequest)
        {
            if (ModelState.IsValid)
            {
                db.UserSearchRequest.Add(userSearchRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userSearchRequest);
        }

        // GET: UserSearchRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearchRequest userSearchRequest = db.UserSearchRequest.Find(id);
            if (userSearchRequest == null)
            {
                return HttpNotFound();
            }
            return View(userSearchRequest);
        }

        // POST: UserSearchRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,RequestDateTime,RequestKeyword,UserID")] UserSearchRequest userSearchRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSearchRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userSearchRequest);
        }

        // GET: UserSearchRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSearchRequest userSearchRequest = db.UserSearchRequest.Find(id);
            if (userSearchRequest == null)
            {
                return HttpNotFound();
            }
            return View(userSearchRequest);
        }

        // POST: UserSearchRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSearchRequest userSearchRequest = db.UserSearchRequest.Find(id);
            db.UserSearchRequest.Remove(userSearchRequest);
            db.SaveChanges();
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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gas_Go_v1.Models;
using WebMatrix.WebData;
using Microsoft.AspNet.Identity;


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
            UserSearchRequestsViewModel model = new UserSearchRequestsViewModel();
            return View();
        }

        // POST: UserSearchRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserSearchRequestsViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (User.Identity.GetUserId() != null)
                {
                    var searchRequest = new UserSearchRequest
                    {
                        RequestKeyword = model.RequestKeyword,
                        UserID = User.Identity.GetUserId(),
                        RequestDateTime = DateTime.Now
                    };
                    db.UserSearchRequest.Add(searchRequest);
                    db.SaveChanges();
                    String keyword = model.RequestKeyword;
                    TempData["keyword"] = keyword;
                    int requestId = searchRequest.RequestId;
                    TempData["RequestId"] = requestId;
                    return RedirectToAction("Index", "RequestResults");
                }
                else
                {
                    String keyword = model.RequestKeyword;
                    TempData["keyword"] = keyword;
                    return RedirectToAction("Index", "RequestResults");
                }
            }

            return View(model);
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

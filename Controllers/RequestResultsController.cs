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
    public class RequestResultsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: RequestResults
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() != null)
            {
                int requestId;
                if (TempData["keyword"] == null)
                {
                    var requestResult = db.GasStations;
                    return View(requestResult.ToList());
                }
                else
                {
                    String keyword = TempData["keyword"] as string;
                    requestId = int.Parse(TempData["requestId"].ToString());
                    var result = db.GasStations.Where(x => x.GasStationName.Contains(keyword));
                    var requestResult = new RequestResult
                    {
                        RequestID = requestId,
                        UserID = User.Identity.GetUserId(),
                        ResultDateTime = DateTime.Now,
                        GasStationID = 1,
                    };
                    db.RequestResult.Add(requestResult);
                    db.SaveChanges();
                    return View(result.ToList());
                }
            }
            else
            {
                String keyword = TempData["keyword"] as string;
                var result = db.GasStations.Where(x => x.GasStationName.Contains(keyword));
                return View(result.ToList());
            }
        }

        // GET: RequestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResult requestResult = db.RequestResult.Find(id);
            if (requestResult == null)
            {
                return HttpNotFound();
            }
            return View(requestResult);
        }

        // GET: RequestResults/Create
        public ActionResult Create()
        {
            ViewBag.GasStationID = new SelectList(db.GasStations, "GasStationId", "GasStationName");
            ViewBag.RequestID = new SelectList(db.UserSearchRequest, "RequestId", "RequestKeyword");
            return View();
        }

        // POST: RequestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultId,ResultDateTime,RequestID,UserID,GasStationID")] RequestResult requestResult)
        {
            if (ModelState.IsValid)
            {
                db.RequestResult.Add(requestResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GasStationID = new SelectList(db.GasStations, "GasStationId", "GasStationName", requestResult.GasStationID);
            ViewBag.RequestID = new SelectList(db.UserSearchRequest, "RequestId", "RequestKeyword", requestResult.RequestID);
            return View(requestResult);
        }

        // GET: RequestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResult requestResult = db.RequestResult.Find(id);
            if (requestResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.GasStationID = new SelectList(db.GasStations, "GasStationId", "GasStationName", requestResult.GasStationID);
            ViewBag.RequestID = new SelectList(db.UserSearchRequest, "RequestId", "RequestKeyword", requestResult.RequestID);
            return View(requestResult);
        }

        // POST: RequestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultId,ResultDateTime,RequestID,UserID,GasStationID")] RequestResult requestResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GasStationID = new SelectList(db.GasStations, "GasStationId", "GasStationName", requestResult.GasStationID);
            ViewBag.RequestID = new SelectList(db.UserSearchRequest, "RequestId", "RequestKeyword", requestResult.RequestID);
            return View(requestResult);
        }

        // GET: RequestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestResult requestResult = db.RequestResult.Find(id);
            if (requestResult == null)
            {
                return HttpNotFound();
            }
            return View(requestResult);
        }

        // POST: RequestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestResult requestResult = db.RequestResult.Find(id);
            db.RequestResult.Remove(requestResult);
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

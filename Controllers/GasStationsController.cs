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
    public class GasStationsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: GasStations
        public ActionResult Index()
        {
            return View(db.GasStations.ToList());
        }

        // GET: GasStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GasStations gasStations = db.GasStations.Find(id);
            if (gasStations == null)
            {
                return HttpNotFound();
            }
            return View(gasStations);
        }

        // GET: GasStations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GasStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GasStationId,GasStationName,Rating,Street,Suburb,Longitude,Latitude,UnleadedPrice,UnleadedPriceUpdatedTime,Premium95Price,Premium95PriceUpdatedTime,Premium98Price,Premium98PriceUpdatedTime,DieselPrice,DieselPriceUpdatedTime")] GasStations gasStations)
        {
            if (ModelState.IsValid)
            {
                db.GasStations.Add(gasStations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gasStations);
        }

        // GET: GasStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GasStations gasStations = db.GasStations.Find(id);
            if (gasStations == null)
            {
                return HttpNotFound();
            }
            return View(gasStations);
        }

        // POST: GasStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GasStationId,GasStationName,Rating,Street,Suburb,Longitude,Latitude,UnleadedPrice,UnleadedPriceUpdatedTime,Premium95Price,Premium95PriceUpdatedTime,Premium98Price,Premium98PriceUpdatedTime,DieselPrice,DieselPriceUpdatedTime")] GasStations gasStations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gasStations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gasStations);
        }

        // GET: GasStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GasStations gasStations = db.GasStations.Find(id);
            if (gasStations == null)
            {
                return HttpNotFound();
            }
            return View(gasStations);
        }

        // POST: GasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GasStations gasStations = db.GasStations.Find(id);
            db.GasStations.Remove(gasStations);
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

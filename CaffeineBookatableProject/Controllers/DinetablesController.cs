using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaffeineBookatableProject.Models;

namespace CaffeineBookatableProject.Controllers
{
    public class DinetablesController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Dinetables
        public ActionResult Index()
        {
            var dinetables = db.Dinetables.Include(d => d.Outlet);
            return View(dinetables.ToList());
        }

        // GET: Dinetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinetable dinetable = db.Dinetables.Find(id);
            if (dinetable == null)
            {
                return HttpNotFound();
            }
            return View(dinetable);
        }

        // GET: Dinetables/Create
        public ActionResult Create()
        {
            ViewBag.Outlet_Bid = new SelectList(db.Outlets, "outlet_id", "outlet_name");
            return View();
        }

        // POST: Dinetables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dine_Id,Person_Name,Person_Number,Dine_Date,Dine_Time,Outlet_Bid")] Dinetable dinetable)
        {
            if (ModelState.IsValid)
            {
                db.Dinetables.Add(dinetable);
                db.SaveChanges();
                return RedirectToAction("");
            }

            ViewBag.Outlet_Bid = new SelectList(db.Outlets, "outlet_id", "outlet_name", dinetable.Outlet_Bid);
            return View(dinetable);
        }

        // GET: Dinetables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinetable dinetable = db.Dinetables.Find(id);
            if (dinetable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Outlet_Bid = new SelectList(db.Outlets, "outlet_id", "outlet_name", dinetable.Outlet_Bid);
            return View(dinetable);
        }

        // POST: Dinetables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dine_Id,Person_Name,Person_Number,Dine_Date,Dine_Time,Outlet_Bid")] Dinetable dinetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dinetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Outlet_Bid = new SelectList(db.Outlets, "outlet_id", "outlet_name", dinetable.Outlet_Bid);
            return View(dinetable);
        }

        // GET: Dinetables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinetable dinetable = db.Dinetables.Find(id);
            if (dinetable == null)
            {
                return HttpNotFound();
            }
            return View(dinetable);
        }

        // POST: Dinetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinetable dinetable = db.Dinetables.Find(id);
            db.Dinetables.Remove(dinetable);
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

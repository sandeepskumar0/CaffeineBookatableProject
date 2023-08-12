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
    public class CarrersController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Carrers
        public ActionResult Index()
        {
            return View(db.Carrers.ToList());
        }

        // GET: Carrers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrer carrer = db.Carrers.Find(id);
            if (carrer == null)
            {
                return HttpNotFound();
            }
            return View(carrer);
        }

        // GET: Carrers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "career_id,carrer_designation,carrer_qualfication,career_description,send_resume")] Carrer carrer)
        {
            if (ModelState.IsValid)
            {
                db.Carrers.Add(carrer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrer);
        }

        // GET: Carrers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrer carrer = db.Carrers.Find(id);
            if (carrer == null)
            {
                return HttpNotFound();
            }
            return View(carrer);
        }

        // POST: Carrers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "career_id,carrer_designation,carrer_qualfication,career_description,send_resume")] Carrer carrer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carrer);
        }

        // GET: Carrers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrer carrer = db.Carrers.Find(id);
            if (carrer == null)
            {
                return HttpNotFound();
            }
            return View(carrer);
        }

        // POST: Carrers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrer carrer = db.Carrers.Find(id);
            db.Carrers.Remove(carrer);
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

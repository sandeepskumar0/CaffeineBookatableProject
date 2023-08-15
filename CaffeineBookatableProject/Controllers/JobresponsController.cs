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
    public class JobresponsController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Jobrespons
        public ActionResult Index()
        {
            var jobresponses = db.Jobresponses.Include(j => j.Carrer);
            return View(jobresponses.ToList());
        }

        // GET: Jobrespons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobrespons jobrespons = db.Jobresponses.Find(id);
            if (jobrespons == null)
            {
                return HttpNotFound();
            }
            return View(jobrespons);
        }

        // GET: Jobrespons/Create
        public ActionResult Create()
        {
            ViewBag.Job_Position = new SelectList(db.Carrers, "career_id", "carrer_designation");
            return View();
        }

        // POST: Jobrespons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Jobrespons jobrespons)
        {
            if (ModelState.IsValid)
            {

                jobrespons.Resum.SaveAs(Server.MapPath("~/Resumes/" + jobrespons.Resum.FileName));
                //product.Prod_Pic = "~/ProPic/" + product.Pro_Pic.FileName;
                if (jobrespons.Resum.FileName != "")
                {
                    jobrespons.Resume = "~/Resumes/" + jobrespons.Resum.FileName;
                    db.Jobresponses.Add(jobrespons);
                    db.SaveChanges();
                }
                else
                {
                    jobrespons.Resume = null;
                }

                return RedirectToAction("Index","Home");
            }

            ViewBag.Job_Position = new SelectList(db.Carrers, "career_id", "carrer_designation", jobrespons.Job_Position);
            return View(jobrespons);
        }

        // GET: Jobrespons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobrespons jobrespons = db.Jobresponses.Find(id);
            if (jobrespons == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job_Position = new SelectList(db.Carrers, "career_id", "carrer_designation", jobrespons.Job_Position);
            return View(jobrespons);
        }

        // POST: Jobrespons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,Name,Email,Job_Position,Resume,Phone")] Jobrespons jobrespons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobrespons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Job_Position = new SelectList(db.Carrers, "career_id", "carrer_designation", jobrespons.Job_Position);
            return View(jobrespons);
        }

        // GET: Jobrespons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobrespons jobrespons = db.Jobresponses.Find(id);
            if (jobrespons == null)
            {
                return HttpNotFound();
            }
            return View(jobrespons);
        }

        // POST: Jobrespons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobrespons jobrespons = db.Jobresponses.Find(id);
            db.Jobresponses.Remove(jobrespons);
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

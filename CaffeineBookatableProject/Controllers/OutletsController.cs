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
    public class OutletsController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Outlets
        public ActionResult Index()
        {
            var outlets = db.Outlets.Include(o => o.Branch);https://localhost:44308/Models/Product.cs
            return View(outlets.ToList());
        }

        // GET: Outlets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // GET: Outlets/Create
        public ActionResult Create()
        {
            ViewBag.branch_fid = new SelectList(db.Branches, "branch_id", "branch_name");
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Outlet outlet)
        {
            
            if (ModelState.IsValid)
            {

                outlet.outle_pic.SaveAs(Server.MapPath("~/OutPic/" + outlet.outle_pic.FileName));
                //product.Prod_Pic = "~/ProPic/" + product.Pro_Pic.FileName;
                if (outlet.outle_pic.FileName != "")
                {
                    outlet.outlet_pic = "~/OutPic/" + outlet.outle_pic.FileName;
                    db.Outlets.Add(outlet);
                    db.SaveChanges();
                }
                else
                {
                    outlet.outlet_pic = null;
                }

                return RedirectToAction("Index");
            }

            ViewBag.branch_fid = new SelectList(db.Branches, "branch_id", "branch_name", outlet.branch_fid);
            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_fid = new SelectList(db.Branches, "branch_id", "branch_name", outlet.branch_fid);
            return View(outlet);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "outlet_id,outlet_name,outlet_disc,outlet_location,outlet_pic,branch_fid")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branch_fid = new SelectList(db.Branches, "branch_id", "branch_name", outlet.branch_fid);
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outlet outlet = db.Outlets.Find(id);
            db.Outlets.Remove(outlet);
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

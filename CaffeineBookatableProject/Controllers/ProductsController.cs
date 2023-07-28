using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaffeineBookatableProject.Models;
using Microsoft.Ajax.Utilities;
using System.IO;

namespace CaffeineBookatableProject.Controllers
{
    public class ProductsController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();
        //private OnlineBookingEntities db = new OnlineBookingEntities();
        

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cat_Fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {

                product.Pro_Pic.SaveAs(Server.MapPath("~/ProPic/" + product.Pro_Pic.FileName));
                //product.Prod_Pic = "~/ProPic/" + product.Pro_Pic.FileName;
                if (product.Pro_Pic.FileName != "")
                {
                    product.Prod_Pic = "~/ProPic/" + product.Pro_Pic.FileName;
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    product.Prod_Pic = null;
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.Cat_Fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.Cat_Fid);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_Fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.Cat_Fid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Prod_Id,Prod_Name,Prod_Disc,Purchase_Price,Sale_Price,Prod_Pic,Cat_Fid")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_Fid = new SelectList(db.Categories, "Cat_Id", "Cat_Name", product.Cat_Fid);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

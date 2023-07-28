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
    public class CustomersController : Controller
    {
        //private OnlineBookingEntities db = new OnlineBookingEntities();
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Customers
        //public ActionResult LogIn()
        //{
        //    return View(db.Customers.ToList());
        //}

        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Customer c)
        {
            var cust= db.Customers.Where(x=> x.Customer_Email == c.Customer_Email && x.Customer_Password == c.Customer_Password).Count();
            var id = db.Customers.Where(x => x.Customer_Email == c.Customer_Email && x.Customer_Password == c.Customer_Password).Select(v=>v.Customer_Id).FirstOrDefault();
            var idd = db.Customers.FirstOrDefault(x => x.Customer_Email == c.Customer_Email && x.Customer_Password == c.Customer_Password);
            Session["CID"] = id;
            Session["CIDD"] = idd;
            if (cust > 0)
            {
                Session["ID"] = cust;
                //Response.Write("<script>alert('Invalid Username/Password'); </script>");
                return RedirectToAction("Cart", "Home");
            }
            else
            {
                
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            return RedirectToAction("LogIn", "Customers");
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Customer_Id,Customer_FirstName,Customer_LastName,Customer_Email,Customer_Password,Customer_Contact,Customer_Address")] Customer customer)
        {
            
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("LogIn","Customers");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_Id,Customer_FirstName,Customer_LastName,Customer_Email,Customer_Password,Customer_Contact,Customer_Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

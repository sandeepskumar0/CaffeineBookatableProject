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
    public class UsersController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
     
        public ActionResult LogIn(User c)
        {
            var cust = db.Users.Where(x => x.User_Email == c.User_Email && x.User_Password == c.User_Password).Count();
            var id = db.Users.Where(x => x.User_Email == c.User_Email && x.User_Password == c.User_Password).Select(v => v.User_Id).FirstOrDefault();
            var idd = db.Users.FirstOrDefault(x => x.User_Email == c.User_Email && x.User_Password == c.User_Password);
            Session["CID"] = id;
            Session["CIDD"] = idd;
            if (cust > 0)
            {
                Session["ID"] = cust;
                //Response.Write("<script>alert('Invalid Username/Password'); </script>");
                return RedirectToAction("userdash", "Home");
            }
            else
            {

                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "User_Id,User_FirstName,User_LastName,User_Email,User_Password,User_Contact,User_Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("LogIn","Users");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,User_FirstName,User_LastName,User_Email,User_Password,User_Contact,User_Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

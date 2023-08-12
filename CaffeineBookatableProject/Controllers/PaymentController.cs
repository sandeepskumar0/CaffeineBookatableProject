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
    public class PaymentController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();

        // GET: Payment
        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }
        [HttpPost]

        public ActionResult CreateOrder(Models.Payment _requestData)
        {
            Random randomobj = new Random();
            string transactionId = randomobj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_zBnWWj9JGkUFHk", "LK97bIwbRbEY3snKZociOeoT");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", _requestData.amount * 100); // Amount will in paíse
            options.Add("receipt", transactionId);

            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1- automatic, 0- manual
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            OrderModel orderModel = new OrderModel

            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_zBnWWj9JGkUFHk",
                amount = _requestData.amount * 100,
                currency = "INR",
                name = _requestData.name,
                email = _requestData.email,
                contactNumber = _requestData.contactNumber,
                address = _requestData.address,
                description = "Testing description"
            };
            return View("PaymentPage", orderModel);
        }
        public class OrderModel
        {
            public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public int contactNumber { get; set; }
            public string address { get; set; }
            public string description { get; set; }
        }
        [HttpPost]
        public ActionResult Complete()

        {

            string paymentId = Request.Params["rzp_paymentid"];
            string orderId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_zBnWWj9JGkUFHk", "LK97bIwbRbEY3snKZociOeoT");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];


            if (paymentCaptured.Attributes["status"] == "captured")

            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }
        public ActionResult Success()
        {
            return View();

        }
        public ActionResult Failed()
        {
            return View();
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "receiptid,name,email,contactNumber,address,amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "receiptid,name,email,contactNumber,address,amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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

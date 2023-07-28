using CaffeineBookatableProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaffeineBookatableProject.Controllers
{
    public class HomeController : Controller
    {
        private OnlineBookingEntities db = new OnlineBookingEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        public ActionResult Admin()
        {


            return View();
        }
        public ActionResult testimonial()
        {


            return View();
        }
        public ActionResult Products(int? id)
        {

            ProductModel p = new ProductModel();

            p.cat = db.Categories.ToList();


            if(id==null)
            {
                p.pro = db.Products.ToList();
            }
            else
            {
                p.pro = db.Products.Where(z => z.Cat_Fid == id).ToList();
            }

            return View(p);
        }
        public ActionResult reservation()
        {

            return View();
        }
        public ActionResult Outlets(int? id)
        {
            OutletModel o = new OutletModel();

            o.br = db.Branches.ToList();


            if (id==null)
            {
                o.Out = db.Outlets.ToList();
            }
            else
            {
                o.Out = db.Outlets.Where(z => z.branch_fid == id).ToList();
            }

            return View(o);
            
        }
        public ActionResult blog()
        {


            return View();
        }
        public ActionResult Cart()
        {


            return View();
        }
        public ActionResult AddToCart(int id)
        {
           
            List<Product> list;
            if (Session["myCart"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                list = (List<Product>)Session["myCart"];
            }
            list.Add(db.Products.Where(p => p.Prod_Id == id).FirstOrDefault());
            list[list.Count - 1].PRO_QUANTITY = 1;

            Session["myCart"] = list;
            return RedirectToAction("cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY--;
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY++;
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }

        public ActionResult paynow()
        {
            if (Session["ID"] !=null)
            {
                order o = new order();
                o.order_status = "paid";
                o.order_date = DateTime.Now;
                o.cus_fid = (int)Session["CID"];
                Session["Order"] = o;
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=sandeep@cafebrew.com&item_name=CaffeineBookatableProject&return=https://localhost:44308/Home/OrderBooked&amount=" + double.Parse(Session["totalAmountS"].ToString()) / 220);
            }
            else
            {
                return RedirectToAction("LogIn", "Customers");
            }
        }

        public ActionResult OrderBooked()
        {
            order o = (order)Session["Order"];
            Customer customer = (Customer)Session["CIDD"];
            db.Order.Add(o);
            db.SaveChanges();
            List<Product> p = (List<Product>)Session["myCart"];
            for (int i=0; i< p.Count; i++)
            {
                orderdetail od = new orderdetail();
                int orderID = db.Order.Max(x => x.order_id);
                od.order_fid = orderID;
                od.product_fid = p[i].Prod_Id;
                od.quantity = p[i].PRO_QUANTITY * -1;
                od.pp_price = Convert.ToDecimal(p[i].Purchase_Price);
                od.psale_price = Convert.ToDecimal(p[i].Sale_Price);

                db.Orderdetails.Add(od);
                db.SaveChanges();
            }


            return View();
        }
    }
}
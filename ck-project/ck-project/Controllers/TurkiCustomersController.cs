using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ck_project.Controllers
{
    public class TurkiCustomersController : Controller
    {
        // GET: TurkiCustomers
        //first create the db connecter
        ckdatabase db = new ckdatabase();
        public ActionResult Customers()
        {
            List<customer> Customers_list = db.customers.ToList();
            ViewBag.Customerslist = Customers_list;
            return View();
        }


        public ActionResult CustomersTest()
        {
            List<customer> Customers_list = db.customers.ToList();
            ViewBag.Customerslist = Customers_list;
            return View();
        }
        public ActionResult AddCustomer() {
           
            return View();
        
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCustomer(FormCollection form) {
            //init new customer
            customer a = new customer
            {
                customer_firstname = form["Item1.customer_firstname"],
                customer_lastname = form["Item1.customer_lastname"],
                customer_middlename = form["Item1.customer_middlename"],
                email = form["Item1.email"],
                phone_number = form["Item1.phone_number"],
                second_phone_number = form["Item1.second_phone_number"]
            };
            //check
            if (string.IsNullOrEmpty(a.customer_firstname) ||
                string.IsNullOrEmpty(a.customer_lastname) ||
                string.IsNullOrEmpty(a.phone_number)) {
                ModelState.AddModelError("customer","customer info incomplete");
            }
            //int new address
            address b = new address
            {
                address1 = form["Item2.address1"],
                address_type = form["Item2.address_type"],
                city = form["Item2.city"],
                state = form["Item2.state"],
                county = form["Item2.county"],
                zipcode = form["Item2.zipcode"]
            };
            //che
            if (string.IsNullOrEmpty(b.address_type) ||
                string.IsNullOrEmpty(b.address1) ||
                string.IsNullOrEmpty(b.city) ||
                string.IsNullOrEmpty(b.county) ||
                string.IsNullOrEmpty(b.zipcode)) {
                ModelState.AddModelError("address", "address info incomplete");
                return View("AddCustomer");
            }
            //write change to db,address first
            if (ModelState.IsValid) {
                db.addresses.Add(b);
                db.SaveChanges();
                //linking 2 table
                a.address_number = b.address_number;
                db.customers.Add(a);
                db.SaveChanges();
            }

            

            return View();
        }
        

       
    }
}
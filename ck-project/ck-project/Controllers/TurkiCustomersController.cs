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
            //var add_dropdown = new List<SelectListItem>();
            //add_dropdown.AddRange(db.addresses.Select(a => new SelectListItem
            //{
            //    Text = a.county + a.city + a.address1,
            //    Value = a.address_number.ToString()                
            //}));
            //ViewBag.add_dropdown = add_dropdown;
       



            return View();
        
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCustomer(FormCollection form) {
            
            customer a = new customer();
            TryUpdateModel(a,);
            address b = new address();
            TryUpdateModel(b);
            var c = a.customer_firstname;
            var d = b.address_type;
            

            return View("Customers");
        }

       
    }
}
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

    }
}
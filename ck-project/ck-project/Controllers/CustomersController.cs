using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class CustomersController : Controller
    {
             
        //Creating the DB connecton
        ckdatabase db = new ckdatabase();
        // GET: Customers


        /*   public ActionResult ListCustomers(string searchby , string search)
           {
               List<customer> Customers_list = db.customers.ToList();
               ViewBag.Customerslist = Customers_list;

               if (searchby == "customer_firstname")
               {
                   return View(db.customers.Where(x => x.customer_firstname == search || search == null).ToList());
               }
               else
               {
                   return View(db.customers.Where(x => x.customer_lastname.StartsWith(search)).ToList());
               }
             //  return View();
           }
           */

          public ActionResult ListCustomers(string search )
{       

                return View(db.customers.Where(x => x.customer_lastname.StartsWith(search) || search == null && x.deleted == false).ToList());
        
            //  return View();
        }
        
        // read from the DB
        public ActionResult Edit(int id)
        {
            List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
            ViewBag.Customerslist = Customers_list;
            customer target = Customers_list[0];
            ViewBag.id = id;
            return View(target);
        }

        // Write to the DB that is why we use POST
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(int id, FormCollection fo)
        //{
        //    List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
        //    ViewBag.Customerslist = Customers_list;
        //    customer target = Customers_list[0];
        //    TryUpdateModel(target, new string[] { "customer_firstname", "customer_middlename", "customer_lastname", "phone_number", "second_phone_number", "email" }, fo.ToValueProvider());
        //    db.SaveChanges();
        //    return View(target);
        //}



      //  Write to the DB that is why we use POST
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection fo)
        {
            List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
            ViewBag.Customerslist = Customers_list;
            customer target = Customers_list[0];
            TryUpdateModel(target, new string[] { "customer_firstname", "customer_middlename", "customer_lastname", "phone_number", "second_phone_number", "email" }, fo.ToValueProvider());
            db.SaveChanges();
            return View(target);
        }

        public ActionResult AddCustomer()
        {
            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=true,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=true,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=true,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=true,Value="KY"}

            };
            ViewBag.Sstate = Sstate;
            return View();

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCustomer(FormCollection form)
        {
            //init new customer
            customer a = new customer


            {
                customer_firstname = form["Item1.customer_firstname"],
                customer_lastname = form["Item1.customer_lastname"],
                customer_middlename = form["Item1.customer_middlename"],
                email = form["Item1.email"],

                phone_number = form["Item1.phone_number"],
                second_phone_number = form["Item1.second_phone_number"],
                deleted = false

            };
            //check
            if (string.IsNullOrEmpty(a.customer_firstname) ||
                string.IsNullOrEmpty(a.customer_lastname) ||
                string.IsNullOrEmpty(a.phone_number))
            {
                ModelState.AddModelError("customer", "customer info incomplete");
            }
            //int new address

            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=false,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=false,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=false,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=false,Value="KY"}

            };
            ViewBag.Sstate = Sstate;
            address b = new address


            {
              
           //     address_type = form["Item2.address_type"],
                address_type = "Billing",
                address1 = form["Item2.address1"],
                city = form["Item2.city"],
                state = form["state_number"],
                county = "None",
                zipcode = form["Item2.zipcode"],
                deleted = false
            };
     

            //che

            //if (string.IsNullOrEmpty(b.address_type) ||
            //    string.IsNullOrEmpty(b.address1) ||
            //    string.IsNullOrEmpty(b.city) ||
            //    string.IsNullOrEmpty(b.county) ||
            //    string.IsNullOrEmpty(b.zipcode))
            //{
            //    ModelState.AddModelError("address", "address info incomplete");
            //    return View("AddCustomer");
            //}





            //write change to db,address first
      //      if (ModelState.IsValid)
            {
                db.addresses.Add(b);
                db.SaveChanges();
                //linking 2 table
                a.address_number = b.address_number;
                db.customers.Add(a);
                db.SaveChanges();
            }



            return RedirectToAction("AddLeadXX/" + a.customer_number, "Lead");
        }


        public ActionResult CreateLead(int id)
        {
        //    List<customer> a = db.customers.Where(d => d.customer_number == id).ToList();
            return RedirectToAction("AddLeadXX/" + id, "Lead");
        }


        public ActionResult Delete(int id)
        {
            List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
            ViewBag.Customerslist = Customers_list;
            customer target = Customers_list[0];
            target.deleted = true;
            db.SaveChanges();
            return RedirectToAction("ListCustomers");


        }



        //public ActionResult Delete(int id)
        //{
        //    //find target by uid
        //    customer target = db.customers.First(s => s.customer_number == id);
        //    //delete

        //   // db.customers.Remove(target);           
        //   // db.SaveChanges();

        //   // return RedirectToAction("ListCustomers");

        //    List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
        //    ViewBag.Customerslist = Customers_list;
        //    customer target = Customers_list[0];
        //    TryUpdateModel(target, new string[] { "customer_firstname", "customer_middlename", "customer_lastname", "phone_number", "second_phone_number", "email" }, fo.ToValueProvider());
        //    db.SaveChanges();
        //    return View(target);
        //}



    }
}
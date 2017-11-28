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

                return View(db.customers.Where(x => x.customer_lastname.Contains(search) || search == null && x.deleted == false).ToList());
        
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
            ViewBag.m = " The customer was successfully updated on " + System.DateTime.Now;


            return View(target);
        }

        public ActionResult AddCustomer()
        {
            var Sstate = new List<SelectListItem> {
               new SelectListItem() {Text="Alabama", Value="AL"},
        new SelectListItem() { Text="Alaska", Value="AK"},
        new SelectListItem() { Text="Arizona", Value="AZ"},
        new SelectListItem() { Text="Arkansas", Value="AR"},
        new SelectListItem() { Text="California", Value="CA"},
        new SelectListItem() { Text="Colorado", Value="CO"},
        new SelectListItem() { Text="Connecticut", Value="CT"},
        new SelectListItem() { Text="District of Columbia", Value="DC"},
        new SelectListItem() { Text="Delaware", Value="DE"},
        new SelectListItem() { Text="Florida", Value="FL"},
        new SelectListItem() { Text="Georgia", Value="GA"},
        new SelectListItem() { Text="Hawaii", Value="HI"},
        new SelectListItem() { Text="Idaho", Value="ID"},
        new SelectListItem() { Text="Illinois", Value="IL"},
        new SelectListItem() { Text="Indiana", Value="IN"},
        new SelectListItem() { Text="Iowa", Value="IA"},
        new SelectListItem() { Text="Kansas", Value="KS"},
        new SelectListItem() { Text="Kentucky", Value="KY"},
        new SelectListItem() { Text="Louisiana", Value="LA"},
        new SelectListItem() { Text="Maine", Value="ME"},
        new SelectListItem() { Text="Maryland", Value="MD"},
        new SelectListItem() { Text="Massachusetts", Value="MA"},
        new SelectListItem() { Text="Michigan", Value="MI"},
        new SelectListItem() { Text="Minnesota", Value="MN"},
        new SelectListItem() { Text="Mississippi", Value="MS"},
        new SelectListItem() { Text="Missouri", Value="MO"},
        new SelectListItem() { Text="Montana", Value="MT"},
        new SelectListItem() { Text="Nebraska", Value="NE"},
        new SelectListItem() { Text="Nevada", Value="NV"},
        new SelectListItem() { Text="New Hampshire", Value="NH"},
        new SelectListItem() { Text="New Jersey", Value="NJ"},
        new SelectListItem() { Text="New Mexico", Value="NM"},
        new SelectListItem() { Text="New York", Value="NY"},
        new SelectListItem() { Text="North Carolina", Value="NC"},
        new SelectListItem() { Text="North Dakota", Value="ND"},
        new SelectListItem() { Text="Ohio", Value="OH"},
        new SelectListItem() { Text="Oklahoma", Value="OK"},
        new SelectListItem() { Text="Oregon", Value="OR"},
        new SelectListItem() { Text="Pennsylvania", Value="PA"},
        new SelectListItem() { Text="Rhode Island", Value="RI"},
        new SelectListItem() { Text="South Carolina", Value="SC"},
        new SelectListItem() { Text="South Dakota", Value="SD"},
        new SelectListItem() { Text="Tennessee", Value="TN"},
        new SelectListItem() { Text="Texas", Value="TX"},
        new SelectListItem() { Text="Utah", Value="UT"},
        new SelectListItem() { Text="Vermont", Value="VT"},
        new SelectListItem() { Text="Virginia", Value="VA"},
        new SelectListItem() { Text="Washington", Value="WA"},
        new SelectListItem() { Text="West Virginia", Value="WV"},
        new SelectListItem() { Text="Wisconsin", Value="WI"},
        new SelectListItem() { Text="Wyoming", Value="WY"}

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
               new SelectListItem() {Text="Alabama", Value="AL"},
        new SelectListItem() { Text="Alaska", Value="AK"},
        new SelectListItem() { Text="Arizona", Value="AZ"},
        new SelectListItem() { Text="Arkansas", Value="AR"},
        new SelectListItem() { Text="California", Value="CA"},
        new SelectListItem() { Text="Colorado", Value="CO"},
        new SelectListItem() { Text="Connecticut", Value="CT"},
        new SelectListItem() { Text="District of Columbia", Value="DC"},
        new SelectListItem() { Text="Delaware", Value="DE"},
        new SelectListItem() { Text="Florida", Value="FL"},
        new SelectListItem() { Text="Georgia", Value="GA"},
        new SelectListItem() { Text="Hawaii", Value="HI"},
        new SelectListItem() { Text="Idaho", Value="ID"},
        new SelectListItem() { Text="Illinois", Value="IL"},
        new SelectListItem() { Text="Indiana", Value="IN"},
        new SelectListItem() { Text="Iowa", Value="IA"},
        new SelectListItem() { Text="Kansas", Value="KS"},
        new SelectListItem() { Text="Kentucky", Value="KY"},
        new SelectListItem() { Text="Louisiana", Value="LA"},
        new SelectListItem() { Text="Maine", Value="ME"},
        new SelectListItem() { Text="Maryland", Value="MD"},
        new SelectListItem() { Text="Massachusetts", Value="MA"},
        new SelectListItem() { Text="Michigan", Value="MI"},
        new SelectListItem() { Text="Minnesota", Value="MN"},
        new SelectListItem() { Text="Mississippi", Value="MS"},
        new SelectListItem() { Text="Missouri", Value="MO"},
        new SelectListItem() { Text="Montana", Value="MT"},
        new SelectListItem() { Text="Nebraska", Value="NE"},
        new SelectListItem() { Text="Nevada", Value="NV"},
        new SelectListItem() { Text="New Hampshire", Value="NH"},
        new SelectListItem() { Text="New Jersey", Value="NJ"},
        new SelectListItem() { Text="New Mexico", Value="NM"},
        new SelectListItem() { Text="New York", Value="NY"},
        new SelectListItem() { Text="North Carolina", Value="NC"},
        new SelectListItem() { Text="North Dakota", Value="ND"},
        new SelectListItem() { Text="Ohio", Value="OH"},
        new SelectListItem() { Text="Oklahoma", Value="OK"},
        new SelectListItem() { Text="Oregon", Value="OR"},
        new SelectListItem() { Text="Pennsylvania", Value="PA"},
        new SelectListItem() { Text="Rhode Island", Value="RI"},
        new SelectListItem() { Text="South Carolina", Value="SC"},
        new SelectListItem() { Text="South Dakota", Value="SD"},
        new SelectListItem() { Text="Tennessee", Value="TN"},
        new SelectListItem() { Text="Texas", Value="TX"},
        new SelectListItem() { Text="Utah", Value="UT"},
        new SelectListItem() { Text="Vermont", Value="VT"},
        new SelectListItem() { Text="Virginia", Value="VA"},
        new SelectListItem() { Text="Washington", Value="WA"},
        new SelectListItem() { Text="West Virginia", Value="WV"},
        new SelectListItem() { Text="Wisconsin", Value="WI"},
        new SelectListItem() { Text="Wyoming", Value="WY"}

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
                ViewBag.m = " The customer "+a.customer_firstname +" "+a.customer_lastname+ " was successfully created " + "on " + System.DateTime.Now;
            }



            return RedirectToAction("Add/" + a.customer_number, "Lead");
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
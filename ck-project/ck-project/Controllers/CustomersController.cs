using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ck_project.Controllers
{
    public class CustomersController : Controller
    {
             
        //Creating the DB connecton
        ckdatabase db = new ckdatabase();
        // GET: Customers


          public ActionResult ListCustomers(int? page, string search,String msg=null )
    {       try {
                ViewBag.m = msg;
               
                return View(db.customers.Where(x => x.customer_lastname.Contains(search) || search == null && x.deleted == false).ToList().ToPagedList(page ?? 1, 8));

            }
            catch (Exception e)
            {
                ViewBag.m = e.Message;
                return View();
            }
        }



        // read from the DB
        public ActionResult Edit(int id)
        {
            ViewBag.addressnumber1 = db.customers.Where(x => x.customer_number == id).Select(v => v.address_number).First();
            try
            {
                List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
                ViewBag.Customerslist = Customers_list;
                customer target = Customers_list[0];
                ViewBag.id = id;
                return View(target);
            }

            catch (Exception e)
            {
                ViewBag.m = " Something went wrong ... " + e.Message;
                return View();
            }

        }


      //  Write to the DB that is why we use POST
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection fo)
        {
            ViewBag.addressnumber1 = db.customers.Where(x => x.customer_number == id).Select(v => v.address_number).First();
            try
            {
                List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
                ViewBag.Customerslist = Customers_list;
                customer target = Customers_list[0];
                TryUpdateModel(target, new string[] { "customer_firstname", "customer_middlename", "customer_lastname", "phone_number", "second_phone_number", "email" }, fo.ToValueProvider());
                db.SaveChanges();
                ViewBag.m = " The customer was successfully updated on " + System.DateTime.Now;
                return View(target);
            }
            //catch
            //{
            //    ViewBag.m = " Something went wrong... the customer was not updated ... please try again";
            //    return View();
            //}
            catch (Exception e)
            {
                ViewBag.m = " Something went wrong ... " + e.Message;
                return View();
            }


        }

        public ActionResult AddCustomer()
        {
            try
            {
                var Sstate = new List<SelectListItem> {
               new SelectListItem() {Text="Alabama", Value="Alabama"},
        new SelectListItem() { Text="Alaska", Value="Alaska"},
        new SelectListItem() { Text="Arizona", Value="Arizona"},
        new SelectListItem() { Text="Arkansas", Value="Arkansas"},
        new SelectListItem() { Text="California", Value="California"},
        new SelectListItem() { Text="Colorado", Value="Colorado"},
        new SelectListItem() { Text="Connecticut", Value="Connecticut"},
        new SelectListItem() { Text="District of Columbia", Value="District of Columbia"},
        new SelectListItem() { Text="Delaware", Value="Delaware"},
        new SelectListItem() { Text="Florida", Value="Florida"},
        new SelectListItem() { Text="Georgia", Value="Georgia"},
        new SelectListItem() { Text="Hawaii", Value="Hawaii"},
        new SelectListItem() { Text="Idaho", Value="Idaho"},
        new SelectListItem() { Text="Illinois", Value="Illinois"},
        new SelectListItem() { Text="Indiana", Value="Indiana"},
        new SelectListItem() { Text="Iowa", Value="Iowa"},
        new SelectListItem() { Text="Kansas", Value="Kansas"},
        new SelectListItem() { Text="Kentucky", Value="Kentucky"},
        new SelectListItem() { Text="Louisiana", Value="Louisiana"},
        new SelectListItem() { Text="Maine", Value="Maine"},
        new SelectListItem() { Text="Maryland", Value="Maryland"},
        new SelectListItem() { Text="Massachusetts", Value="Massachusetts"},
        new SelectListItem() { Text="Michigan", Value="Michigan"},
        new SelectListItem() { Text="Minnesota", Value="Minnesota"},
        new SelectListItem() { Text="Mississippi", Value="Mississippi"},
        new SelectListItem() { Text="Missouri", Value="Missouri"},
        new SelectListItem() { Text="Montana", Value="Montana"},
        new SelectListItem() { Text="Nebraska", Value="Nebraska"},
        new SelectListItem() { Text="Nevada", Value="Nevada"},
        new SelectListItem() { Text="New Hampshire", Value="New Hampshire"},
        new SelectListItem() { Text="New Jersey", Value="New Jersey"},
        new SelectListItem() { Text="New Mexico", Value="New Mexico"},
        new SelectListItem() { Text="New York", Value="New York"},
        new SelectListItem() { Text="North Carolina", Value="North Carolina"},
        new SelectListItem() { Text="North Dakota", Value="North Dakota"},
        new SelectListItem() { Text="Ohio", Value="Ohio"},
        new SelectListItem() { Text="Oklahoma", Value="Oklahoma"},
        new SelectListItem() { Text="Oregon", Value="Oregon"},
        new SelectListItem() { Text="Pennsylvania", Value="Pennsylvania"},
        new SelectListItem() { Text="Rhode Island", Value="Rhode Island"},
        new SelectListItem() { Text="South Carolina", Value="South Carolina"},
        new SelectListItem() { Text="South Dakota", Value="South Dakota"},
        new SelectListItem() { Text="Tennessee", Value="Tennessee"},
        new SelectListItem() { Text="Texas", Value="Texas"},
        new SelectListItem() { Text="Utah", Value="Utah"},
        new SelectListItem() { Text="Vermont", Value="Vermont"},
        new SelectListItem() { Text="Virginia", Value="Virginia"},
        new SelectListItem() { Text="Washington", Value="Washington"},
        new SelectListItem() { Text="West Virginia", Value="West Virginia"},
        new SelectListItem() { Text="Wisconsin", Value="Wisconsin"},
        new SelectListItem() { Text="Wyoming", Value="Wyoming"}

            };
                ViewBag.Sstate = Sstate;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.m = " Something went wrong ... " + e.Message;
                return View();
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCustomer(FormCollection form)
        {
            try
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
               new SelectListItem() {Text="Alabama", Value="Alabama"},
        new SelectListItem() { Text="Alaska", Value="Alaska"},
        new SelectListItem() { Text="Arizona", Value="Arizona"},
        new SelectListItem() { Text="Arkansas", Value="Arkansas"},
        new SelectListItem() { Text="California", Value="California"},
        new SelectListItem() { Text="Colorado", Value="Colorado"},
        new SelectListItem() { Text="Connecticut", Value="Connecticut"},
        new SelectListItem() { Text="District of Columbia", Value="District of Columbia"},
        new SelectListItem() { Text="Delaware", Value="Delaware"},
        new SelectListItem() { Text="Florida", Value="Florida"},
        new SelectListItem() { Text="Georgia", Value="Georgia"},
        new SelectListItem() { Text="Hawaii", Value="Hawaii"},
        new SelectListItem() { Text="Idaho", Value="Idaho"},
        new SelectListItem() { Text="Illinois", Value="Illinois"},
        new SelectListItem() { Text="Indiana", Value="Indiana"},
        new SelectListItem() { Text="Iowa", Value="Iowa"},
        new SelectListItem() { Text="Kansas", Value="Kansas"},
        new SelectListItem() { Text="Kentucky", Value="Kentucky"},
        new SelectListItem() { Text="Louisiana", Value="Louisiana"},
        new SelectListItem() { Text="Maine", Value="Maine"},
        new SelectListItem() { Text="Maryland", Value="Maryland"},
        new SelectListItem() { Text="Massachusetts", Value="Massachusetts"},
        new SelectListItem() { Text="Michigan", Value="Michigan"},
        new SelectListItem() { Text="Minnesota", Value="Minnesota"},
        new SelectListItem() { Text="Mississippi", Value="Mississippi"},
        new SelectListItem() { Text="Missouri", Value="Missouri"},
        new SelectListItem() { Text="Montana", Value="Montana"},
        new SelectListItem() { Text="Nebraska", Value="Nebraska"},
        new SelectListItem() { Text="Nevada", Value="Nevada"},
        new SelectListItem() { Text="New Hampshire", Value="New Hampshire"},
        new SelectListItem() { Text="New Jersey", Value="New Jersey"},
        new SelectListItem() { Text="New Mexico", Value="New Mexico"},
        new SelectListItem() { Text="New York", Value="New York"},
        new SelectListItem() { Text="North Carolina", Value="North Carolina"},
        new SelectListItem() { Text="North Dakota", Value="North Dakota"},
        new SelectListItem() { Text="Ohio", Value="Ohio"},
        new SelectListItem() { Text="Oklahoma", Value="Oklahoma"},
        new SelectListItem() { Text="Oregon", Value="Oregon"},
        new SelectListItem() { Text="Pennsylvania", Value="Pennsylvania"},
        new SelectListItem() { Text="Rhode Island", Value="Rhode Island"},
        new SelectListItem() { Text="South Carolina", Value="South Carolina"},
        new SelectListItem() { Text="South Dakota", Value="South Dakota"},
        new SelectListItem() { Text="Tennessee", Value="Tennessee"},
        new SelectListItem() { Text="Texas", Value="Texas"},
        new SelectListItem() { Text="Utah", Value="Utah"},
        new SelectListItem() { Text="Vermont", Value="Vermont"},
        new SelectListItem() { Text="Virginia", Value="Virginia"},
        new SelectListItem() { Text="Washington", Value="Washington"},
        new SelectListItem() { Text="West Virginia", Value="West Virginia"},
        new SelectListItem() { Text="Wisconsin", Value="Wisconsin"},
        new SelectListItem() { Text="Wyoming", Value="Wyoming"}

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

                {
                    db.addresses.Add(b);
                    db.SaveChanges();
                    //linking 2 table
                    a.address_number = b.address_number;
                    db.customers.Add(a);
                    db.SaveChanges();  }



                ViewBag.m = " The customer " + a.customer_firstname + " " + a.customer_lastname + " was successfully created " + "on " + System.DateTime.Now;

                return RedirectToAction("Add/" + a.customer_number, "Lead", new { msg = ViewBag.m });

            }
          
            catch (Exception e)
            {
                ViewBag.m = "The customer was not created ... " + e.Message;
                return View();
            }
   
        }


        public ActionResult CreateLead(int id)
        {
            try {
                return RedirectToAction("Add/" + id, "Lead");
            }

            catch (Exception e)
            {
                ViewBag.m = " Something went wrong ... " + e.Message;
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try { 
            List<customer> Customers_list = db.customers.Where(d => d.customer_number == id).ToList();
            ViewBag.Customerslist = Customers_list;
            customer target = Customers_list[0];
            target.deleted = true;
            db.SaveChanges();
                ViewBag.m = "The customer was successfully deleted.";
                return RedirectToAction("ListCustomers", new { search = "", msg = ViewBag.m });
            }

            catch (Exception e)
            {
                ViewBag.m = "The customer was not deleted ..." + e.Message;
                return RedirectToAction("ListCustomers", new { search = "", msg = ViewBag.m });
            }
        }
       
    }
}
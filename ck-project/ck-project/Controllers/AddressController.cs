using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id) {
            int addres_id = (int)db.customers.Where(a => a.customer_number == id).Select(s => s.address_number).FirstOrDefault();
            address target = db.addresses.Where(v => v.address_number == addres_id).FirstOrDefault();
            

            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=false,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=false,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=false,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=false,Value="KY"}

            };
            ViewBag.Sstate = Sstate;

            return View(target);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection fo)
        {

            int addres_id = (int)db.customers.Where(a => a.customer_number == id).Select(s => s.address_number).FirstOrDefault();
            address target = db.addresses.Where(v => v.address_number == addres_id).FirstOrDefault();

            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=false,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=false,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=false,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=false,Value="KY"}

            };
            ViewBag.Sstate = Sstate;

            target.deleted = false;
            target.state = fo["state"];
            TryUpdateModel(target, new string[] { "address_type", "address1","city", "county", "zipcode" }, fo.ToValueProvider());
         
            db.SaveChanges();
            
            return View(target);
        }

        public ActionResult EditL(int id)
        {
            List<address> Address_list = db.addresses.Where(d => d.address_number == id).ToList();
            ViewBag.Addresslist = Address_list;
            address target = Address_list[0];
     
            return View(target);
        }

        [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult EditL(int id, FormCollection fo)
    {

            List<address> Address_list = db.addresses.Where(d => d.address_number == id).ToList();
            ViewBag.Addresslist = Address_list;
            address target = Address_list[0];
            TryUpdateModel(target, new string[] { "address", "city", "state", "county", "zipcode"}, fo.ToValueProvider());
            target.deleted = false;
            target.address_type = "Jobsite";
            db.SaveChanges();
            return View(target);
        }


}
}
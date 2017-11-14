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
            return View(target);

        }
    }
}
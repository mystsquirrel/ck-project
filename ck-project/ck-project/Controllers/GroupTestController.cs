using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class GroupTestController : Controller
    {
        // GET: GroupTest
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            var utype = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            utype.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.city,
                Selected = false,
                Value = a.address_number.ToString()
            }));
            ViewBag.Branches = utype;
            return View();
        }

        public ActionResult Psge2()
        {



            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Psge2(FormCollection form)
        {
            users_types a = new users_types();
            TryUpdateModel(a, new string[] { "user_type_name" }, form.ToValueProvider());
            if (string.IsNullOrEmpty(a.user_type_name))
            {
                ModelState.AddModelError("usertypename", "this field is required");
            }

            if (ModelState.IsValid)
            {
                db.users_types.Add(a);
                db.SaveChanges();
            }
            return View(a);

        }


    }
}
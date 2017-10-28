using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
   
    public class SampleController : Controller
    {
        // GET: sample
        //first create the db connecter
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            List<employee> employee_list = db.employees.ToList();
            ViewBag.employeelist = employee_list;
            return View();
        }
        
        public ActionResult Getemployee(int id) {
            List<employee> employee_list = db.employees.Where(d => d.emp_number==id).ToList();
            ViewBag.employeelist = employee_list;

            return View("Index");
        }

        //get user info in a editable view
        public ActionResult Mofifyemployee(int id) {
            employee target = db.employees.Where(a => a.emp_number == id).FirstOrDefault();
            var utype = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected=false,
                Value=a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text=b.branch_name,
                Selected=false,
                Value=b.branch_number.ToString()
            }));

            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            return View(target);
        }

        [HttpPost]
        public ActionResult Viewemployee(int id=2) {
            employee target = db.employees.Where(e => e.emp_number == id).FirstOrDefault();
            Console.WriteLine(Request.Form["emp_firstname"]);
            return View(target);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection form) {
            //setting dropdown list for forgern key
            var utype = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));
            //setting variable passing
            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            //create instance
            employee target = new employee();
            //get property
            TryUpdateModel(target, new string[] { "emp_firstname", "emp_middlename", "emp_lastname", "emp_username", "user_type_number", "branch_number","emp_number","phone_number" }, form.ToValueProvider());
            //validate
            if (string.IsNullOrEmpty(target.emp_firstname))
                ModelState.AddModelError("firstname", "firstname is required");

            if (ModelState.IsValid) {
                db.employees.Add(target);
                db.SaveChanges();
            }

            return View(target);
        }

        public ActionResult Add() {
            var utype = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> {
                new SelectListItem{ Text="---select---",Selected=true,Value=""}
            };
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));

            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            return View();
        }

        public ActionResult Delete(int id) {
            //find target by uid
            employee target = db.employees.First(s => s.emp_number == id);
            //delete
            db.employees.Remove(target);
            db.SaveChanges();

            return RedirectToAction("index");
        }

    }

}
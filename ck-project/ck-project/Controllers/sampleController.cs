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
            List<employee> employee_list = db.employees.Where(d => d.users_types.user_type_number==1).ToList();
            ViewBag.employeelist = employee_list;
            return View();
        }
        
        public ActionResult Getemployee(int id) {
            List<employee> employee_list = db.employees.Where(d => d.emp_number==id).ToList();
            ViewBag.employeelist = employee_list;

            return View("Index");
        }
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
    }
}
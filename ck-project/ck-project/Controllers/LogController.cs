using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ck_project.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        ckdatabase db = new ckdatabase();
        public ActionResult Index(int? page)
        {
            List<lead_log_file> lis = db.lead_log_file.OrderByDescending(x => x.update_date).Take(300).ToList();
            return View(lis.ToPagedList(page??1,20));
        }
        [HttpPost]
        public ActionResult Index(FormCollection fo) {
            DateTime start =DateTime.Parse( fo["start"]);
            DateTime end = DateTime.Parse(fo["end"]);
            string emp = fo["Emp"];
            string leadname = fo["name"];
            List<lead_log_file> lis = db.lead_log_file.Where(r => r.emp_username == emp && r.update_date >= start.Date && r.update_date<=end.Date).ToList();

            return View(lis);
        }
        public ActionResult Search() {
            var a = new ck_project.Models.Search();
            a.start = DateTime.Today;
            a.end = DateTime.Today.AddDays(1);
            a.Emp = new List<SelectListItem>();
            foreach (var b in db.employees.Where(q => q.deleted == false).ToList()) {
                a.Emp.Add(new SelectListItem {Text=b.emp_firstname+" "+b.emp_lastname, Value=b.emp_username });
            }
            
            return View(a);
        }
        [HttpPost]
        public ActionResult Vlog(FormCollection fo, string order="") {
            ViewBag.order = String.IsNullOrEmpty(order) ? "name" : "";
            ViewBag.date = order == "date" ? "date_d" : "date";
            //default return top 30 changes sort by time
            var logs = from l in db.lead_log_file select l;
            switch (order) {
                case"name":
                    logs = from nlog in logs where (nlog.emp_username == fo["name"]) select nlog;
                    break;
                case "date":
                    logs = logs.OrderByDescending(e => e.update_date);
                    break;
                case "date_d":
                    logs = logs.OrderBy(w => w.update_date);
                    break;
                default:
                    logs = logs.OrderBy(t => t.update_date).Take(30);
                    break;
            }
            return View(logs.ToList());
        }

        public ActionResult Vlog() {
            return View();
        }
    }

    
}
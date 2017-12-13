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


        //string Emp = null, 



        public ActionResult ListLog(int? page, FormCollection fo, string search = null, string Emp = null, string Start = null, string end = null)
        {

            //target.email = form["Item1.email"];
            //int a = Int32.Parse(form["class_number"]);

            //var Emp1 = fo["item.emp_username"];
            //var Emp2 = fo["emp_username"];
            //string Emp3 = fo["item.emp_username"];
            //string Emp4 = fo["emp_username"];
            //var Emp5 = fo["Emp"];
            //string Emp7 = fo["Emp"];
        

            
            //      int a = Int32.Parse(fo["item.emp_username"]);
            //int a2 = Int32.Parse(fo["emp_username"]);

            DateTime start = string.IsNullOrEmpty(Start) ? DateTime.MinValue : DateTime.Parse(Start);
            DateTime end2 = string.IsNullOrEmpty(end) ? DateTime.MaxValue : DateTime.Parse(end);
            TimeSpan ts = new TimeSpan(23, 59, 59);
            end2 = end2.Date + ts;


            try
            {

                var EmpInfo = new List<SelectListItem>();
                EmpInfo.AddRange(db.employees.Select(b => new SelectListItem

                {
                    Text = b.emp_username,
                    Selected = false,
                    Value = b.emp_number.ToString()
                }));
                ViewBag.EmpInfo = EmpInfo;

                var result = db.lead_log_file.Where(l => l.update_date >= start && l.update_date <= end2 &&
                (string.IsNullOrEmpty(search) || l.lead.project_name.Contains(search))
              && (string.IsNullOrEmpty(Emp) || l.emp_username ==Emp)).ToList();


                return View(result.ToPagedList(page ?? 1, 8));
            }
            catch (Exception e)
            {
                ViewBag.m = "Something went wrong ... " + e.Message;
                return View();
            }
        }


        public ActionResult Index(int? page)
        {
            return RedirectToAction("ListLog", "Log");
        }
        [HttpPost]
        public ActionResult Vlog(FormCollection fo, string order = "")
        {
            ViewBag.order = String.IsNullOrEmpty(order) ? "name" : "";
            ViewBag.date = order == "date" ? "date_d" : "date";
            //default return top 30 changes sort by time
            var logs = from l in db.lead_log_file select l;
            switch (order)
            {
                case "name":
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

        public ActionResult Vlog()
        {
            return View();
        }
    }


}
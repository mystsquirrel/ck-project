using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ck_project.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LogController : Controller
    {
        // GET: Log
        ckdatabase db = new ckdatabase();


        //string Emp = null, 



        public ActionResult ListLog(int? page, string search, string Emp, string Start, string end)
        {
            DateTime startDt = string.IsNullOrEmpty(Start) ? DateTime.MinValue : DateTime.Parse(Start);
            DateTime endDt = string.IsNullOrEmpty(end) ? DateTime.MaxValue : DateTime.Parse(end);
            TimeSpan ts = new TimeSpan(23, 59, 59);
            endDt = endDt.Date + ts;


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

                string employeeUserName = null;
                if (!string.IsNullOrEmpty(Emp))
                {
                    int employeeNbr = Int32.Parse(Emp);
                    var employee = db.employees.Where(e => e.emp_number == employeeNbr).First();
                    employeeUserName = employee.emp_username;
                }


                var result = db.lead_log_file.Where(l => l.update_date >= startDt && l.update_date <= endDt
                                                    && (string.IsNullOrEmpty(search) || l.lead.project_name.Contains(search))
                                                    && (string.IsNullOrEmpty(employeeUserName) || l.emp_username == employeeUserName)).ToList();


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
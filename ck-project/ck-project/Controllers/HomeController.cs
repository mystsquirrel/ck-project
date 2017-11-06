using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MainPage()
        {
            var lead = (from l in db.leads where l.emp_number == 1 orderby l.Last_update_date select l).Take(10);
            return View(lead);
        }

        public ActionResult LeadPage()
        {
            return View();
        }
        
        public ActionResult LeadTab()
        {
            return View();
        }

        public ActionResult PrintTab()
        {
            return View();
        }
    }
}
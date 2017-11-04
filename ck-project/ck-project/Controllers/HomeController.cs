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

        [HttpPost]
        public ActionResult MainPage(int employeeNbr)
        {
            //List<lead> leadList = db.leads.Where(l => l.emp_number == employeeNbr).ToList()
            return View(db.leads.ToList());
        }

        public ActionResult LeadPage()
        {
            return View();
        }

        public ActionResult LeadTab()
        {
            return View();
        }

        public ActionResult PrintHub()
        {
            return View();
        }
    }
}
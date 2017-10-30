using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
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

        public ActionResult DisplayProjects()
        {
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
    }
}
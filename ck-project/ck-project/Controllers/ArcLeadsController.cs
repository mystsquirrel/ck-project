using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class ArcLeadsController : Controller
    {

        //Creating the DB connecton
        ckdatabase db = new ckdatabase();


        // GET: ArcLeads
        public ActionResult ListArcLead(string search = null)
        {
            try
            {
                return View(db.archive_leads.Where(x => x.project_name.Contains(search) || search == null).ToList());
            }
            catch
            {
                ViewBag.m = " Something went wrong ... please try again";
                return View();
            }
        }


        public ActionResult Details(int id)
        {
            try
            {
                return View(db.archive_leads.Where(x => x.lead_number == id).ToList());
            }
            catch
            {
                ViewBag.m = " Something went wrong ... please try again";
                return View();
            }
        }


    }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        ckdatabase db = new ckdatabase();
        // GET: Admin
        public ActionResult AdminMainPage()
        {
            var lead = (from l in db.leads.Take(10) where l.deleted == false orderby l.Last_update_date select l);
            return View(lead);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ckdatabase db = new ckdatabase();
        // GET: Admin
        public ActionResult AdminMainPage()
        {
            var lead = (from l in db.leads orderby l.Last_update_date select l).Take(10);
            return View(lead);
        }
    }
}
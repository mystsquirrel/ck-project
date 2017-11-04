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
            return View(db.leads.ToList());
        }
    }
}
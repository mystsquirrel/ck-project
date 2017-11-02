using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            return View();
        }
    }
}
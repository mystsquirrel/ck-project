using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class InstallController : Controller
    { // GET: Install
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lis(int lid = 9)
        {
            installation target = db.installations.Where(a => a.lead_number == lid).FirstOrDefault();
            ViewBag.mainc = new List<SelectListItem>
            {
               new SelectListItem{ Text="Kitchen",Value="kitchen"},
               new SelectListItem{ Text="Framing",Value="Framing"},
               new SelectListItem{ Text="Doors&Windows",Value="Doors_Windows"},
               new SelectListItem{ Text="Mechanicals",Value="Mechanicals"},
               new SelectListItem{ Text="Electrical",Value="Electrical"},
               new SelectListItem{ Text="Finish",Value="Finish"},
               new SelectListItem{ Text="Cabinetry",Value="Cabinetry"},
               new SelectListItem{ Text="Countertops",Value="Countertops"},
               new SelectListItem{ Text="Appliance/Fixture Install",Value="Appliance"},
               new SelectListItem{ Text="372",Value="372"},
               new SelectListItem{ Text="BATH",Value="BATH"},
               new SelectListItem{ Text="MISC",Value="MISC"}
            };

            ViewBag.lid = lid;

            ViewBag.kitchen = new List<SelectListItem>
            {
                new SelectListItem{ Text="Appliance & Sink Demo",Value="asd"},
                 new SelectListItem{ Text="Cabinet & Top Demo",Value="ctd"},
                  new SelectListItem{ Text="Millwork Demo",Value="md"},
                   new SelectListItem{ Text="Floor - Ceiling - Wall Covering Demo",Value="fcwcd"},
                    new SelectListItem{ Text="Plumbing Demo",Value="pd"},
                     new SelectListItem{ Text="Electrical Demo",Value="ed"},
                      new SelectListItem{ Text="HVAC Demo",Value="hd"},
                       new SelectListItem{ Text="Windows & Door Demo",Value="wdd"},
                        new SelectListItem{ Text="Wood Framing Demo",Value="wfd"},
                         new SelectListItem{ Text="Misc Demo",Value="mid"}
            };

            ViewBag.framing = new List<SelectListItem> {
                new SelectListItem{ Text="Wall Framing",Value=""},
                new SelectListItem{ Text="Ceiling Framing",Value=""},
                new SelectListItem{ Text="Floor Framing",Value=""},
                new SelectListItem{ Text="Insulation",Value=""}
            };

            ViewBag.doorswindows = new List<SelectListItem> {
                new SelectListItem{Text="Doors",Value="Doors" },
                new SelectListItem{Text="Windows",Value="Windows" }
            };

            ViewBag.mech = new List<SelectListItem> {
                new SelectListItem{Text="Plumbing(Rough-In)",Value="plumbing" },
                new SelectListItem{Text="HVAC",Value="hvac" }
            };

            ViewBag.elect = new List<SelectListItem> {
                new SelectListItem{Text="Wiring/Device",Value="WD" },
                new SelectListItem{Text="Lighting",Value="lighting" }
            };

            ViewBag.finish = new List<SelectListItem> {
                new SelectListItem{Text="Ceiling and Walls",Value="caw" },
                new SelectListItem{Text="Flooring",Value="flooring" },
                new SelectListItem{Text="Millwork",Value="millwork" }
            };

            ViewBag.cabin = new List<SelectListItem> {
                new SelectListItem{Text="Cabinetry & Trim",Value="ct" }
            };

            ViewBag.counter = new List<SelectListItem> {
                new SelectListItem{Text="Countertop Installation",Value="CI" }
            };

            ViewBag.appliance = new List<SelectListItem> {
                new SelectListItem{ Text="Appliance",Value="app"}
            };
            //372
            ViewBag.tbd = new List<SelectListItem> {
                new SelectListItem{ Text="Granite & Solid Surface Tops",Value="gsst"}
            };

            ViewBag.bath = new List<SelectListItem> {
                new SelectListItem{ Text="Demo",Value="demo"},
                new SelectListItem{ Text="Wiring/Drvice",Value="wd"},
                new SelectListItem{ Text="Walls/Finish",Value="wf"},
                new SelectListItem{ Text="Flooring",Value="flooring"},
                new SelectListItem{ Text="Plumbing",Value="plumbing"},
                new SelectListItem{Text="Cabinetry & Trim",Value="ct" },
                new SelectListItem{Text="Countertop Installation",Value="CI" },
                new SelectListItem{Text="Lighting & Fixtures",Value="lighting" },
                new SelectListItem{Text="Millwork",Value="Millwork" }
            };

            ViewBag.misc = new List<SelectListItem> {
                new SelectListItem{ Text="Disclaimers",Value="disclaimers"}
            };

            

            return View(target);


        }
        public ActionResult custT()
        {
            task ntask = new task();
            return View();

        }

        [HttpPost]
        public ActionResult cusT(FormCollection fo)
        {
            int lid = int.Parse(fo["lid"]);


            return RedirectToAction("lis", new { lid = lid });
        }

        public ActionResult AddJob(string maincat,string subcat) {
            task temp = new task();
            temp.task_main_cat = maincat;
            temp.task_sub_cat = subcat;
           


            return PartialView();

        }

        public ActionResult readtask(int lid,string maincat,string subcat) {
            installation ins = db.installations.Where(a => a.lead_number == lid).First();
            int ind = ins.installation_number;
            List<int> target = ins.tasks_installation.Where(c => c.installation_number == ind).Select(d => d.task_number).ToList();
            List<task> task = new List<task>();
            try
            {
                foreach (int a in target)
                {
                    task.Add(db.tasks.Single(x => x.task_number == a && string.Equals(x.task_main_cat, maincat) && string.Equals(x.task_sub_cat, subcat)));
                }
            } catch (Exception e) {
                ViewBag.error = e.Message;
            }
           

            return PartialView(task);
        }

        [HttpPost]
        public ActionResult savetask(FormCollection fo) {

            return null;
        }

       
    }
}
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
                new SelectListItem{ Text="Wall Framing",Value="WF"},
                new SelectListItem{ Text="Ceiling Framing",Value="CF"},
                new SelectListItem{ Text="Floor Framing",Value="FF"},
                new SelectListItem{ Text="Insulation",Value="ins"}
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
                new SelectListItem{ Text="Appliances And Fixtures Installation",Value="app"}
            };
            //372
            ViewBag.tbd = new List<SelectListItem> {
                new SelectListItem{ Text="Granite & Solid Surface Tops",Value="gsst"}
            };

            ViewBag.bath = new List<SelectListItem> {
                new SelectListItem{ Text="Demo",Value="demo"},
                new SelectListItem{ Text="Wiring/Devices",Value="wd"},
                new SelectListItem{ Text="Walls/Finishes",Value="wf"},
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

        public ActionResult AddJob(string maincat,string subcat,int insnum) {
            List<task> option = db.tasks.Where(x => x.special_task == false && x.task_main_cat == maincat && x.task_sub_cat == subcat).ToList();
            List<SelectListItem> taskname = new List<SelectListItem>();
            foreach (task a in option) {
                taskname.Add(new SelectListItem { Text = a.task_name, Value =a.task_number.ToString() });
            }
            ViewBag.taskname = taskname;
            ViewBag.insnum = insnum;
           


            return PartialView();

        }

        [HttpPost]
        public ActionResult handler(FormCollection fo) {
            tasks_installation target = new tasks_installation();
            int lid =int.Parse(fo["installation_number"]);
            target.hours = float.Parse(fo["hours"]);
            target.task_number = int.Parse(fo["task_number"]);
            target.m_cost = float.Parse(fo["m_cost"]);



            return RedirectToAction("lis", new { lid = lid });
        }

        public ActionResult readtask(int lid,string maincat,string subcat) {
            List<tasks_installation> taskset = db.leads.Where(l => l.lead_number == lid).First().installations.First().tasks_installation.ToList();
            taskset = taskset.FindAll(x => x.task.task_main_cat == maincat && x.task.task_sub_cat == subcat).ToList();
            ViewBag.maincat = maincat;
            ViewBag.subcat = subcat;
            return PartialView(taskset);
        }
        
        [HttpPost]
        public ActionResult savetask(FormCollection fo) {
            
            return null;
        }

        public ActionResult Delete(int tin,int lid) {


            return RedirectToAction("lis", new { lid = lid });
        }

       
    }
}
using ck_project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ck_project.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //IEnumerable<Claim> claims = identity.Claims;
            ViewBag.uid = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.role = identity.FindFirst(ClaimTypes.Role).Value;
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
            var identity = (ClaimsIdentity)User.Identity;
            var currUserIDStr = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int currUserID = Int32.Parse(currUserIDStr);
                var leadList = (from l in db.leads.Take(10) where l.emp_number == currUserID && l.deleted == false orderby l.Last_update_date select l);
                return View(leadList);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return View();
        }

        public ActionResult LeadPage(int id, bool edit)
        {
            ViewBag.leadNumber = id;
            ViewBag.editable = edit;
            return View(ViewBag);           
        }

        public ActionResult LeadTab(int id, bool edit)
        {
            return View();
        }

        public ActionResult PrintTab(int id)
        {
            ViewBag.leadNumber = id;
            return View(ViewBag);
        }

        public ActionResult SummaryTab(int id)
        {
            if (id != 0)
            {
                var lead = db.leads.Where(l => l.lead_number == id).FirstOrDefault();
                var projSummary = new ProjectSummary
                {
                    Lead = lead,
                    TotalCost = db.total_cost.Single(c => c.lead_number == id)
                };

                foreach (var item in lead.installations)
                {
                    double catCost = 0;
                    double catHours = 0;
                    double catMaterialCost = 0;

                    //get category list
                    ArrayList catList = new ArrayList();
                    foreach (var task in item.tasks_installation)
                    {
                        string currCat = task.task.task_main_cat;
                        if (!catList.Contains(currCat)) {
                            catList.Add(currCat);
                        }                         
                    }

                    Dictionary<string, List<double>> installTasks = new Dictionary<string, List<double>>();
                    List<double> costList = new List<double>();
                    //calculate each category total cost and hours
                    foreach (string cat in catList)
                    {
                        foreach (var task in item.tasks_installation)
                        {
                            string taskCat = task.task.task_main_cat;
                            if (cat.Equals(taskCat))
                            {
                                catCost += (task.labor_cost * task.hours) + task.m_cost;
                                catHours += task.hours;
                                catMaterialCost += task.m_cost;
                                costList.Add(catHours);
                                costList.Add(catMaterialCost);
                                costList.Add(catCost);
                            }
                        }
                        installTasks.Add(cat, costList);
                        projSummary.InstallCategories.Add(installTasks);
                    }
                }
                return View(projSummary);
            }
            else
            {
                return View();
            }
        }
    }
}
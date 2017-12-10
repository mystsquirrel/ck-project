using ck_project.Helpers;
using ck_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static List<lead> result=new List<lead>();
        ckdatabase db = new ckdatabase();
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewBag.uid = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.role = identity.FindFirst(ClaimTypes.Role).Value;
            return View();
        }

        public ActionResult MainPage(string search, string searchby)
        {
            var projStatus = new List<SelectListItem>();
            projStatus.AddRange(db.project_status.Where(ps => ps.project_status_name != "closed").Select(b => new SelectListItem
            {
                Text = b.project_status_name,
                Selected = false,
                Value = b.project_status_number.ToString()
            }));
            ViewBag.projStatus = projStatus;

            var identity = (ClaimsIdentity)User.Identity;
            var currUserIDStr = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int currUserID = Int32.Parse(currUserIDStr);
                if (searchby == "Status" && search != "")
                {
                    var result = (from l in db.leads
                                  join s in db.project_status on l.project_status_number equals s.project_status_number
                                  where l.emp_number == currUserID && l.deleted == false && s.project_status_name != "Closed" && s.project_status_name.StartsWith(search)
                                  orderby l.Last_update_date
                                  select l).ToList();
                   HomeController.result = result;
                    return View(result);
                }
                else
                {
                    var result = (from l in db.leads
                                  join s in db.project_status on l.project_status_number equals s.project_status_number
                                  where l.emp_number == currUserID && l.deleted == false && s.project_status_name != "Closed"
                                  orderby l.Last_update_date
                                  select l).ToList();
                    HomeController.result = result;
                    return View(result);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return View();
        }

        public ActionResult ProjPrint(int id)
        {
            // only recalculate if lead is not close
            if (id != 0)
            {
                var lead = db.leads.Where(l => l.lead_number == id).First();
                if (lead != null && !lead.project_status.project_status_name.Equals(Constants.proj_Status_Closed, StringComparison.OrdinalIgnoreCase))
                {
                    new GeneralHelper().SaveProjectTotal(lead.lead_number);
                }
                ViewBag.lead = lead;
            }

            ViewBag.leadNumber = id;
            return View();
        }

        public ActionResult ProjSummary(int id)
        {
            var projSummary = new ProjectSummary();
            if (id != 0)
            {
                var lead = db.leads.Where(l => l.lead_number == id).First();

                if (lead != null)
                {
                    // only recalculate if lead is not close
                    if (!lead.project_status.project_status_name.Equals(Constants.proj_Status_Closed, StringComparison.OrdinalIgnoreCase))
                    {
                        new GeneralHelper().SaveProjectTotal(lead.lead_number);
                    }

                    lead = db.leads.Where(l => l.lead_number == id).First();
                    ProjSummaryHelper projSummaryHelper = new ProjSummaryHelper();
                    if (db.total_cost.Where(c => c.lead_number == id).Any())
                    {
                        projSummary.TotalCost = db.total_cost.Where(c => c.lead_number == id).First();
                    }
                    projSummary = projSummaryHelper.CalculateInstallCategoryCostMap(lead, projSummary);
                    projSummary = projSummaryHelper.GetProductCategoryList(lead, projSummary);
                    projSummary = projSummaryHelper.CalculateInstallationsData(lead, projSummary);
                    projSummary = projSummaryHelper.SetCustomerData(lead, projSummary);
                    projSummary = projSummaryHelper.SetAddresses(lead, projSummary);
                    projSummary.ProductTotalMap = projSummaryHelper.GetProductTotalMap(lead);
                    projSummary.Lead = lead;
                }
            }

            return View(projSummary);
        }

        public ActionResult Sort(string by) {
            List<lead> pro = new List<lead>();
            switch (by)
            {
                case "pn":
                    pro =HomeController.result.OrderBy(a => a.project_name).ToList();
                        break;
                case "cu":
                    pro = HomeController.result.OrderBy(a => a.customer.customer_firstname).ToList();
                    break;
                case "pt":
                    pro = HomeController.result.OrderBy(a => a.project_type_number).ToList();
                    break;
                case "cd":
                    pro = HomeController.result.OrderBy(a => a.lead_date).ToList();
                    break;
                case "lmd":
                    pro = HomeController.result.OrderBy(a => a.Last_update_date).ToList();
                    break;
                case "de":
                   pro = HomeController.result.OrderBy(a => a.employee.emp_firstname).ToList();
                    break;

            }

            return View("Mainpage", pro);
        }
    }
}
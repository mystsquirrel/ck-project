using ck_project.Helpers;
using ck_project.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    [Authorize]
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

        public ActionResult MainPage(string search, string searchby)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var currUserIDStr = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                int currUserID = Int32.Parse(currUserIDStr);
                if (searchby == "Status" && search != "")
                {
                    var result = (from l in db.leads.Take(10)
                                  join s in db.project_status on l.project_status_number equals s.project_status_number
                                  where (l.emp_number == currUserID && l.deleted == false)
                                  where (s.project_status_name != "Closed" && s.project_status_name == search)
                                  orderby l.Last_update_date
                                  select l);
                    return View(result);
                }
                else
                {
                    var result = (from l in db.leads.Take(10)
                                  join s in db.project_status on l.project_status_number equals s.project_status_number
                                  where (l.emp_number == currUserID && l.deleted == false)
                                  where s.project_status_name != "Closed"
                                  orderby l.Last_update_date
                                  select l);
                    return View(result);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return View();
        }

        public ActionResult LeadPage(int id, int cid, bool edit)
        {
            ViewBag.leadNumber = id;
            ViewBag.customerNumber = cid;
            ViewBag.editable = edit;
            return View();           
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

        public ActionResult SummaryTab(int id, int cid)
        {
            var projSummary = new ProjectSummary();
            if (id != 0)
            {
                var lead = db.leads.Where(l => l.lead_number == id).FirstOrDefault();
                if (lead != null)
                {
                    projSummary.Lead = lead;
                    projSummary.Customer = lead.customer;
                    projSummary.TotalCost = db.total_cost.Where(c => c.lead_number == id).FirstOrDefault();

                    projSummary = new ProjSummaryHelper().CalculateInstallCategoryCostMap(lead, projSummary);
                    projSummary.ProductTotalMap = new ProjSummaryHelper().GetProductTotalMap(lead);
                }
            }
            else if (cid != 0)
            {
                var customer = db.customers.Where(c => c.customer_number == cid).FirstOrDefault();
                projSummary.Customer = customer;
            }
            return View(projSummary);
        }
    }
}
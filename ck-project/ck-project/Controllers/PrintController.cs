using ck_project.Models;
using System.Linq;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {
        ckdatabase db = new ckdatabase();
        // GET: Printout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Proposal_Page1()
        {
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(l => l.lead_number == 9).FirstOrDefault(),
                Branch = db.branches.ToList()
            };
            return View(proposal);
        }

        public ActionResult Proposal_Page2()
        {
            var totalCost = db.total_cost.Where(c => c.lead_number == 9).FirstOrDefault();
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == 9).FirstOrDefault()
            };

            if (totalCost != null)
            {
                proposal.TotalCost = totalCost;
                proposal.AmtDueAtSignProposal = (double)totalCost.total_cost1 * 0.5;
                proposal.AmtDueUponStartWork = (double)totalCost.total_cost1 * 0.4;
                proposal.AmtDueUponCompletion = (double)totalCost.total_cost1 * 0.1;
            }
            return View(proposal);
        }

        public ActionResult Proposal_Page3()
        {
            return View();
        }

        public ActionResult Proposal_Page4()
        {
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == 9).FirstOrDefault()
            };
            return View(proposal);
        }

        public ActionResult Proposal_Page1Copy()
        {
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(l => l.lead_number == 9).FirstOrDefault(),
                Branch = db.branches.ToList()
            };
            return View(proposal);
        }

        public ActionResult PrintProposal()
        {
            var totalCost = db.total_cost.Where(c => c.lead_number == 9).FirstOrDefault();
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == 9).FirstOrDefault(),
                Branch = db.branches.ToList()
            };

            if (totalCost != null)
            {
                proposal.TotalCost = totalCost;
                proposal.AmtDueAtSignProposal = (double)totalCost.total_cost1 * 0.5;
                proposal.AmtDueUponStartWork = (double)totalCost.total_cost1 * 0.4;
                proposal.AmtDueUponCompletion = (double)totalCost.total_cost1 * 0.1;
            }
            return new Rotativa.ViewAsPdf("Proposal", proposal)
            { FileName = "Proposal.pdf",
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                MinimumFontSize = 10,
                PageMargins = { Left = 7, Right = 7 },
                CustomSwitches = "--disable-smart-shrinking"

            };
        }

        public ActionResult PrintProposal2(int id)
        {
            var totalCost = db.total_cost.Where(c => c.lead_number == id).FirstOrDefault();
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == id).FirstOrDefault(),
                Branch = db.branches.ToList()
            };

            if (totalCost != null)
            {
                proposal.TotalCost = totalCost;
                proposal.AmtDueAtSignProposal = (double)totalCost.total_cost1 * 0.5;
                proposal.AmtDueUponStartWork = (double)totalCost.total_cost1 * 0.4;
                proposal.AmtDueUponCompletion = (double)totalCost.total_cost1 * 0.1;
            }
            return new Rotativa.ViewAsPdf("Proposal2", proposal)
            {
                FileName = "Proposal.pdf",
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                MinimumFontSize = 10,
                PageMargins = { Left = 7, Right = 7 },
                CustomSwitches = "--disable-smart-shrinking"

            };
        }

        public ActionResult PrintSoldJobNotice(int id)
        {
            ViewBag.Lead = db.leads.Where(c => c.lead_number == id).FirstOrDefault();
            ViewBag.TotalCost = db.total_cost.Where(c => c.lead_number == id).FirstOrDefault();

            return new Rotativa.ViewAsPdf("SoldJobNotice", ViewBag)
            {
                FileName = "SoldJobNotice.pdf",
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                MinimumFontSize = 10,
                PageMargins = { Left = 7, Right = 7 },
                CustomSwitches = "--disable-smart-shrinking"

            };
            //           return new RazorPDF.PdfActionResult("SoldJobNotice", ViewBag);
        }
    }
}
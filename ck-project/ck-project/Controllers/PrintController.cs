using ck_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
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
                Lead = db.leads.Where(l => l.lead_number == 1).FirstOrDefault(),
                Branch = db.branches.ToList()
            };
            return View(proposal);
        }

        public ActionResult Proposal_Page2()
        {
            var totalCost = db.total_cost.Where(c => c.lead_number == 1).FirstOrDefault();
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == 1).FirstOrDefault()
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
            return View();
        }

        public ActionResult PrintProposal()
        {
            var totalCost = db.total_cost.Where(c => c.lead_number == 1).FirstOrDefault();
            var proposal = new ProposalViewModel
            {
                Lead = db.leads.Where(c => c.lead_number == 1).FirstOrDefault(),
                Branch = db.branches.ToList()
            };

            if (totalCost != null)
            {
                proposal.TotalCost = totalCost;
                proposal.AmtDueAtSignProposal = (double)totalCost.total_cost1 * 0.5;
                proposal.AmtDueUponStartWork = (double)totalCost.total_cost1 * 0.4;
                proposal.AmtDueUponCompletion = (double)totalCost.total_cost1 * 0.1;
            }
            return new Rotativa.ViewAsPdf("Proposal", proposal) { FileName = "Proposal.pdf" };
        }
    }
}
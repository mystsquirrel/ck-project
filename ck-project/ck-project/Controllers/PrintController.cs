using ck_project.Models;
using SelectPdf;
using System;
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
            return View(ViewBag);
            //return new Rotativa.ViewAsPdf("SoldJobNotice", ViewBag)
            //{
            //    FileName = "SoldJobNotice.pdf",
            //    PageOrientation = Rotativa.Options.Orientation.Portrait,
            //    PageSize = Rotativa.Options.Size.A4,
            //    MinimumFontSize = 10,
            //    PageMargins = { Left = 7, Right = 7 },
            //    CustomSwitches = "--disable-smart-shrinking"

            //};
            //           return new RazorPDF.PdfActionResult("SoldJobNotice", ViewBag);
        }

        [HttpPost]
        public ActionResult Convert(FormCollection collection)
        {
            // read parameters from the webpage
            string url = collection["TxtUrl"];
            string pdf_page_size = collection["DdlPageSize"];
            PdfPageSize pageSize = (PdfPageSize)System.Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            string pdf_orientation = collection["DdlPageOrientation"];
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(
                typeof(PdfPageOrientation), pdf_orientation, true);  

            int webPageWidth = 1024;
            try
            {
                webPageWidth = System.Convert.ToInt32(collection["TxtWidth"]);
            }
            catch { }

            int webPageHeight = 0;
            try
            {
                webPageHeight = System.Convert.ToInt32(collection["TxtHeight"]);
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }
    }
}
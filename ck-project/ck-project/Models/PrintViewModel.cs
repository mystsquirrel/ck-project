using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ck_project.Models
{
    public class ProposalViewModel
    {
        public lead Lead { get; set; }
        public List<branch> Branch { get; set; }
        public double AmtDueAtSignProposal { get; set; }
        public double AmtDueUponStartWork { get; set; }
        public double AmtDueUponCompletion { get; set; }
        public total_cost TotalCost { get; set; }

    }
}
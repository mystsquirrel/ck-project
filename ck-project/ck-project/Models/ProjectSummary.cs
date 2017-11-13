using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ck_project.Models
{
    public class ProjectSummary
    {
        public lead Lead { get; set; }
        public total_cost TotalCost { get; set; }
        public List<Dictionary<string, List<double>>> InstallCategories { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ck_project.Helpers
{
    public class TaxCalculationHelper
    {
        ckdatabase db = new ckdatabase();
        public double CalculateContractTotalWithoutOperationCost(int leadNbr)
        {
            double contractTotal = 0;
            var totalcosts = db.total_cost.Where(c => c.lead_number == leadNbr).First();
            if (totalcosts != null && totalcosts.product_cost != null)
            {
                contractTotal += (double)totalcosts.product_cost;
            }

            return contractTotal;  
        }
    }
}
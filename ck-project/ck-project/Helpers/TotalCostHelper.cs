using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ck_project.Helpers
{
    public class TotalCostHelper
    {
        public double CalculateProductCost(lead lead)
        {
            double totalProductCost = 0;
            if (lead.products != null)
            {
                foreach (var item in lead.products)
                {
                    totalProductCost += item.price * item.quantity;
                }
            }

            return totalProductCost;
        }

        public double CalculateInstallationCost(lead lead)
        {
            double totalInstallCost = 0;
            if (lead.installations != null)
            {
                foreach (var item in lead.installations)
                {
                    if (item.total_installation_labor_cost != null)
                    {
                        totalInstallCost += (double)item.total_installation_labor_cost;
                    }
                }
            }

            return totalInstallCost;
        }

        public double CalculateApplicableTax(lead lead)
        {
            TaxCalculationHelper taxHelper = new TaxCalculationHelper(lead);
            if (!lead.tax_exempt)
            {
                return taxHelper.CalculateStateTax() + taxHelper.CalculateCountyTax() + taxHelper.CalculateMunicipalTax() + taxHelper.CalculateBOTax();
            }

            return 0;
        }

        public double CalculateBuildingPermitCost(lead lead)
        {
            double totalInstallCost = 0;
            if (lead.installations != null)
            {
                foreach (var item in lead.installations)
                {
                    if (item.building_permit_cost != null)
                    {
                        totalInstallCost += (double)item.building_permit_cost;
                    }
                }
            }

            return totalInstallCost;
        }
    }
}
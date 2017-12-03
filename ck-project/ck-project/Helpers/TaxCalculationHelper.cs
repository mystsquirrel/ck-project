using System;
using System.Linq;

namespace ck_project.Helpers
{
    public class TaxCalculationHelper
    {
        ckdatabase db = new ckdatabase();
        static DateTime today = DateTime.Now;
        static DateTime date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
        FeeCalculationHelper feeHelper = new FeeCalculationHelper();
        public double CalculateStateTax(lead lead)
        {
            // not a installed project
            double stateTax = 0;
            if (lead.address != null)
            {
                string state = lead.address.state;
                if (state != null)
                {
                    var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "County Tax" && today <= t.end_date && today >= t.start_date && t.state == state).First();
                    if (taxData != null)
                    {
                        if (lead.delivery_status_number == 1)
                        {
                            // use tax
                            stateTax = taxData.tax_value / 100 * feeHelper.CalculateAvgMaterialCost(lead);
                        }
                        else
                        {
                            //sale tax
                            stateTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                        }
                    }
                }
            }

            return stateTax;
        }

        public double CalculateCountyTax(lead lead)
        {
            // not a installed project
            double countyTax = 0;
            if (lead.address != null)
            {
                string state = lead.address.state;
                string county = lead.address.county;
                if (state != null && county != null)
                {
                    var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "County Tax" && today <= t.end_date && today >= t.start_date && t.state == state && t.county == county).First();
                    if (taxData != null)
                    {
                        if (lead.delivery_status_number == 1)
                        {
                            // installed project
                            countyTax = taxData.tax_value / 100 * feeHelper.CalculateAvgProjectCost(lead);
                        }
                        else if (lead.delivery_status_number == 4)
                        {
                            //delivery project
                            countyTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                        }
                    }
                }
            }

            return countyTax;
        }

        public double CalculateMunicipalTax(lead lead)
        {
            // not a installed project
            double municipalTax = 0;
            if (lead.address != null)
            {
                string state = lead.address.state;
                string city = lead.address.city;
                if (state != null && city != null)
                {
                    var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "City Tax" && today <= t.end_date && today >= t.start_date && t.state == state && t.city == city).First();
                    if (taxData != null)
                    {
                        if (lead.in_city)
                        {
                            // use tax - only apply to installed project
                            // sale tax - apply to non-installed project
                            if (lead.delivery_status_number == 1)
                            {
                                municipalTax = taxData.tax_value / 100 * feeHelper.CalculateAvgMaterialCost(lead);
                            }
                            else
                            {
                                municipalTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                            }
                        }
                        else
                        {
                            // sale tax - only apply to pick-up project
                            if (lead.delivery_status_number == 2)
                            {
                                municipalTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                            } 
                        }
                    }
                }
            }

            return municipalTax;
        }

        public double CalculateBOTax(lead lead)
        {
            // not a installed project
            double boTax = 0;
            if (lead.address != null)
            {
                string state = lead.address.state;
                string city = lead.address.city;
                if (state != null && city != null)
                {
                    var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "B&O Tax" && today <= t.end_date && today >= t.start_date && t.state == state && t.city == city).First();
                    if (taxData != null)
                    {
                        if (lead.in_city && lead.delivery_status_number == 1)
                        {
                            // installed project
                            boTax = taxData.tax_value / 100 * feeHelper.CalculateTotalProjForBO(lead);
                        }
                    }
                }
            }

            return boTax;
        }
    }
}
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
                    if (db.taxes.Where(t => t.deleted == false && t.tax_anme == "State Tax" && date <= t.end_date && date >= t.start_date && t.state == state).Any())
                    {
                        var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "State Tax" && date <= t.end_date && date >= t.start_date && t.state == state).First();
                        if (taxData != null)
                        {
                            if (lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Installed))
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
            }

            return Math.Round(stateTax, 2);
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
                    if (db.taxes.Where(t => t.deleted == false && t.tax_anme == "County Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.county == county).Any())
                    {
                        var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "County Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.county == county).First();
                        if (taxData != null)
                        {
                            if (lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Installed))
                            {
                                // installed project
                                countyTax = taxData.tax_value / 100 * feeHelper.CalculateAvgProjectCost(lead);
                            }
                            else if (lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Delivery))
                            {
                                //delivery project
                                countyTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                            }
                        }
                    }
                }
            }

            return Math.Round(countyTax, 2);
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
                    if (db.taxes.Where(t => t.deleted == false && t.tax_anme == "City Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.city == city).Any())
                    {
                        var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "City Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.city == city).First();
                        if (taxData != null)
                        {
                            if (lead.in_city)
                            {
                                // use tax - only apply to installed project
                                // sale tax - apply to non-installed project
                                if (lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Installed))
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
                                if (lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Pickup))
                                {
                                    municipalTax = taxData.tax_value / 100 * feeHelper.CalculateRetailTotalOfAllMaterials(lead);
                                }
                            }
                        }
                    }
                }
            }

            return Math.Round(municipalTax, 2);
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
                    if (db.taxes.Where(t => t.deleted == false && t.tax_anme == "B&O Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.city == city).Any())
                    {
                        var taxData = db.taxes.Where(t => t.deleted == false && t.tax_anme == "B&O Tax" && date <= t.end_date && date >= t.start_date && t.state == state && t.city == city).First();
                        if (taxData != null)
                        {
                            if (lead.in_city && lead.delivery_status.delivery_status_name.Equals(Constants.deliver_Status_Installed))
                            {
                                // installed project
                                boTax = taxData.tax_value / 100 * feeHelper.CalculateTotalProjForBO(lead);
                            }
                        }
                    }
                }
            }

            return Math.Round(boTax, 2);
        }
    }
}
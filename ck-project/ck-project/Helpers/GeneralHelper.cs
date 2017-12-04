using System;
using System.Collections.Generic;
using System.Linq;

namespace ck_project.Helpers
{
    public class GeneralHelper
    {
        ckdatabase db = new ckdatabase();
        static DateTime today = DateTime.Now;
        static DateTime date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
        public double GetApplicableRate(string rateName)
        {
            double amount = 0;
            if (db.rates.Where(r => r.deleted == false && r.rate_name == rateName && today <= r.end_date && today >= r.start_date).Any())
            {
                var rate = db.rates.Where(r => r.deleted == false && r.rate_name == rateName && today <= r.end_date && today >= r.start_date).First();
                if (rate != null)
                {
                    amount = rate.amount;
                    if (rate.rate_measurement != null && rate.rate_measurement.Equals(Constants.rate_Measurement_Percent))
                    {
                        amount = amount / 100;
                    }
                }
            }

            return amount;
        }

        public double GetBuildingPermitAmount(double buildingPermitCost)
        {
            var buildingPermit = db.building_permit.Where(b => b.deleted == false && b.start_date <= today && b.end_date >= today && b.start_range <= buildingPermitCost && b.end_range >= buildingPermitCost).First();
            if (buildingPermit != null)
            {
                return buildingPermit.adjustable_fee + buildingPermit.fixed_fee;
            }

            return 0;
        }
   
        public lead SetAllInstallationCosts(lead lead)
        {
            InstallationCalculationHelper installHelper = new InstallationCalculationHelper();
            if (lead.installations != null)
            {
                foreach (var item in lead.installations)
                {
                    item.recommendation = installHelper.GetRecommendation(item.oneway_mileages_to_destination);
                    item.estimated_hours = installHelper.CalculateEstimatedHours(lead);
                    item.billable_hours = installHelper.CalculateBillableHours((double)item.estimated_hours);
                    item.installation_days = installHelper.CalculateInstallationDays((double)item.billable_hours);
                    item.tile_installation_days = installHelper.CalculateTileInstallationDays(item.total_tile_cost);
                    item.required_hotel_nights = installHelper.CalculateNumberOfHotelNights((double)item.installation_days, item.recommendation);
                    item.travel_time_one_way = installHelper.CalculateTravelTimeOneWay(item.oneway_mileages_to_destination);
                    item.hotel_round_trip = installHelper.CalculateHotelRoundTrip((double)item.installation_days);
                    item.total_per_diem_cost = installHelper.CalculatePerDiem((double)item.installation_days, item.recommendation);
                    item.total_miles = installHelper.CalculateTotalMiles((double)item.installation_days, item.recommendation, item.oneway_mileages_to_destination);
                    item.installation_labor_only_cost = installHelper.CalculateLaborOnlyExpense((double)item.billable_hours);
                    item.mileage_expense = installHelper.CalculateMileageExpense((double)item.total_miles, item.recommendation);
                    item.total_travel_cost = installHelper.CalculateTravelExpense((double)item.total_miles, (double)item.travel_time_one_way, item.recommendation);
                    item.hotel_expense = installHelper.CalculateHotelExpense((double)item.required_hotel_nights, item.recommendation);
                    item.total_operational_expenses = new FeeCalculationHelper().CalculateTotalOperationalExpense(lead);
                    item.total_installation_labor_cost = installHelper.CalculateTotalLaborExpense(lead);
                    item.total_construction_materials_cost = installHelper.CalculateMaterialRetailPrice(lead);
                }
            }
            return lead;
        }

        public void SaveProjectTotal(int leadNbr)
        {
            var lead = db.leads.Where(l => l.lead_number == leadNbr).First();

            // set calculated installation data
            lead = this.SetAllInstallationCosts(lead);

            //set totalcost data
            TotalCostHelper cHelper = new TotalCostHelper();
            if (lead.total_cost == null)
            {
                total_cost total = new total_cost
                {
                    lead_number = (int)lead.lead_number,
                    product_cost = cHelper.CalculateProductCost(lead),
                    installation_cost = cHelper.CalculateInstallationCost(lead),
                    tax_cost = cHelper.CalculateApplicableTax(lead),
                    building_permit_cost = cHelper.CalculateBuildingPermitCost(lead)
                };
                total.total_cost1 = total.product_cost + total.installation_cost + total.tax_cost;
                List<total_cost> costList = new List<total_cost>
                {
                    total
                };

                lead.total_cost = costList;
            }
            else
            {
                foreach (var item in lead.total_cost)
                {
                    item.product_cost = cHelper.CalculateProductCost(lead);
                    item.installation_cost = cHelper.CalculateInstallationCost(lead);
                    item.tax_cost = cHelper.CalculateApplicableTax(lead);
                    item.building_permit_cost = cHelper.CalculateBuildingPermitCost(lead);
                    item.total_cost1 = item.product_cost + item.installation_cost + item.tax_cost;
                }
            }

            db.SaveChanges();
        }
    }
}
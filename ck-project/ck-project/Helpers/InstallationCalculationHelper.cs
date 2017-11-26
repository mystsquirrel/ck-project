using System;
using System.Linq;

namespace ck_project.Helpers
{
    public class InstallationCalculationHelper
    {
        ckdatabase db = new ckdatabase();
        public double CalculateMaterialRetailPrice(lead lead)
        {
            double materialRetailCost = 0;
            foreach (var item in lead.installations)
            {
                foreach (var task in item.tasks_installation)
                {
                    materialRetailCost += task.m_cost;
                }
            }

            return materialRetailCost;
        }

        public double CalculateEstimatedHours(lead lead)
        {
            double hours = 0;
            foreach (var item in lead.installations)
            {
                foreach (var task in item.tasks_installation)
                {
                    hours += task.hours;
                }
            }

            return hours;
        }

        public double CalculateBillableHours(double estimatedHours)
        {
            if ((estimatedHours % 8) != 0)
            {
                return ((int)(estimatedHours / 8) * 8) + 8;
            }

            return Convert.ToDouble(estimatedHours);
        }

        public double CalculateInstallationDays(double billablehours)
        {
            return billablehours / 8;
        }

        public double CalculateTileInstallationDays(double totalTilePrice)
        {
            var today = DateTime.Now;
            var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Tile Installer Hourly Rate" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
            return Math.Ceiling(totalTilePrice / rate.tax_value / 8);
        }

        public double CalculateTotalInstallationDays(double installationdays, double? tileInstallationDays) 
        {
            return tileInstallationDays != null ? installationdays + (double)tileInstallationDays : installationdays;
        }

        public double CalculateNumberOfHotelNights(double installationdays, string recommendation)
        {
            return recommendation.Equals(Constants.install_recommendation_hotel) ? installationdays - 1 : 0;
        }

        public double CalculateTravelTimeOneWay(double oneWayMileage)
        {
            return Math.Round(oneWayMileage * 1.35 / 60, 2);
        }

        public double CalculatePaidTravelTimeOneWay(double? traveltimeOneway)
        {
            return traveltimeOneway != null && (double) traveltimeOneway >= 1 ? (double)traveltimeOneway - 1 : 0;
        }

        public double CalculateBothWayMilesToJob(double? oneWayMileage)
        {
            return oneWayMileage != null ? (double)oneWayMileage * 2 : 0;
        }

        public double CalculateHotelRoundTrip(double installationdays)
        {
            double h = installationdays / 5;
            if ((h % 1) != 0)
            {
                return (int)(h) + 1;
            }

            return Convert.ToDouble(h);
        }

        public double CalculateTotalMiles(double installationdays, string recommendation, double onewayMile)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return this.CalculateHotelRoundTrip(installationdays) * (onewayMile * 2);
            }

            return installationdays * (onewayMile * 2);
        }

        public double CalculateTotalApplicableTravelHours(double totalMiles, double? traveltimeOneway)
        {
            return totalMiles * this.CalculatePaidTravelTimeOneWay(traveltimeOneway) * 2;
        }

        public double CalculatePerDiem(double installationDays, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                var today = DateTime.Now;
                var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Per Diem Rate" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
                return installationDays * 2 * rate.tax_value;
            }

            return 0;
        }

        public double CalculateLaborOnlyExpense(double billableHours)
        {
            var today = DateTime.Now;
            var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Crew Hours" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
            return billableHours * rate.tax_value;
        }

        public double CalculateTravelExpense(double totalMiles, double? traveltimeOneway, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                var today = DateTime.Now;
                var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Travel Rate" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
                return this.CalculateTotalApplicableTravelHours(totalMiles, traveltimeOneway) * rate.tax_value;
            }

            return this.CalculateTotalApplicableTravelHours(totalMiles, traveltimeOneway);
        }

        public double CalculateMileageExpense(double totalMiles, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                var today = DateTime.Now;
                var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Mileage Rate" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
                return totalMiles * rate.tax_value;
            }

            return totalMiles;
        }

        public double CalculateHotelExpense(double numberOfHotelNights, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                var today = DateTime.Now;
                var date = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
                var rate = db.taxes.Where(t => t.deleted == false && t.tax_anme == "Hotel Rate" && today <= t.end_date && today >= t.start_date).FirstOrDefault();
                return numberOfHotelNights * rate.tax_value;
            }
            return 0;
        }

        public double CalculateTotalLaborExpense(lead lead)
        {
            double totalLaborCost = 0;
            foreach (var item in lead.installations)
            {
                totalLaborCost = (double)item.installation_labor_only_cost + (double)item.total_travel_cost + (double)item.mileage_expense
                                + (double)item.hotel_expense + (double)item.building_permit_cost + (double)item.total_operational_expenses + (double)item.total_per_diem_cost;
            }

            return totalLaborCost;
        }
    }
}
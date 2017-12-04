using System;

namespace ck_project.Helpers
{
    public class InstallationCalculationHelper
    {
        GeneralHelper helper = new GeneralHelper();
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

            return materialRetailCost * 2;
        }

        public double CalculateEstimatedHours(lead lead)
        {
            double hours = 0;
            foreach (var item in lead.installations)
            {
                if (item.tasks_installation != null)
                {
                    foreach (var task in item.tasks_installation)
                    {
                        hours += task.hours;
                    }
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

            return estimatedHours;
        }

        public double CalculateInstallationDays(double billablehours)
        {
            return billablehours / 8;
        }

        public double CalculateTileInstallationDays(double totalTilePrice)
        {
            double rate = helper.GetApplicableRate(Constants.rate_Name_Hourly_Tile_Installer);
            if (rate != 0)
            {
                return Math.Ceiling(totalTilePrice / rate / 8);
            }
            return 0;
        }

        public double CalculateTotalInstallationDays(double? installationdays, double? tileInstallationDays) 
        {
            double totalInstallDays = 0;
            if (tileInstallationDays != null)
            {
                totalInstallDays += (double)tileInstallationDays;
            }
            else if (installationdays != null)
            {
                totalInstallDays += (double)installationdays;
            }
            return totalInstallDays;
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

        public double CalculateBothWayMilesToJob(double oneWayMileage)
        {
            return (double)oneWayMileage * 2;
        }

        public double CalculateHotelRoundTrip(double installationdays)
        {
            if (installationdays != 0)
            {
                double h = installationdays / 5;
                if ((h % 1) != 0)
                {
                    return (int)(h) + 1;
                }

                return h;
            }

            return 0;
        }

        public double CalculateTotalMiles(double installationdays, string recommendation, double oneWayMile)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return this.CalculateHotelRoundTrip(installationdays) * this.CalculateBothWayMilesToJob(oneWayMile);
            }

            return installationdays * this.CalculateBothWayMilesToJob(oneWayMile);
        }

        public double CalculateTotalApplicableTravelHours(double? totalMiles, double? traveltimeOneway)
        {
            return totalMiles != null ? (double)totalMiles * this.CalculatePaidTravelTimeOneWay(traveltimeOneway) * 2 : 0 ;
        }

        public double CalculatePerDiem(double installationDays, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return installationDays * 2 * helper.GetApplicableRate(Constants.rate_Name_Per_Diem);
            }

            return 0;
        }

        public double CalculateLaborOnlyExpense(double billableHours)
        {
            return billableHours * (helper.GetApplicableRate(Constants.rate_Name_Hourly_Lead_Installer) + helper.GetApplicableRate(Constants.rate_Name_Hourly_Junior_Installer));
        }

        public double CalculateTravelExpense(double totalMiles, double? traveltimeOneway, string recommendation)
        {
            if (!recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return this.CalculateTotalApplicableTravelHours(totalMiles, traveltimeOneway) * helper.GetApplicableRate(Constants.rate_Name_Travel);
            }

            return 0;
        }

        public double CalculateMileageExpense(double totalMiles, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return totalMiles * helper.GetApplicableRate(Constants.rate_Name_Mileage);
            }

            return totalMiles;
        }

        public double CalculateHotelExpense(double numberOfHotelNights, string recommendation)
        {
            if (recommendation.Equals(Constants.install_recommendation_hotel))
            {
                return numberOfHotelNights * helper.GetApplicableRate(Constants.rate_Name_Hotel);
            }

            return 0;
        }

        // include labor cost, travel cost, mileage expense, hotel expense, building permit cost, operational cost and per diem cost
        public double CalculateTotalLaborExpense(lead lead)
        {
            double totalLaborCost = 0;
            if (lead.installations != null)
            {
                foreach (var item in lead.installations)
                {
                    if (item.installation_labor_only_cost != null)
                    {
                        totalLaborCost += (double)item.installation_labor_only_cost;
                    }

                    if (item.mileage_expense != null)
                    {
                        totalLaborCost += (double)item.total_travel_cost;
                    }

                    if (item.mileage_expense != null)
                    {
                        totalLaborCost += (double)item.mileage_expense;
                    }

                    if (item.hotel_expense != null)
                    {
                        totalLaborCost += (double)item.hotel_expense;
                    }

                    if (item.building_permit_cost != null)
                    {
                        totalLaborCost = (double)item.building_permit_cost;
                    }

                    if (item.total_operational_expenses != null)
                    {
                        totalLaborCost = (double)item.total_operational_expenses;
                    }

                    if (item.total_per_diem_cost != null)
                    {
                        totalLaborCost = (double)item.total_per_diem_cost;
                    }
                }
            }
            return totalLaborCost;
        }

        // only need buildling permit if the project is installed and jobsite is in the city
        public string GetRecommendation(double onewaymile) {
            return onewaymile > 89 ? "Hotel" : "Travel";
        }

        public double CalculateBuildingPermit(lead lead)
        {
            double buildingPermitCost = 0;
            if (lead.in_city && lead.delivery_status_number == 1)
            {
                if (lead.installations != null)
                {
                    foreach (var item in lead.installations)
                    {
                        if (item.installation_labor_only_cost != null)
                        {
                            buildingPermitCost += (double)item.installation_labor_only_cost;
                        }

                        if (item.total_construction_materials_cost != null)
                        {
                            buildingPermitCost += (double)item.total_construction_materials_cost;
                        }
                    }

                    if (lead.total_cost != null)
                    {
                        foreach (var item in lead.total_cost)
                        {
                            if (item.product_cost != null)
                            {
                                buildingPermitCost += (double)item.product_cost;
                            }
                        }
                    }

                    return helper.GetBuildingPermitAmount(buildingPermitCost);
                }
            }
            
            return 0;
        }
    }

}
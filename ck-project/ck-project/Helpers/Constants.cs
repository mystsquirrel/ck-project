using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ck_project.Helpers
{
    public class Constants
    {
        public static readonly IList<String> catDefList1 = new ReadOnlyCollection<string>(new List<String>
        {
            "CABINETRY",
            "HARDWARE",
            "APPLIANCES",
            "OWNER PROVIDED PRODUCT",
            "ACCESSORIES & MIRRORS"
        });

        public static readonly IList<String> catDefList2 = new ReadOnlyCollection<string>(new List<String>
        {
            "COUNTERS",
            "SINK, STRAINER, FAUCET, FITTINGS",
            "DECORATIVE SURFACES",
            "ADDITIONAL ITEMS"
        });

        public static readonly IList<String> catDefList = new ReadOnlyCollection<string>(new List<String>
        {
            "CABINETRY",
            "COUNTERS",
            "HARDWARE",
            "SINK, STRAINER, FAUCET, FITTINGS",
            "APPLIANCES",
            "OWNER PROVIDED PRODUCT",
            "DECORATIVE SURFACES",
            "ACCESSORIES & MIRRORS",
            "ADDITIONAL ITEMS"
        });

        public const string install_hours = "Hours";
        public const string install_laborCost = "Labor Cost";
        public const string install_materialCost = "Material Cost";
        public const string install_materialRetail = "Material Retail";
        public const string install_totalCost = "Total Cost";
        public const string install_recommendation_hotel = "Hotel";
        public const string install_recommendation_travel = "Travel";

        public const string proj_Status_Closed = "Closed";

        public const int deliver_Status_Installed = 1;
        public const int deliver_Status_Pickup = 2;
        public const int deliver_Status_Delivery = 3;

        public const string rate_Measurement_Percent = "Percent";

        public const string rate_Name_Hourly_Tile_Installer = "Tile Installer Hourly Rate";
        public const string rate_Name_Hourly_Lead_Installer = "Lead Installer Hourly Rate";
        public const string rate_Name_Hourly_Junior_Installer = "Junior Installer Hourly Rate";
        public const string rate_Name_Travel = "Travel Rate";
        public const string rate_Name_Hotel = "Hotel Rate";
        public const string rate_Name_Mileage = "Mileage Rate";
        public const string rate_Name_Per_Diem = "Per Diem Rate";
        public const string rate_Name_BO = "B&O Rate";
        public const string rate_Name_Avg_Material_Cost = "Avg Material Cost Rate";
        public const string rate_Name_Avg_Project_Cost = "Avg Project Cost Rate";
        public const string rate_Name_Avg_Install_Cost = "Avg Install Cost Rate";
        public const string rate_Name_Operational_Admin = "Operational Admin Expense Rate";
        public const string rate_Name_Operational_CreditCard = "Operational Expense For Credit Card Rate";
        public const string rate_Name_Operational_Installation = "Operational Expense For Installation Rate";
    }
}
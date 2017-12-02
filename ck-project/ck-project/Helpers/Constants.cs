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
    }
}
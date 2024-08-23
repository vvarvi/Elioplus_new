using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum StripePlans : int
    {
        Elio_Premium_Free_Plan = 0,
        Elio_Premium_Plan = 1,
        Elio_Startup_Plan = 8,
        Elio_Growth_Plan = 9,
        Elio_Enterprise_Plan = 10,
        Elio_Premium_NoTrial_Plan = 2,
        Elio_Premium_SemiTrial_Plan = 3,
        //Elio_Daily_Premium_Plan = 4
        Elio_Startup_Discount_Plan = 11,
        Elio_Premium_Service_Plan = 6,
        Elio_Startup_Discount_Plan_30 = 13,
        Elio_Growth_Discount_Plan_30 = 17,
        Elio_SelfService_Plan = 21
    }
}
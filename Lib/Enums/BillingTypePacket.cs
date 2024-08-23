using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum BillingTypePacket
    {
        FreemiumPacketType = 1,
        PremiumPacketType = 2,
        PremiumStartupPacketType = 3,
        PremiumGrowthPacketType = 4,
        PremiumEnterprisePacketType = 5,
        PremiumSelfServicePacketType = 6,

        StartupPRMAutomationType = 37,
        GrowthPRMAutomationType = 38,
        StartupPRMDatabaseType = 39,
        GrowthPRMDatabaseType = 40
    }
}
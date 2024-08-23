using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.StripeAPI.Enums
{
    public enum StripeSubscriptionStatus
    {
        trialing,
        active,
        past_due,
        unpaid,
        canceled,
        all
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.StripeAPI.Enums
{
    public enum StripeCvcCheck
    {
        Unknown = 0,
        Pass = 1,
        Fail = 2,
        Unchecked = 3,
        Unavailable = 4
    }
}
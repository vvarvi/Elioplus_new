using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum OrderMode
    {
        Active,
        Canceled,
        PastDue,
        Trialing,
        Unknown,
        Unpaid
    }
}
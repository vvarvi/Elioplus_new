using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.StripeAPI.Enums
{
    public enum PaymentStatus : int
    {
        NotPaid = 0,
        Paid = 1
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.StripeAPI.Enums
{
    public enum PaymentMethodType : int
    {
        None = 0,
        CardPayment = 1,
        BankPayment = 2
    }
}
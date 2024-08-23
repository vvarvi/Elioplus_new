using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum OrderStatus
    {
        NotReadyToUse = 0,
        ReadyToUse = 1,
        Active = 1,
        Completed = 2,
        Closed = -1,
        Canceled = -3,
        Expired = -2,
        NotPaid = 0,
        Paid = 1
    }
}
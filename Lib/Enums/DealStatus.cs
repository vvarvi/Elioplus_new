using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum DealStatus : int
    {
        Open = 1,
        Closed = 2,
        Expired = -3,
        InProgress = 4,
        OpenDeal = 5,
        Unqualified = 6,
        AttemptedToContact = 7,
        Connected = 8,
        BadTiming = 9,
        New = 10,
        None = 11,
        Deleted = -5
    }
}
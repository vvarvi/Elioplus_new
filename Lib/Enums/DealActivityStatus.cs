using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum DealActivityStatus : int
    {
        NotConfirmed = 0,
        Approved = 1,
        Rejected = -1,
        Deleted = -5
    }
}
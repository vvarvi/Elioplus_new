using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum Types
    {
        All = 0,
        Vendors = 1,
        [Description("Channel Partners")]
        Resellers = 2,
        Developers = 3
    }
}
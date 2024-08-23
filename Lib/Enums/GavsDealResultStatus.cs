using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum GavsDealResultStatus : int
    {
        Qualify = 0,
        Develop = 1,
        Propose = 2,
        Close = 3,
        Won = 4,
        Pending = 5,
        Lost = -3
    }
}
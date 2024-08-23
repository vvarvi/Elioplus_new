using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum LoginException
    {
        AccountBlocked = -1,
        AccountNotPublic = -2,
        AccountNotActive = -3,
        AccountNotConfirmed = -4,
        AccountDeleted = -5
    }
}
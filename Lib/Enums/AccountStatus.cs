using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum AccountStatus : int
    {
        NotCompleted = 0,
        Completed = 1,
        Deleted = 2,
        Blocked = 3,
        Public = 1,
        NotPublic = 0,
        Confirmed = 1,
        NotConfirmed = 4,
        Active = 1,
        NotActive = 5,
        Deleting = 6
    }
}
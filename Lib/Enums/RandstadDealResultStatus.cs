using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Enums
{
    public enum RandstadDealResultStatus : int
    {
        Won = 1,
        Pending = 2,
        Lost = -3,
        Qualified = 3,
        Solution_Developed = 4,
        Proposal_Presented = 5,
        Negotiation_and_Close = 6,
        Awarded_Contract_Pending = 7
    }
}
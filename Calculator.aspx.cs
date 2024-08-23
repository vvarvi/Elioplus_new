using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl header = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "header");
            header.Attributes["class"] = "header headbg-img navbar-fixed-top";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace WdS.ElioPlus.Lib.Utils
{
    public class FindRootNode
    {
        public static RadTreeNode FindRootComment(RadTreeView rtv, int commentId)
        {
            RadTreeNode root = new RadTreeNode();

            return (rtv.Nodes.FindNodeByValue(commentId.ToString()) != null) ? root : null;
        }
    }
}
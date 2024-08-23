using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WdS.ElioPlus.Lib.Utils
{
    public class ControlFinder
    {
        public static int controlFinderCalls = 0;
        public static TimeSpan controlFinderTotalTime = TimeSpan.Zero;

        public static Control FindControlRecursive(Control Root, string Id, bool isRootCall = true)
        {
            if (isRootCall)
            {
                controlFinderCalls++;
            }
            DateTime startTime = DateTime.Now;
            Control returnControl = null;

            if (Root.ID == Id)
            {
                returnControl = Root;
            }
            else
            {
                foreach (Control Ctl in Root.Controls)
                {
                    Control FoundCtl = FindControlRecursive(Ctl, Id, false);
                    if (FoundCtl != null)
                    {
                        returnControl = FoundCtl;
                    }
                }
            }

            if (isRootCall)
            {
                controlFinderTotalTime += DateTime.Now - startTime;
            }

            return returnControl;
        }

        public static Control FindControlBackWards(Control currentControl, string Id, bool isRootCall = true)
        {
            if (isRootCall)
            {
                controlFinderCalls++;
            }
            DateTime startTime = DateTime.Now;
            Control returnControl = null;

            if (currentControl.ID == Id)
            {
                returnControl = currentControl;
            }
            else
            {
                Control parentControl = currentControl.NamingContainer;
                Control FoundCtl = null;
                while (FoundCtl == null)
                {
                    FoundCtl = FindControlRecursive(parentControl, Id, false);
                    if (parentControl.NamingContainer != null)
                        parentControl = parentControl.NamingContainer;
                    else
                        break;
                }
                returnControl = FoundCtl;
            }

            if (isRootCall)
            {
                controlFinderTotalTime += DateTime.Now - startTime;
            }

            return returnControl;
        }

    }
}
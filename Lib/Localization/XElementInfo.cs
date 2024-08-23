using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Localization
{
    public class XElementInfo
    {
        public string Section;
        public string Page;
        public string ControlType;
        public string ID;

        public string ControlTypeLvl2;
        public string IDLvl2;

        public XElementInfo(string section, string page, string controlType, string id)
        {
            Section = section;
            Page = page;
            ControlType = controlType;
            ID = id;
        }

        public XElementInfo(string section, string page, string controlType, string id, string controlTypeLvl2, string idLvl2)
        {
            Section = section;
            Page = page;
            ControlType = controlType;
            ID = id;
            ControlTypeLvl2 = controlTypeLvl2;
            IDLvl2 = idLvl2;
        }
    }
}
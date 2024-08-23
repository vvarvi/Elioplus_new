using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WdS.ElioPlus.Lib.Localization.Xml;

namespace WdS.ElioPlus.Lib.Localization
{
    public class XmlHelper
    {
        public static XElement FindXElementInGrid(XElement root, XElementInfo info)
        {
            IEnumerable<XElement> gridElements =
                from c in root.Elements(info.Section).Elements(info.Page).Elements(info.ControlType + "s").Elements(info.ControlType)
                where (string)c.Attribute("id") == info.ID
                select c;

            if (GetCountOfElements(gridElements) == 0)
                return null;

            IEnumerable<XElement> foundElements =
                from c in gridElements.ElementAt(0).Elements(info.ControlTypeLvl2 + "s").Elements(info.ControlTypeLvl2)
                where (string)c.Attribute("id") == info.IDLvl2
                select c;

            return GetCountOfElements(foundElements) != 0 ? foundElements.ElementAt(0) : null;
        }

        public static IEnumerable<XElement> FindXElementsInContent(XElement root, XElementInfo info)
        {
            IEnumerable<XElement> foundElements =
                from c in root.Elements(info.Section).Elements(info.Page).Elements(info.ControlType + "s").Elements(info.ControlType)
                where (string)c.Attribute("id") == info.ID
                select c;

            return foundElements;
        }

        public static XElement FindXElementInContent(XElement root, XElementInfo info)
        {
            IEnumerable<XElement> foundElements =
                from c in root.Elements(info.Section).Elements(info.Page).Elements(info.ControlType + "s").Elements(info.ControlType)
                where (string)c.Attribute("id") == info.ID
                select c;
                       
            return GetCountOfElements(foundElements) != 0 ? foundElements.ElementAt(0) : null;
        }

        public static XElement FindXElementInGridInGlobal(XElement root, XElementInfo info)
        {
            IEnumerable<XElement> gridElements =
                from c in root.Elements(info.Section).Elements(info.ControlType + "s").Elements(info.ControlType)
                where (string)c.Attribute("id") == info.ID
                select c;

            if (GetCountOfElements(gridElements) == 0)
                return null;

            IEnumerable<XElement> foundElements =
                from c in gridElements.ElementAt(0).Elements(info.ControlTypeLvl2)
                where (string)c.Attribute("id") == info.IDLvl2
                select c;

            return GetCountOfElements(foundElements) != 0 ? foundElements.ElementAt(0) : null;
        }

        public static XElement FindXElementInGlobal(XElement root, XElementInfo info)
        {
            IEnumerable<XElement> foundElements =
                from c in root.Elements("global").Elements(info.ControlType + "s").Elements(info.ControlType)
                where (string)c.Attribute("id") == info.ID
                select c;

            return GetCountOfElements(foundElements) != 0 ? foundElements.ElementAt(0) : null;
        }

        public static XElement ReadXml(string xmlFilename, string lang)
        {
            string path = HttpContext.Current.Server.MapPath("~") + @"\App_Data\Localization\" + xmlFilename + "." + lang + ".xml";
            return XmlReader.ParseXmlFromFile(path);
        }

        public static void WriteXml(string xmlFilename, string lang, string xml)
        {
            string path = HttpContext.Current.Server.MapPath("~") + @"\App_Data\Localization\" + xmlFilename + "." + lang + ".xml";
            XmlWriter.WriteXmlContents(path, xml);
        }

        public static int GetCountOfElements(IEnumerable<XElement> elements)
        {
            int i = 0;
            foreach (XElement element in elements)
            {
                i++;
            }
            return i;
        }

        public static void InsertNewElementInContent(XElement root, XElementInfo info, List<XAttribute> xAttributes)
        {
            XElement parentElement = root.Element("content").Element(info.Page).Element(info.ControlType + "s");
            XElement newElement = new XElement(info.ControlType);
            newElement.ReplaceAttributes(xAttributes);
            parentElement.Add(newElement);
        }

        public static void InsertNewElementInGlobal(XElement root, XElementInfo info, List<XAttribute> xAttributes)
        {
            XElement parentElement = root.Element("global").Element(info.ControlType + "s");
            XElement newElement = new XElement(info.ControlType);
            newElement.ReplaceAttributes(xAttributes);
            parentElement.Add(newElement);
        }

        public static void InsertNewElementInGrid(XElement root, XElementInfo info, List<XAttribute> xAttributes)
        {
            XElement parentElement = root.Element("content").Element(info.Page).Element(info.ControlType + "s").Element(info.ControlType).Element(info.ControlTypeLvl2 + "s");
            XElement newElement = new XElement(info.ControlTypeLvl2);
            newElement.ReplaceAttributes(xAttributes);
            parentElement.Add(newElement);
        }

        public static void ReplaceAttributes(XElement element, List<XAttribute> xAttributes)
        {
            if (element != null)
            {
                element.ReplaceAttributes(xAttributes);
            }
        }
    }
}
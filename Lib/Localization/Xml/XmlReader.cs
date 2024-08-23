using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WdS.ElioPlus.Lib.Localization.Xml
{
    public class XmlReader
    {
        public static XElement ParseXmlFromFile(string filePath)
        {
            return XElement.Load(filePath);
        }

        public static XElement ParseXmlFromString(string xml)
        {
            return XDocument.Parse(xml).Root;
        }

        public static string GetXmlAsString(string filePath)
        {
            string contents = "";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.StreamReader r = new System.IO.StreamReader(filePath, System.Text.Encoding.UTF8);
                contents = r.ReadToEnd();
                r.Close();
                r.Dispose();
            }
            return contents;
        }
    }
}
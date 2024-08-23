using System;
using System.Linq;
using System.Xml.Linq;
using System.Configuration;
using WdS.ElioPlus.Lib.Localization.Xml;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Localization
{
    public class Localizer
    {
        public static TranslationResults GetTranslationResults(string lang, XElementInfo info)
        {
            return GetTranslationResults("elio", lang, info);
        }

        public static TranslationResults GetTranslationResults(string xmlFilename, string lang, XElementInfo info)
        {
            XElement root = XmlReader.ParseXmlFromFile(FileHelper.AddRootToPath(ConfigurationManager.AppSettings["LocalizationXmlPath"]) + "\\" + xmlFilename + "." + lang + ".xml");
            XElement foundElement = null;

            switch (info.Section)
            {
                case "header":
                    if (info.ControlType == "grid")
                        foundElement = XmlHelper.FindXElementInGrid(root, info);
                    else
                        foundElement = XmlHelper.FindXElementInContent(root, info);
                    break;
                case "content":
                    if (info.ControlType == "grid")
                        foundElement = XmlHelper.FindXElementInGrid(root, info);
                    else
                        foundElement = XmlHelper.FindXElementInContent(root, info);
                    break;
                case "controls":
                    if (info.ControlType == "grid")
                        foundElement = XmlHelper.FindXElementInGrid(root, info);
                    else
                        foundElement = XmlHelper.FindXElementInContent(root, info);
                    break;
                case "footer":
                    if (info.ControlType == "grid")
                        foundElement = XmlHelper.FindXElementInGrid(root, info);
                    else
                        foundElement = XmlHelper.FindXElementInContent(root, info);
                    break;
                case "global":
                    if (info.ControlType == "grid")
                        foundElement = XmlHelper.FindXElementInGridInGlobal(root, info);
                    else
                        foundElement = XmlHelper.FindXElementInGlobal(root, info);
                    break;
                default:
                    return null;
            }

            if (foundElement == null)
                new TranslationResults("", "", "", true, false, "", false, false, false, false, false, false, false);

            string text = (foundElement != null && foundElement.Attribute("text") != null) ? foundElement.Attribute("text").Value : "";
            string toolTip = (foundElement != null && foundElement.Attribute("tooltip") != null) ? foundElement.Attribute("tooltip").Value : "";
            string description = (foundElement != null && foundElement.Attribute("desc") != null) ? foundElement.Attribute("desc").Value : "";
            string type = (foundElement != null && foundElement.Attribute("type") != null) ? foundElement.Attribute("type").Value : "";
            bool isTextMandatory = (foundElement != null && foundElement.Attribute("isTextMandatory") != null) ? Convert.ToBoolean(foundElement.Attribute("isTextMandatory").Value) : true;
            bool isTooltipMandatory = (foundElement != null && foundElement.Attribute("isTooltipMandatory") != null) ? Convert.ToBoolean(foundElement.Attribute("isTooltipMandatory").Value) : false;
            bool hasTooltip = (foundElement != null && foundElement.Attribute("tooltip") != null) ? true : false;
            bool hasText = (foundElement != null && foundElement.Attribute("text") != null) ? true : false;
            bool hasDesc = (foundElement != null && foundElement.Attribute("desc") != null) ? true : false;
            bool hasIsTextMandatory = (foundElement != null && foundElement.Attribute("isTextMandatory") != null) ? true : false;
            bool hasIsTooltipMandatory = (foundElement != null && foundElement.Attribute("isTooltipMandatory") != null) ? true : false;
            bool hasType = (foundElement != null && foundElement.Attribute("type") != null) ? true : false;
            return new TranslationResults(text, toolTip, description, isTextMandatory, isTooltipMandatory, type, hasTooltip, hasText, hasDesc, foundElement != null, hasIsTextMandatory, hasIsTooltipMandatory, hasType);
        }

    }
}
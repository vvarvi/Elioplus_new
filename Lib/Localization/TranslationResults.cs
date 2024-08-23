using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Localization
{
    public class TranslationResults
    {
        public string Text;
        public string Tooltip;
        public string Description;
        public string Type;
        public bool IsTextMandatory;
        public bool IsTooltipMandatory;
        public bool HasTooltip;
        public bool HasText;
        public bool HasDesc;
        public bool HasIsTextMandatory;
        public bool HasIsTooltipMandatory;
        public bool HasType;
        public bool ElementExists;

        public TranslationResults(string text, string toolTip, string description, bool isTextMandatory, bool isTooltipMandatory, string type, bool hasTooltip, bool hasText, bool hasDesc, bool elementExists, bool hasIsTextMandatory, bool hasIsTooltipMandatory, bool hasType)
        {
            Type = type;
            Text = text;
            Tooltip = toolTip;
            Description = description;
            IsTextMandatory = isTextMandatory;
            IsTooltipMandatory = isTooltipMandatory;
            HasTooltip = hasTooltip;
            HasText = hasText;
            HasDesc = hasDesc;
            ElementExists = elementExists;
            HasIsTextMandatory = hasIsTextMandatory;
            HasIsTooltipMandatory = hasIsTooltipMandatory;
            HasType = hasType;
        }
    }
}
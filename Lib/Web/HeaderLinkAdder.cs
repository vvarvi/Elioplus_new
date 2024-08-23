using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace WdS.ElioPlus.Lib.Web
{
    public class HeaderLinkAdder
    {
        public static void AddMetaToHeaderWithProperty(string property, string content, Page page)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes["property"] = property;
            meta.Content = content;
            page.Header.Controls.Add(meta);
        }

        public static void AddMetaToHeader(string name, string content, Page page)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = name;
            meta.Content = content;
            page.Header.Controls.Add(meta);
        }

        public static void AddCssFileToHeader(string href, Page page)
        {
            HtmlLink css = new HtmlLink();
            css.Href = href;
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            //css.Attributes["media"] = "all";
            page.Header.Controls.Add(css);
        }

        public static void AddIconFileToHeader(string href, Page page)
        {
            HtmlLink css = new HtmlLink();
            css.Href = href;
            css.Attributes["rel"] = "icon";
            css.Attributes["type"] = "image/png";
            css.Attributes["media"] = "all";
            page.Header.Controls.Add(css);
        }

        public static void AddJavascriptFileToHeader(string src, Page page)
        {
            LiteralControl jsResource = new LiteralControl();
            jsResource.Text = "<script type=\"text/javascript\" src=\"" + src + "\"></script>";
            page.Header.Controls.Add(jsResource);
        }

        public static void AddJsScriptToHeader(string jsScript, Page page)
        {
            LiteralControl js = new LiteralControl();
            js.Text = "<script type=\"text/javascript\">" + jsScript + "</script>";
            page.Header.Controls.Add(js);
        }

        public static void AddFullJsScriptToHeader(string jsScript, Page page)
        {
            LiteralControl js = new LiteralControl();
            js.Text = jsScript;
            page.Header.Controls.Add(js);
        }
    }
}
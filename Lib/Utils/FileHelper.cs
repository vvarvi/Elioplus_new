using System;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Web;

namespace WdS.ElioPlus.Lib.Utils
{
    public class FileHelper
    {
        public static string AddRootToPath(string path)
        {
            string root = ConfigurationManager.AppSettings["RootPath"];

            if (!string.IsNullOrEmpty(root))
            {
                if (!Directory.Exists(root))
                    throw new Exception(String.Format("RootPath does not exists. Check value in web.config. RootPath {0}", ConfigurationManager.AppSettings["RootPath"].ToString()));

                if (!root.EndsWith("\\") && !path.StartsWith("\\")) root += "\\";
            }

            return (root + path);
        }

        public static string AddToPhysicalRootPath(HttpRequest request)
        {
            string root = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath;

            if (root.EndsWith("/"))
                root = root.Substring(0, root.Length - 1);

            return root;
        }
    }
}
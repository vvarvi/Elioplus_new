using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;

namespace WdS.ElioPlus.Lib.Localization.Xml
{
    public class XmlWriter
    {
        public static void WriteXmlContents(string path, string xml)
        {
            while (IsFileInUse(new FileInfo(path)))
            {
                Thread.Sleep(500);
            }

            using (System.IO.StreamWriter w = new System.IO.StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                w.Write(xml);
                w.Close();
                w.Dispose();
            }
        }

        public static bool IsFileInUse(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
    }
}
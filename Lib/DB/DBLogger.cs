using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using System.Threading;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.DB
{
    public class DBLogger
    {
        /// <summary>
        /// Detects if the db logging is enabled and records the query to the appropriate log file
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="sessionGuid"></param>
        /// <param name="query"></param>
        /// <param name="rowId"></param>
        /// <param name="rowsAffected"></param>
        public static void AppendToDbLog(DateTime startTime, string sessionGuid, string query, int rowId, int rowsAffected)
        {
            try
            {
                TimeSpan delay = DateTime.Now - startTime;
                bool recordToNormalFlag = shouldRecordToNormalLog(delay);
                bool recordToHeavyFlag = shouldRecordToHeavyLog(delay);

                if (recordToNormalFlag)
                {
                    recordToNormalLog(sessionGuid, query, rowId, rowsAffected, delay);
                }

                if (recordToHeavyFlag)
                {
                    recordToHeavyLog(sessionGuid, query, rowId, rowsAffected, delay);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("DB Log Error: " + ex.Message);
            }
        }

        private static bool shouldRecordToNormalLog(TimeSpan delay)
        {
            if (ConfigurationManager.AppSettings["RootPath"] == null)
            {
                return false;
            }

            if (ConfigurationManager.AppSettings["DB_Access_Normal_Logging_Enabled"] != null
                    && ConfigurationManager.AppSettings["DBNormalLogPath"] != null)
            {
                if (ConfigurationManager.AppSettings["DB_Access_Normal_Logging_Enabled"].ToString().ToLower() == "true")
                {
                    double normalDelayThreshold = 0;
                    if (ConfigurationManager.AppSettings["DB_Access_Normal_Delay_Threshold"] != null)
                    {
                        if (!double.TryParse(ConfigurationManager.AppSettings["DB_Access_Normal_Delay_Threshold"].ToString(),
                                             out normalDelayThreshold))
                        {
                            return false;
                        }
                    }

                    if (delay.TotalSeconds > normalDelayThreshold)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool shouldRecordToHeavyLog(TimeSpan delay)
        {
            if (ConfigurationManager.AppSettings["RootPath"] == null)
            {
                return false;
            }

            if (ConfigurationManager.AppSettings["DB_Access_Heavy_Logging_Enabled"] != null
                    && ConfigurationManager.AppSettings["DBHeavyLogPath"] != null)
            {
                if (ConfigurationManager.AppSettings["DB_Access_Heavy_Logging_Enabled"].ToString().ToLower() == "true")
                {
                    double heavyDelayThreshold = 0;
                    if (ConfigurationManager.AppSettings["DB_Access_Heavy_Delay_Threshold"] != null)
                    {
                        if (!double.TryParse(ConfigurationManager.AppSettings["DB_Access_Heavy_Delay_Threshold"].ToString(),
                                             out heavyDelayThreshold))
                        {
                            return false;
                        }
                    }

                    if (delay.TotalSeconds > heavyDelayThreshold)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void recordToNormalLog(string sessionGuid, string query, int rowId, int rowsAffected, TimeSpan delay)
        {
            string dbNormalLogFilePath = retrieveOrCreateNormalLogFilePath();
            recordToLog(sessionGuid, query, rowId, rowsAffected, delay, dbNormalLogFilePath);
        }

        private static void recordToHeavyLog(string sessionGuid, string query, int rowId, int rowsAffected, TimeSpan delay)
        {
            string dbHeavyLogFilePath = retrieveOrCreateHeavyLogFilePath();
            recordToLog(sessionGuid, query, rowId, rowsAffected, delay, dbHeavyLogFilePath);
        }

        private static void recordToLog(string sessionGuid, string query, int rowId, int rowsAffected, TimeSpan delay, string logFilePath)
        {
            XElement dbLogRoot = XElement.Load(logFilePath);

            string page = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Session["page"] != null)
            {
                page = HttpContext.Current.Session["page"].ToString();
            }

            XElement sessionElement = dbLogRoot.Elements().Where(e => e.Attribute("sessionGUID").Value == sessionGuid).FirstOrDefault();

            if (sessionElement == null)
            {
                sessionElement = new XElement("session",
                                              new XAttribute("sessionGUID", sessionGuid),
                                              new XElement("Sysdate", DateTime.Now),
                                              new XElement("Page", page));

                dbLogRoot.Add(sessionElement);
            }

            sessionElement.Add(new XElement("query",
                                           new XElement("Sysdate", DateTime.Now),
                                           new XElement("Query", query),
                                           new XElement("rowId", rowId),
                                           new XElement("rowsAffected", rowsAffected),
                                           new XElement("time", delay.TotalSeconds),
                                           new XElement("threadId", Thread.CurrentThread.ManagedThreadId)));

            dbLogRoot.Save(logFilePath);
        }

        private static string retrieveOrCreateNormalLogFilePath()
        {
            DateTime fileCreationCompareTime = DateTime.Now;

            string rootPath = ConfigurationManager.AppSettings["RootPath"].ToString() + "\\";
            string dbLogPath = ConfigurationManager.AppSettings["DBNormalLogPath"].ToString();
            string dbLogFilePath = Path.Combine(rootPath, dbLogPath);

            if (!Directory.Exists(Path.GetDirectoryName(dbLogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbLogFilePath));
            }

            XElement newNormalLogRoot = new XElement("Sessions");

            if (!File.Exists(dbLogFilePath))
            {
                newNormalLogRoot.Save(dbLogFilePath);
            }

            if (File.GetCreationTime(dbLogFilePath).Day != fileCreationCompareTime.Day)
            {
                File.Delete(dbLogFilePath);
                newNormalLogRoot.Save(dbLogFilePath);
            }

            return dbLogFilePath;
        }

        private static string retrieveOrCreateHeavyLogFilePath()
        {
            string rootPath = ConfigurationManager.AppSettings["RootPath"].ToString() + "\\";
            string dbLogPath = ConfigurationManager.AppSettings["DBHeavyLogPath"].ToString();
            string dbLogFilePath = Path.Combine(rootPath, dbLogPath);

            if (!Directory.Exists(Path.GetDirectoryName(dbLogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbLogFilePath));
            }

            XElement newHeavyLogRoot = new XElement("Sessions");

            if (!File.Exists(dbLogFilePath))
            {
                newHeavyLogRoot.Save(dbLogFilePath);
            }

            return dbLogFilePath;
        }
    }
}
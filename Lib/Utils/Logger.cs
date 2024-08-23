using System;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Objects;
using System.Configuration;
using WdS.ElioPlus.Lib.DBQueries;

namespace WdS.ElioPlus.Lib.Utils
{
    public class Logger
    {

        public delegate void LogMessageEventHandler(string logType, string logMessage);
        static public event LogMessageEventHandler LogMessage;

        static private object Locker = new object();
        static public string LogPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
        static public string LogPathClearBit = System.Configuration.ConfigurationManager.AppSettings["LogPathClearBit"];
        static public string LogInfoPath = System.Configuration.ConfigurationManager.AppSettings["LogInfoPath"];
        static public string LogAnonymousPath = System.Configuration.ConfigurationManager.AppSettings["LogAnonymousPath"];

        static public string LogFileFormat = System.Configuration.ConfigurationManager.AppSettings["LogFileFormat"];
        static public bool IsLogForWebApp = System.Configuration.ConfigurationManager.AppSettings["IsLogForWebApp"] == null ? true : Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsLogForWebApp"]);

        static public bool LogErrorMessages = (("" + System.Configuration.ConfigurationManager.AppSettings["LogErrorMessages"]).ToLower() == "true");
        static public bool LogDebugMessages = (("" + System.Configuration.ConfigurationManager.AppSettings["LogDebugMessages"]).ToLower() == "true");
        static public bool LogInfoMessages = (("" + System.Configuration.ConfigurationManager.AppSettings["LogInfoMessages"]).ToLower() == "true");

        static private string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //return (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true") ? DateTime.Now.AddDays(1).AddHours(-15).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        static public string GetLogFileName(string logPath)
        {
            if (string.IsNullOrEmpty(logPath))
                return null;
            string path = FileHelper.AddRootToPath(logPath);
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string format = Logger.LogFileFormat;
            if (string.IsNullOrEmpty(format))
                format = "yyyy_MM_dd";
            return path + "/Log_" + DateTime.Now.ToString(format) + ".txt";// System.IO.Path.Combine(path, "Log_" + DateTime.Now.ToString(format) + ".txt");
        }

        static public void Info(string message, params object[] parameters)
        {
            if (!LogInfoMessages) return;

            string s = GetDateTime() + " \t LOG \t " + string.Format(message, parameters);

            if (Logger.LogMessage != null)
                Logger.LogMessage("INFO", s);

            string path = Logger.GetLogFileName(Logger.LogInfoPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {

                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(s);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }

            if (ConfigurationManager.AppSettings["WriteLogsInDB"] != null && ConfigurationManager.AppSettings["WriteLogsInDB"].ToString() == "true")
            {
                bool isClosed = false;
                DBSession session = new DBSession(ConfigurationManager.ConnectionStrings["ConnStr_Archive"].ConnectionString);
                if (session.Connection.State == System.Data.ConnectionState.Closed)
                {
                    isClosed = true;
                    session.OpenConnection();
                }

                ExceptionHandler.SaveErrorInDB(GetSessionUser(), "Info", s, "", session);

                if (isClosed)
                    session.CloseConnection();
            }
        }

        static public void PaymentBtnTrackInfo(string message, params object[] parameters)
        {
            if (!LogInfoMessages) return;

            string s = GetDateTime() + " \t LOG \t " + string.Format(message, parameters);

            if (Logger.LogMessage != null)
                Logger.LogMessage("INFO", s);

            string path = Logger.GetLogFileName(Logger.LogInfoPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {

                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(s);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }
        }

        static public void Debug(string message, params object[] parameters)
        {
            if (!LogDebugMessages) return;
            string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " \t DEBUG \t " + string.Format(message, parameters);

            if (Logger.LogMessage != null)
                Logger.LogMessage("DEBUG", s);

            string path = Logger.GetLogFileName(Logger.LogPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(s);
                w.Close();
            }

            if (ConfigurationManager.AppSettings["WriteLogsInDB"] != null && ConfigurationManager.AppSettings["WriteLogsInDB"].ToString() == "true")
            {
                bool isClosed = false;
                DBSession session = new DBSession(ConfigurationManager.ConnectionStrings["ConnStr_Archive"].ConnectionString);
                if (session.Connection.State == System.Data.ConnectionState.Closed)
                {
                    isClosed = true;
                    session.OpenConnection();
                }

                ExceptionHandler.SaveErrorInDB(GetSessionUser(), "Debug", s, "", session);

                if (isClosed)
                    session.CloseConnection();
            }
        }

        static public void Error(string message, params object[] parameters)
        {
            if (!LogErrorMessages) return;

            string s = GetDateTime() + " \t ERROR \t " + string.Format(message, parameters);

            if (Logger.LogMessage != null)
                Logger.LogMessage("ERROR", s);

            string path = Logger.GetLogFileName(Logger.LogPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(s);
                w.Close();

            }

            if (ConfigurationManager.AppSettings["WriteLogsInDB"] != null && ConfigurationManager.AppSettings["WriteLogsInDB"].ToString() == "true")
            {
                bool isClosed = false;
                DBSession session = new DBSession(ConfigurationManager.ConnectionStrings["ConnStr_Archive"].ConnectionString);
                if (session.Connection.State == System.Data.ConnectionState.Closed)
                {
                    isClosed = true;
                    session.OpenConnection();
                }

                ExceptionHandler.SaveErrorInDB(GetSessionUser(), "Error", parameters[1].ToString(), parameters[2].ToString(), session);

                if (isClosed)
                    session.CloseConnection();
            }
        }

        static public void DetailedError1(params object[] parameters)
        {
            if (!LogErrorMessages) return;

            string date = "DATE: " + GetDateTime();
            string type = "TYPE: ERROR";
            string url = (parameters.Length > 0) ? "URL: " + parameters[0].ToString() : "";
            string msg = (parameters.Length > 1) ? "MESSAGE: " + parameters[1].ToString() : "";
            string stackTrace = (parameters.Length > 2) ? "STACK TRACE: " + parameters[2].ToString() : "";
            string user = "USER_ID: ";
            string page = "PAGE: ";
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    object usr = HttpContext.Current.Session["User"];
                    System.Reflection.PropertyInfo pi = usr.GetType().GetProperty("id");
                    if (pi != null)
                    {
                        int userId = Convert.ToInt32((pi.GetValue(usr, null)));
                        user += userId.ToString();
                    }
                    else
                    {
                        ElioUsers userInSession = usr as ElioUsers;
                        if (userInSession != null)
                        {
                            user += userInSession.Id.ToString();
                        }
                    }
                }

                if (HttpContext.Current.Session["page"] != null)
                {
                    page += HttpContext.Current.Session["page"].ToString();
                }
            }

            if (Logger.LogMessage != null)
                Logger.LogMessage("ERROR", date + " " + type + " " + url + " " + msg + " " + stackTrace);

            string path = Logger.GetLogFileName(Logger.LogPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(date);
                w.WriteLine(type);
                if (!string.IsNullOrEmpty(url))
                    w.WriteLine(url);
                if (!string.IsNullOrEmpty(user))
                    w.WriteLine(user);
                if (!string.IsNullOrEmpty(page))
                    w.WriteLine(page);
                if (!string.IsNullOrEmpty(msg))
                    w.WriteLine(msg);
                if (!string.IsNullOrEmpty(stackTrace))
                    w.WriteLine(stackTrace);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }
        }

        public static int GetSessionUser()
        {
            int sessionUser = -1;

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    object usr = HttpContext.Current.Session["User"];
                    System.Reflection.PropertyInfo pi = usr.GetType().GetProperty("id");
                    if (pi != null)
                        sessionUser = Convert.ToInt32((pi.GetValue(usr, null)));
                    else
                    {
                        ElioUsers userInSession = usr as ElioUsers;
                        if (userInSession != null)
                        {
                            sessionUser = userInSession.Id;
                        }
                    }
                }
            }

            return sessionUser;
        }

        static public void DetailedError(params object[] parameters)
        {
            if (!LogErrorMessages) return;

            string date = "DATE: " + GetDateTime();
            string type = "TYPE: ERROR";
            string url = (parameters.Length > 0) ? "URL: " + parameters[0].ToString() : "";
            string msg = (parameters.Length > 1) ? "MESSAGE: " + parameters[1].ToString() : "";
            string stackTrace = (parameters.Length > 2) ? "STACK TRACE: " + parameters[2].ToString() : "";
            string user = "USER_ID: ";
            string page = "PAGE: ";
            int userId = -1;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["User"] != null)
                    {
                        object usr = HttpContext.Current.Session["User"];
                        System.Reflection.PropertyInfo pi = usr.GetType().GetProperty("id");
                        if (pi != null)
                        {
                            userId = Convert.ToInt32((pi.GetValue(usr, null)));
                            user += userId.ToString();
                        }
                        else
                        {
                            ElioUsers userInSession = usr as ElioUsers;
                            if (userInSession != null)
                            {
                                user += userInSession.Id.ToString();
                            }
                        }
                    }

                    if (HttpContext.Current.Session["page"] != null)
                    {
                        page += HttpContext.Current.Session["page"].ToString();
                    }
                }
            }
            if (Logger.LogMessage != null)
                Logger.LogMessage("ERROR", date + " " + type + " " + url + " " + msg + " " + stackTrace);

            string path = Logger.GetLogFileName(Logger.LogPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(date);
                w.WriteLine(type);
                if (!string.IsNullOrEmpty(url))
                    w.WriteLine(url);
                if (!string.IsNullOrEmpty(user))
                    w.WriteLine(user);
                if (!string.IsNullOrEmpty(page))
                    w.WriteLine(page);
                if (!string.IsNullOrEmpty(msg))
                    w.WriteLine(msg);
                if (!string.IsNullOrEmpty(stackTrace))
                    w.WriteLine(stackTrace);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }

            if (ConfigurationManager.AppSettings["WriteLogsInDB"] != null && ConfigurationManager.AppSettings["WriteLogsInDB"].ToString() == "true")
            {
                bool isClosed = false;
                DBSession session = new DBSession(ConfigurationManager.ConnectionStrings["ConnStr_Archive"].ConnectionString);
                if (session.Connection.State == System.Data.ConnectionState.Closed)
                {
                    isClosed = true;
                    session.OpenConnection();
                }

                ExceptionHandler.SaveErrorInDB(userId, "DetailedError", parameters[1].ToString(), parameters[2].ToString(), session);

                if (isClosed)
                    session.CloseConnection();
            }
        }

        static public void DetailedClearBitError(params object[] parameters)
        {
            if (!LogErrorMessages) return;

            //string date = "DATE: " + GetDateTime();
            //string type = "TYPE: ERROR";
            //string url = (parameters.Length > 0) ? "URL: " + parameters[0].ToString() : "";
            string msg = (parameters.Length > 0) ? parameters[0].ToString() : "";
            //string stackTrace = (parameters.Length > 2) ? "STACK TRACE: " + parameters[2].ToString() : "";
            //string user = "USER_ID: ";
            //string page = "PAGE: ";
            //int userId = -1;

            //if (HttpContext.Current.Session != null)
            //{
            //    if (HttpContext.Current.Session["User"] != null)
            //    {
            //        object usr = HttpContext.Current.Session["User"];
            //        System.Reflection.PropertyInfo pi = usr.GetType().GetProperty("id");
            //        if (pi != null)
            //        {
            //            userId = Convert.ToInt32((pi.GetValue(usr, null)));
            //            user += userId.ToString();
            //        }
            //        else
            //        {
            //            ElioUsers userInSession = usr as ElioUsers;
            //            if (userInSession != null)
            //            {
            //                user += userInSession.Id.ToString();
            //            }
            //        }
            //    }

            //    if (HttpContext.Current.Session["page"] != null)
            //    {
            //        page += HttpContext.Current.Session["page"].ToString();
            //    }
            //}

            //if (Logger.LogMessage != null)
            //    Logger.LogMessage("ERROR", date + " " + type + " " + url + " " + msg + " " + stackTrace);

            string path = Logger.GetLogFileName(Logger.LogPathClearBit);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                //w.WriteLine(date);
                //w.WriteLine(type);
                //if (!string.IsNullOrEmpty(url))
                //    w.WriteLine(url);
                //if (!string.IsNullOrEmpty(user))
                //    w.WriteLine(user);
                //if (!string.IsNullOrEmpty(page))
                //    w.WriteLine(page);
                if (!string.IsNullOrEmpty(msg))
                    w.WriteLine(msg + ",");
                //if (!string.IsNullOrEmpty(stackTrace))
                //    w.WriteLine(stackTrace);
                //w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }

            if (ConfigurationManager.AppSettings["WriteLogsInDB"] != null && ConfigurationManager.AppSettings["WriteLogsInDB"].ToString() == "true")
            {
                bool isClosed = false;
                DBSession session = new DBSession(ConfigurationManager.ConnectionStrings["ConnStr_Archive"].ConnectionString);
                if (session.Connection.State == System.Data.ConnectionState.Closed)
                {
                    isClosed = true;
                    session.OpenConnection();
                }

                ExceptionHandler.SaveErrorInDB(1, "DetailedError", parameters[1].ToString(), parameters[2].ToString(), session);

                if (isClosed)
                    session.CloseConnection();
            }
        }

        static public void StatisticsData(params object[] parameters)
        {
            if (!LogErrorMessages) return;

            string date = "DATE: " + GetDateTime();
            string type = "TYPE: ANONYMOUS DATA";
            string url = (parameters.Length > 0) ? "URL: " + parameters[1].ToString() : "";
            string msg = (parameters.Length > 1) ? "PATH: " + parameters[2].ToString() : "";
            string stackTrace = (parameters.Length > 2) ? "VISITOR IP ADDRESS: " + parameters[3].ToString() : "";
            
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session != null)
                {
                    //string addr = HttpContext.Current.Request.ServerVariables["remote_addr"];
                    //string var1 = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
                    //string var2 = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
                    //string var3 = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
                    //string var4 = HttpContext.Current.Request.ServerVariables["remote_addr"];

                    //string hostName = HttpContext.Current.Request.UserHostName;
                    //string hostAddr = HttpContext.Current.Request.UserHostAddress;
                    //string browser = HttpContext.Current.Request.Browser.Browser;
                }
            }

            if (Logger.LogMessage != null)
                Logger.LogMessage("ERROR", date + " " + type + " " + url + " " + msg + " " + stackTrace);

            string path = Logger.GetLogFileName(Logger.LogAnonymousPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(date);
                w.WriteLine(type);
                if (!string.IsNullOrEmpty(url))
                    w.WriteLine(url);
                //if (!string.IsNullOrEmpty(user))
                //    w.WriteLine(user);
                //if (!string.IsNullOrEmpty(page))
                //    w.WriteLine(page);
                if (!string.IsNullOrEmpty(msg))
                    w.WriteLine(msg);
                if (!string.IsNullOrEmpty(stackTrace))
                    w.WriteLine(stackTrace);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }
        }

        static public void DetailedErrorWithEmailNotification(DBSession session, params object[] parameters)
        {
            if (!LogErrorMessages) return;

            string date = "DATE: " + GetDateTime();
            string type = "TYPE: ERROR";
            string url = (parameters.Length > 0) ? "URL: " + parameters[0].ToString() : "";
            string msg = (parameters.Length > 1) ? "MESSAGE: " + parameters[1].ToString() : "";
            string stackTrace = (parameters.Length > 2) ? "STACK TRACE: " + parameters[2].ToString() : "";
            string user = "USER_ID: ";
            string page = "PAGE: ";
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    object usr = HttpContext.Current.Session["User"];
                    System.Reflection.PropertyInfo pi = usr.GetType().GetProperty("ID");
                    if (pi != null)
                    {
                        int userId = Convert.ToInt32((pi.GetValue(usr, null)));
                        user += userId.ToString();
                    }
                    else
                    {
                        ElioUsers userInSession = usr as ElioUsers;
                        if (userInSession != null)
                        {
                            user += userInSession.Id.ToString();
                        }
                    }
                }

                if (HttpContext.Current.Session["page"] != null)
                {
                    page += HttpContext.Current.Session["page"].ToString();
                }
            }

            if (Logger.LogMessage != null)
                Logger.LogMessage("ERROR", date + " " + type + " " + url + " " + msg + " " + stackTrace);

            string path = Logger.GetLogFileName(Logger.LogPath);
            if (string.IsNullOrEmpty(path))
                return;

            lock (Locker)
            {
                System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);
                w.WriteLine(date);
                w.WriteLine(type);
                if (!string.IsNullOrEmpty(url))
                    w.WriteLine(url);
                if (!string.IsNullOrEmpty(user))
                    w.WriteLine(user);
                if (!string.IsNullOrEmpty(page))
                    w.WriteLine(page);
                if (!string.IsNullOrEmpty(msg))
                    w.WriteLine(msg);
                if (!string.IsNullOrEmpty(stackTrace))
                    w.WriteLine(stackTrace);
                w.WriteLine("---------------------------------------------------------------------------------------------------------------------------");
                w.Close();
            }

            EmailSenderLib.SendErrorNotificationEmail(type, url, msg, stackTrace, "en", session);
        }
    }
}
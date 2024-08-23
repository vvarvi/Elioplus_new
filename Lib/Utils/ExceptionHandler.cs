using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Utils
{
    public class ExceptionHandler : Exception
    {
        public ExceptionHandler()
        {

        }

        public static void SaveErrorInDB(int userId, string traceFunction, string message, string stackTrace, DBSession session)
        {
            //Sql.SaveInDB(userId, HttpContext.Current.Session["page"].ToString(), ex.Message, ex.StackTrace.ToString(), session);

            ElioExceptions exception = new ElioExceptions();

            exception.UserInSession = userId;
            exception.CurrentSessionId = HttpContext.Current.Session.SessionID;
            exception.PageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            exception.TraceFunction = traceFunction;
            exception.ExceptionMessage = message;
            exception.StackTrace = stackTrace;
            exception.RemoteIp = HttpContext.Current.Request.ServerVariables["remote_addr"];
            exception.sysdate = DateTime.Now;
            exception.LastUpdate = DateTime.Now;

            DataLoader<ElioExceptions> loader = new DataLoader<ElioExceptions>(session);
            loader.Insert(exception);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus
{
    public partial class TaskReminderSenderPage : System.Web.UI.Page
    {
        DBSession session = new DBSession();
        ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    if (Request.QueryString["TaskViewID"] != null)
                    {
                        try
                        {
                            EmailSenderLib.SendTaskReminderEmail(Convert.ToInt32(Request.QueryString["TaskViewID"]), vSession.Lang, session);

                            GlobalMethods.ShowMessageControl(UcMessageControl, "Reminder Email for Task was send successfully", MessageTypes.Success, true, true, false);
                        }
                        catch (Exception ex)
                        {
                            GlobalMethods.ShowMessageControl(UcMessageControl, "Reminder Email for Task could not be send. Message Error:" + ex.Message.ToString(), MessageTypes.Error, true, true, false);
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnElioHome_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.Default(), false);
            }

            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}
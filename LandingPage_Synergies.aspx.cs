using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus
{
    public partial class LandingPage_Synergies : System.Web.UI.Page
    {
        DBSession session = new DBSession();
 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSendEmail_OnClick(object sender, EventArgs args)
        {
            try
            {
                LblError.Text = string.Empty;

                if (TbxEmail.Text.Trim() == string.Empty)
                {
                    LblError.Text = "Please enter email address";
                    return;
                }
                else
                {
                    if (!Validations.IsEmail(TbxEmail.Text))
                    {
                        LblError.Text = "Please enter a valid email address";
                        return;
                    }
                }

                session.OpenConnection();

                EmailSenderLib.SaveUsersEmailFromLandingPages(TbxEmail.Text, "Video Intro - elio", "en", session);

                TbxEmail.Text = string.Empty;
                LblError.Text = string.Empty;

                Response.Redirect("https://www.elioplus.com", false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                Response.Redirect("https://www.elioplus.com", false);

                TbxEmail.Text = string.Empty;
                LblError.Text = string.Empty;
            }
            finally
            {
                session.CloseConnection();
            }
        }
    }
}
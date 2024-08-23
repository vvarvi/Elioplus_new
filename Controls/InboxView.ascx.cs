using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxView : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && HttpContext.Current.Request.Url.AbsolutePath != null)
                {
                    FixPage();
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

        private void FixPage()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathArray = path.Split('/');

            List<string> pathElements = new List<string>();

            foreach (string item in pathArray)
            {
                if (item != string.Empty)
                {
                    pathElements.Add(item);
                }
            }

            int number;
            int messageId = (int.TryParse(pathElements[4], out number)) ? number : 0;

            if (messageId > 0)
            {
                ElioUsersMessages messageToView = Sql.GetUsersMessageById(messageId, session);
                if (messageToView != null)
                {
                    ElioUsers senderUser = Sql.GetUserById(messageToView.SenderUserId, session);
                    ElioUsers receiverUser = Sql.GetUserById(messageToView.ReceiverUserId, session);

                    LblTitle.Text = "View message";
                    LblMessageTitle.Text = messageToView.Subject;
                    ImgSenderPhoto.ImageUrl = (senderUser != null) ? senderUser.CompanyLogo : "/images/no_logo_company.png";
                    ImgSenderPhoto.AlternateText = (senderUser != null) ? senderUser.CompanyName + "logo" : "company logo not available";
                    LblSenderName.Text = (senderUser != null) ? senderUser.CompanyName : "sender company name";
                    LblSenderEmail.Text = messageToView.SenderEmail;
                    LblReceiverName.Text = (receiverUser != null) ? receiverUser.CompanyName : "receiver company name";
                    LblDateMessageReceived.Text = messageToView.Sysdate.ToString("MM/dd/yyyy");
                    LblMessageContent.Text = messageToView.Message;
                    aReply.HRef = ControlLoader.Dashboard(vSession.User, "messages/" + messageId + "/reply");
                }
            }
        }
    }
}
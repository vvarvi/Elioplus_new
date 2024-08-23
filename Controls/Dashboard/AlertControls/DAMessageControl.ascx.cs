using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Controls.Dashboard.AlertControls
{
    public partial class DAMessageControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();

        public string Message { get; set; }
        public MessageTypes MessageType { get; set; }
        public HtmlGenericControl ImageIcon { get { return Icon; } }
        public bool IsLong { get; set; }
        public bool HasClose { get; set; }
        public bool IsLightColor { get; set; }
        public bool ShowImg { get; set; }
        public bool ShowLnkBtnRgstr { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.PreRender += new EventHandler(MessageAlert_PreRender);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        void MessageAlert_PreRender(object sender, EventArgs e)
        {
            Icon.Visible = ShowImg;
            LblMessage.Text = Message;
            PnlRegisterMsg.Visible = ShowLnkBtnRgstr;
            PnlMessage.Visible = (string.IsNullOrEmpty(Message)) ? false : true;
            divClose.Visible = HasClose;

            switch (this.MessageType)
            {
                case MessageTypes.Error:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "alert alert-custom alert-danger" : "alert alert-custom alert-light-danger fade show";
                    
                    if(ShowImg)
                    {
                        Icon.Attributes["class"] = "flaticon-warning";
                    }

                    break;

                case MessageTypes.Info:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "alert alert-custom alert-primary" : "alert alert-custom alert-light-primary fade show";
                    
                    if (ShowImg)
                    {
                        Icon.Attributes["class"] = "flaticon-warning";
                    }

                    break;

                case MessageTypes.Success:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "alert alert-custom alert-success" : "alert alert-custom alert-light-success fade show";
                    
                    if (ShowImg)
                    {
                        Icon.Attributes["class"] = "flaticon-warning";
                    }
                    
                    break;

                case MessageTypes.Warning:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "alert alert-custom alert-warning" : "alert alert-custom alert-light-warning fade show";
                    
                    if (ShowImg)
                    {
                        Icon.Attributes["class"] = "flaticon-warning";
                    }

                    break;
            }
        }

        #region Buttons

        protected void LnkBtnRegister_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.FullRegistrationPage, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}
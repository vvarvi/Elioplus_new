using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Controls.AlertControls
{
    public partial class MessageControl2Msgs : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();

        public string Message { get; set; }
        public string Message2 { get; set; }
        public MessageTypes MessageType { get; set; }
        public HtmlImage ImageIcon { get { return Icon; } }
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
            LblMessage2.Text = Message2;
            //PnlRegisterMsg.Visible = ShowLnkBtnRgstr;
            PnlMessage.Visible = (string.IsNullOrEmpty(Message)) ? false : true;
            //divClose.Visible = HasClose;
            contentMsg.Attributes["class"] = contentMsg2.Attributes["class"] = (!IsLightColor) ? "text-sm text-white" : "text-sm text-textGray";
            titleMsg.Attributes["class"] = (!IsLightColor) ? "font-bold text-base text-white" : "font-bold text-base text-blackBlue";

            switch (this.MessageType)
            {
                case MessageTypes.Error:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "card bg-red text-white gap-15px" : "card border-2 border-red gap-15px";
                    LblTitle.Text = "Error!";

                    if (ShowImg)
                    {
                        Icon.Src = (!IsLightColor) ? "/assets_out/images/alerts/error-white.svg" : "/assets_out/images/alerts/error.svg";
                    }

                    break;

                case MessageTypes.Info:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "card bg-blue text-white gap-15px" : "card border-2 border-blue gap-15px";
                    LblTitle.Text = "Information!";

                    if (ShowImg)
                    {
                        Icon.Src = (!IsLightColor) ? "/assets_out/images/alerts/info-white.svg" : "/assets_out/images/alerts/info.svg";
                    }

                    break;

                case MessageTypes.Success:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "card bg-green text-white gap-15px" : "card border-2 border-green gap-15px";
                    LblTitle.Text = "Success!";

                    if (ShowImg)
                    {
                        Icon.Src = (!IsLightColor) ? "/assets_out/images/alerts/success-white.svg" : "/assets_out/images/alerts/success.svg";
                    }

                    break;

                case MessageTypes.Warning:
                    PnlMessage.Attributes["class"] = (!IsLightColor) ? "card bg-yellow text-white gap-15px" : "card border-2 border-yellow gap-15px";
                    LblTitle.Text = "Warning!";

                    if (ShowImg)
                    {
                        Icon.Src = (!IsLightColor) ? "/assets_out/images/alerts/warning-white.svg" : "/assets_out/images/alerts/warning.svg";
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
using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls.AlertControls
{
    public partial class InfoMessageControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();

        public string Message { get; set; }
        public string ToolTipMessage { get; set; }
        public MessageTypes MessageType { get; set; }
        public ImageButton ImageButtonIcon { get { return ImgBtnIcon; } }
        public bool IsLong { get; set; }
        public bool ShowImg { get; set; }
        public bool ShowToolTip { get; set; }
        public bool LnkBtnRgstr { get; set; }

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
            ImgBtnIcon.Visible = ShowImg;
            LblMessage.Text = Message;
            LnkBtnRegister.Visible = LnkBtnRgstr;
            PnlMessage.Visible = (string.IsNullOrEmpty(Message)) ? false : true;
            if (ShowToolTip)
                RttMessage.Text = ToolTipMessage;
            else
                RttMessage.Visible = false;

            switch (this.MessageType)
            {
                case MessageTypes.Error:
                    PnlMessage.CssClass = (IsLong) ? "card bg-red text-white gap-15px" : "card bg-red text-white gap-15px";
                    
                    if(ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "../assets/images/alerts/error-white.svg";
                    }

                    break;

                case MessageTypes.Info:
                    PnlMessage.CssClass = (IsLong) ? "card bg-blue text-white gap-15px" : "card bg-blue text-white gap-15px";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "../assets/images/alerts/info-white.svg";
                    }

                    break;

                case MessageTypes.Success:
                    PnlMessage.CssClass = (IsLong) ? "card bg-green text-white gap-15px" : "card bg-green text-white gap-15px";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "../assets/images/alerts/success-white.svg";
                    }
                    
                    break;

                case MessageTypes.Warning:
                    PnlMessage.CssClass = (IsLong) ? "card bg-yellow text-white gap-15px" : "card bg-yellow text-white gap-15px";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "../assets/images/alerts/warning-white.svg";
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
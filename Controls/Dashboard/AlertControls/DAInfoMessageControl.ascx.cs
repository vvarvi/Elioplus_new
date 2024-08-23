using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls.Dashboard.AlertControls
{
    public partial class DAInfoMessageControl : System.Web.UI.UserControl
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
                    PnlMessage.CssClass = (IsLong) ? "message_error_send" : "message_error";
                    
                    if(ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "~/Images/icons/small/error.png";
                    }

                    break;

                case MessageTypes.Info:
                    PnlMessage.CssClass = (IsLong) ? "message_info_send" : "message_info";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "~/Images/icons/small/info.png";
                    }

                    break;

                case MessageTypes.Success:
                    PnlMessage.CssClass = (IsLong) ? "message_success_send" : "message_success";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "~/Images/icons/small/success.png";
                    }
                    
                    break;

                case MessageTypes.Warning:
                    PnlMessage.CssClass = (IsLong) ? "message_warning_send" : "message_warning";
                    
                    if (ShowImg)
                    {
                        ImgBtnIcon.ImageUrl = "~/Images/icons/small/warning.png";
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
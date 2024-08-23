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
    public partial class NotificationControl : System.Web.UI.UserControl
    {
        public string Text { get; set; }
        public MessageTypes MessageType { get; set; }
        public bool IsLightColor { get; set; }
        public bool IsBold { get; set; }
        public bool IsVisible { get; set; }
        public bool IsNumber { get; set; }
        public bool HasLeftMargin { get; set; }
        public bool HasRightMargin { get; set; }

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
            spanNaviLabel.Visible = IsVisible;
            LblNotif.Text = Text;
            spanNotif.Attributes["class"] = "label label-rounded";

            switch (this.MessageType)
            {
                case MessageTypes.Error:
                    spanNotif.Attributes["class"] += (!IsLightColor) ? " label-danger" : " label-light-danger";

                    break;

                case MessageTypes.Info:
                    spanNotif.Attributes["class"] += (!IsLightColor) ? " label-primary" : " label-light-primary";

                    break;

                case MessageTypes.Success:
                    spanNotif.Attributes["class"] += (!IsLightColor) ? " label-success" : " label-light-success";

                    break;

                case MessageTypes.Warning:
                    spanNotif.Attributes["class"] += (!IsLightColor) ? " label-warning" : " label-light-warning";

                    break;
            }

            if (IsBold)
                spanNotif.Attributes["class"] += " font-weight-bolder";

            if (!IsNumber)
                spanNotif.Attributes["style"] = "min-width:35px;";

            if (HasLeftMargin && !HasRightMargin)
                spanNaviLabel.Attributes["style"] = "margin-left:10px;";
            else if (HasRightMargin && !HasLeftMargin)
                spanNaviLabel.Attributes["style"] = "margin-right:10px;";
            else if (HasRightMargin && HasLeftMargin)
                spanNaviLabel.Attributes["style"] = "margin-left:10px;margin-right:10px;";
        }      
    }
}
//using HelperSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Controls.Dashboard.LibraryStorage
{
    public partial class UcStorageVolume : System.Web.UI.UserControl
    {
        DBSession session = new DBSession();
        ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            GetUserStorageInfo();
                            UpdateStrings();

                            divDataVolumeTitle.Visible = true;
                            divDataVolumeContent.Visible = true;
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        private void UpdateStrings()
        {
            LblDataVolume.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "storageVolume", "label", "1")).Text;
            LblLibraryUsedSpace.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "storageVolume", "label", "2")).Text;
            LblLibraryFreeSpace.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "storageVolume", "label", "3")).Text;
        }

        public void GetUserStorageInfo()
        {
            bool mustClose = false;
            if (session.Connection.State == ConnectionState.Closed)
            {
                mustClose = true;
                session.OpenConnection();
            }

            decimal totalPacketItemValue = 0;
            decimal availablePacketItemValue = 0;
            decimal usedSpace = 0;
            decimal freeSpacePercent = 0;

            divTransitionProgress.Attributes["data-transitiongoal"] = GlobalDBMethods.GetUserUsedStorageSpacePercent2(vSession.User.Id, "LibraryStorage", ref totalPacketItemValue, ref availablePacketItemValue, ref usedSpace, ref freeSpacePercent, session).ToString();
            if (usedSpace.ToString().EndsWith("0"))
            {
                LblUsedSpace.Text = usedSpace.ToString().Substring(0, usedSpace.ToString().Length - 1);
                if (LblUsedSpace.Text.EndsWith("0"))
                    LblUsedSpace.Text = LblUsedSpace.Text.Substring(0, LblUsedSpace.Text.Length - 2);

                LblUsedSpace.Text += " GB";
            }
            else
                LblUsedSpace.Text = usedSpace.ToString() + " GB";

            if (availablePacketItemValue < 2 && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ImgInfo.Visible = true;
                divTransitionProgress.Attributes["class"] = "progress-bar progress-bar-danger";
                spanUsedSpace.Attributes["class"] = "pull-right label label-outline label-danger";
            }
            else
                ImgInfo.Visible = false;

            divFreeTransitionProgress.Attributes["data-transitiongoal"] = freeSpacePercent.ToString();
            if (availablePacketItemValue.ToString().EndsWith("0"))
            {
                LblFreeSpace.Text = availablePacketItemValue.ToString().Substring(0, availablePacketItemValue.ToString().Length - 1);
                if (LblFreeSpace.Text.EndsWith("0"))
                    LblFreeSpace.Text = LblFreeSpace.Text.Substring(0, LblFreeSpace.Text.Length - 2);

                LblFreeSpace.Text += " GB";
            }
            else
                LblFreeSpace.Text = availablePacketItemValue.ToString() + " GB";

            if (usedSpace > totalPacketItemValue - 2 && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ImgFreeInfo.Visible = true;
                divFreeTransitionProgress.Attributes["class"] = "progress-bar progress-bar-danger";
                spanFreeSpace.Attributes["class"] = "pull-right label label-outline label-danger";
            }
            else
                ImgFreeInfo.Visible = false;

            if (mustClose)
                session.CloseConnection();
        }
    }
}
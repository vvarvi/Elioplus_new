using System;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class ProfileDataViewMode : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStrings();

                if (vSession.User != null)
                {
                    LoadCompanyData();
                }
                else
                {
                    #region Redirect To Home

                    Response.Redirect(ControlLoader.Default(), false);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void LoadCompanyData()
        {
            ImgLogo.ImageUrl = (string.IsNullOrEmpty(vSession.User.PersonalImage)) ? "~/images/personal_img_3.png" : vSession.User.PersonalImage;
            ImgLogo.AlternateText = vSession.User.CompanyName + "logo";
            LblLastName.Text = (string.IsNullOrEmpty(vSession.User.LastName)) ? "-" : vSession.User.LastName;
            LblFirstName.Text = (string.IsNullOrEmpty(vSession.User.FirstName)) ? "-" : vSession.User.FirstName;
                        
            RbtnRegister.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;

            LblJobPosition.Text = (string.IsNullOrEmpty(vSession.User.Position)) ? "-" : vSession.User.Position;
            LblSummary.Text = (string.IsNullOrEmpty(vSession.User.CommunitySummaryText)) ? "-" : vSession.User.CommunitySummaryText;            
        }

        private void UpdateStrings()
        {
            LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "6")).Text;
            LblPersonalImage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "1")).Text;
            LblLastNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "2")).Text;
            LblFirstNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "3")).Text;
            LblJobPositionText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "4")).Text;
            LblSummaryText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "5")).Text;
           
            Label lblRegisterText = (Label)ControlFinder.FindControlRecursive(RbtnRegister, "LblRegisterText");
            lblRegisterText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "13")).Text;

            Label lblEditText = (Label)ControlFinder.FindControlRecursive(RbtnEdit, "LblEditText");
            lblEditText.Text = (vSession.User.CommunityStatus == 1) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "5")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "14")).Text;
        }

        #endregion

        #region Buttons

        protected void RbtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                PlaceHolder phProfiledata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhProfiledata");
                phProfiledata.Controls.Clear();

                ControlLoader.LoadElioControls(this, phProfiledata, vSession.LoadedDashboardProfileEditControl = ControlLoader.ProfileDataEditMode);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnRegister_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.SignUp, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
               
        #endregion
    }
}
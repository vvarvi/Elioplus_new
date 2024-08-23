using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.IO;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class ProfileDataEditMode : System.Web.UI.UserControl
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
                    FixPage();
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

        private void FixPage()
        {
            LoadCompanyData();
        }

        private void LoadCompanyData()
        {
            RtbxLastName.Text = vSession.User.LastName;
            RtbxFirstName.Text = vSession.User.FirstName;

            RtbxPosition.Text = vSession.User.Position;
            RtbxSummary.Text = vSession.User.CommunitySummaryText;
        }

        private void UpdateStrings()
        {
            LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "7")).Text;
            LblPersonalImage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "1")).Text;
            LblLastNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "2")).Text;
            LblFirstNameText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "3")).Text;
            LblJobPositionText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "4")).Text;
            LblSummaryText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "5")).Text;

            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;

            Label lblCancelText = (Label)ControlFinder.FindControlRecursive(RbtnCancel, "LblCancelText");
            lblCancelText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "7")).Text;

            Label lblClearFieldsText = (Label)ControlFinder.FindControlRecursive(RbtnClearFields, "LblClearFieldsText");
            lblClearFieldsText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "15")).Text;
            
        }

        private bool DataHasError()
        {
            if (string.IsNullOrEmpty(RtbxLastName.Text))
            {
                LblLastNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "message", "1")).Text;
                return true;
            }

            if (string.IsNullOrEmpty(RtbxFirstName.Text))
            {
                LblFirstNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "message", "2")).Text;
                return true;
            }

            if (string.IsNullOrEmpty(RtbxPosition.Text))
            {
                LblPositionError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "message", "3")).Text;
                return true;
            }

            //if (!vSession.HasSelectedPersonalImageToUpload)
            //{
            //    LblPersonalImageUploadError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "message", "5")).Text;
            //    return true;
            //}

            //if (string.IsNullOrEmpty(RtbxSummary.Text))
            //{
            //    LblSummaryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "message", "4")).Text;
            //    return true;
            //}
            
            return false;
        }

        private void ClearFields()
        {
            LblPersonalImageUploadError.Text = string.Empty;
            LblLastNameError.Text = string.Empty;
            LblFirstNameError.Text = string.Empty;
            LblPositionError.Text = string.Empty;
            LblSummaryError.Text = string.Empty;

            RtbxLastName.Text = string.Empty;
            RtbxFirstName.Text = string.Empty;
            RtbxPosition.Text = string.Empty;
            RtbxSummary.Text = string.Empty;
            PersonalImage.FileFilters.Clear();
        }

        #endregion

        #region Buttons

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (DataHasError()) return;

                if (vSession.User != null)
                {
                    try
                    {
                        ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
                        if (user != null)
                        {
                            session.BeginTransaction();

                            user.LastName = RtbxLastName.Text;
                            user.FirstName = RtbxFirstName.Text;

                            user.Position = GlobalMethods.FixStringEntryToParagraphs(RtbxPosition.Text);
                            user.CommunitySummaryText = GlobalMethods.FixStringEntryToParagraphs(RtbxSummary.Text);
                            user.CommunityStatus = Convert.ToInt32(AccountStatus.Completed);
                            user.CommunityProfileCreated = DateTime.Now;
                            user.CommunityProfileLastUpdated = DateTime.Now;
                            user.LastUpdated = DateTime.Now;

                            vSession.User = GlobalDBMethods.UpDateUser(user, session);

                            session.CommitTransaction();

                            ClearFields();

                            PlaceHolder phProfiledata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhProfiledata");
                            phProfiledata.Controls.Clear();

                            vSession.LoadedDashboardProfileEditControl = ControlLoader.ProfileDataViewMode;
                            ControlLoader.LoadElioControls(this, phProfiledata, vSession.LoadedDashboardProfileEditControl);

                            GlobalMethods.ClearCriteriaSession(vSession, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
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

        protected void RbtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    #region Delete Uploaded Images

                    if (vSession.User.CommunityStatus == Convert.ToInt32(AccountStatus.NotCompleted) && (!string.IsNullOrEmpty(vSession.User.PersonalImage)))
                    {
                        DirectoryInfo originaldir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PersonalImageTargetFolder"].ToString()) + vSession.User.GuId + "\\");
                        if (originaldir != null)
                        {
                            foreach (FileInfo fi in originaldir.GetFiles())
                            {
                                fi.Delete();
                            }
                        }

                        vSession.User.PersonalImage = string.Empty;

                        vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);
                    }

                    #endregion
                }

                GlobalMethods.ClearCriteriaSession(vSession, false);

                PlaceHolder phProfiledata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhProfiledata");
                phProfiledata.Controls.Clear();

                vSession.LoadedDashboardProfileEditControl = ControlLoader.ProfileDataViewMode;
                ControlLoader.LoadElioControls(this, phProfiledata, vSession.LoadedDashboardProfileEditControl);
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

        protected void RbtnClearFields_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearFields();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Upload Logo

        protected void PersonalImage_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                vSession.SuccessfullPersonalImageUpload = false;

                if (vSession.User != null)
                {
                    vSession.SuccessfullPersonalImageUpload = UpLoadImage.UpLoadPersonalImage(vSession.User, Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PersonalImageTargetFolder"].ToString()), e, session);

                    if (vSession.SuccessfullPersonalImageUpload)
                    {
                        string imgNewName=ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());
                        vSession.User.PersonalImage = "~/images/PersonalImages/" + vSession.User.GuId + "/" + imgNewName;
                    }
                    vSession.HasSelectedPersonalImageToUpload = true;
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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

        #endregion
    }
}
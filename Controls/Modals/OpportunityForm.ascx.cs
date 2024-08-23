using System;
using System.Linq;
using System.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;

namespace WdS.ElioPlus.Controls.Modals
{
    public partial class OpportunityForm : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int OpportunityId
        {
            get
            {
                return ViewState["OpportunityId"] == null ? 0 : (int)ViewState["OpportunityId"];
            }
            set
            {
                ViewState["OpportunityId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    FixPage();
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

        #region Methods

        private void FixPage()
        {
            ResetFields(true);
            LoadSteps();
        }

        private void LoadSteps()
        {
            List<ElioOpportunitiesStatus> steps = Sql.GetOpportunityPublicStatus(session);

            RcbxSteps.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Text = "-- Select status --";
            item.Value = "0";
            RcbxSteps.Items.Add(item);

            foreach (ElioOpportunitiesStatus step in steps)
            {
                item = new RadComboBoxItem();

                item.Text = step.StepDescription;
                item.Value = step.Id.ToString();

                RcbxSteps.Items.Add(item);
            }
        }
               
        private void ResetFields(bool errorsOnly)
        {
            if (!errorsOnly)
            {
                RtbxId.Text = "0";
                RtbxUserId.Text = "0";
                RtbxFirstname.Text = string.Empty;
                RtxLastname.Text = string.Empty;
                RtbxOrganization.Text = string.Empty;
                RtbxOccupation.Text = string.Empty;
                RtbxAddress.Text = string.Empty;
                RtbxEmail.Text = string.Empty;
                RtbxAddress.Text = string.Empty;
                RtbxPhone.Text = string.Empty;
                RtbxWebsite.Text = string.Empty;
                RtbxLinkedin.Text = string.Empty;
                RtbxTwitter.Text = string.Empty;

                RbtnCancelEdit.Visible = false;

                RcbxSteps.FindItemByValue("0").Selected = true;
            }

            LblFirstnameError.Text = string.Empty;
            LblLastnameError.Text = string.Empty;
            LblOrganizationError.Text = string.Empty;
            LblOccupationError.Text = string.Empty;
            LblEmailError.Text = string.Empty;
            LblWebsiteError.Text = string.Empty;
            LblAddressError.Text = string.Empty;
            LblPhoneError.Text = string.Empty;
            LblLinkedinError.Text = string.Empty;
            LblTwitterError.Text = string.Empty;
            LblStepsError.Text = string.Empty;

            divOpportunityGeneralSuccess.Visible = false;
            divOpportunityGeneralFailure.Visible = false;

            //LblFailure.Text = string.Empty;
            //LblSuccess.Text = string.Empty;
        }

        #endregion

        #region Buttons

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ResetFields(true);

                    string alert = string.Empty;

                    #region Check Fields

                    //if (RtxLastname.Text == string.Empty)
                    //{
                    //    LblLastnameError.Text = "Please add Last Name";
                    //    return;
                    //}

                    //if (RtbxFirstname.Text == string.Empty)
                    //{
                    //    LblFirstnameError.Text = "Please add First Name";
                    //    return;
                    //}

                    if (RtbxOrganization.Text == string.Empty)
                    {
                        LblOrganizationError.Text = "Please add Organization Name";
                        return;
                    }

                    //if (RtbxOccupation.Text == string.Empty)
                    //{
                    //    LblOccupationError.Text = "Please add Contact Role";
                    //    return;
                    //}

                    if (RtbxEmail.Text == string.Empty)
                    {
                        LblEmailError.Text = "Please add Email";
                        return;
                    }
                    else
                    {
                        if (!Validations.IsEmail(RtbxEmail.Text))
                        {
                            LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "3")).Text;
                            return;
                        }
                    }

                    //if (RtbxAddress.Text == string.Empty)
                    //{
                    //    LblAddressError.Text = "Please add Address";
                    //    return;
                    //}

                    //if (RtbxPhone.Text == string.Empty)
                    //{
                    //    LblPhoneError.Text = "Please add Phone";
                    //    return;
                    //}

                    //if (RtbxWebsite.Text == string.Empty)
                    //{
                    //    LblWebsiteError.Text = "Please add Website";
                    //    return;
                    //}

                    if (RcbxSteps.SelectedValue == "0")
                    {
                        LblStepsError.Text = "Please select step";
                        return;
                    }

                    #endregion

                    #region Contact Info

                    //ElioUsers user = new ElioUsers();

                    //user.Username = "";
                    //user.UsernameEncrypted = "";
                    //user.Password = "";
                    //user.PasswordEncrypted = "";
                    //user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                    //user.Phone = RtbxPhone.Text;
                    //user.Address = RtbxAddress.Text;
                    //user.Country = "";
                    //user.WebSite = RtbxWebsite.Text;
                    //user.Email = RtbxEmail.Text;
                    //user.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                    //user.Overview = "";
                    //user.Description = "";
                    //user.CompanyName = RtbxOrganization.Text;
                    //user.CompanyType = "";
                    //user.CompanyLogo = "";
                    //user.GuId = Guid.NewGuid().ToString();
                    //user.OfficialEmail = "";
                    //user.FeaturesNo = 0;
                    //user.UserLoginCount = 0;
                    //user.LastLogin = DateTime.Now;
                    //user.MashapeUsername = "";
                    //user.IsPublic = Convert.ToInt32(AccountStatus.NotPublic);
                    //user.FirstName = RtbxFirstname.Text;
                    //user.LastName = RtxLastname.Text;
                    //user.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                    //user.PersonalImage = "";
                    //user.CommunityProfileCreated = null;
                    //user.CommunityProfileLastUpdated = null;
                    //user.Position = "";
                    //user.CommunitySummaryText = "";
                    //user.LinkedInUrl = RtbxLinkedin.Text = "";
                    //user.TwitterUrl = RtbxTwitter.Text;
                    //user.HasBillingDetails = 0;
                    //user.BillingType = Convert.ToInt32(UserApplicationType.Opportunity);
                    //user.CustomerStripeId = "";

                    #endregion

                    DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);
                    ElioOpportunitiesUsers opportunity = new ElioOpportunitiesUsers();

                    opportunity.UserId = vSession.User.Id;
                    opportunity.LastName = RtxLastname.Text;
                    opportunity.FirstName = RtbxFirstname.Text;
                    opportunity.OrganizationName = RtbxOrganization.Text;
                    opportunity.Occupation = RtbxOccupation.Text;
                    opportunity.Address = RtbxAddress.Text;
                    opportunity.Email = RtbxEmail.Text;
                    opportunity.Phone = RtbxPhone.Text;
                    opportunity.WebSite = RtbxWebsite.Text;
                    opportunity.LinkedInUrl = RtbxLinkedin.Text;
                    opportunity.TwitterUrl = RtbxTwitter.Text;
                    opportunity.SysDate = DateTime.Now;
                    opportunity.LastUpdated = DateTime.Now;
                    opportunity.GuId = Guid.NewGuid().ToString();
                    opportunity.IsPublic = 1;
                    opportunity.StatusId = Convert.ToInt32(RcbxSteps.SelectedValue);

                    if (RtbxId.Text == "0")
                    {
                        loader.Insert(opportunity);

                        LblOpportunitySuccess.Text = "You have successfully add a new contact";
                    }
                    else
                    {
                        opportunity.Id = Convert.ToInt32(RtbxId.Text);

                        loader.Update(opportunity);

                        LblOpportunitySuccess.Text = "You have successfully updated your contact";
                    }

                    ResetFields(false);

                    divOpportunityGeneralSuccess.Visible = true;
                    LblOpportunityGeneralSuccess.Visible = true;
                    divOpportunityGeneralSuccess.Visible = true;
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

        protected void RbtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields(false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnCancelEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields(false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}
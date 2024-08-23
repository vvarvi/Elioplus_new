using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Objects;
using System.Web.UI.WebControls;
using System.Net.Mail;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI;

namespace WdS.ElioPlus.pages
{
    public partial class ReferralSoftwareGetAccessPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {              
                if (!IsPostBack)
                    FixPage();               
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixPage()
        {
            ResetRFPsFields();
            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            //aClose.HRef = ControlLoader.SearchForChannelPartners;
        }

        private void UpdateStrings()
        {
            try
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");

                string msg1 = "";
                string msg2 = "";

                if (pgTitle != null)
                    pgTitle.InnerText = "Get Access Form For Referral Software";

                LblTitleStep1.Text = LblTitleStep1F.Text = "1. Email address";
                LblTitleStep2.Text = LblTitleStep2F.Text = "2. Your details";
                LblTitleStep3.Text = LblTitleStep3F.Text = "3. Success";
                msg1 = "Thank you!";
                msg2 = "We have received your request and will reach out to you once we launch our referral software.";

                GlobalMethods.ShowMessageControlWith2Messages(UcMessageSuccess, msg1, msg2, MessageTypes.Success, true, true, false, false, true);

                BtnBack.Text = "Back";
                BtnProceed.Text = "Next";

                UcMessageAlert.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void ResetRFPsFields()
        {
            UcMessageAlert.Visible = false;

            divStepOne.Visible = true;
            divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

            divStepTwo.Visible = divStepThree.Visible = false;
            divStepTwo.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

            LoadStep(1);

            BtnBack.Visible = false;
            BtnProceed.Text = "Next";
        }

        private void LoadStep(int step)
        {
            switch (step)
            {
                case 1:

                    aStep1.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepOne.Visible = true;
                    divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                    aStep2.Attributes["class"] = aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepTwo.Visible = divStepThree.Visible = false;
                    divStepTwo.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                    //if (TbxEmailAddressTop.Text == "")
                    //    TbxEmailAddressTop.Text = (vSession.User != null) ? vSession.User.Email : "";

                    break;

                case 2:

                    aStep2.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepTwo.Visible = true;
                    divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                    aStep1.Attributes["class"] = aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepOne.Visible = divStepThree.Visible = false;
                    divStepOne.Attributes["class"] = divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content";
                                        
                    //if (vSession.User != null && vSession.User.AccountStatus == (int)AccountStatus.Completed)
                    //{
                    //    if (TbxFirstName.Text == "")
                    //        TbxFirstName.Text = (!string.IsNullOrEmpty(vSession.User.FirstName)) ? vSession.User.FirstName : "";

                    //    if (TbxLastName.Text == "")
                    //        TbxLastName.Text = (!string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.LastName : "";

                    //    if (TbxBusinessName.Text == "")
                    //        TbxBusinessName.Text = (!string.IsNullOrEmpty(vSession.User.CompanyName)) ? vSession.User.CompanyName : "";
                    //}
                    //else
                    //{
                    //    if (TbxFirstName.Text == "")
                    //        TbxFirstName.Text = "";

                    //    if (TbxLastName.Text == "")
                    //        TbxLastName.Text = "";

                    //    if (TbxBusinessName.Text == "")
                    //        TbxBusinessName.Text = "";
                    //}

                    break;

                case 3:

                    aStep3.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active";
                    divStepThree.Visible = true;
                    divStepThree.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                    aStep1.Attributes["class"] = aStep2.Attributes["class"] = "stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2";
                    divStepOne.Visible = divStepTwo.Visible = false;
                    divStepOne.Attributes["class"] = divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                    break;
            }
        }

        private bool ShowStep(int step)
        {
            if (step == 2)
            {
                if (TbxEmailAddressTop.Text == "")
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your email address", MessageTypes.Error, true, true, false, false, false);
                    return false;
                }
                else
                {
                    if (!Validations.IsValidEmail(TbxEmailAddressTop.Text))
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill a valid email address", MessageTypes.Error, true, true, false, false, false);
                        return false;
                    }
                }
            }
            else if (step == 3)
            {
                if (TbxEmailAddressTop.Text == "")
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your email address", MessageTypes.Error, true, true, false, false, false);
                    return false;
                }
                else
                {
                    if (!Validations.IsValidEmail(TbxEmailAddressTop.Text))
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill a valid email address", MessageTypes.Error, true, true, false, false, false);
                        return false;
                    }
                }

                if (TbxFirstName.Text == "")
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your first name", MessageTypes.Error, true, true, false, false, false);
                    return false;
                }

                if (TbxLastName.Text == "")
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your last name", MessageTypes.Error, true, true, false, false, false);
                    return false;
                }

                if (TbxBusinessName.Text == "")
                {
                    GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your company name", MessageTypes.Error, true, true, false, false, false);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Buttons

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlertTop.Visible = UcMessageAlert.Visible = false;

                if (divStepOne.Visible)
                {
                    if (TbxEmailAddressTop.Text == "")
                    {                        
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your email address", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                    else
                    {
                        if (!Validations.IsValidEmail(TbxEmailAddressTop.Text))
                        {
                            GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill a valid email address", MessageTypes.Error, true, true, false, false, false);
                            return;
                        }
                    }

                    LoadStep(2);

                    BtnProceed.Text = "Send";
                    BtnBack.Visible = true;
                }
                else if (divStepTwo.Visible)
                {
                    if (TbxFirstName.Text == "")
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your first name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxLastName.Text == "")
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your last name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    if (TbxBusinessName.Text == "")
                    {
                        GlobalMethods.ShowMessageAlertControl(UcMessageAlert, "Please fill your company name", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }

                    EmailSenderLib.ContactElioplus(TbxBusinessName.Text, TbxEmailAddressTop.Text, "Referral Software", string.Format("A user with name {0} of company {1} send his email: {2} address from referral software page at {3} in order to get in touch with him. Please do.", TbxLastName.Text + " " + TbxFirstName.Text, TbxBusinessName.Text, TbxEmailAddressTop.Text, DateTime.Now.ToShortDateString()), "", vSession.Lang, session);

                    LoadStep(3);
                    UpdateStrings();

                    BtnBack.Visible = false;
                    BtnProceed.Text = "Send new";
                }
                else
                {
                    TbxEmailAddressTop.Text = "";
                    TbxFirstName.Text = "";
                    TbxLastName.Text = "";
                    TbxBusinessName.Text = "";

                    ResetRFPsFields();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageAlertControl(UcMessageAlertTop, "Something went wrong! Please try again later.", MessageTypes.Error, true, true, false, false, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                divStepOne.Visible = true;
                divStepOne.Attributes["class"] = "w-full flex flex-col gap-30px step-content active";

                divStepTwo.Visible = false;
                divStepTwo.Attributes["class"] = "w-full flex flex-col gap-30px step-content";

                UcMessageAlert.Visible = UcMessageAlertTop.Visible = false;
                
                BtnBack.Visible = false;
                BtnProceed.Text = "Next";
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

        protected void aStep1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                LoadStep(1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aStep2_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ShowStep(2))
                    LoadStep(2);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aStep3_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (ShowStep(3))
                    LoadStep(3);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion        
    }
}
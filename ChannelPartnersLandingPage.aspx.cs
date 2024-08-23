using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class ChannelPartnersLandingPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    LoadClients();
                    FixButtons();
                    SetLinks();
                    ClearMessageData();
                    UpdateStrings();
                    PageTitle();
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

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);

            //MetaDescription = metaDescription;
            //MetaKeywords = metaKeywords;
        }

        private void UpdateStrings()
        {
            if (vSession.User == null)
            {
                LblSignUp.Text = "Free Sign up";
                aSignUpBottom.HRef = ControlLoader.SignUp;
            }
            else
            {
                if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                {
                    LblSignUp.Text = "Go to your dashboard";
                    aSignUpBottom.HRef = ControlLoader.Dashboard(vSession.User, "dashboard");
                }
                else
                {
                    LblSignUp.Text = "Complete your registration";
                    aSignUpBottom.HRef = ControlLoader.FullRegistrationPage;
                }
            }
        }

        private void FixButtons()
        {
            aSignIn.Visible = aSignUp.Visible = vSession.User == null;
        }

        private void SetLinks()
        {
            aSignIn.HRef = ControlLoader.Login;
            aSignUp.HRef = ControlLoader.SignUp;
            aLogo.HRef = ControlLoader.Default();
            aP2pLearnMore.HRef = ControlLoader.PartnerToPartnerDeals;
        }

        private void LoadClients()
        {
            try
            {
                List<ElioUsers> clients = GlobalDBMethods.GetHomePageClientsProfiles("2226, 2701, 12082, 18192, 18297, 26011", session);
                if (clients.Count > 0 && clients.Count == 6)
                {
                    ImgPartner1.ImageUrl = clients[4].CompanyLogo;
                    ImgPartner1.AlternateText = clients[4].CompanyName;
                    ImgPartner4.ImageUrl = clients[0].CompanyLogo;
                    ImgPartner4.AlternateText = clients[0].CompanyName;
                    ImgPartner5.ImageUrl = clients[3].CompanyLogo;
                    ImgPartner5.AlternateText = clients[3].CompanyName;
                    ImgPartner2.ImageUrl = clients[1].CompanyLogo;
                    ImgPartner2.AlternateText = clients[1].CompanyName;
                    ImgPartner3.ImageUrl = clients[5].CompanyLogo;
                    ImgPartner3.AlternateText = clients[5].CompanyName;
                    ImgPartner6.ImageUrl = clients[2].CompanyLogo;
                    ImgPartner6.AlternateText = clients[2].CompanyName;

                    aClient1.HRef = "/profiles/" + clients[4].CompanyType.ToLower() + "/" + clients[4].Id + "/" + Regex.Replace(clients[4].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient4.HRef = "/profiles/" + clients[0].CompanyType.ToLower() + "/" + clients[0].Id + "/" + Regex.Replace(clients[0].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient5.HRef = "/profiles/" + clients[3].CompanyType.ToLower() + "/" + clients[3].Id + "/" + Regex.Replace(clients[3].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient2.HRef = "/profiles/" + clients[1].CompanyType.ToLower() + "/" + clients[1].Id + "/" + Regex.Replace(clients[1].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient3.HRef = "/profiles/" + clients[5].CompanyType.ToLower() + "/" + clients[5].Id + "/" + Regex.Replace(clients[5].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient6.HRef = "/profiles/" + clients[2].CompanyType.ToLower() + "/" + clients[2].Id + "/" + Regex.Replace(clients[2].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();

                    if (aClient1.HRef.EndsWith("-"))
                        aClient1.HRef = aClient1.HRef.Substring(0, aClient1.HRef.Length - 1);
                    if (aClient4.HRef.EndsWith("-"))
                        aClient4.HRef = aClient4.HRef.Substring(0, aClient4.HRef.Length - 1);
                    if (aClient5.HRef.EndsWith("-"))
                        aClient5.HRef = aClient5.HRef.Substring(0, aClient5.HRef.Length - 1);
                    if (aClient2.HRef.EndsWith("-"))
                        aClient2.HRef = aClient2.HRef.Substring(0, aClient2.HRef.Length - 1);
                    if (aClient3.HRef.EndsWith("-"))
                        aClient3.HRef = aClient3.HRef.Substring(0, aClient3.HRef.Length - 1);
                    if (aClient6.HRef.EndsWith("-"))
                        aClient6.HRef = aClient6.HRef.Substring(0, aClient6.HRef.Length - 1);
                }
                else
                {
                    ImgPartner1.ImageUrl = "/Images/Logos/a0cff7cb-37b8-46ed-b56d-61cddbee7a09/iri-logo-anniversary-2018-750px-white-bg_8_1_2018_12345.png";
                    ImgPartner2.ImageUrl = "/Images/Logos/d3f7053c-ee32-4053-9f73-9b90fe953cb4/logo-contentverse-by-computhink_10_4_2017_12345.png";
                    ImgPartner3.ImageUrl = "/Images/Logos/cf85f912-83b3-4890-b60d-90319f5eb7b8/mon_elioplus_logo_27_1_2017_12345.png";
                    ImgPartner4.ImageUrl = "/Images/Logos/d24fc38f-a7a3-4633-85a4-43b64de87649/csslogo-400_25_6_2016_12345.jpg";
                    ImgPartner5.ImageUrl = "/Images/Logos/4d65eb5d-a68b-4508-a63f-f4d1fed5f426/IglooLogo_Green_16_1_2017_12345.png";
                    ImgPartner6.ImageUrl = "/Images/Logos/95a126cb-3ffc-4c8a-9a13-56e9c288666d/spinbackup-logo%20(1).jpg_15_6_2016_12345.jpeg";
                    aClient1.HRef = "/profiles/vendors/2226/iri-the-cosort-company";
                    aClient2.HRef = "/profiles/vendors/4297/computhink-inc-";
                    aClient3.HRef = "profiles/vendors/5898/monitis";
                    aClient4.HRef = "/profiles/vendors/2747/christiansteven-software";
                    aClient5.HRef = "profiles/vendors/5708/igloo-software";
                    aClient6.HRef = "/profiles/vendors/2386/spinbackup";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool CheckData()
        {
            bool isError = false;

            //if (string.IsNullOrEmpty(TbxFirstName.Text))
            //{
            //    LblWarningMessage.Text = "Please enter your first name";
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxLastName.Text))
            //{
            //    LblWarningMessage.Text = "Please enter your last name";
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxCompanyName.Text))
            //{
            //    LblWarningMessage.Text = "Please enter your company name";
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxEmail.Text))
            //{
            //    LblWarningMessage.Text = "Please enter an email address";
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}
            //else
            //{
            //    if (!Validations.IsEmail(TbxEmail.Text))
            //    {
            //        LblWarningMessage.Text = "Please enter a valid email address";
            //        divWarningMessage.Visible = true;
            //        return isError = true;
            //    }
            //}
            
            return isError;     
        }

        private void ClearMessageData()
        {
            //divWarningMessage.Visible = false;
            //divSuccessMessage.Visible = false;

            //TbxFirstName.Text = "";
            //TbxLastName.Text = "";
            //TbxCompanyName.Text = "";
            //TbxEmail.Text = "";
        }

        #endregion

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                bool isError = CheckData();

                if (isError) return;

                //try
                //{
                //    EmailSenderLib.ContactElioplusForDemoRequest(TbxFirstName.Text, TbxLastName.Text, TbxCompanyName.Text, TbxEmail.Text, vSession.Lang, session);
                //}
                //catch (Exception ex)
                //{
                //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                //    divWarningMessage.Visible = true;
                //    LblWarningMessage.Text = "Sorry, but something went wrong. Please try again later.";
                //    return;
                //}

                //ClearMessageData();

                //LblSuccessMessage.Text = "Thank you! Your demo request was successfully sent.";
                //divSuccessMessage.Visible = true;
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

    }
}
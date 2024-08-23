using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;

namespace WdS.ElioPlus
{
    public partial class DashboardFindPartners : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page Properties

        public int ConnectionId
        {
            get
            {
                if (ViewState["ConnectionId"] != null)
                    return Convert.ToInt32(ViewState["ConnectionId"]);
                else
                    return 0;
            }
            set
            {
                ViewState["ConnectionId"] = value;
            }
        }

        public DataTable MatchesDataTable
        {
            get
            {
                if (ViewState["MatchesDataTable"] != null)
                    return ViewState["MatchesDataTable"] as DataTable;
                else
                    return null;
            }
            set
            {
                ViewState["MatchesDataTable"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();
                    
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(aShowAlgBtn);
                    scriptManager.RegisterPostBackControl(aRunAlgBtn);
                    scriptManager.RegisterPostBackControl(ImgBtnExport);

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }

                if (!IsPostBack)
                {
                    if (vSession.HasToUpdateNewConnections)
                        vSession.HasToUpdateNewConnections = !Sql.SetUserConnectionsAsViewed(vSession.User.Id, session);
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

        # region Methods

        private void FixPage()
        {
            LblShowAlgBtnText.Text = "Edit Matching Criteria";

            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                UcConnectionsMessageAlert.Visible = false;
                ConnectionId = 0;
                vSession.ViewStateDataStore = null;
                vSession.HasSearchCriteria = false;

                RdgConnections.MasterTableView.GetColumn("company_more_details").Display = Sql.IsUserAdministrator(vSession.User.Id, session) || ((!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ClearbitSettings"].ToString())) && ConfigurationManager.AppSettings["ClearbitSettings"].ToString() == "true");


                #region top-right menu

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                    if (packet != null)
                    {
                        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                    }

                    LblRenewalHead.Visible = LblRenewal.Visible = true;
                    LblRenewalHead.Text = "Renwal date: ";

                    try
                    {
                        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                    }
                    catch (Exception)
                    {
                        LblRenewalHead.Visible = LblRenewal.Visible = false;

                        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                    }

                    //divFreemiumUser.Visible = false;
                    //divPremiumUser.Visible = true;
                }
                else
                {
                    //divFreemiumUser.Visible = true;
                    //divPremiumUser.Visible = false;
                    //divConnections.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                    //divPremiumUser.Visible = divConnections.Visible;

                    LblRenewalHead.Visible = LblRenewal.Visible = false;
                    LblPricingPlan.Text = "You are currently on a free plan";
                }

                aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
                aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

                LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

                LblDashboard.Text = "Dashboard";

                if (aBtnGoPremium.Visible)
                {
                    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                    LblPricingPlan.Visible = false;
                }

                LblGoFull.Text = "Complete your registration";
                LblDashPage.Text = "Find partners";
                LblDashSubTitle.Text = "find your future partners";

                #endregion

                if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                {
                    #region Account Completed

                    bool hasConnections = Sql.HasUserConnections(vSession.User.Id, session);

                    if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                    {
                        PnlFindPartners.Visible = true;
                        divNoMatchesForPremiumUserNotification.Visible = !hasConnections;
                    }

                    aCancelRunCriteriaSelection.Visible = hasConnections;
                    List<ElioSubIndustriesGroupItems> userVerticals = Sql.GetUserSubIndustries(vSession.User.Id, session);

                    if (userVerticals.Count == 0)
                    {
                        PnlNotRegisteredOrNoVerticals.Visible = true;
                        PnlFindPartners.Visible = false;
                        aShowAlgBtn.Visible = false;
                        divAlgorithmHolder.Visible = false;

                        return;
                    }

                    List<ElioUsersAlgorithmSubcategories> userAlgorithmVerticals = Sql.GetUserAlgorithmSubcategoriesById(vSession.User.Id, session);

                    PnlNotRegisteredOrNoVerticals.Visible = false;
                    PnlFindPartners.Visible = vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType || (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType && !hasConnections);

                    if (userVerticals.Count > 0)
                    {
                        divAlgInfo.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType;

                        if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType && vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            divOpportunitiesForPremiumVendors.Visible = true;
                            FixCheckBoxList(userVerticals, userAlgorithmVerticals);
                            LblOpportunitiesForPremiumVendors.Text = string.Format("Below you can see your matches so far out of {0} that matches your criteria", FindCompanyOpportunities(CbxSubcategories).ToString());
                        }
                        else
                            divOpportunitiesForPremiumVendors.Visible = false;

                        #region Fix CheckBox List

                        if (PnlFindPartners.Visible)
                        {
                            if (!IsPostBack)
                            {
                                FixCheckBoxList(userVerticals, userAlgorithmVerticals);
                            }
                        }
                        else
                        {
                            PnlNotRegisteredOrNoVerticals.Visible = userVerticals.Count == 0;
                        }

                        #endregion
                    }
                    else
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            divOpportunitiesForPremiumVendors.Visible = false;

                    UserOpportunities.Attributes["data-value"] = FindCompanyOpportunities(CbxSubcategories).ToString();
                    LblUserOpportunities.Text = FindCompanyOpportunities(CbxSubcategories).ToString();

                    if (vSession.LoadedDashboardControl.Contains("Controls/Dashboard/ProfileDataEditMode"))
                    {
                        divAlgorithmHolder.Visible = true;
                        aShowAlgBtn.Visible = false;
                        aCancelRunCriteriaSelection.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                        aRunAlgBtn.Visible = true;
                        divConnectionsTableHolder.Visible = false;

                        PnlCriteriaSelection.Visible = false;
                        PnlSelectionProcess.Visible = true;
                        PnlFindPartners.Visible = false;

                        LblAlgBtnText.Text = "Close MATCHING PROCESS";

                        ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);

                        return;
                    }
                    else if (vSession.LoadedDashboardControl.Contains("Controls/Dashboard/ProfileDataViewMode"))
                    {
                        divAlgorithmHolder.Visible = true;
                        aShowAlgBtn.Visible = false;
                        aCancelRunCriteriaSelection.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                        aRunAlgBtn.Visible = true;
                        divConnectionsTableHolder.Visible = false;

                        PnlCriteriaSelection.Visible = true;
                        PnlSelectionProcess.Visible = false;

                        LblAlgBtnText.Text = "Run MATCHING PROCESS again";

                        List<ElioSubIndustriesGroupItems> verticalsDescription = Sql.GetUserSubIndustriesGroupItemsIJAlgorithmSubIndustries(vSession.User.Id, session);
                        List<ElioUsersCriteria> userAlgorithmCriteria = Sql.GetUserCriteria(vSession.User.Id, session);
                        ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.User.Id, session);
                        ElioUsersFiles userCsv = Sql.GetUserCsvFile(vSession.User.Id, session);

                        if (userAlgorithmCriteria.Count > 0)
                        {
                            #region criteria overview

                            rowVerticals.Visible = true;
                            LblVerticalsValue.Text = string.Empty;
                            foreach (ElioSubIndustriesGroupItems vert in verticalsDescription)
                            {
                                LblVerticalsValue.Text += vert.Description + ", ";
                            }
                            LblVerticalsValue.Text = !string.IsNullOrEmpty(LblVerticalsValue.Text) ? LblVerticalsValue.Text.Substring(0, LblVerticalsValue.Text.Length - 2) : "";

                            LblSupportValue.Text = string.Empty;
                            LblCountryValue.Text = string.Empty;
                            foreach (ElioUsersCriteria crit in userAlgorithmCriteria)
                            {
                                #region criteria table

                                if (crit.CriteriaId == 12)
                                {
                                    rowFee.Visible = true;
                                    LblFeeValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 13)
                                {
                                    rowRevenue.Visible = true;
                                    LblRevenueValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 14)
                                {
                                    rowSupport.Visible = true;
                                    LblSupportValue.Text += crit.CriteriaValue + ", ";
                                }

                                else if (crit.CriteriaId == 1)
                                {
                                    rowCompMat.Visible = true;
                                    LblCompMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 2)
                                {
                                    rowProgMat.Visible = true;
                                    LblProgMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 3)
                                {
                                    rowNumPartn.Visible = true;
                                    LblNumPartnValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 4)
                                {
                                    rowTiers.Visible = true;
                                    LblTiersValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 5)
                                {
                                    rowTrain.Visible = true;
                                    LblTrainValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 6)
                                {
                                    rowFreeTrain.Visible = true;
                                    LblFreeTrainValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 7)
                                {
                                    rowMarkMat.Visible = true;
                                    LblMarkMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 8)
                                {
                                    rowCert.Visible = true;
                                    LblCertValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 9)
                                {
                                    rowLocal.Visible = true;
                                    LblLocalValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 10)
                                {
                                    rowMdf.Visible = true;
                                    LblMdfValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 11)
                                {
                                    rowPortal.Visible = true;
                                    LblPortalValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 15)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        rowCountry.Visible = true;
                                        LblCountryValue.Text += crit.CriteriaValue + ", ";
                                    }
                                }

                                #endregion
                            }

                            LblSupportValue.Text = !string.IsNullOrEmpty(LblSupportValue.Text) ? LblSupportValue.Text.Substring(0, LblSupportValue.Text.Length - 2) : "";
                            LblCountryValue.Text = !string.IsNullOrEmpty(LblCountryValue.Text) ? LblCountryValue.Text.Substring(0, LblCountryValue.Text.Length - 2) : "";

                            if (userPdf != null)
                            {
                                rowPdf.Visible = true;
                                string[] pdfArray = userPdf.FilePath.Split('/');
                                string pdfName = pdfArray[4];
                                LblPdfValue.Text = pdfName;
                                aPdf.HRef = userPdf.FilePath;
                                aPdf.Target = "_blank";
                            }

                            if (userCsv != null)
                            {
                                rowCsv.Visible = true;
                                string[] csvArray = userCsv.FilePath.Split('/');
                                string csvName = csvArray[4];
                                LblCsvValue.Text = csvName;
                                aCsv.HRef = userCsv.FilePath;
                                aCsv.Target = "_blank";
                            }

                            #endregion
                        }

                        return;
                    }

                    divConnectionsTableHolder.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                    if (divConnectionsTableHolder.Visible)
                    {
                        aShowAlgBtn.Visible = true;
                        divAlgorithmHolder.Visible = false;
                    }
                    else
                    {
                        divAlgorithmHolder.Visible = true;
                        aShowAlgBtn.Visible = false;

                        if (userAlgorithmVerticals.Count > 0)
                        {
                            PnlSelectionProcess.Visible = false;
                            PnlCriteriaSelection.Visible = true;
                            aRunAlgBtn.Visible = true;

                            LblAlgBtnText.Text = "Run MATCHING PROCESS again";

                            List<ElioSubIndustriesGroupItems> verticalsDescription = Sql.GetUserSubIndustriesGroupItemsIJAlgorithmSubIndustries(vSession.User.Id, session);
                            List<ElioUsersCriteria> userAlgorithmCriteria = Sql.GetUserCriteria(vSession.User.Id, session);
                            ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.User.Id, session);
                            ElioUsersFiles userCsv = Sql.GetUserCsvFile(vSession.User.Id, session);

                            if (userAlgorithmCriteria.Count > 0)
                            {
                                #region criteria overview

                                rowVerticals.Visible = true;
                                LblVerticalsValue.Text = string.Empty;
                                foreach (ElioSubIndustriesGroupItems vert in verticalsDescription)
                                {
                                    LblVerticalsValue.Text += vert.Description + ", ";
                                }
                                LblVerticalsValue.Text = !string.IsNullOrEmpty(LblVerticalsValue.Text) ? LblVerticalsValue.Text.Substring(0, LblVerticalsValue.Text.Length - 2) : "";

                                LblSupportValue.Text = string.Empty;
                                LblCountryValue.Text = string.Empty;
                                foreach (ElioUsersCriteria crit in userAlgorithmCriteria)
                                {
                                    #region criteria table

                                    if (crit.CriteriaId == 12)
                                    {
                                        rowFee.Visible = true;
                                        LblFeeValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 13)
                                    {
                                        rowRevenue.Visible = true;
                                        LblRevenueValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 14)
                                    {
                                        rowSupport.Visible = true;
                                        LblSupportValue.Text += crit.CriteriaValue + ", ";
                                    }

                                    else if (crit.CriteriaId == 1)
                                    {
                                        rowCompMat.Visible = true;
                                        LblCompMatValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 2)
                                    {
                                        rowProgMat.Visible = true;
                                        LblProgMatValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 3)
                                    {
                                        rowNumPartn.Visible = true;
                                        LblNumPartnValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 4)
                                    {
                                        rowTiers.Visible = true;
                                        LblTiersValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 5)
                                    {
                                        rowTrain.Visible = true;
                                        LblTrainValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 6)
                                    {
                                        rowFreeTrain.Visible = true;
                                        LblFreeTrainValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 7)
                                    {
                                        rowMarkMat.Visible = true;
                                        LblMarkMatValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 8)
                                    {
                                        rowCert.Visible = true;
                                        LblCertValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 9)
                                    {
                                        rowLocal.Visible = true;
                                        LblLocalValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 10)
                                    {
                                        rowMdf.Visible = true;
                                        LblMdfValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 11)
                                    {
                                        rowPortal.Visible = true;
                                        LblPortalValue.Text = crit.CriteriaValue;
                                    }

                                    else if (crit.CriteriaId == 15)
                                    {
                                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                                        {
                                            rowCountry.Visible = true;
                                            LblCountryValue.Text += crit.CriteriaValue + ", ";
                                        }
                                    }

                                    #endregion
                                }

                                LblSupportValue.Text = !string.IsNullOrEmpty(LblSupportValue.Text) ? LblSupportValue.Text.Substring(0, LblSupportValue.Text.Length - 2) : "";
                                LblCountryValue.Text = !string.IsNullOrEmpty(LblCountryValue.Text) ? LblCountryValue.Text.Substring(0, LblCountryValue.Text.Length - 2) : "";

                                if (userPdf != null)
                                {
                                    rowPdf.Visible = true;
                                    string[] pdfArray = userPdf.FilePath.Split('/');
                                    string pdfName = pdfArray[4];
                                    LblPdfValue.Text = pdfName;
                                    aPdf.HRef = userPdf.FilePath;
                                    aPdf.Target = "_blank";
                                }

                                if (userCsv != null)
                                {
                                    rowCsv.Visible = true;
                                    string[] csvArray = userCsv.FilePath.Split('/');
                                    string csvName = csvArray[4];
                                    LblCsvValue.Text = csvName;
                                    aCsv.HRef = userCsv.FilePath;
                                    aCsv.Target = "_blank";
                                }

                                #endregion
                            }
                            else
                            {
                                #region no criteria yet

                                if (userVerticals.Count > 0)
                                {
                                    PnlCriteriaSelection.Visible = false;
                                    PnlSelectionProcess.Visible = true;
                                    LblAlgBtnText.Text = "Close MATCHING PROCESS";
                                    PnlFindPartners.Visible = false;

                                    vSession.LoadedDashboardControl = ControlLoader.ProfileDataEditMode;
                                    ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);
                                }
                                else
                                {
                                    if (userAlgorithmVerticals.Count == 0)
                                    {
                                        PnlNotRegisteredOrNoVerticals.Visible = true;
                                        PnlFindPartners.Visible = false;
                                        divAlgorithmHolder.Visible = false;
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            divAlgorithmHolder.Visible = true;
                            divConnectionsTableHolder.Visible = false;
                            PnlCriteriaSelection.Visible = false;
                            PnlSelectionProcess.Visible = true;
                            aRunAlgBtn.Visible = false;
                            PnlFindPartners.Visible = false;

                            LblAlgBtnText.Text = "Close MATCHING PROCESS";

                            vSession.LoadedDashboardControl = ControlLoader.ProfileDataEditMode;
                            ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Account Not Completed

                    PnlNotRegisteredOrNoVerticals.Visible = true;
                    PnlFindPartners.Visible = false;
                    divAlgorithmHolder.Visible = false;
                    aShowAlgBtn.Visible = false;
                    divConnectionsTableHolder.Visible = false;

                    UserOpportunities.Attributes["data-value"] = "0";
                    LblUserOpportunities.Text = "0";

                    #endregion
                }

                aCancelRunCriteriaSelection.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                //ImgBtnExport.Visible = false; // Sql.IsUserAdministrator(vSession.User.Id, session);
            }

            #region To delete

            ////List<ElioSubIndustriesGroupItems> userVerticals = Sql.GetUserSubIndustries(vSession.User.Id, session);
            ////List<ElioUsersAlgorithmSubcategories> userAlgorithmVerticals = Sql.GetUserAlgorithmSubcategoriesById(vSession.User.Id, session);

            ////PnlNotRegisteredOrNoVerticals.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || userVerticals.Count == 0) ? true : false;
            ////PnlFindPartners.Visible = PnlNotRegisteredOrNoVerticals.Visible ? false : true;
            ////LblShowAlgBtnText.Text = "Edit Matching Criteria";

            ////if (!IsPostBack)
            ////{
            ////    UpdateStrings();
            ////    SetLinks();
            ////    UcConnectionsMessageAlert.Visible = false;
            ////    ConnectionId = 0;

            ////divAlgInfo.Visible = (vSession.User.CompanyType == Types.Vendors.ToString());

            ////aRunAlgBtn.Visible = userAlgorithmVerticals.Count > 0 ? true : false;
            ////aCancelRunCriteriaSelection.Visible = (vSession.User != null && vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? false : true;

            ////if (userVerticals != null && userVerticals.Count > 0)
            ////{
            #region Fix CheckBox List

            //CbxSubcategories.DataSource = userVerticals;
            //CbxSubcategories.DataBind();

            ////foreach (ElioSubIndustriesGroupItems userVertical in userVerticals)
            ////{
            ////    ListItem li = new ListItem();
            ////    li.Value = userVertical.Id.ToString();
            ////    li.Text = userVertical.Description;

            ////    if (userAlgorithmVerticals.Count > 0)
            ////        li.Selected = Sql.ExistUserAlgorithmSubcategory(vSession.User.Id, userVertical.Id, session);
            ////    else
            ////        li.Selected = true;

            ////    CbxSubcategories.Items.Add(li);
            ////}

            #endregion
            ////}

            #region to delete

            //if (userAlgorithmVerticals.Count > 0)
            //{
            //    foreach (ElioUsersAlgorithmSubcategories vert in userAlgorithmVerticals)
            //    {
            //        foreach (ListItem li in CbxSubcategories.Items)
            //        {
            //            if (vert.SubcategoryId == Convert.ToInt32(li.Value))
            //            {
            //                li.Selected = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (ListItem li in CbxSubcategories.Items)
            //    {
            //        li.Selected = true;
            //    }
            //}

            #endregion

            ////if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            ////{
            ////    UserOpportunities.Attributes["data-value"] = FindCompanyOpportunities(CbxSubcategories).ToString();
            ////    LblUserOpportunities.Text = FindCompanyOpportunities(CbxSubcategories).ToString();
            ////}
            ////else
            ////{
            ////    UserOpportunities.Attributes["data-value"] = "0";
            ////    LblUserOpportunities.Text = "0";
            ////}
            ////}

            ////List<ElioSubIndustriesGroupItems> verticalsDescription = Sql.GetUserSubIndustriesGroupItemsIJAlgorithmSubIndustries(vSession.User.Id, session);
            ////List<ElioUsersCriteria> userAlgorithmCriteria = Sql.GetUserCriteria(vSession.User.Id, session);
            ////ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.User.Id, session);
            ////ElioUsersFiles userCsv = Sql.GetUserCsvFile(vSession.User.Id, session);

            ////if (userAlgorithmVerticals.Count > 0 && userAlgorithmCriteria.Count > 0)
            ////{
            #region criteria overview

            ////PnlCriteriaSelection.Visible = true;
            ////PnlSelectionProcess.Visible = false;

            ////LblAlgBtnText.Text = "Run MATCHING PROCESS again";

            ////rowVerticals.Visible = true;
            ////LblVerticalsValue.Text = string.Empty;
            ////foreach (ElioSubIndustriesGroupItems vert in verticalsDescription)
            ////{
            ////    LblVerticalsValue.Text += vert.Description + ", ";
            ////}
            ////LblVerticalsValue.Text = !string.IsNullOrEmpty(LblVerticalsValue.Text) ? LblVerticalsValue.Text.Substring(0, LblVerticalsValue.Text.Length - 2) : "";

            ////LblSupportValue.Text = string.Empty;
            ////LblCountryValue.Text = string.Empty;

            ////foreach (ElioUsersCriteria crit in userAlgorithmCriteria)
            ////{
            #region criteria table

            ////if (crit.CriteriaId == 12)
            ////{
            ////    rowFee.Visible = true;
            ////    LblFeeValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 13)
            ////{
            ////    rowRevenue.Visible = true;
            ////    LblRevenueValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 14)
            ////{
            ////    rowSupport.Visible = true;
            ////    LblSupportValue.Text += crit.CriteriaValue + ", ";
            ////}

            ////else if (crit.CriteriaId == 1)
            ////{
            ////    rowCompMat.Visible = true;
            ////    LblCompMatValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 2)
            ////{
            ////    rowProgMat.Visible = true;
            ////    LblProgMatValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 3)
            ////{
            ////    rowNumPartn.Visible = true;
            ////    LblNumPartnValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 4)
            ////{
            ////    rowTiers.Visible = true;
            ////    LblTiersValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 5)
            ////{
            ////    rowTrain.Visible = true;
            ////    LblTrainValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 6)
            ////{
            ////    rowFreeTrain.Visible = true;
            ////    LblFreeTrainValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 7)
            ////{
            ////    rowMarkMat.Visible = true;
            ////    LblMarkMatValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 8)
            ////{
            ////    rowCert.Visible = true;
            ////    LblCertValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 9)
            ////{
            ////    rowLocal.Visible = true;
            ////    LblLocalValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 10)
            ////{
            ////    rowMdf.Visible = true;
            ////    LblMdfValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 11)
            ////{
            ////    rowPortal.Visible = true;
            ////    LblPortalValue.Text = crit.CriteriaValue;
            ////}

            ////else if (crit.CriteriaId == 15)
            ////{
            ////    if (vSession.User.CompanyType == Types.Vendors.ToString())
            ////    {
            ////        rowCountry.Visible = true;
            ////        LblCountryValue.Text += crit.CriteriaValue + ", ";
            ////    }
            ////}

            #endregion
            ////}

            ////LblSupportValue.Text = !string.IsNullOrEmpty(LblSupportValue.Text) ? LblSupportValue.Text.Substring(0, LblSupportValue.Text.Length - 2) : "";
            ////LblCountryValue.Text = !string.IsNullOrEmpty(LblCountryValue.Text) ? LblCountryValue.Text.Substring(0, LblCountryValue.Text.Length - 2) : "";

            ////if (userPdf != null)
            ////{
            ////    rowPdf.Visible = true;
            ////    string[] pdfArray = userPdf.FilePath.Split('/');
            ////    string pdfName = pdfArray[4];
            ////    LblPdfValue.Text = pdfName;
            ////    aPdf.HRef = userPdf.FilePath;
            ////    aPdf.Target = "_blank";
            ////}

            ////if (userCsv != null)
            ////{
            ////    rowCsv.Visible = true;
            ////    string[] csvArray = userCsv.FilePath.Split('/');
            ////    string csvName = csvArray[4];
            ////    LblCsvValue.Text = csvName;
            ////    aCsv.HRef = userCsv.FilePath;
            ////    aCsv.Target = "_blank";
            ////}

            #endregion
            ////}
            ////else
            ////{
            #region no criteria yet

            ////if (userVerticals.Count > 0)
            ////{
            ////    PnlCriteriaSelection.Visible = false;
            ////    PnlSelectionProcess.Visible = true;
            ////    LblAlgBtnText.Text = "Close MATCHING PROCESS";

            ////    vSession.LoadedDashboardControl = ControlLoader.ProfileDataEditMode;
            ////    ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);
            ////}
            ////else
            ////{
            ////    if (userAlgorithmVerticals.Count == 0)
            ////    {
            ////        divFreemiumUser.Visible = true;
            ////        divPremiumUser.Visible = false;
            ////        divRunAlgorithmCriteria.Visible = false;
            ////        PnlFindPartners.Visible = false;
            ////    }
            ////}

            #endregion
            ////}

            ////if (vSession.LoadedDashboardControl.Contains("Controls/Dashboard/ProfileDataEditMode"))
            ////{
            ////    PnlCriteriaSelection.Visible = false;
            ////    PnlSelectionProcess.Visible = true;
            ////    LblAlgBtnText.Text = "Close MATCHING PROCESS";
            ////    ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);

            ////    divPremiumUser.Visible = false;
            ////    divRunAlgorithmCriteria.Visible = true;
            ////}
            ////else if (vSession.LoadedDashboardControl.Contains("Controls/Dashboard/ProfileDataViewMode"))
            ////{
            ////    PnlCriteriaSelection.Visible = true;
            ////    PnlSelectionProcess.Visible = false;
            ////    LblAlgBtnText.Text = "Run MATCHING PROCESS again";

            ////    divPremiumUser.Visible = false;
            ////    divRunAlgorithmCriteria.Visible = true;
            ////}

            #endregion
        }

        private void FixCheckBoxList(List<ElioSubIndustriesGroupItems> userVerticals, List<ElioUsersAlgorithmSubcategories> userAlgorithmVerticals)
        {
            CbxSubcategories.Items.Clear();

            foreach (ElioSubIndustriesGroupItems userVertical in userVerticals)
            {
                ListItem li = new ListItem();
                li.Value = userVertical.Id.ToString();
                li.Text = userVertical.Description;

                if (userAlgorithmVerticals.Count > 0)
                    li.Selected = Sql.ExistUserAlgorithmSubcategory(vSession.User.Id, userVertical.Id, session);
                else
                    li.Selected = true;

                CbxSubcategories.Items.Add(li);
            }
        }

        private void UpdateStrings()
        {
            RdgConnections.MasterTableView.GetColumn("company_logo").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "1")).Text;
            RdgConnections.MasterTableView.GetColumn("company_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "2")).Text;
            RdgConnections.MasterTableView.GetColumn("country").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "8")).Text;
            RdgConnections.MasterTableView.GetColumn("company_email").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "3")).Text;
            RdgConnections.MasterTableView.GetColumn("company_website").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "4")).Text;
            RdgConnections.MasterTableView.GetColumn("company_more_details").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "7")).Text;
            RdgConnections.MasterTableView.GetColumn("linkedin_url").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "5")).Text;
            RdgConnections.MasterTableView.GetColumn("is_new").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "7", "column", "6")).Text;

            //if (PnlNotRegisteredOrNoVerticals.Visible)
            //{
            LblNotRegistHeader.Text = "Get connected with your ";
            LblNotRegistSubHeader.Text = "business partners";
            LblActionNotRegist.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "In order to use our MATCHING PROCESS you have to be full registered. Click on the 'Complete registration' option at the top right corner and " : "In order to use our MATCHING PROCESS you must first choose your company's industry verticals. Click on the 'Edit profile' option at the top right corner and then on 'Industry verticals' and ";
            LblActionLink.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "complete your company's profile!" : "save your verticals selection!";
            //}

            LblVerticalsSelection.Text = "Your business verticals selection";
            LblAvaiOpportunities.Text = "Partnership opportunities";
            LblAvailPartners.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Resellers on our platform" : "Vendors on our platform";
            LblOpportInfo.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "These are the reseller opportunities available based on your verticals selection. If you change the options on your left you will see the number to adjust." : "These are the vendor opportunities available based on your verticals selection. If you change the options on your left you will see the number to adjust.";
            //LblAlgInfo.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Select below all that apply so we can start matching you with resellers that best fit your needs!" : "Select below all that apply so we can start matching you with vendors that best fit your needs!";

            LblAlgInfo.Text = "Upgrade your account to get matched with the best potential channel partners.";
            LblNoMatchesForPremiumUserNotification.Text = "We are processing your Premium Profile criteria to find you matches to collaborate with. It won't take long, please wait.";
            LblVerticals.Text = "Business verticals";
            LblFee.Text = "Set up fee";
            LblRevenue.Text = "Revenue model";
            LblSupport.Text = "Support";
            LblCompMat.Text = "Company maturity (years)";
            LblProgMat.Text = "Partner program maturity (years)";
            LblNumPartn.Text = "Number of business partners";
            LblTiers.Text = "Partner program tiers";
            LblTrain.Text = "Training";
            LblFreeTrain.Text = "Free training";
            LblMarkMat.Text = "Marketing material";
            LblCert.Text = "Certification required";
            LblLocal.Text = "Localisation";
            LblMdf.Text = "Marketing development funds (MDF)";
            LblPortal.Text = "Partner portal / resources";
            LblCountry.Text = "Countries / regions";
            LblPdf.Text = "Partner program pdf";
            LblCsv.Text = "Current partners csv";
            LblCriteriaName.Text = "Your saved selection categories";
            LblCriteriaValue.Text = "Your saved selection values. You can run the selection process again.";
            LblRenewalHead.Text = "Renewal date: ";

            #region connections Page

            LblGoFullOrReg.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "No connections? Complete your registration and upgrade to premium!" : "No connections? Upgrade to premium!";

            //LblConnectionsPageInfo.Text = "When you start your free trial and onwards, we'll send day by day your partner program data to potential partner that match with your company. The list below will be updating daily with more partners that you can follow up.";
            //if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            //    LblConnectionsPageInfo.Text += " Complete your registration to start your 14-day free trial!";

            LblConnectionsTitle.Text = "Your recent connections. You can have a look at their profile!";
            LblConfTitle.Text = "Confirmation";
            LblConfMsg.Text = "Are you sure you want to delete this connection?";

            #endregion
        }

        private void SetLinks()
        {
            aFullRegist.HRef = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : ControlLoader.Dashboard(vSession.User, "edit-company-profile");
            aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            //aCancelRunCriteriaSelection.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");
            //aBtnGoPremium.HRef = ControlLoader.Dashboard(vSession.User, "billing");
        }

        protected void CbxSubcategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    UserOpportunities.Attributes["data-value"] = FindCompanyOpportunities(CbxSubcategories).ToString();
                    LblUserOpportunities.Text = FindCompanyOpportunities(CbxSubcategories).ToString();
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

        private int FindCompanyOpportunities(CheckBoxList cbxSubCategoriesList)
        {
            int opportunities = 0;
            int dbOpportunities = 0;
            int otherOpportunities = 0;

            string subcategoriesId = string.Empty;

            foreach (ListItem li in cbxSubCategoriesList.Items)
            {
                if (li.Selected)
                {
                    subcategoriesId += li.Value + ",";
                }
            }

            if (subcategoriesId.Length > 0)
            {
                otherOpportunities = Sql.GetUserOpportunitiesSum(subcategoriesId.Substring(0, subcategoriesId.Length - 1), session);

                dbOpportunities = (!string.IsNullOrEmpty(subcategoriesId)) ? Sql.GetPossibleOpportunitiesForUserBySubCategoriesIdAndCompanyType(subcategoriesId.Substring(0, subcategoriesId.Length - 1), (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString(), session) : 0;
            }

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                opportunities = dbOpportunities + otherOpportunities;
            }
            else
            {
                opportunities = dbOpportunities;
            }

            return opportunities;
        }

        private void FillDataForExport(bool hasSearchCriteria)
        {
            string strQueryExport = @"select 
                                u.company_name as 'COMPANY NAME',
                                ISNULL(u.first_name, up.given_name) as 'FIRST NAME',
                                ISNULL(u.last_name, up.family_name) as 'LAST NAME',
                                ISNULL(u.position, up.title) as 'POSITION-TITLE',
                                ISNULL(u.phone, up.phone) as 'PHONE',
                                up.time_zone AS 'TIMEZONE',
                                u.address AS 'ADDRESS',
                                u.email AS 'EMAIL', 
                                u.official_email AS 'OFFICIAL EMAIL', 
                                u.website AS 'WEBSITE',
                                u.vendor_product_demo_link AS 'PRODUCT DEMO LINK',
                                upc.sector AS 'SECTOR',
                                upc.industry AS 'INDUSTRY',
                                upc.industry_group AS 'INDUSTRY GROUP',
                                upc.sub_industry AS 'SUB INDUSTRY',
                                upc.founded_year AS 'FOUNDED YEAR',
                                upc.fund_amount AS 'FUND AMOUNT',
                                upc.employees_number AS 'EMPLOYEES NUMBER',
                                upc.employees_range AS 'EMPLOYEES RANGE NUMBER',
                                u.country AS 'COUNTRY'
                                from Elio_users_connections uc
                                inner join Elio_users u
                    	            on u.id = uc.connection_id
                                left join Elio_users_person up
                    	            on u.id = up.user_id
                                left join dbo.Elio_users_person_companies upc
                    	            on u.id = upc.user_id and up.id = upc.elio_person_id
                                where uc.user_id=" + vSession.User.Id + " " +
            "and can_be_viewed=1";

            if (hasSearchCriteria)
            {
                if (RdpConnectionsFrom.SelectedDate != null)
                {
                    strQueryExport += " and uc.sysdate>='" + Convert.ToDateTime(RdpConnectionsFrom.SelectedDate).ToString("yyyy/MM/dd") + "'";
                }

                if (RdpConnectionsTo.SelectedDate != null)
                {
                    strQueryExport += " and uc.sysdate<='" + Convert.ToDateTime(RdpConnectionsTo.SelectedDate).ToString("yyyy/MM/dd") + "'";
                }

                if (RtbxCompanyName.Text != "")
                {
                    strQueryExport += " and company_name like '" + RtbxCompanyName.Text.Trim() + "%'";
                }
            }

            strQueryExport += " order by uc.sysdate desc";

            DataTable exportTable = session.GetDataTable(strQueryExport);
            if (exportTable != null && exportTable.Rows.Count > 0)
            {
                vSession.ViewStateDataStore = new DataTable();
                vSession.ViewStateDataStore = exportTable;
                ImgBtnExport.Visible = true;
            }
            else
            {
                vSession.ViewStateDataStore = null;
                ImgBtnExport.Visible = false;
            }
        }

        # endregion

        # region Buttons

        protected void aCancelRunCriteriaSelection_OnClick(object sender, EventArgs args)
        {
            try
            {
                divAlgorithmHolder.Visible = false;
                divConnectionsTableHolder.Visible = true;
                aShowAlgBtn.Visible = true;
                PnlFindPartners.Visible = vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType;

                LblAlgBtnText.Text = "Run MATCHING PROCESS again";
                vSession.LoadedDashboardControl = null;

                RdgConnections.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aShowAlgBtn_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    divConnectionsTableHolder.Visible = false;
                    PnlFindPartners.Visible = false;
                    divAlgorithmHolder.Visible = true;
                    aShowAlgBtn.Visible = !aShowAlgBtn.Visible;
                    aCancelRunCriteriaSelection.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                    aRunAlgBtn.Visible = true;

                    PnlCriteriaSelection.Visible = true;
                    PnlSelectionProcess.Visible = false;

                    LblAlgBtnText.Text = "Close MATCHING PROCESS";
                    vSession.LoadedDashboardControl = ControlLoader.ProfileDataEditMode;
                    ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "find-new-partners"), false);
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

        protected void RunAlgBtn_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    aCancelRunCriteriaSelection.Visible = Sql.HasUserConnections(vSession.User.Id, session);

                    if (PnlCriteriaSelection.Visible)
                    {
                        PnlCriteriaSelection.Visible = false;
                        PnlSelectionProcess.Visible = true;

                        vSession.LoadedDashboardControl = ControlLoader.ProfileDataEditMode;
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "find-new-partners"), false);
                    }
                    else if (PnlSelectionProcess.Visible)
                    {
                        PnlCriteriaSelection.Visible = true;
                        PnlSelectionProcess.Visible = false;

                        LblAlgBtnText.Text = "Run MATCHING PROCESS again";
                        vSession.LoadedDashboardControl = ControlLoader.ProfileDataViewMode;

                        List<ElioSubIndustriesGroupItems> verticalsDescription = Sql.GetUserSubIndustriesGroupItemsIJAlgorithmSubIndustries(vSession.User.Id, session);
                        List<ElioUsersCriteria> userAlgorithmCriteria = Sql.GetUserCriteria(vSession.User.Id, session);
                        ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.User.Id, session);
                        ElioUsersFiles userCsv = Sql.GetUserCsvFile(vSession.User.Id, session);

                        if (userAlgorithmCriteria.Count > 0)
                        {
                            #region criteria overview

                            rowVerticals.Visible = true;
                            LblVerticalsValue.Text = string.Empty;
                            foreach (ElioSubIndustriesGroupItems vert in verticalsDescription)
                            {
                                LblVerticalsValue.Text += vert.Description + ", ";
                            }
                            LblVerticalsValue.Text = !string.IsNullOrEmpty(LblVerticalsValue.Text) ? LblVerticalsValue.Text.Substring(0, LblVerticalsValue.Text.Length - 2) : "";

                            LblSupportValue.Text = string.Empty;
                            LblCountryValue.Text = string.Empty;
                            foreach (ElioUsersCriteria crit in userAlgorithmCriteria)
                            {
                                #region criteria table

                                if (crit.CriteriaId == 12)
                                {
                                    rowFee.Visible = true;
                                    LblFeeValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 13)
                                {
                                    rowRevenue.Visible = true;
                                    LblRevenueValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 14)
                                {
                                    rowSupport.Visible = true;
                                    LblSupportValue.Text += crit.CriteriaValue + ", ";
                                }

                                else if (crit.CriteriaId == 1)
                                {
                                    rowCompMat.Visible = true;
                                    LblCompMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 2)
                                {
                                    rowProgMat.Visible = true;
                                    LblProgMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 3)
                                {
                                    rowNumPartn.Visible = true;
                                    LblNumPartnValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 4)
                                {
                                    rowTiers.Visible = true;
                                    LblTiersValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 5)
                                {
                                    rowTrain.Visible = true;
                                    LblTrainValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 6)
                                {
                                    rowFreeTrain.Visible = true;
                                    LblFreeTrainValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 7)
                                {
                                    rowMarkMat.Visible = true;
                                    LblMarkMatValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 8)
                                {
                                    rowCert.Visible = true;
                                    LblCertValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 9)
                                {
                                    rowLocal.Visible = true;
                                    LblLocalValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 10)
                                {
                                    rowMdf.Visible = true;
                                    LblMdfValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 11)
                                {
                                    rowPortal.Visible = true;
                                    LblPortalValue.Text = crit.CriteriaValue;
                                }

                                else if (crit.CriteriaId == 15)
                                {
                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        rowCountry.Visible = true;
                                        LblCountryValue.Text += crit.CriteriaValue + ", ";
                                    }
                                }

                                #endregion
                            }

                            LblSupportValue.Text = !string.IsNullOrEmpty(LblSupportValue.Text) ? LblSupportValue.Text.Substring(0, LblSupportValue.Text.Length - 2) : "";
                            LblCountryValue.Text = !string.IsNullOrEmpty(LblCountryValue.Text) ? LblCountryValue.Text.Substring(0, LblCountryValue.Text.Length - 2) : "";

                            if (userPdf != null)
                            {
                                rowPdf.Visible = true;
                                string[] pdfArray = userPdf.FilePath.Split('/');
                                string pdfName = pdfArray[4];
                                LblPdfValue.Text = pdfName;
                                aPdf.HRef = userPdf.FilePath;
                                aPdf.Target = "_blank";
                            }

                            if (userCsv != null)
                            {
                                rowCsv.Visible = true;
                                string[] csvArray = userCsv.FilePath.Split('/');
                                string csvName = csvArray[4];
                                LblCsvValue.Text = csvName;
                                aCsv.HRef = userCsv.FilePath;
                                aCsv.Target = "_blank";
                            }

                            #endregion
                        }
                        else
                        {
                            vSession.LoadedDashboardControl = null;
                            divAlgorithmHolder.Visible = false;
                            divConnectionsTableHolder.Visible = true;
                            aShowAlgBtn.Visible = true;
                            RdgConnections.Rebind();
                        }
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

        # region Buttons of Connections Page

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    HtmlAnchor aDelBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)aDelBtn.NamingContainer;

                    ConnectionId = Convert.ToInt32(item["connection_id"].Text);

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (ConnectionId != 0)
                    {
                        ElioUsersConnections connection = Sql.GetUserConnection(ConnectionId, session);

                        if (connection != null)
                        {
                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                            connection.CanBeViewed = 0;
                            loader.Update(connection);

                            RdgConnections.Rebind();
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                        ConnectionId = 0;
                    }
                    else
                    {
                        Logger.DetailedError(string.Format("User {0} tried to delete connection at {1}, but connection ID was 0", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, "Something went wrong! Your connection could not be deleted. Please try again later or contact us", MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
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

        protected void BtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    string strQuery = @"select Elio_users_connections.id as connection_id, Elio_users_connections.user_id as connection_user_id, Elio_users_connections.connection_id as connection_company_id,
                                       Elio_users_connections.sysdate,  Elio_users_connections.current_period_start, Elio_users_connections.current_period_end, Elio_users_connections.can_be_viewed, Elio_users_connections.is_new, 
                                       Elio_users.company_name, Elio_users.email as company_email, Elio_users.official_email, Elio_users.website as company_website,
                                       Elio_users.company_logo, Elio_users.billing_type, Elio_users.user_application_type, Elio_users.country, Elio_users.company_type, Elio_users.is_public
                                       from Elio_users_connections
                                       inner join Elio_users on Elio_users.id = Elio_users_connections.connection_id
                                       where Elio_users_connections.user_id=" + vSession.User.Id + " " +
                        "and can_be_viewed=1";

                    if (RdpConnectionsFrom.SelectedDate != null)
                    {
                        strQuery += " and Elio_users_connections.sysdate>='" + Convert.ToDateTime(RdpConnectionsFrom.SelectedDate).ToString("yyyy/MM/dd") + "'";
                    }

                    if (RdpConnectionsTo.SelectedDate != null)
                    {
                        strQuery += " and Elio_users_connections.sysdate<='" + Convert.ToDateTime(RdpConnectionsTo.SelectedDate).ToString("yyyy/MM/dd") + "'";
                    }

                    if (RtbxCompanyName.Text != "")
                    {
                        strQuery += " and company_name like '" + RtbxCompanyName.Text.Trim() + "%'";
                    }

                    strQuery += " order by sysdate desc";

                    ViewState["StrQuery"] = strQuery;
                    RdgConnections.Rebind();

                    FillDataForExport(true);
                    vSession.HasSearchCriteria = true;
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

        protected void BtnAddOpportunity_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor btn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)btn.NamingContainer;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["connection_company_id"].Text), session);

                    if (company != null)
                    {
                        bool existOpportunity = Sql.IsUserOpportunityByEmailOrName(vSession.User.Id, item["company_name"].Text, item["company_email"].Text, session);
                        if (!existOpportunity)
                        {
                            ElioOpportunitiesUsers opportunity = new ElioOpportunitiesUsers();

                            opportunity.UserId = vSession.User.Id;
                            opportunity.LastName = company.LastName;
                            opportunity.FirstName = company.FirstName;
                            opportunity.OrganizationName = company.CompanyName;
                            opportunity.Occupation = "";
                            opportunity.Address = company.Address;
                            opportunity.Email = company.Email;
                            opportunity.Phone = company.Phone;
                            opportunity.WebSite = company.WebSite;
                            opportunity.LinkedInUrl = company.LinkedInUrl;
                            opportunity.TwitterUrl = company.TwitterUrl;
                            opportunity.SysDate = DateTime.Now;
                            opportunity.LastUpdated = DateTime.Now;
                            opportunity.GuId = Guid.NewGuid().ToString();
                            opportunity.IsPublic = 1;
                            opportunity.StatusId = Convert.ToInt32(OpportunityStep.Contact);

                            DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);
                            loader.Insert(opportunity);

                            RdgConnections.Rebind();
                            //Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
                            //Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            string alert = "This connection is already saved as your opportunity.";
                            GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, alert, MessageTypes.Error, true, true, false);
                        }
                    }
                    else
                    {
                        string alert = "Something went wrong! This connection could not be added as your contact too. Try again later or contact with us.";
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, alert, MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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

        protected void BtnNew_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor btnNew = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)btnNew.NamingContainer;

                    ElioUsersConnections connection = Sql.GetUserConnection(Convert.ToInt32(item["connection_id"].Text), session);
                    if (connection != null)
                    {
                        connection.IsNew = 0;
                        connection.LastUpdated = DateTime.Now;

                        DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                        loader.Update(connection);
                    }

                    RdgConnections.Rebind();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
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

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (vSession.ViewStateDataStore != null)
                        Response.Redirect("download-csv?case=MyMatchesData", false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #endregion

        # region Grids

        protected void RdgConnections_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    session.OpenConnection();

                    GridDataItem item = (GridDataItem)e.Item;

                    if (vSession.User != null)
                    {
                        //ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["company_id"].Text), session);

                        //if (company != null)
                        //{
                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        //Image imgUserApplicationType = (Image)ControlFinder.FindControlRecursive(item, "ImgUserApplicationType");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        if (Convert.ToInt32(item["user_application_type"].Text) == Convert.ToInt32(UserApplicationType.Elioplus))
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.ProfileForConnectionspage(Convert.ToInt32(item["connection_company_id"].Text), item["company_name"].Text, item["company_type"].Text);        //ControlLoader.Profile(company);

                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.ImageUrl = (!string.IsNullOrEmpty(item["company_logo"].Text)) ? item["company_logo"].Text : "/images/no_logo1.png";  // item["company_logo"].Text;
                            imgCompanyLogo.AlternateText = "Company logo";
                        }
                        else
                        {
                            if (Convert.ToInt32(item["user_application_type"].Text) == Convert.ToInt32(UserApplicationType.ThirdParty))
                            {
                                if (Convert.ToInt32(item["is_public"].Text) == (int)AccountPublicStatus.IsPublic)
                                {
                                    aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.ProfileForConnectionspage(Convert.ToInt32(item["connection_company_id"].Text), item["company_name"].Text, item["company_type"].Text);            //ControlLoader.Profile(company);

                                    imgCompanyLogo.ToolTip = "View company's profile";
                                    imgCompanyLogo.ImageUrl = (!string.IsNullOrEmpty(item["company_logo"].Text)) ? item["company_logo"].Text : (!string.IsNullOrEmpty(item["logo"].Text)) ? item["logo"].Text : (!string.IsNullOrEmpty(item["avatar"].Text)) ? item["avatar"].Text : "/images/no_logo1.png";  // item["company_logo"].Text;
                                    imgCompanyLogo.AlternateText = "Company logo";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(item["company_website"].Text) && item["company_website"].Text != "&nbsp;")
                                    {
                                        aCompanyLogo.HRef = aCompanyName.HRef = item["company_website"].Text;

                                        aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                        imgCompanyLogo.ToolTip = "View company's site";
                                        imgCompanyLogo.ImageUrl = "/images/icons/partners_th_party_2.png";
                                        imgCompanyLogo.AlternateText = "Third party partners logo";
                                    }
                                    else
                                    {
                                        aCompanyLogo.HRef = aCompanyName.HRef = "#";
                                    }
                                }
                            }
                        }

                        ////if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false")
                        ////{
                        ////    aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.DashboardPersonProfile(company);
                        ////    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                        ////}

                        Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                        lblCompanyNameContent.Text = item["company_name"].Text;

                        Label lblCompanyEmail = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyEmail");
                        lblCompanyEmail.Text = item["company_email"].Text;

                        Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");
                        lblWebsite.Text = item["company_website"].Text;

                        HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                        aWebsite.HRef = item["company_website"].Text;
                        aWebsite.Target = "_blank";

                        HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");

                        ////if (company.AccountStatus == (int)AccountStatus.Completed && company.IsPublic == (int)AccountStatus.Public)
                        ////{
                        ////    aMoreDetails.HRef = ControlLoader.Profile(company);
                        ////}
                        ////else
                        ////{
                        ////    aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                        ////}

                        aMoreDetails.HRef = ControlLoader.PersonProfileForConnectionsPage(Convert.ToInt32(item["connection_company_id"].Text), item["company_name"].Text, item["company_type"].Text);       //ControlLoader.PersonProfile(company);
                        aMoreDetails.Target = "_blank";

                        Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                        lblMoreDetails.Text = "more details";

                        Image imgLinkedin = (Image)ControlFinder.FindControlRecursive(item, "ImgLinkedin");
                        Label lblNoLinkedin = (Label)ControlFinder.FindControlRecursive(item, "LblNoLinkedin");
                        HtmlAnchor aLinkedin = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aLinkedin");

                        if (!string.IsNullOrEmpty(item["linkedin_url"].Text))
                        {
                            imgLinkedin.Visible = true;
                            lblNoLinkedin.Visible = false;

                            aLinkedin.HRef = item["linkedin_url"].Text;
                            imgLinkedin.ToolTip = "View Linkedin profile";
                            aLinkedin.Target = "_blank";
                        }
                        else
                        {
                            imgLinkedin.Visible = false;
                            lblNoLinkedin.Visible = true;
                        }

                        //ElioUsersConnections connection = Sql.GetUserConnection(Convert.ToInt32(item["id"].Text), session);
                        //if (connection != null)
                        //{
                        Label lblViewGivenDate = (Label)ControlFinder.FindControlRecursive(item, "LblViewGivenDate");
                        lblViewGivenDate.Text = item["sysdate"].Text;

                        //HtmlAnchor btnNew = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnNew");
                        //btnNew.Disabled = !(connection.IsNew == 1);    // Convert.ToInt32(item["is_new"].Text) == 1 ? false : true;
                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        divNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1;
                        if (Convert.ToInt32(item["is_new"].Text) == 1)
                        {
                            vSession.HasToUpdateNewConnections = true;
                        }
                        //Label lblBtnNew = (Label)ControlFinder.FindControlRecursive(item, "LblBtnNew");
                        //lblBtnNew.Text = (connection.IsNew == 1) ? "New" : "Viewed"; // Convert.ToInt32(item["is_new"].Text) == 1 ? "New" : "Viewed";

                        Label lblBtnDelete = (Label)ControlFinder.FindControlRecursive(item, "LblBtnDelete");
                        lblBtnDelete.Text = "";

                        HtmlAnchor btnAddOpportunity = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnAddOpportunity");
                        btnAddOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
                        btnAddOpportunity.Visible = !Sql.IsUserOpportunity(vSession.User.Id, item["company_name"].Text, session);

                        Image imgOpportunitySuccess = (Image)ControlFinder.FindControlRecursive(item, "ImgOpportunitySuccess");
                        imgOpportunitySuccess.Visible = !btnAddOpportunity.Visible;

                        Label lblOpportunitySuccess = (Label)ControlFinder.FindControlRecursive(item, "LblOpportunitySuccess");
                        lblOpportunitySuccess.Visible = imgOpportunitySuccess.Visible;
                        if (imgOpportunitySuccess.Visible)
                            lblOpportunitySuccess.Text = "Added";

                        Label lblAddOpportunity = (Label)ControlFinder.FindControlRecursive(item, "LblAddOpportunity");
                        lblAddOpportunity.Text = "Add Opportunity";
                        //}
                        //else
                        //{
                        //    Response.Redirect(vSession.Page, false);
                        //}
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Login, false);
                    }
                    //}
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

        protected void RdgConnections_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcConnectionsMessageAlert.Visible = false;
                divConnections.Visible = divConnectionsTableHolder.Visible = true;

                if (vSession.User != null)
                {
                    //int canBeViewed = 1;

                    //List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> userConnections = new List<ElioUsersConnectionsIJUsersIJPersonIJCompanies>();
                    DataTable table = null;
                    if (ViewState["StrQuery"] == null)
                    {
                        //userConnections = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompanies(vSession.User.Id, canBeViewed, session);
                        table = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesTable(vSession.User.Id, 1, session);
                        FillDataForExport(false);
                    }
                    else
                    {
                        //DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
                        //userConnections = loader.Load(ViewState["StrQuery"].ToString());
                        table = session.GetDataTable(ViewState["StrQuery"].ToString());
                    }

                    if (table.Rows.Count > 0)
                    {
                        divConnections.Visible = divConnectionsTableHolder.Visible = true;
                        RdgConnections.Visible = true;

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("company_id");
                        //table.Columns.Add("company_logo");
                        //table.Columns.Add("user_application_type");
                        //table.Columns.Add("company_name");
                        //table.Columns.Add("country");
                        //table.Columns.Add("company_email");
                        //table.Columns.Add("company_website");
                        //table.Columns.Add("linkedin_url");
                        //table.Columns.Add("sysdate");
                        //table.Columns.Add("current_period_start");
                        //table.Columns.Add("current_period_end");
                        //table.Columns.Add("is_new");
                        //table.Columns.Add("avatar");
                        //table.Columns.Add("logo");

                        //foreach (ElioUsersConnectionsIJUsersIJPersonIJCompanies con in userConnections)
                        //{
                        //    if (con.CanBeViewed == 1)
                        //    {
                        //        table.Rows.Add(con.Id, con.ConnectionId, con.CompanyLogo, con.UserApplicationType, con.CompanyName, con.Country, !string.IsNullOrEmpty(con.OfficialEmail) ? con.Email + ", " + con.OfficialEmail : con.Email, con.WebSite, con.Linkedin, con.SysDate.ToString("MM/dd/yyyy"), con.CurrentPeriodStart.ToString("MM/dd/yyyy"), con.CurrentPeriodEnd.ToString("MM/dd/yyyy"), con.IsNew, con.Avatar, con.Logo);
                        //    }
                        //}

                        RdgConnections.DataSource = table;

                        //vSession.ViewStateDataStore = table;
                        //ImgBtnExport.Visible = true;

                        LblConLogo.Text = "Logo";
                        //LblUserApplicationType.Text = "User Type";
                        LblConName.Text = "Name";
                        LblConEmail.Text = "E-mail";
                        //LblConWebsite.Text = "Website";
                        LblLinkedin.Text = "Contact Person";
                        LblConDateStarted.Text = "Date";
                        //LblConDateFrom.Text = "From Date";
                        //LblConDateTo.Text = "To Date";
                        LblStatus.Text = "Status";
                        LblConAdd.Text = "Actions";
                    }
                    else
                    {
                        divConnections.Visible = false;
                        //divConnectionsTableHolder.Visible = vSession.HasSearchCriteria;
                        RdgConnections.Visible = false;
                        ImgBtnExport.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    divConnections.Visible = divConnectionsTableHolder.Visible = false;
                    RdgConnections.Visible = false;

                    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
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

        # endregion
    }
}
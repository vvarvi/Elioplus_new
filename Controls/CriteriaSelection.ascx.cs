using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Enums;
using System.IO;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class CriteriaSelection : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(aSubmit);

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

        # region methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                UpdateStrings();
                LoadVerticals();
                LoadFee();
                LoadRevenue();
                LoadSupport();
                LoadOptionals();
            }

            LblOverviewCountries.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? true : false;
        }

        private void UpdateStrings()
        {
            LblWizardTitle.Text = "Criteria selection of matching process";
            LblBasicCriteria.Text = "Basic criteria";
            LblOptionalCriteria.Text = "Optional criteria";
            LblOptionalCriteria2.Text = "Optional criteria";
            LblSummary.Text = "Summary";
            LblAlertDanger.Text = "You have some form errors. Please check below.";
            LblAlertSuccess.Text = "Your form validation is successful!";
            LblRequiredCriteria.Text = "The following criteria are required!";
            LblOptCriteria.Text = "The following criteria are optional!";
            LblOverview.Text = "Overview of your selection";
            LblOverviewRequired.Text = "Required criteria selection";
            LblOverviewOptional.Text = "Optional criteria selection";
            LblOverviewVerticals.Text = "Industry verticals: ";
            LblOverviewFee.Text = "Set up fee: ";
            LblOverviewRevenue.Text = "Revenue model: ";
            LblOverviewSupport.Text = "Support: ";
            LblOverviewTraining.Text = "Training: ";
            LblOverviewFreeTraining.Text = "Free training: ";
            LblOverviewCompMaturity.Text = "Company maturity(years): ";
            LblProgramMaturity.Text = "Partner program maturity(years): ";
            LblOverviewMarkMaterial.Text = "Marketing material: ";
            LblOverviewLocalization.Text = "Localization: ";
            LblOverviewMDF.Text = "Marketing development funds: ";
            LblOverviewCertification.Text = "Certification required: ";
            LblOverviewPortal.Text = "Partner portal / resources: ";
            LblOverviewNumPartners.Text = "Number of partners: ";
            LblOverviewTiers.Text = "Partner program tiers: ";
            LblOverviewCountries.Text = "Countries: ";
            LblWizardTitleSteps.Text = "Step 1 of 4";
            LblPdfTitle.Text = "Pdf";
            LblCsvTitle.Text = "Csv";
            LblPdfSelect.Text = LblCsvSelect.Text = "Select";
            LblPdfChange.Text = LblCsvChange.Text = "Change";
            LblPdfRemove.Text = LblCsvRemove.Text = "Remove";
            LblFilesInfo.Text = "Upload files! (optional): ";
            LblFilesInfoContent.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Upload a 'pdf' file with your partner program summary for your future partners (max size: " +  int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPdfSize"]).ToString() + " Bytes). "
                                    + "Also upload a 'csv' file with your current resellers' e-mails or domains so we exclude them from our matching procedure (max sixe: " + int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxCsvSize"]).ToString() + " Bytes)." : "Upload a csv file with your current vendors' e-mails or domains so we can exclude them from the matching process (max sixe: 100 kb).";

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divPdfFile.Visible = true;
                ElioUsersFiles userPdf = Sql.GetUserPdfFile(vSession.User.Id, session);

                if (userPdf != null)
                {
                    string[] pdfArray = userPdf.FilePath.Split('/');
                    string pdfName = pdfArray[4];
                    LblExistingPdf.Text = pdfName;
                }
            }

            ElioUsersFiles userCsv = Sql.GetUserCsvFile(vSession.User.Id, session);

            if (userCsv != null)
            {
                string[] csvArray = userCsv.FilePath.Split('/');
                string csvName = csvArray[4];
                LblExistingCsv.Text = csvName;
            }
        }

        private void LoadVerticals()
        {
            LblIndVerticals.Text = "Industry verticals";
            LblVerticalsHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Choose the products you'd like to find resellers for" : "Choose the products that you want to resell";

            # region set vertical names

            LblVertical1.Text = "Email Marketing";
            LblVertical2.Text = "Campaign Management";
            LblVertical3.Text = "Marketing Automation";
            LblVertical4.Text = "Content Marketing";
            LblVertical5.Text = "SEO & SEM";
            LblVertical6.Text = "Social Media Marketing";
            LblVertical7.Text = "Affiliate Marketing";
            LblVertical8.Text = "Surveys & Forms";
            LblVertical9.Text = "Ad Serving";
            LblVertical10.Text = "Event Management";
            LblVertical11.Text = "Sales Process Management";
            LblVertical12.Text = "Quotes & Orders";
            LblVertical13.Text = "Document Generation";
            LblVertical14.Text = "Sales Intelligence";
            LblVertical15.Text = "Engagement Tools";
            LblVertical16.Text = "POS";
            LblVertical17.Text = "E-Signature";
            LblVertical18.Text = "CRM";
            LblVertical19.Text = "Help Desk";
            LblVertical20.Text = "Live Chat";
            LblVertical21.Text = "Feedback Management";
            LblVertical22.Text = "Gamification & Loyalty";
            LblVertical23.Text = "Project Management Tools";
            LblVertical24.Text = "Chat & Web Conference";
            LblVertical25.Text = "Knowledge Management";
            LblVertical26.Text = "File Sharing Software";
            LblVertical27.Text = "Business Process Management";
            LblVertical28.Text = "Digital Asset Management";
            LblVertical29.Text = "ERP";
            LblVertical30.Text = "Inventory Management";
            LblVertical31.Text = "Shipping & Tracking";
            LblVertical32.Text = "Supply Chain Management";
            LblVertical33.Text = "Analytics Software";
            LblVertical34.Text = "Business Intelligence";
            LblVertical35.Text = "Data Visualization";
            LblVertical36.Text = "Competitive Intelligence";
            LblVertical37.Text = "Accounting";
            LblVertical38.Text = "Payment Processing";
            LblVertical39.Text = "Time & Expenses";
            LblVertical40.Text = "Billing & Invoicing";
            LblVertical41.Text = "Budgeting";
            LblVertical42.Text = "Applicant Tracking";
            LblVertical43.Text = "HR Administration";
            LblVertical44.Text = "Payroll";
            LblVertical45.Text = "Performance Management";
            LblVertical46.Text = "Recruiting";
            LblVertical47.Text = "Learning Management System";
            LblVertical48.Text = "Time & Expense";
            LblVertical49.Text = "API Tools";
            LblVertical50.Text = "Bug Trackers";
            LblVertical51.Text = "Development Tools";
            LblVertical52.Text = "eCommerce";
            LblVertical53.Text = "Frameworks & Libraries";
            LblVertical54.Text = "Mobile Development";
            LblVertical55.Text = "Optimization";
            LblVertical56.Text = "Usability Testing";
            LblVertical57.Text = "Websites";
            LblVertical58.Text = "Cloud Integration (iPaaS)";
            LblVertical59.Text = "Cloud Management";
            LblVertical60.Text = "Cloud Storage";
            LblVertical61.Text = "Remote Access";
            LblVertical62.Text = "Virtualization";
            LblVertical63.Text = "Web Hosting";
            LblVertical64.Text = "Web Monitoring";
            LblVertical65.Text = "VOIP";
            LblVertical66.Text = "Calendar & Scheduling";
            LblVertical67.Text = "Email";
            LblVertical68.Text = "Note Taking";
            LblVertical69.Text = "Password Management";
            LblVertical70.Text = "Presentations";
            LblVertical71.Text = "Productivity Suites";
            LblVertical72.Text = "Spreadsheets";
            LblVertical73.Text = "Task Management";
            LblVertical74.Text = "Time Management";
            LblVertical75.Text = "Cybersecurity";
            LblVertical76.Text = "Vulnerability Management";
            LblVertical77.Text = "Firewall";
            LblVertical78.Text = "Mobile Data Security";
            LblVertical79.Text = "Backup & Restore";
            LblVertical80.Text = "Graphic Design";
            LblVertical81.Text = "Infographics";
            LblVertical82.Text = "Video Editing";
            LblVertical83.Text = "Warehouse Management";
            LblVertical84.Text = "Supply Chain Execution";
            LblVertical85.Text = "eLearning";
            LblVertical86.Text = "Healthcare";
            LblVertical87.Text = "Big Data";
            LblVertical88.Text = "Data Warehousing";
            LblVertical89.Text = "Data Masking";
            LblVertical90.Text = "Databases";
            LblVertical91.Text = "Data Integration";
            LblVertical92.Text = "Data Management";
            LblVertical93.Text = "Identity Management";
            LblVertical94.Text = "Risk Management";
            LblVertical95.Text = "ECM";
            LblVertical96.Text = "Mobility";
            LblVertical97.Text = "Collaboration";
            LblVertical98.Text = "Conferencing";
            LblVertical99.Text = "Unified Messaging";
            LblVertical100.Text = "Unified Communications";
            LblVertical101.Text = "Team Collaboration";
            LblVertical102.Text = "Video Conferencing";
            LblVertical103.Text = "General-Purpose CAD";
            LblVertical104.Text = "CAM";
            LblVertical105.Text = "PLM";
            LblVertical106.Text = "PDM (Product Data Management)";
            LblVertical107.Text = "BIM";
            LblVertical108.Text = "3D Architecture";
            LblVertical109.Text = "3D CAD";
            LblVertical110.Text = "Location Intelligence";
            LblVertical111.Text = "Track Management";
            LblVertical112.Text = "Workflow Management";
            LblVertical113.Text = "Enterprise Asset Management";
            LblVertical114.Text = "Facility Management";
            LblVertical115.Text = "Asset Lifecycle Management";
            LblVertical116.Text = "CMMS";
            LblVertical117.Text = "Fleet Management";
            LblVertical118.Text = "Change Management";
            LblVertical119.Text = "Procurement";
            LblVertical120.Text = "Chatbot";
            LblVertical121.Text = "Penetration Testing";
            LblVertical122.Text = "Application Security";
            LblVertical123.Text = "Governance, Risk & Compliance (GRC)";
            LblVertical124.Text = "Compliance";
            LblVertical125.Text = "Fraud Prevention";

            # endregion

            List<ElioSubIndustriesGroupItems> userGroupItems = Sql.GetUserSubIndustries(vSession.User.Id, session);

            List<ElioUsersAlgorithmSubcategories> userAlgSubcategories = Sql.GetUserAlgorithmSubcategoriesById(vSession.User.Id, session);

            # region set visible verticals

            foreach (ElioSubIndustriesGroupItems sub in userGroupItems)
            {
                switch (sub.Id)
                {
                    case 1:
                        {
                            lblV1.Visible = true;
                            break;
                        }
                    case 2:
                        {
                            lblV2.Visible = true;
                            break;
                        }
                    case 3:
                        {
                            lblV3.Visible = true;
                            break;
                        }
                    case 4:
                        {
                            lblV4.Visible = true;
                            break;
                        }
                    case 5:
                        {
                            lblV5.Visible = true;
                            break;
                        }
                    case 6:
                        {
                            lblV6.Visible = true;
                            break;
                        }
                    case 7:
                        {
                            lblV7.Visible = true;
                            break;
                        }
                    case 8:
                        {
                            lblV8.Visible = true;
                            break;
                        }
                    case 9:
                        {
                            lblV9.Visible = true;
                            break;
                        }
                    case 10:
                        {
                            lblV10.Visible = true;
                            break;
                        }
                    case 11:
                        {
                            lblV11.Visible = true;
                            break;
                        }
                    case 12:
                        {
                            lblV12.Visible = true;
                            break;
                        }
                    case 13:
                        {
                            lblV13.Visible = true;
                            break;
                        }
                    case 14:
                        {
                            lblV14.Visible = true;
                            break;
                        }
                    case 15:
                        {
                            lblV15.Visible = true;
                            break;
                        }
                    case 16:
                        {
                            lblV16.Visible = true;
                            break;
                        }
                    case 17:
                        {
                            lblV17.Visible = true;
                            break;
                        }
                    case 18:
                        {
                            lblV18.Visible = true;
                            break;
                        }
                    case 19:
                        {
                            lblV19.Visible = true;
                            break;
                        }
                    case 20:
                        {
                            lblV20.Visible = true;
                            break;
                        }
                    case 21:
                        {
                            lblV21.Visible = true;
                            break;
                        }
                    case 22:
                        {
                            lblV22.Visible = true;
                            break;
                        }
                    case 23:
                        {
                            lblV23.Visible = true;
                            break;
                        }
                    case 24:
                        {
                            lblV24.Visible = true;
                            break;
                        }
                    case 25:
                        {
                            lblV25.Visible = true;
                            break;
                        }
                    case 26:
                        {
                            lblV26.Visible = true;
                            break;
                        }
                    case 27:
                        {
                            lblV27.Visible = true;
                            break;
                        }
                    case 28:
                        {
                            lblV28.Visible = true;
                            break;
                        }
                    case 29:
                        {
                            lblV29.Visible = true;
                            break;
                        }
                    case 30:
                        {
                            lblV30.Visible = true;
                            break;
                        }
                    case 31:
                        {
                            lblV31.Visible = true;
                            break;
                        }
                    case 32:
                        {
                            lblV32.Visible = true;
                            break;
                        }
                    case 33:
                        {
                            lblV33.Visible = true;
                            break;
                        }
                    case 34:
                        {
                            lblV34.Visible = true;
                            break;
                        }
                    case 35:
                        {
                            lblV35.Visible = true;
                            break;
                        }
                    case 36:
                        {
                            lblV36.Visible = true;
                            break;
                        }
                    case 37:
                        {
                            lblV37.Visible = true;
                            break;
                        }
                    case 38:
                        {
                            lblV38.Visible = true;
                            break;
                        }
                    case 39:
                        {
                            lblV39.Visible = true;
                            break;
                        }
                    case 40:
                        {
                            lblV40.Visible = true;
                            break;
                        }
                    case 41:
                        {
                            lblV41.Visible = true;
                            break;
                        }
                    case 42:
                        {
                            lblV42.Visible = true;
                            break;
                        }
                    case 43:
                        {
                            lblV43.Visible = true;
                            break;
                        }
                    case 44:
                        {
                            lblV44.Visible = true;
                            break;
                        }
                    case 45:
                        {
                            lblV45.Visible = true;
                            break;
                        }
                    case 46:
                        {
                            lblV46.Visible = true;
                            break;
                        }
                    case 47:
                        {
                            lblV47.Visible = true;
                            break;
                        }
                    case 48:
                        {
                            lblV48.Visible = true;
                            break;
                        }
                    case 49:
                        {
                            lblV49.Visible = true;
                            break;
                        }
                    case 50:
                        {
                            lblV50.Visible = true;
                            break;
                        }
                    case 51:
                        {
                            lblV51.Visible = true;
                            break;
                        }
                    case 52:
                        {
                            lblV52.Visible = true;
                            break;
                        }
                    case 53:
                        {
                            lblV53.Visible = true;
                            break;
                        }
                    case 54:
                        {
                            lblV54.Visible = true;
                            break;
                        }
                    case 55:
                        {
                            lblV55.Visible = true;
                            break;
                        }
                    case 56:
                        {
                            lblV56.Visible = true;
                            break;
                        }
                    case 57:
                        {
                            lblV57.Visible = true;
                            break;
                        }
                    case 58:
                        {
                            lblV58.Visible = true;
                            break;
                        }
                    case 59:
                        {
                            lblV59.Visible = true;
                            break;
                        }
                    case 60:
                        {
                            lblV60.Visible = true;
                            break;
                        }
                    case 61:
                        {
                            lblV61.Visible = true;
                            break;
                        }
                    case 62:
                        {
                            lblV62.Visible = true;
                            break;
                        }
                    case 63:
                        {
                            lblV63.Visible = true;
                            break;
                        }
                    case 64:
                        {
                            lblV64.Visible = true;
                            break;
                        }
                    case 65:
                        {
                            lblV65.Visible = true;
                            break;
                        }
                    case 66:
                        {
                            lblV66.Visible = true;
                            break;
                        }
                    case 67:
                        {
                            lblV67.Visible = true;
                            break;
                        }
                    case 68:
                        {
                            lblV68.Visible = true;
                            break;
                        }
                    case 69:
                        {
                            lblV69.Visible = true;
                            break;
                        }
                    case 70:
                        {
                            lblV70.Visible = true;
                            break;
                        }
                    case 71:
                        {
                            lblV71.Visible = true;
                            break;
                        }
                    case 72:
                        {
                            lblV72.Visible = true;
                            break;
                        }
                    case 73:
                        {
                            lblV73.Visible = true;
                            break;
                        }
                    case 74:
                        {
                            lblV74.Visible = true;
                            break;
                        }
                    case 75:
                        {
                            lblV75.Visible = true;
                            break;
                        }
                    case 76:
                        {
                            lblV76.Visible = true;
                            break;
                        }
                    case 77:
                        {
                            lblV77.Visible = true;
                            break;
                        }
                    case 78:
                        {
                            lblV78.Visible = true;
                            break;
                        }
                    case 79:
                        {
                            lblV79.Visible = true;
                            break;
                        }
                    case 80:
                        {
                            lblV80.Visible = true;
                            break;
                        }
                    case 81:
                        {
                            lblV81.Visible = true;
                            break;
                        }
                    case 82:
                        {
                            lblV82.Visible = true;
                            break;
                        }
                    case 83:
                        {
                            lblV83.Visible = true;
                            break;
                        }
                    case 84:
                        {
                            lblV84.Visible = true;
                            break;
                        }
                    case 85:
                        {
                            lblV85.Visible = true;
                            break;
                        }
                    case 86:
                        {
                            lblV86.Visible = true;
                            break;
                        }
                    case 87:
                        {
                            lblV87.Visible = true;
                            break;
                        }
                    case 88:
                        {
                            lblV88.Visible = true;
                            break;
                        }
                    case 89:
                        {
                            lblV89.Visible = true;
                            break;
                        }
                    case 90:
                        {
                            lblV90.Visible = true;
                            break;
                        }
                    case 91:
                        {
                            lblV91.Visible = true;
                            break;
                        }
                    case 92:
                        {
                            lblV92.Visible = true;
                            break;
                        }
                    case 93:
                        {
                            lblV93.Visible = true;
                            break;
                        }
                    case 94:
                        {
                            lblV94.Visible = true;
                            break;
                        }
                    case 95:
                        {
                            lblV95.Visible = true;
                            break;
                        }
                    case 96:
                        {
                            lblV96.Visible = true;
                            break;
                        }
                    case 97:
                        {
                            lblV97.Visible = true;
                            break;
                        }
                    case 98:
                        {
                            lblV98.Visible = true;
                            break;
                        }
                    case 99:
                        {
                            lblV99.Visible = true;
                            break;
                        }
                    case 100:
                        {
                            lblV100.Visible = true;
                            break;
                        }
                    case 101:
                        {
                            lblV101.Visible = true;
                            break;
                        }
                    case 102:
                        {
                            lblV102.Visible = true;
                            break;
                        }
                    case 103:
                        {
                            lblV103.Visible = true;
                            break;
                        }
                    case 104:
                        {
                            lblV104.Visible = true;
                            break;
                        }
                    case 105:
                        {
                            lblV105.Visible = true;
                            break;
                        }
                    case 106:
                        {
                            lblV106.Visible = true;
                            break;
                        }
                    case 107:
                        {
                            lblV107.Visible = true;
                            break;
                        }
                    case 108:
                        {
                            lblV108.Visible = true;
                            break;
                        }
                    case 109:
                        {
                            lblV109.Visible = true;
                            break;
                        }
                    case 110:
                        {
                            lblV110.Visible = true;
                            break;
                        }
                    case 111:
                        {
                            lblV111.Visible = true;
                            break;
                        }
                    case 112:
                        {
                            lblV112.Visible = true;
                            break;
                        }
                    case 113:
                        {
                            lblV113.Visible = true;
                            break;
                        }
                    case 114:
                        {
                            lblV114.Visible = true;
                            break;
                        }
                    case 115:
                        {
                            lblV115.Visible = true;
                            break;
                        }
                    case 116:
                        {
                            lblV116.Visible = true;
                            break;
                        }
                    case 117:
                        {
                            lblV117.Visible = true;
                            break;
                        }
                    case 118:
                        {
                            lblV118.Visible = true;
                            break;
                        }
                    case 119:
                        {
                            lblV119.Visible = true;
                            break;
                        }
                    case 120:
                        {
                            lblV120.Visible = true;
                            break;
                        }
                    case 121:
                        {
                            lblV121.Visible = true;
                            break;
                        }
                    case 122:
                        {
                            lblV122.Visible = true;
                            break;
                        }
                    case 123:
                        {
                            lblV123.Visible = true;
                            break;
                        }
                    case 124:
                        {
                            lblV94.Visible = true;
                            break;
                        }
                    case 125:
                        {
                            lblV125.Visible = true;
                            break;
                        }
                }
            }

            # endregion

            # region set selected verticals

            foreach (ElioUsersAlgorithmSubcategories sub in userAlgSubcategories)
            {
                switch (sub.SubcategoryId)
                {
                    case 1:
                        {
                            HdnVert1Ckd.Value = "1";
                            break;
                        }
                    case 2:
                        {
                            HdnVert2Ckd.Value = "1";
                            break;
                        }
                    case 3:
                        {
                            HdnVert3Ckd.Value = "1";
                            break;
                        }
                    case 4:
                        {
                            HdnVert4Ckd.Value = "1";
                            break;
                        }
                    case 5:
                        {
                            HdnVert5Ckd.Value = "1";
                            break;
                        }
                    case 6:
                        {
                            HdnVert6Ckd.Value = "1";
                            break;
                        }
                    case 7:
                        {
                            HdnVert7Ckd.Value = "1";
                            break;
                        }
                    case 8:
                        {
                            HdnVert8Ckd.Value = "1";
                            break;
                        }
                    case 9:
                        {
                            HdnVert9Ckd.Value = "1";
                            break;
                        }
                    case 10:
                        {
                            HdnVert10Ckd.Value = "1";
                            break;
                        }
                    case 11:
                        {
                            HdnVert11Ckd.Value = "1";
                            break;
                        }
                    case 12:
                        {
                            HdnVert12Ckd.Value = "1";
                            break;
                        }
                    case 13:
                        {
                            HdnVert13Ckd.Value = "1";
                            break;
                        }
                    case 14:
                        {
                            HdnVert14Ckd.Value = "1";
                            break;
                        }
                    case 15:
                        {
                            HdnVert15Ckd.Value = "1";
                            break;
                        }
                    case 16:
                        {
                            HdnVert16Ckd.Value = "1";
                            break;
                        }
                    case 17:
                        {
                            HdnVert17Ckd.Value = "1";
                            break;
                        }
                    case 18:
                        {
                            HdnVert18Ckd.Value = "1";
                            break;
                        }
                    case 19:
                        {
                            HdnVert19Ckd.Value = "1";
                            break;
                        }
                    case 20:
                        {
                            HdnVert20Ckd.Value = "1";
                            break;
                        }
                    case 21:
                        {
                            HdnVert21Ckd.Value = "1";
                            break;
                        }
                    case 22:
                        {
                            HdnVert22Ckd.Value = "1";
                            break;
                        }
                    case 23:
                        {
                            HdnVert23Ckd.Value = "1";
                            break;
                        }
                    case 24:
                        {
                            HdnVert24Ckd.Value = "1";
                            break;
                        }
                    case 25:
                        {
                            HdnVert25Ckd.Value = "1";
                            break;
                        }
                    case 26:
                        {
                            HdnVert26Ckd.Value = "1";
                            break;
                        }
                    case 27:
                        {
                            HdnVert27Ckd.Value = "1";
                            break;
                        }
                    case 28:
                        {
                            HdnVert28Ckd.Value = "1";
                            break;
                        }
                    case 29:
                        {
                            HdnVert29Ckd.Value = "1";
                            break;
                        }
                    case 30:
                        {
                            HdnVert30Ckd.Value = "1";
                            break;
                        }
                    case 31:
                        {
                            HdnVert31Ckd.Value = "1";
                            break;
                        }
                    case 32:
                        {
                            HdnVert32Ckd.Value = "1";
                            break;
                        }
                    case 33:
                        {
                            HdnVert33Ckd.Value = "1";
                            break;
                        }
                    case 34:
                        {
                            HdnVert34Ckd.Value = "1";
                            break;
                        }
                    case 35:
                        {
                            HdnVert35Ckd.Value = "1";
                            break;
                        }
                    case 36:
                        {
                            HdnVert36Ckd.Value = "1";
                            break;
                        }
                    case 37:
                        {
                            HdnVert37Ckd.Value = "1";
                            break;
                        }
                    case 38:
                        {
                            HdnVert38Ckd.Value = "1";
                            break;
                        }
                    case 39:
                        {
                            HdnVert39Ckd.Value = "1";
                            break;
                        }
                    case 40:
                        {
                            HdnVert40Ckd.Value = "1";
                            break;
                        }
                    case 41:
                        {
                            HdnVert41Ckd.Value = "1";
                            break;
                        }
                    case 42:
                        {
                            HdnVert42Ckd.Value = "1";
                            break;
                        }
                    case 43:
                        {
                            HdnVert43Ckd.Value = "1";
                            break;
                        }
                    case 44:
                        {
                            HdnVert44Ckd.Value = "1";
                            break;
                        }
                    case 45:
                        {
                            HdnVert45Ckd.Value = "1";
                            break;
                        }
                    case 46:
                        {
                            HdnVert46Ckd.Value = "1";
                            break;
                        }
                    case 47:
                        {
                            HdnVert47Ckd.Value = "1";
                            break;
                        }
                    case 48:
                        {
                            HdnVert48Ckd.Value = "1";
                            break;
                        }
                    case 49:
                        {
                            HdnVert49Ckd.Value = "1";
                            break;
                        }
                    case 50:
                        {
                            HdnVert50Ckd.Value = "1";
                            break;
                        }
                    case 51:
                        {
                            HdnVert51Ckd.Value = "1";
                            break;
                        }
                    case 52:
                        {
                            HdnVert52Ckd.Value = "1";
                            break;
                        }
                    case 53:
                        {
                            HdnVert53Ckd.Value = "1";
                            break;
                        }
                    case 54:
                        {
                            HdnVert54Ckd.Value = "1";
                            break;
                        }
                    case 55:
                        {
                            HdnVert55Ckd.Value = "1";
                            break;
                        }
                    case 56:
                        {
                            HdnVert56Ckd.Value = "1";
                            break;
                        }
                    case 57:
                        {
                            HdnVert57Ckd.Value = "1";
                            break;
                        }
                    case 58:
                        {
                            HdnVert58Ckd.Value = "1";
                            break;
                        }
                    case 59:
                        {
                            HdnVert59Ckd.Value = "1";
                            break;
                        }
                    case 60:
                        {
                            HdnVert60Ckd.Value = "1";
                            break;
                        }
                    case 61:
                        {
                            HdnVert61Ckd.Value = "1";
                            break;
                        }
                    case 62:
                        {
                            HdnVert62Ckd.Value = "1";
                            break;
                        }
                    case 63:
                        {
                            HdnVert63Ckd.Value = "1";
                            break;
                        }
                    case 64:
                        {
                            HdnVert64Ckd.Value = "1";
                            break;
                        }
                    case 65:
                        {
                            HdnVert65Ckd.Value = "1";
                            break;
                        }
                    case 66:
                        {
                            HdnVert66Ckd.Value = "1";
                            break;
                        }
                    case 67:
                        {
                            HdnVert67Ckd.Value = "1";
                            break;
                        }
                    case 68:
                        {
                            HdnVert68Ckd.Value = "1";
                            break;
                        }
                    case 69:
                        {
                            HdnVert69Ckd.Value = "1";
                            break;
                        }
                    case 70:
                        {
                            HdnVert70Ckd.Value = "1";
                            break;
                        }
                    case 71:
                        {
                            HdnVert71Ckd.Value = "1";
                            break;
                        }
                    case 72:
                        {
                            HdnVert72Ckd.Value = "1";
                            break;
                        }
                    case 73:
                        {
                            HdnVert73Ckd.Value = "1";
                            break;
                        }
                    case 74:
                        {
                            HdnVert74Ckd.Value = "1";
                            break;
                        }
                    case 75:
                        {
                            HdnVert75Ckd.Value = "1";
                            break;
                        }
                    case 76:
                        {
                            HdnVert76Ckd.Value = "1";
                            break;
                        }
                    case 77:
                        {
                            HdnVert77Ckd.Value = "1";
                            break;
                        }
                    case 78:
                        {
                            HdnVert78Ckd.Value = "1";
                            break;
                        }
                    case 79:
                        {
                            HdnVert79Ckd.Value = "1";
                            break;
                        }
                    case 80:
                        {
                            HdnVert80Ckd.Value = "1";
                            break;
                        }
                    case 81:
                        {
                            HdnVert81Ckd.Value = "1";
                            break;
                        }
                    case 82:
                        {
                            HdnVert82Ckd.Value = "1";
                            break;
                        }
                    case 83:
                        {
                            HdnVert83Ckd.Value = "1";
                            break;
                        }
                    case 84:
                        {
                            HdnVert84Ckd.Value = "1";
                            break;
                        }
                    case 85:
                        {
                            HdnVert85Ckd.Value = "1";
                            break;
                        }
                    case 86:
                        {
                            HdnVert86Ckd.Value = "1";
                            break;
                        }
                    case 87:
                        {
                            HdnVert87Ckd.Value = "1";
                            break;
                        }
                    case 88:
                        {
                            HdnVert88Ckd.Value = "1";
                            break;
                        }
                    case 89:
                        {
                            HdnVert89Ckd.Value = "1";
                            break;
                        }
                    case 90:
                        {
                            HdnVert90Ckd.Value = "1";
                            break;
                        }
                    case 91:
                        {
                            HdnVert91Ckd.Value = "1";
                            break;
                        }
                    case 92:
                        {
                            HdnVert92Ckd.Value = "1";
                            break;
                        }
                    case 93:
                        {
                            HdnVert93Ckd.Value = "1";
                            break;
                        }
                    case 94:
                        {
                            HdnVert94Ckd.Value = "1";
                            break;
                        }
                    case 95:
                        {
                            HdnVert95Ckd.Value = "1";
                            break;
                        }
                    case 96:
                        {
                            HdnVert96Ckd.Value = "1";
                            break;
                        }
                    case 97:
                        {
                            HdnVert97Ckd.Value = "1";
                            break;
                        }
                    case 98:
                        {
                            HdnVert98Ckd.Value = "1";
                            break;
                        }
                    case 99:
                        {
                            HdnVert99Ckd.Value = "1";
                            break;
                        }
                    case 100:
                        {
                            HdnVert100Ckd.Value = "1";
                            break;
                        }
                    case 101:
                        {
                            HdnVert101Ckd.Value = "1";
                            break;
                        }
                    case 102:
                        {
                            HdnVert102Ckd.Value = "1";
                            break;
                        }
                    case 103:
                        {
                            HdnVert103Ckd.Value = "1";
                            break;
                        }
                    case 104:
                        {
                            HdnVert104Ckd.Value = "1";
                            break;
                        }
                    case 105:
                        {
                            HdnVert105Ckd.Value = "1";
                            break;
                        }
                    case 106:
                        {
                            HdnVert106Ckd.Value = "1";
                            break;
                        }
                    case 107:
                        {
                            HdnVert107Ckd.Value = "1";
                            break;
                        }
                    case 108:
                        {
                            HdnVert108Ckd.Value = "1";
                            break;
                        }
                    case 109:
                        {
                            HdnVert109Ckd.Value = "1";
                            break;
                        }
                    case 110:
                        {
                            HdnVert110Ckd.Value = "1";
                            break;
                        }
                    case 111:
                        {
                            HdnVert111Ckd.Value = "1";
                            break;
                        }
                    case 112:
                        {
                            HdnVert112Ckd.Value = "1";
                            break;
                        }
                    case 113:
                        {
                            HdnVert113Ckd.Value = "1";
                            break;
                        }
                    case 114:
                        {
                            HdnVert114Ckd.Value = "1";
                            break;
                        }
                    case 115:
                        {
                            HdnVert115Ckd.Value = "1";
                            break;
                        }
                    case 116:
                        {
                            HdnVert116Ckd.Value = "1";
                            break;
                        }
                    case 117:
                        {
                            HdnVert117Ckd.Value = "1";
                            break;
                        }
                    case 118:
                        {
                            HdnVert118Ckd.Value = "1";
                            break;
                        }
                    case 119:
                        {
                            HdnVert119Ckd.Value = "1";
                            break;
                        }
                    case 120:
                        {
                            HdnVert120Ckd.Value = "1";
                            break;
                        }
                    case 121:
                        {
                            HdnVert121Ckd.Value = "1";
                            break;
                        }
                    case 122:
                        {
                            HdnVert122Ckd.Value = "1";
                            break;
                        }
                    case 123:
                        {
                            HdnVert123Ckd.Value = "1";
                            break;
                        }
                    case 124:
                        {
                            HdnVert124Ckd.Value = "1";
                            break;
                        }
                    case 125:
                        {
                            HdnVert125Ckd.Value = "1";
                            break;
                        }
                }
            }

            # endregion

            ScriptManager.RegisterStartupScript(this, GetType(), "SetCheckedVertivals", "SetCheckedVertivals();", true);
        }

        private void LoadSupport()
        {
            List<ElioUsersCriteria> supportCriteria = Sql.GetUserCriteriaByCritId(vSession.User.Id, 14, session);

            foreach (ElioUsersCriteria sup in supportCriteria)
            {
                if (sup.CriteriaValue == "Indifferent")
                {
                    HdnSuppIndifCkd.Value = "1";
                }

                if (sup.CriteriaValue == "Dedicated")
                {
                    HdnSuppDedicCkd.Value = "1";
                }

                if (sup.CriteriaValue == "Phone")
                {
                    HdnSuppPhoneCkd.Value = "1";
                }

                if (sup.CriteriaValue == "Mail")
                {
                    HdnSuppMailCkd.Value = "1";
                }
            }

            LblSupport.Text = "Support";
            LblSupport1.Text = "Indifferent";
            LblSupport2.Text = "Dedicated";
            LblSupport3.Text = "Phone";
            LblSupport4.Text = "Mail";
            LblSupportHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "What type of support will you offer to your partners?" : "What type of support you would like to have from your vendor?";
            lblS1.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;
            ScriptManager.RegisterStartupScript(this, GetType(), "SetCheckedSupport", "SetCheckedSupport();", true);
        }

        private void LoadRevenue()
        {
            ElioUsersCriteria criteriaRevenue = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 13, session);

            if (criteriaRevenue != null)
            {
                if (criteriaRevenue.CriteriaValue == "Commission")
                {
                    optRevenue3.Attributes.Add("selected", "true");
                }
                else if (criteriaRevenue.CriteriaValue == "Annual fee")
                {
                    optRevenue4.Attributes.Add("selected", "true");
                }
                else if (criteriaRevenue.CriteriaValue == "Indifferent")
                {
                    optRevenue2.Attributes.Add("selected", "true");
                }
            }

            LblOptRevue1.Text = "Select one";
            LblOptRevue2.Text = "Indifferent";
            LblOptRevue3.Text = "Commission";
            LblOptRevue4.Text = "Annual fee";
            LblRevenue.Text = "Revenue model";
            LblRevenueHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "What type of revenue model are you using?" : "What type of revenue model are you interested in?";
            optRevenue2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;
        }

        private void LoadFee()
        {
            ElioUsersCriteria criteriaFee = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 12, session);

            if (criteriaFee != null)
            {
                if (criteriaFee.CriteriaValue == "Yes")
                {
                    optFee3.Attributes.Add("selected", "true");
                }
                else if (criteriaFee.CriteriaValue == "No")
                {
                    optFee4.Attributes.Add("selected", "true");
                }
                else if (criteriaFee.CriteriaValue == "Indifferent")
                {
                    optFee2.Attributes.Add("selected", "true");
                }
            }

            LblOptFee1.Text = "Select one";
            LblOptFee2.Text = "Indifferent";
            LblOptFee3.Text = "Yes";
            LblOptFee4.Text = "No";
            LblSetUpFee.Text = "Set up fee";
            LblSetUpFeeHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you require a set up fee payment from your partners?" : "Are you willing to pay a set up fee to your ideal partner?";
            optFee2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;
        }

        private void LoadOptionals()
        {
            # region company maturity

            ElioUsersCriteria criteriaCompMat = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 1, session);

            if (criteriaCompMat != null)
            {
                if (criteriaCompMat.CriteriaValue == "1 - 5")
                {
                    optCompMat3.Attributes.Add("selected", "true");
                }
                else if (criteriaCompMat.CriteriaValue == "6 +")
                {
                    optCompMat4.Attributes.Add("selected", "true");
                }
                else if (criteriaCompMat.CriteriaValue == "Indifferent")
                {
                    optCompMat2.Attributes.Add("selected", "true");
                }
            }

            LblCompMaturity.Text = "Company maturity (years)";
            LblCompMat1.Text = "Select one";
            LblCompMat2.Text = "Indifferent";
            LblCompMat3.Text = "1 - 5";
            LblCompMat4.Text = "6 +";
            LblCompMatHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "How long is your company in operation?" : "How long your partner company should be in operation?";
            optCompMat2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region program maturity

            ElioUsersCriteria criteriaProgMat = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 2, session);

            if (criteriaProgMat != null)
            {
                if (criteriaProgMat.CriteriaValue == "1 - 3")
                {
                    optProgMat3.Attributes.Add("selected", "true");
                }
                else if (criteriaProgMat.CriteriaValue == "4 +")
                {
                    optProgMat4.Attributes.Add("selected", "true");
                }
                else if (criteriaProgMat.CriteriaValue == "Indifferent")
                {
                    optProgMat2.Attributes.Add("selected", "true");
                }
            }

            LblProgMaturity.Text = "Partner program maturity (years)";
            LblProgMat1.Text = "Select one";
            LblProgMat2.Text = "Indifferent";
            LblProgMat3.Text = "1 - 3";
            LblProgMat4.Text = "4 +";
            LblProgMatHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "How long have you been offering your partner program?" : "How experienced should your vendor be offering the partner program?";
            optProgMat2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region partners number

            ElioUsersCriteria criteriaNumPartn = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 3, session);

            if (criteriaNumPartn != null)
            {
                if (criteriaNumPartn.CriteriaValue == "< 50")
                {
                    optNumPartn3.Attributes.Add("selected", "true");
                }
                else if (criteriaNumPartn.CriteriaValue == "50 - 100")
                {
                    optNumPartn4.Attributes.Add("selected", "true");
                }
                else if (criteriaNumPartn.CriteriaValue == "101 +")
                {
                    optNumPartn5.Attributes.Add("selected", "true");
                }
                else if (criteriaNumPartn.CriteriaValue == "Indifferent")
                {
                    optNumPartn2.Attributes.Add("selected", "true");
                }
            }

            LblNumPartners.Text = "Number of partners";
            LblNumPartn1.Text = "Select one";
            LblNumPartn2.Text = "Indifferent";
            LblNumPartn3.Text = "< 50";
            LblNumPartn4.Text = "50 - 100";
            LblNumPartn5.Text = "101 +";
            LblNumPartnHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "How many partners do you currently have?" : "How many partners should your ideal vendor already have?";
            optNumPartn2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region program tiers

            ElioUsersCriteria criteriaProgTier = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 4, session);

            if (criteriaProgTier != null)
            {
                if (criteriaProgTier.CriteriaValue == "1")
                {
                    optProgTier3.Attributes.Add("selected", "true");
                }
                else if (criteriaProgTier.CriteriaValue == "2")
                {
                    optProgTier4.Attributes.Add("selected", "true");
                }
                else if (criteriaProgTier.CriteriaValue == "3 +")
                {
                    optProgTier5.Attributes.Add("selected", "true");
                }
                else if (criteriaProgTier.CriteriaValue == "Indifferent")
                {
                    optProgTier2.Attributes.Add("selected", "true");
                }
            }

            LblProgTiers.Text = "Partner program tiers";
            LblProgTier1.Text = "Select one";
            LblProgTier2.Text = "Indifferent";
            LblProgTier3.Text = "1";
            LblProgTier4.Text = "2";
            LblProgTier5.Text = "3 +";
            LblProgTiersHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "How many tiers you offer?" : "How many partnership tiers should your ideal vendor offer?";
            optProgTier2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region training

            ElioUsersCriteria criteriaTrain = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 5, session);

            if (criteriaTrain != null)
            {
                if (criteriaTrain.CriteriaValue == "Yes")
                {
                    optTrain3.Attributes.Add("selected", "true");
                }
                else if (criteriaTrain.CriteriaValue == "No")
                {
                    optTrain4.Attributes.Add("selected", "true");
                }
                else if (criteriaTrain.CriteriaValue == "Indifferent")
                {
                    optTrain2.Attributes.Add("selected", "true");
                }
            }

            LblTraining.Text = "Training";
            LblTrain1.Text = "Select one";
            LblTrain2.Text = "Indifferent";
            LblTrain3.Text = "Yes";
            LblTrain4.Text = "No";
            LblTrainingHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you offer training for your new partners?" : "Do you require training from your vendor?";
            optTrain2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region free training

            ElioUsersCriteria criteriaFTrain = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 6, session);

            if (criteriaFTrain != null)
            {
                if (criteriaFTrain.CriteriaValue == "Yes")
                {
                    optFTrain3.Attributes.Add("selected", "true");
                }
                else if (criteriaFTrain.CriteriaValue == "No")
                {
                    optFTrain4.Attributes.Add("selected", "true");
                }
                else if (criteriaFTrain.CriteriaValue == "Indifferent")
                {
                    optFTrain2.Attributes.Add("selected", "true");
                }
            }

            LblFreeTraining.Text = "Free training";
            LblFTrain1.Text = "Select one";
            LblFTrain2.Text = "Indifferent";
            LblFTrain3.Text = "Yes";
            LblFTrain4.Text = "No";
            LblFreeTrainingHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you offer free or paid training?" : "Would you be willing to pay for training or not?";
            optFTrain2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region marketing material

            ElioUsersCriteria criteriaMarkMat = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 7, session);

            if (criteriaMarkMat != null)
            {
                if (criteriaMarkMat.CriteriaValue == "Yes")
                {
                    optMarkMat3.Attributes.Add("selected", "true");
                }
                else if (criteriaMarkMat.CriteriaValue == "No")
                {
                    optMarkMat4.Attributes.Add("selected", "true");
                }
                else if (criteriaMarkMat.CriteriaValue == "Indifferent")
                {
                    optMarkMat2.Attributes.Add("selected", "true");
                }
            }

            LblMarkMaterial.Text = "Marketing material";
            LblMarkMat1.Text = "Select one";
            LblMarkMat2.Text = "Indifferent";
            LblMarkMat3.Text = "Yes";
            LblMarkMat4.Text = "No";
            LblMarkMaterialHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you distribute marketing material to your partners?" : "Is the marketing material distributed from your vendor important to you?";
            optMarkMat2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region certification

            ElioUsersCriteria criteriaCertReq = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 8, session);

            if (criteriaCertReq != null)
            {
                if (criteriaCertReq.CriteriaValue == "Yes")
                {
                    optCertReq3.Attributes.Add("selected", "true");
                }
                else if (criteriaCertReq.CriteriaValue == "No")
                {
                    optCertReq4.Attributes.Add("selected", "true");
                }
                else if (criteriaCertReq.CriteriaValue == "Indifferent")
                {
                    optCertReq2.Attributes.Add("selected", "true");
                }
            }

            LblCertifRequired.Text = "Certification required";
            LblCertReq1.Text = "Select one";
            LblCertReq2.Text = "Indifferent";
            LblCertReq3.Text = "Yes";
            LblCertReq4.Text = "No";
            LblCertifRequiredHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do your new partners need certification?" : "Are you willing to complete a certification program if you find an ideal product?";
            optCertReq2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region localisation

            ElioUsersCriteria criteriaLocal = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 9, session);

            if (criteriaLocal != null)
            {
                if (criteriaLocal.CriteriaValue == "Yes")
                {
                    optLocal3.Attributes.Add("selected", "true");
                }
                else if (criteriaLocal.CriteriaValue == "No")
                {
                    optLocal4.Attributes.Add("selected", "true");
                }
                else if (criteriaLocal.CriteriaValue == "Indifferent")
                {
                    optLocal2.Attributes.Add("selected", "true");
                }
            }

            LblLocalization.Text = "Localization";
            LblLocal1.Text = "Select one";
            LblLocal2.Text = "Indifferent";
            LblLocal3.Text = "Yes";
            LblLocal4.Text = "No";
            LblLocalizationHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you offer localized version of your product to your target markets?" : "Do you need a localized version of the product?";
            optLocal2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region Mdf

            ElioUsersCriteria criteriaMdf = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 10, session);

            if (criteriaMdf != null)
            {
                if (criteriaMdf.CriteriaValue == "Yes")
                {
                    optMDF3.Attributes.Add("selected", "true");
                }
                else if (criteriaMdf.CriteriaValue == "No")
                {
                    optMDF4.Attributes.Add("selected", "true");
                }
                else if (criteriaMdf.CriteriaValue == "Indifferent")
                {
                    optMDF2.Attributes.Add("selected", "true");
                }
            }

            LblMDF.Text = "Marketing development funds";
            LblMDF1.Text = "Select one";
            LblMDF2.Text = "Indifferent";
            LblMDF3.Text = "Yes";
            LblMDF4.Text = "No";
            LblMDFHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you offer MDFs to your partners?" : "Do you require  MDFs from your partners?";
            optMDF2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region portal

            ElioUsersCriteria criteriaPortal = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 11, session);

            if (criteriaPortal != null)
            {
                if (criteriaPortal.CriteriaValue == "Yes")
                {
                    optPortal3.Attributes.Add("selected", "true");
                }
                else if (criteriaPortal.CriteriaValue == "No")
                {
                    optPortal4.Attributes.Add("selected", "true");
                }
                else if (criteriaPortal.CriteriaValue == "Indifferent")
                {
                    optPortal2.Attributes.Add("selected", "true");
                }
            }

            LblPortal.Text = "Partner portal/resources";
            LblPortal1.Text = "Select one";
            LblPortal2.Text = "Indifferent";
            LblPortal3.Text = "Yes";
            LblPortal4.Text = "No";
            LblPortalHelp.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Do you have in place a partner portal or any other type of resources for your partners?" : "Is a partner portal or any other type of resources important for you?";
            optPortal2.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? false : true;

            # endregion

            # region countries

            divCountries.Visible = vSession.User.CompanyType == Types.Vendors.ToString() ? true : false;

            if (divCountries.Visible)
            {
                LblCountries.Text = "Country / Region";
                LblCountriesHelp.Text = "Select the region or country where you'd like to find resellers! If nothing selected, 'Select All' will be saved.";

                LblCntr1.Text = "Select All";
                LblCntr2.Text = "Asia-Pacific";
                LblCntr3.Text = "Africa";
                LblCntr4.Text = "Europe";
                LblCntr5.Text = "Middle East";
                LblCntr6.Text = "North America";
                LblCntr7.Text = "South America";
                LblCntr8.Text = "Argentina";
                LblCntr9.Text = "Australia";
                LblCntr10.Text = "Brazil";
                LblCntr11.Text = "Canada";
                LblCntr12.Text = "France";
                LblCntr13.Text = "Germany";
                LblCntr14.Text = "India";
                LblCntr15.Text = "Mexico";
                LblCntr16.Text = "Pakistan";
                LblCntr17.Text = "Spain";
                LblCntr18.Text = "United Kingdom";
                LblCntr19.Text = "United States";

                List<ElioUsersCriteria> countriesCriteria = Sql.GetUserCriteriaByCritId(vSession.User.Id, 15, session);

                foreach (ElioUsersCriteria country in countriesCriteria)
                {
                    if (country.CriteriaValue == "Select All")
                    {
                        HdnCountriesSelAllCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Asia-Pacific")
                    {
                        HdnCountriesAsiaPacifCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Africa")
                    {
                        HdnCountriesAfricaCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Europe")
                    {
                        HdnCountriesEuropeCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Middle East")
                    {
                        HdnCountriesMidEastCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "North America")
                    {
                        HdnCountriesNortAmerCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "South America")
                    {
                        HdnCountriesSoutAmerCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Argentina")
                    {
                        HdnCountriesArgenCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Australia")
                    {
                        HdnCountriesAustrCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Brazil")
                    {
                        HdnCountriesBrazCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Canada")
                    {
                        HdnCountriesCanadaCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "France")
                    {
                        HdnCountriesFranceCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Germany")
                    {
                        HdnCountriesGermanyCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "India")
                    {
                        HdnCountriesIndiaCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Mexico")
                    {
                        HdnCountriesMexicoCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Pakistan")
                    {
                        HdnCountriesPakistanCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "Spain")
                    {
                        HdnCountriesSpainCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "United Kingdom")
                    {
                        HdnCountriesUnKingCkd.Value = "1";
                    }
                    else if (country.CriteriaValue == "United States")
                    {
                        HdnCountriesUnStatCkd.Value = "1";
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "SetCheckedCountries", "SetCheckedCountries();", true);
            }

            # endregion
        }

        private void SaveCriteria(int userId, int criteriaId, string criteriaValue)
        {
            DataLoader<ElioUsersCriteria> criteriaLoader = new DataLoader<ElioUsersCriteria>(session);

            ElioUsersCriteria newCriteriaValue = new ElioUsersCriteria();

            ElioUsersCriteria existingCriteriaValue = Sql.GetUserCriteriaByCriteriaId(userId, criteriaId, session);

            if (existingCriteriaValue == null)
            {
                if (!string.IsNullOrEmpty(criteriaValue) && criteriaValue != "Select one")
                {
                    newCriteriaValue.UserId = userId;
                    newCriteriaValue.CriteriaId = criteriaId;
                    newCriteriaValue.CriteriaValue = criteriaValue;
                    newCriteriaValue.SysDate = DateTime.Now;
                    newCriteriaValue.LastUpdated = DateTime.Now;

                    criteriaLoader.Insert(newCriteriaValue);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(criteriaValue))
                {
                    existingCriteriaValue.LastUpdated = DateTime.Now;

                    criteriaLoader.Update(existingCriteriaValue);
                }
                else if (criteriaValue == "Select one")
                {
                    Sql.DeleteUserCriteriaValue(userId, criteriaId, existingCriteriaValue.CriteriaValue, session);
                }
                else
                {
                    existingCriteriaValue.CriteriaValue = criteriaValue;
                    existingCriteriaValue.SysDate = DateTime.Now;
                    existingCriteriaValue.LastUpdated = DateTime.Now;

                    criteriaLoader.Update(existingCriteriaValue);
                }
            }
        }

        # endregion

        # region Buttons

        protected void BtnSubmit_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    bool csvReadyToUp = false;
                    bool pdfReadyToUp = false;
                    bool verticalsReadyToSave = false;
                    bool feeReadyToSave = false;
                    bool supportReadyToSave = false;
                    bool revenueReadyToSave = false;

                    session.OpenConnection();

                    divFilesError.Visible = false;

                    # region check files

                    # region check pdf file

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        var pdfFileContent = pdfFile.PostedFile;

                        if (pdfFileContent != null)
                        {
                            var pdfFileSize = pdfFileContent.ContentLength;
                            var pdfFileType = pdfFileContent.ContentType;

                            int pdfMaxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPdfSize"]);

                            if (pdfFileType != "application/octet-stream")
                            {
                                if (pdfFileType == "application/pdf")
                                {
                                    if (pdfFileSize < pdfMaxSize)
                                    {
                                        pdfReadyToUp = true;
                                    }
                                    else
                                    {
                                        LblFilesError.Text = "File size error! ";
                                        LblFilesErrorContent.Text = "The '.pdf' file size is " + pdfFileSize + " Bytes and must be less than " + pdfMaxSize + " Bytes.";
                                        divFilesError.Visible = true;
                                        divFilesError.Focus();
                                        LoadVerticals();
                                        LoadFee();
                                        LoadRevenue();
                                        LoadSupport();
                                        LoadOptionals();
                                        return;
                                    }
                                }
                                else
                                {
                                    LblFilesError.Text = "Wrong file type! ";
                                    LblFilesErrorContent.Text = "The 'Pdf' input selection must contain a '.pdf' file.";
                                    divFilesError.Visible = true;
                                    divFilesError.Focus();
                                    LoadVerticals();
                                    LoadFee();
                                    LoadRevenue();
                                    LoadSupport();
                                    LoadOptionals();
                                    return;
                                }
                            }
                        }
                    }

                    # endregion

                    # region check csv file

                    var csvFileContent = csvFile.PostedFile;

                    if (csvFileContent != null)
                    {
                        var csvFileSize = csvFileContent.ContentLength;
                        var csvFileType = csvFileContent.ContentType;
                        int csvMaxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxCsvSize"]);

                        if (csvFileType != "application/octet-stream")
                        {
                            if (csvFileType == "application/csv" || csvFileType == "application/vnd.ms-excel")
                            {
                                if (csvFileSize < csvMaxSize)
                                {
                                    csvReadyToUp = true;
                                }
                                else
                                {
                                    LblFilesError.Text = "File size error! ";
                                    LblFilesErrorContent.Text = "The '.csv' file size is " + csvFileSize + " Bytes and must be less than " + csvMaxSize + " Bytes.";
                                    divFilesError.Visible = true;
                                    divFilesError.Focus();
                                    LoadVerticals();
                                    LoadFee();
                                    LoadRevenue();
                                    LoadSupport();
                                    LoadOptionals();
                                    return;
                                }
                            }
                            else
                            {
                                LblFilesError.Text = "Wrong file type! ";
                                LblFilesErrorContent.Text = "The 'Csv' input selection must contain a '.csv' file.";
                                divFilesError.Visible = true;
                                divFilesError.Focus();
                                LoadVerticals();
                                LoadFee();
                                LoadRevenue();
                                LoadSupport();
                                LoadOptionals();
                                return;
                            }
                        }
                    }

                    # endregion

                    # endregion

                    # region check verticals

                    List<HiddenField> selectedVerticals = new List<HiddenField>();

                    selectedVerticals.Add(HdnVert1);
                    selectedVerticals.Add(HdnVert2);
                    selectedVerticals.Add(HdnVert3);
                    selectedVerticals.Add(HdnVert4);
                    selectedVerticals.Add(HdnVert5);
                    selectedVerticals.Add(HdnVert6);
                    selectedVerticals.Add(HdnVert7);
                    selectedVerticals.Add(HdnVert8);
                    selectedVerticals.Add(HdnVert9);
                    selectedVerticals.Add(HdnVert10);
                    selectedVerticals.Add(HdnVert11);
                    selectedVerticals.Add(HdnVert12);
                    selectedVerticals.Add(HdnVert13);
                    selectedVerticals.Add(HdnVert14);
                    selectedVerticals.Add(HdnVert15);
                    selectedVerticals.Add(HdnVert16);
                    selectedVerticals.Add(HdnVert17);
                    selectedVerticals.Add(HdnVert18);
                    selectedVerticals.Add(HdnVert19);
                    selectedVerticals.Add(HdnVert20);
                    selectedVerticals.Add(HdnVert21);
                    selectedVerticals.Add(HdnVert22);
                    selectedVerticals.Add(HdnVert23);
                    selectedVerticals.Add(HdnVert24);
                    selectedVerticals.Add(HdnVert25);
                    selectedVerticals.Add(HdnVert26);
                    selectedVerticals.Add(HdnVert27);
                    selectedVerticals.Add(HdnVert28);
                    selectedVerticals.Add(HdnVert29);
                    selectedVerticals.Add(HdnVert30);
                    selectedVerticals.Add(HdnVert31);
                    selectedVerticals.Add(HdnVert32);
                    selectedVerticals.Add(HdnVert33);
                    selectedVerticals.Add(HdnVert34);
                    selectedVerticals.Add(HdnVert35);
                    selectedVerticals.Add(HdnVert36);
                    selectedVerticals.Add(HdnVert37);
                    selectedVerticals.Add(HdnVert38);
                    selectedVerticals.Add(HdnVert39);
                    selectedVerticals.Add(HdnVert40);
                    selectedVerticals.Add(HdnVert41);
                    selectedVerticals.Add(HdnVert42);
                    selectedVerticals.Add(HdnVert43);
                    selectedVerticals.Add(HdnVert44);
                    selectedVerticals.Add(HdnVert45);
                    selectedVerticals.Add(HdnVert46);
                    selectedVerticals.Add(HdnVert47);
                    selectedVerticals.Add(HdnVert48);
                    selectedVerticals.Add(HdnVert49);
                    selectedVerticals.Add(HdnVert50);
                    selectedVerticals.Add(HdnVert51);
                    selectedVerticals.Add(HdnVert52);
                    selectedVerticals.Add(HdnVert53);
                    selectedVerticals.Add(HdnVert54);
                    selectedVerticals.Add(HdnVert55);
                    selectedVerticals.Add(HdnVert56);
                    selectedVerticals.Add(HdnVert57);
                    selectedVerticals.Add(HdnVert58);
                    selectedVerticals.Add(HdnVert59);
                    selectedVerticals.Add(HdnVert60);
                    selectedVerticals.Add(HdnVert61);
                    selectedVerticals.Add(HdnVert62);
                    selectedVerticals.Add(HdnVert63);
                    selectedVerticals.Add(HdnVert64);
                    selectedVerticals.Add(HdnVert65);
                    selectedVerticals.Add(HdnVert66);
                    selectedVerticals.Add(HdnVert67);
                    selectedVerticals.Add(HdnVert68);
                    selectedVerticals.Add(HdnVert69);
                    selectedVerticals.Add(HdnVert70);
                    selectedVerticals.Add(HdnVert71);
                    selectedVerticals.Add(HdnVert72);
                    selectedVerticals.Add(HdnVert73);
                    selectedVerticals.Add(HdnVert74);
                    selectedVerticals.Add(HdnVert75);
                    selectedVerticals.Add(HdnVert76);
                    selectedVerticals.Add(HdnVert77);
                    selectedVerticals.Add(HdnVert78);
                    selectedVerticals.Add(HdnVert79);
                    selectedVerticals.Add(HdnVert80);
                    selectedVerticals.Add(HdnVert81);
                    selectedVerticals.Add(HdnVert82);
                    selectedVerticals.Add(HdnVert83);
                    selectedVerticals.Add(HdnVert84);
                    selectedVerticals.Add(HdnVert85);
                    selectedVerticals.Add(HdnVert86);
                    selectedVerticals.Add(HdnVert87);
                    selectedVerticals.Add(HdnVert88);
                    selectedVerticals.Add(HdnVert89);
                    selectedVerticals.Add(HdnVert90);
                    selectedVerticals.Add(HdnVert91);
                    selectedVerticals.Add(HdnVert92);
                    selectedVerticals.Add(HdnVert93);
                    selectedVerticals.Add(HdnVert94);
                    selectedVerticals.Add(HdnVert95);
                    selectedVerticals.Add(HdnVert96);
                    selectedVerticals.Add(HdnVert97);
                    selectedVerticals.Add(HdnVert98);   
                    selectedVerticals.Add(HdnVert99);
                    selectedVerticals.Add(HdnVert100);
                    selectedVerticals.Add(HdnVert101);
                    selectedVerticals.Add(HdnVert102);
                    selectedVerticals.Add(HdnVert103);
                    selectedVerticals.Add(HdnVert104);
                    selectedVerticals.Add(HdnVert105);
                    selectedVerticals.Add(HdnVert106);
                    selectedVerticals.Add(HdnVert107);
                    selectedVerticals.Add(HdnVert108);
                    selectedVerticals.Add(HdnVert109);
                    selectedVerticals.Add(HdnVert110);
                    selectedVerticals.Add(HdnVert111);
                    selectedVerticals.Add(HdnVert112);
                    selectedVerticals.Add(HdnVert113);
                    selectedVerticals.Add(HdnVert114);
                    selectedVerticals.Add(HdnVert115);
                    selectedVerticals.Add(HdnVert116);
                    selectedVerticals.Add(HdnVert117);
                    selectedVerticals.Add(HdnVert118);
                    selectedVerticals.Add(HdnVert119);
                    selectedVerticals.Add(HdnVert120);
                    selectedVerticals.Add(HdnVert121);
                    selectedVerticals.Add(HdnVert122);
                    selectedVerticals.Add(HdnVert123);
                    selectedVerticals.Add(HdnVert124);
                    selectedVerticals.Add(HdnVert125);

                    int vertCounter = 0;
                    foreach (HiddenField vert in selectedVerticals)
                    {
                        if (vert.Value != "0")
                        {
                            vertCounter++;
                        }
                    }

                    if (vertCounter <= 0)
                    {
                        LblFilesError.Text = "Error! ";
                        LblFilesErrorContent.Text = "You have to select at least one vertical.";
                        divFilesError.Visible = true;
                        divFilesError.Focus();
                        LoadVerticals();
                        LoadFee();
                        LoadRevenue();
                        LoadSupport();
                        LoadOptionals();
                        verticalsReadyToSave = false;
                        return;
                    }
                    else
                    {
                        verticalsReadyToSave = true;
                    }

                    # endregion

                    # region check support

                    List<HiddenField> selectedSupport = new List<HiddenField>();

                    selectedSupport.Add(HdnSuppIndif);
                    selectedSupport.Add(HdnSuppDedic);
                    selectedSupport.Add(HdnSuppPhone);
                    selectedSupport.Add(HdnSuppMail);

                    int counterSupport = 0;
                    foreach (HiddenField sup in selectedSupport)
                    {
                        if (sup.Value != "0")
                        {
                            counterSupport++;
                        }
                    }

                    if (counterSupport <= 0)
                    {
                        LblFilesError.Text = "Error! ";
                        LblFilesErrorContent.Text = "You have to select at least one support method.";
                        divFilesError.Visible = true;
                        divFilesError.Focus();
                        LoadVerticals();
                        LoadFee();
                        LoadRevenue();
                        LoadSupport();
                        LoadOptionals();
                        supportReadyToSave = false;
                        return;
                    }
                    else
                    {
                        supportReadyToSave = true;
                    }

                    # endregion

                    # region get rest criteria

                    string hdnSetUpFee = Request.Form["HdnSetUpFee"];
                    string hdnRevenue = Request.Form["HdnRevenue"];
                    string hdnTraining = Request.Form["HdnTraining"];
                    string hdnFTraining = Request.Form["HdnFTraining"];
                    string hdnProgMatur = Request.Form["HdnProgMatur"];
                    string hdnCompMatur = Request.Form["HdnCompMatur"];
                    string hdnMarkMat = Request.Form["HdnMarkMat"];
                    string hdnLocaliz = Request.Form["HdnLocaliz"];
                    string hdnMdf = Request.Form["HdnMdf"];
                    string hdnCertif = Request.Form["HdnCertif"];
                    string hdnPortal = Request.Form["HdnPortal"];
                    string hdnPartners = Request.Form["HdnPartners"];
                    string hdnTiers = Request.Form["HdnTiers"];

                    hdnPartners = hdnPartners == "&lt; 50" ? "< 50" : hdnPartners;

                    # endregion

                    # region check fee

                    ElioUsersCriteria existingFee = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 12, session);

                    if (existingFee == null && string.IsNullOrEmpty(hdnSetUpFee))
                    {
                        LblFilesError.Text = "Error! ";
                        LblFilesErrorContent.Text = "You have to select at least one fee method.";
                        divFilesError.Visible = true;
                        divFilesError.Focus();
                        LoadVerticals();
                        LoadFee();
                        LoadRevenue();
                        LoadSupport();
                        LoadOptionals();
                        feeReadyToSave = false;
                        return;
                    }
                    else
                    {
                        feeReadyToSave = true;
                    }

                    # endregion

                    # region check revenue

                    ElioUsersCriteria existingRevenue = Sql.GetUserCriteriaByCriteriaId(vSession.User.Id, 13, session);

                    if (existingRevenue == null && string.IsNullOrEmpty(hdnRevenue))
                    {
                        LblFilesError.Text = "Error! ";
                        LblFilesErrorContent.Text = "You have to select at least one revenue model.";
                        divFilesError.Visible = true;
                        divFilesError.Focus();
                        LoadVerticals();
                        LoadFee();
                        LoadRevenue();
                        LoadSupport();
                        LoadOptionals();
                        revenueReadyToSave = false;
                        return;
                    }
                    else
                    {
                        revenueReadyToSave = true;
                    }

                    # endregion

                    if (verticalsReadyToSave && feeReadyToSave && supportReadyToSave && revenueReadyToSave)
                    {
                        # region save verticals

                        List<ElioUsersAlgorithmSubcategories> userAlgSubcategories = Sql.GetUserAlgorithmSubcategoriesById(vSession.User.Id, session);

                        foreach (ElioUsersAlgorithmSubcategories sub in userAlgSubcategories)
                        {
                            Sql.DeleteUserAlgorithmSubcategory(vSession.User.Id, Convert.ToInt32(sub.SubcategoryId), session);
                        }

                        ElioUsersAlgorithmSubcategories subcategory = new ElioUsersAlgorithmSubcategories();
                        DataLoader<ElioUsersAlgorithmSubcategories> subcategoryLoader = new DataLoader<ElioUsersAlgorithmSubcategories>(session);

                        foreach (HiddenField vert in selectedVerticals)
                        {
                            if (vert.Value != "0")
                            {
                                subcategory.UserId = vSession.User.Id;
                                subcategory.SubcategoryId = Convert.ToInt32(vert.Value);
                                subcategory.SysDate = DateTime.Now;
                                subcategory.LastUpdated = DateTime.Now;

                                subcategoryLoader.Insert(subcategory);
                            }
                        }

                        # endregion

                        # region save support

                        List<ElioUsersCriteria> userSupport = Sql.GetUserCriteriaByCritId(vSession.User.Id, 14, session);

                        foreach (ElioUsersCriteria crit in userSupport)
                        {
                            Sql.DeleteUserCriteriaValue(vSession.User.Id, 14, crit.CriteriaValue, session);
                        }

                        DataLoader<ElioUsersCriteria> criteriaLoader = new DataLoader<ElioUsersCriteria>(session);

                        ElioUsersCriteria newCriteriaValue = new ElioUsersCriteria();

                        foreach (HiddenField sup in selectedSupport)
                        {
                            if (sup.Value != "0")
                            {
                                newCriteriaValue.UserId = vSession.User.Id;
                                newCriteriaValue.CriteriaId = 14;
                                newCriteriaValue.CriteriaValue = sup.Value;
                                newCriteriaValue.SysDate = DateTime.Now;
                                newCriteriaValue.LastUpdated = DateTime.Now;

                                criteriaLoader.Insert(newCriteriaValue);
                            }
                        }

                        # endregion

                        # region save rest criteria

                        if (hdnSetUpFee != null)
                        {
                            SaveCriteria(vSession.User.Id, 12, hdnSetUpFee);
                        }
                        if (hdnRevenue != null)
                        {
                            SaveCriteria(vSession.User.Id, 13, hdnRevenue);
                        }
                        if (hdnTraining != null)
                        {
                            SaveCriteria(vSession.User.Id, 5, hdnTraining);
                        }
                        if (hdnFTraining != null)
                        {
                            SaveCriteria(vSession.User.Id, 6, hdnFTraining);
                        }
                        if (hdnProgMatur != null)
                        {
                            SaveCriteria(vSession.User.Id, 2, hdnProgMatur);
                        }
                        if (hdnCompMatur != null)
                        {
                            SaveCriteria(vSession.User.Id, 1, hdnCompMatur);
                        }
                        if (hdnMarkMat != null)
                        {
                            SaveCriteria(vSession.User.Id, 7, hdnMarkMat);
                        }
                        if (hdnLocaliz != null)
                        {
                            SaveCriteria(vSession.User.Id, 9, hdnLocaliz);
                        }
                        if (hdnMdf != null)
                        {
                            SaveCriteria(vSession.User.Id, 10, hdnMdf);
                        }
                        if (hdnCertif != null)
                        {
                            SaveCriteria(vSession.User.Id, 8, hdnCertif);
                        }
                        if (hdnPortal != null)
                        {
                            SaveCriteria(vSession.User.Id, 11, hdnPortal);
                        }
                        if (hdnPartners != null)
                        {
                            SaveCriteria(vSession.User.Id, 3, hdnPartners);
                        }
                        if (hdnTiers != null)
                        {
                            SaveCriteria(vSession.User.Id, 4, hdnTiers);
                        }

                        # endregion

                        # region save countries

                        List<HiddenField> selectedCountries = new List<HiddenField>();

                        selectedCountries.Add(HdnCountriesSelAll);
                        selectedCountries.Add(HdnCountriesAsiaPacif);
                        selectedCountries.Add(HdnCountriesAfrica);
                        selectedCountries.Add(HdnCountriesEurope);
                        selectedCountries.Add(HdnCountriesMidEast);
                        selectedCountries.Add(HdnCountriesNortAmer);
                        selectedCountries.Add(HdnCountriesSoutAmer);
                        selectedCountries.Add(HdnCountriesArgen);
                        selectedCountries.Add(HdnCountriesAustr);
                        selectedCountries.Add(HdnCountriesBraz);
                        selectedCountries.Add(HdnCountriesCanada);
                        selectedCountries.Add(HdnCountriesFrance);
                        selectedCountries.Add(HdnCountriesGermany);
                        selectedCountries.Add(HdnCountriesIndia);
                        selectedCountries.Add(HdnCountriesMexico);
                        selectedCountries.Add(HdnCountriesPakistan);
                        selectedCountries.Add(HdnCountriesSpain);
                        selectedCountries.Add(HdnCountriesUnKing);
                        selectedCountries.Add(HdnCountriesUnStat);

                        List<ElioUsersCriteria> userCountries = Sql.GetUserCriteriaByCritId(vSession.User.Id, 15, session);

                        foreach (ElioUsersCriteria crit in userCountries)
                        {
                            Sql.DeleteUserCriteriaValue(vSession.User.Id, 15, crit.CriteriaValue, session);
                        }

                        int counterCountries = 0;
                        foreach (HiddenField count in selectedCountries)
                        {
                            if (count.Value != "0")
                            {
                                newCriteriaValue.UserId = vSession.User.Id;
                                newCriteriaValue.CriteriaId = 15;
                                newCriteriaValue.CriteriaValue = count.Value;
                                newCriteriaValue.SysDate = DateTime.Now;
                                newCriteriaValue.LastUpdated = DateTime.Now;

                                criteriaLoader.Insert(newCriteriaValue);

                                counterCountries++;
                            }
                        }

                        if (counterCountries == 0)
                        {
                            newCriteriaValue.UserId = vSession.User.Id;
                            newCriteriaValue.CriteriaId = 15;
                            newCriteriaValue.CriteriaValue = "Select All";
                            newCriteriaValue.SysDate = DateTime.Now;
                            newCriteriaValue.LastUpdated = DateTime.Now;

                            criteriaLoader.Insert(newCriteriaValue);
                        }

                        # endregion

                        # region save csv

                        if (csvReadyToUp)
                        {
                            var csvFileName = csvFileContent.FileName;

                            string csvServerMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CsvTargetFolder"].ToString());
                            string csvTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["CsvTargetFolder"]).ToString();

                            string csvFormattedName = ImageResize.ChangeFileName(csvFileName, ".csv");

                            csvServerMapPathTargetFolder = csvServerMapPathTargetFolder + vSession.User.GuId + "\\";

                            if (!Directory.Exists(csvServerMapPathTargetFolder))
                                Directory.CreateDirectory(csvServerMapPathTargetFolder);

                            DirectoryInfo originalCsvDir = new DirectoryInfo(csvServerMapPathTargetFolder);

                            foreach (FileInfo fi in originalCsvDir.GetFiles())
                            {
                                fi.Delete();
                            }

                            csvFileContent.SaveAs(csvServerMapPathTargetFolder + csvFormattedName);

                            ElioUsersFiles userFile = new ElioUsersFiles();
                            DataLoader<ElioUsersFiles> userFileLoader = new DataLoader<ElioUsersFiles>(session);
                            ElioUsersFiles userCsvFile = Sql.GetUserCsvFile(vSession.User.Id, session);

                            if (userCsvFile == null)
                            {
                                userFile.UserId = vSession.User.Id;
                                userFile.FilePath = csvTargetFolder + vSession.User.GuId + "/" + csvFormattedName;
                                userFile.SysDate = DateTime.Now;
                                userFile.LastUpdated = DateTime.Now;
                                userFile.IsPublic = 1;

                                userFileLoader.Insert(userFile);
                            }
                            else
                            {
                                userCsvFile.FilePath = csvTargetFolder + vSession.User.GuId + "/" + csvFormattedName;
                                userCsvFile.LastUpdated = DateTime.Now;

                                userFileLoader.Update(userCsvFile);
                            }
                        }

                        # endregion

                        # region save pdf

                        if (pdfReadyToUp)
                        {
                            var pdfFileContent = pdfFile.PostedFile;
                            var pdfFileName = pdfFileContent.FileName;

                            string pdfServerMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"].ToString());
                            string pdfTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"]).ToString();

                            string pdfFormattedName = ImageResize.ChangeFileName(pdfFileName, ".pdf");

                            pdfServerMapPathTargetFolder = pdfServerMapPathTargetFolder + vSession.User.GuId + "\\";

                            if (!Directory.Exists(pdfServerMapPathTargetFolder))
                                Directory.CreateDirectory(pdfServerMapPathTargetFolder);

                            DirectoryInfo originalPdfDir = new DirectoryInfo(pdfServerMapPathTargetFolder);

                            foreach (FileInfo fi in originalPdfDir.GetFiles())
                            {
                                fi.Delete();
                            }

                            pdfFileContent.SaveAs(pdfServerMapPathTargetFolder + pdfFormattedName);

                            ElioUsersFiles userFile = new ElioUsersFiles();
                            DataLoader<ElioUsersFiles> userFileLoader = new DataLoader<ElioUsersFiles>(session);
                            ElioUsersFiles userPdfFile = Sql.GetUserPdfFile(vSession.User.Id, session);

                            if (userPdfFile == null)
                            {
                                userFile.UserId = vSession.User.Id;
                                userFile.FilePath = pdfTargetFolder + vSession.User.GuId + "/" + pdfFormattedName;
                                userFile.SysDate = DateTime.Now;
                                userFile.LastUpdated = DateTime.Now;
                                userFile.IsPublic = 1;

                                userFileLoader.Insert(userFile);
                            }
                            else
                            {
                                userPdfFile.FilePath = pdfTargetFolder + vSession.User.GuId + "/" + pdfFormattedName;
                                userPdfFile.LastUpdated = DateTime.Now;

                                userFileLoader.Update(userPdfFile);
                            }
                        }
                        else
                        {
                            ElioUsersFiles userPdfFile = Sql.GetUserPdfFile(vSession.User.Id, session);

                            if (userPdfFile != null && pdfFile.PostedFile == null)
                            {
                                string pdfServerMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"].ToString());
                                string pdfTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"]).ToString();

                                pdfServerMapPathTargetFolder = pdfServerMapPathTargetFolder + vSession.User.GuId + "\\";

                                DirectoryInfo originalPdfDir = new DirectoryInfo(pdfServerMapPathTargetFolder);

                                foreach (FileInfo fi in originalPdfDir.GetFiles())
                                {
                                    fi.Delete();
                                }

                                System.IO.Directory.Delete(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"].ToString()) + vSession.User.GuId);

                                DataLoader<ElioUsersFiles> loader = new DataLoader<ElioUsersFiles>(session);
                                loader.Delete(userPdfFile);
                            }
                        }

                        # endregion
                    }

                    vSession.LoadedDashboardControl = ControlLoader.ProfileDataViewMode;

                    if (HttpContext.Current.Request.Url.AbsolutePath.Contains("full-registration") && HttpContext.Current.Request.Url.AbsolutePath.Contains("criteria-selection"))
                    {
                        Response.Redirect(ControlLoader.ThankYouPage, false);
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "find-new-partners"), false);
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

        # endregion
    }
}
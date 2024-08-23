using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;
using System.Configuration;
using System.Windows.Input;

namespace WdS.ElioPlus
{
    public partial class DashboardTrainingLibraryPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    if (!IsPostBack)
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

        #region methods

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                divVendorsList.Visible = true;
                GetCollaborationVendors();
            }
            else
            {
                divVendorsList.Visible = false;
            }

            UcRgd1.Visible = false;
            MessageControlUp.Visible = false;
            vSession.VendorsResellersList.Clear();
            vSession.VendorsResellersList = null;
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        private void GetPartnersList(Repeater rpt, DAMessageControl control, string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                DataTable table = new DataTable();

                table.Columns.Add("id1");
                table.Columns.Add("id2");
                table.Columns.Add("id3");
                table.Columns.Add("master_user_id1");
                table.Columns.Add("master_user_id2");
                table.Columns.Add("master_user_id3");
                table.Columns.Add("invitation_status1");
                table.Columns.Add("invitation_status2");
                table.Columns.Add("invitation_status3");
                table.Columns.Add("partner_user_id1");
                table.Columns.Add("partner_user_id2");
                table.Columns.Add("partner_user_id3");
                table.Columns.Add("company_name1");
                table.Columns.Add("company_name2");
                table.Columns.Add("company_name3");
                table.Columns.Add("email1");
                table.Columns.Add("email2");
                table.Columns.Add("email3");
                table.Columns.Add("company_logo1");
                table.Columns.Add("company_logo2");
                table.Columns.Add("company_logo3");

                int isPublic = 1;
                int isDeleted = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    rpt.Visible = true;
                    control.Visible = false;

                    int rows = partners.Count / 3;
                    int columns = partners.Count % 3;
                    int index = 0;

                    for (int i = 0; i < rows; i++)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = partners[index + 1].Id.ToString();
                        row["id3"] = partners[index + 2].Id.ToString();
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = partners[index + 1].MasterUserId.ToString();
                        row["master_user_id3"] = partners[index + 2].MasterUserId.ToString();
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = partners[index + 1].InvitationStatus;
                        row["invitation_status3"] = partners[index + 2].InvitationStatus;
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = partners[index + 1].PartnerUserId;
                        row["partner_user_id3"] = partners[index + 2].PartnerUserId;
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = partners[index + 1].CompanyName;
                        row["company_name3"] = partners[index + 2].CompanyName;
                        row["email1"] = partners[index].Email;
                        row["email2"] = partners[index + 1].Email;
                        row["email3"] = partners[index + 2].Email;
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = partners[index + 1].CompanyLogo;
                        row["company_logo3"] = partners[index + 2].CompanyLogo;
                        index = index + 3;

                        table.Rows.Add(row);
                    }

                    if (columns == 1)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = "";
                        row["id3"] = "";
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = "";
                        row["master_user_id3"] = "";
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = "";
                        row["invitation_status3"] = "";
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = "";
                        row["partner_user_id3"] = "";
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = "";
                        row["company_name3"] = "";
                        row["email1"] = partners[index].Email;
                        row["email2"] = "";
                        row["email3"] = "";
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = "";
                        row["company_logo3"] = "";

                        table.Rows.Add(row);
                    }
                    else if (columns == 2)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = partners[index + 1].Id.ToString();
                        row["id3"] = "";
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = partners[index + 1].MasterUserId.ToString();
                        row["master_user_id3"] = "";
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = partners[index + 1].InvitationStatus;
                        row["invitation_status3"] = "";
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = partners[index + 1].PartnerUserId;
                        row["partner_user_id3"] = "";
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = partners[index + 1].CompanyName;
                        row["company_name3"] = "";
                        row["email1"] = partners[index].Email;
                        row["email2"] = partners[index + 1].Email;
                        row["email3"] = "";
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = partners[index + 1].CompanyLogo;
                        row["company_logo3"] = "";

                        table.Rows.Add(row);
                    }

                    //foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    //{
                    //    int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? partner.PartnerUserId : partner.MasterUserId;
                    //    ElioUsers company = Sql.GetUserById(partnerId, session);

                    //    if (company != null)
                    //    {
                    //        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, company.CompanyLogo);
                    //    }
                    //}

                    rpt.DataSource = table;
                    rpt.DataBind();
                }
                else
                {
                    rpt.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers yet" : "You have no Vendors yet";
                    GlobalMethods.ShowMessageControlDA(control, alert, MessageTypes.Info, true, true, false, true, false);
                }
            }
        }

        private void LoadGridData(Repeater rpt, DAMessageControl control)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("user_id");
                    table.Columns.Add("category_id");

                    table.Columns.Add("id1");
                    table.Columns.Add("id2");
                    table.Columns.Add("id3");
                    table.Columns.Add("id4");

                    table.Columns.Add("course_description1");
                    table.Columns.Add("course_description2");
                    table.Columns.Add("course_description3");
                    table.Columns.Add("course_description4");

                    table.Columns.Add("overview_text1");
                    table.Columns.Add("overview_text2");
                    table.Columns.Add("overview_text3");
                    table.Columns.Add("overview_text4");

                    table.Columns.Add("overview_image_name1");
                    table.Columns.Add("overview_image_name2");
                    table.Columns.Add("overview_image_name3");
                    table.Columns.Add("overview_image_name4");

                    table.Columns.Add("overview_image_path1");
                    table.Columns.Add("overview_image_path2");
                    table.Columns.Add("overview_image_path3");
                    table.Columns.Add("overview_image_path4");

                    //table.Columns.Add("overview_image_type1");
                    //table.Columns.Add("overview_image_type2");
                    //table.Columns.Add("overview_image_type3");
                    //table.Columns.Add("overview_image_type4");

                    //table.Columns.Add("is_new1");
                    //table.Columns.Add("is_new2");
                    //table.Columns.Add("is_new3");
                    //table.Columns.Add("is_new4");

                    int userId = -1;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        userId = vSession.User.Id;
                    }
                    else
                    {
                        if (DrpVendors.SelectedValue != "" && DrpVendors.SelectedValue != "0")
                        {
                            userId = Convert.ToInt32(DrpVendors.SelectedValue);
                        }
                    }

                    if (userId > -1)
                    {
                        List<ElioUsersTrainingCategoriesCourses> files = new List<ElioUsersTrainingCategoriesCourses>();

                        files = Sql.GetElioPartnerTrainingCategoryCoursesAll(userId, vSession.User.Id, session);

                        if (files.Count > 0)
                        {
                            rpt.Visible = true;
                            control.Visible = false;

                            int rows = files.Count / 3;
                            int columns = files.Count % 3;
                            int index = 0;

                            for (int i = 0; i < rows; i++)
                            {
                                //if (string.IsNullOrEmpty(files[index].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew1(table, files[index].OverviewImageName))
                                //        files[index].OverviewImageType = GetItemImageByType(files[index].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index].OverviewImageType = "/TrainingLibrary/" + files[index].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 1].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew2(table, files[index + 1].OverviewImageName))
                                //        files[index + 1].OverviewImageType = GetItemImageByType(files[index + 1].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 1].OverviewImageType = "/TrainingLibrary/" + files[index + 1].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 2].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew3(table, files[index + 2].OverviewImageName))
                                //        files[index + 2].OverviewImageType = GetItemImageByType(files[index + 2].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 2].OverviewImageType = "/TrainingLibrary/" + files[index + 2].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 3].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew4(table, files[index + 3].OverviewImageName))
                                //        files[index + 3].OverviewImageType = GetItemImageByType(files[index + 3].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 3].OverviewImageType = "/TrainingLibrary/" + files[index + 3].OverviewImagePath;
                                //}

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                //row["id4"] = files[index + 3].Id.ToString();
                                row["course_description1"] = files[index].CourseDescription;
                                row["course_description2"] = files[index + 1].CourseDescription;
                                row["course_description3"] = files[index + 2].CourseDescription;
                                //row["course_description4"] = files[index + 3].CourseDescription;
                                row["overview_text1"] = files[index].OverviewText;
                                row["overview_text2"] = files[index + 1].OverviewText;
                                row["overview_text3"] = files[index + 2].OverviewText;
                                //row["overview_text4"] = files[index + 3].OverviewText;
                                row["overview_image_name1"] = files[index].OverviewImageName;
                                row["overview_image_name2"] = files[index + 1].OverviewImageName;
                                row["overview_image_name3"] = files[index + 2].OverviewImageName;
                                //row["overview_image_name4"] = files[index + 3].OverviewImageName;
                                row["overview_image_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath;
                                row["overview_image_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath;
                                row["overview_image_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 2].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 2].OverviewImagePath;
                                //row["overview_image_path4"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 3].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 3].OverviewImagePath;
                                //row["overview_image_type1"] = files[index].OverviewImageType;
                                //row["overview_image_type2"] = files[index + 1].OverviewImageType;
                                //row["overview_image_type3"] = files[index + 2].OverviewImageType;
                                //row["overview_image_type4"] = files[index + 3].OverviewImageType;
                                //row["is_new1"] = files[index].IsNew.ToString();
                                //row["is_new2"] = files[index + 1].IsNew.ToString();
                                //row["is_new3"] = files[index + 2].IsNew.ToString();
                                //row["is_new4"] = files[index + 3].IsNew.ToString();

                                index = index + 3;

                                table.Rows.Add(row);
                            }

                            if (columns == 1)
                            {
                                //if (string.IsNullOrEmpty(files[index].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew1(table, files[index].OverviewImageName))
                                //        files[index].OverviewImageType = GetItemImageByType(files[index].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index].OverviewImageType = "/TrainingLibrary/" + files[index].OverviewImagePath;
                                //}

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = "0";
                                row["id3"] = "0";
                                //row["id4"] = "0";
                                row["course_description1"] = files[index].CourseDescription;
                                row["course_description2"] = "";
                                row["course_description3"] = "";
                                //row["course_description4"] = "";
                                row["overview_text1"] = files[index].OverviewImageName;
                                row["overview_text2"] = "-";
                                row["overview_text3"] = "-";
                                //row["overview_text4"] = "-";
                                row["overview_image_name1"] = files[index].OverviewImageName;
                                row["overview_image_name2"] = "-";
                                row["overview_image_name3"] = "-";
                                //row["overview_image_name4"] = "-";
                                row["overview_image_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + files[index].OverviewImagePath;
                                row["overview_image_path2"] = "#";
                                row["overview_image_path3"] = "#";
                                //row["overview_image_path4"] = "#";
                                //row["overview_image_type1"] = files[index].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type2"] = "-";
                                //row["overview_image_type3"] = "-";
                                //row["overview_image_type4"] = "-";
                                //row["is_new1"] = files[index].IsNew.ToString();
                                //row["is_new2"] = "0";
                                //row["is_new3"] = "0";
                                //row["is_new4"] = "0";

                                table.Rows.Add(row);
                            }
                            else if (columns == 2)
                            {
                                //if (string.IsNullOrEmpty(files[index].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew1(table, files[index].OverviewImageName))
                                //        files[index].OverviewImageType = GetItemImageByType(files[index].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index].OverviewImageType = "/TrainingLibrary/" + files[index].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 1].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew2(table, files[index + 1].OverviewImageName))
                                //        files[index + 1].OverviewImageType = GetItemImageByType(files[index + 1].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 1].OverviewImageType = "/TrainingLibrary/" + files[index + 1].OverviewImagePath;
                                //}

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = "0";
                                //row["id4"] = "0";
                                row["course_description1"] = files[index].CourseDescription;
                                row["course_description2"] = files[index + 1].CourseDescription;
                                row["course_description3"] = "";
                                //row["course_description4"] = "";
                                row["overview_text1"] = files[index].OverviewText;
                                row["overview_text2"] = files[index + 1].OverviewText;
                                row["overview_text3"] = "-";
                                //row["overview_text4"] = "-";
                                row["overview_image_name1"] = files[index].OverviewImageName;
                                row["overview_image_name2"] = files[index + 1].OverviewImageName;
                                row["overview_image_name3"] = "-";
                                //row["overview_image_name4"] = "-";
                                row["overview_image_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath;
                                row["overview_image_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath;
                                row["overview_image_path3"] = "#";
                                //row["overview_image_path4"] = "#";
                                //row["overview_image_type1"] = files[index].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type2"] = files[index + 1].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type3"] = "-";
                                //row["overview_image_type4"] = "-";
                                //row["is_new1"] = files[index].IsNew.ToString();
                                //row["is_new2"] = files[index + 1].IsNew.ToString();
                                //row["is_new3"] = "0";
                                //row["is_new4"] = "0";

                                table.Rows.Add(row);
                            }
                            else if (columns == 3)
                            {
                                //if (string.IsNullOrEmpty(files[index].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew1(table, files[index].OverviewImageName))
                                //        files[index].OverviewImageType = GetItemImageByType(files[index].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index].OverviewImageType = "/TrainingLibrary/" + files[index].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 1].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew2(table, files[index + 1].OverviewImageName))
                                //        files[index + 1].OverviewImageType = GetItemImageByType(files[index + 1].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 1].OverviewImageType = "/TrainingLibrary/" + files[index + 1].OverviewImagePath;
                                //}

                                //if (string.IsNullOrEmpty(files[index + 2].OverviewImagePath))
                                //{
                                //    if (!FileExistsNew3(table, files[index + 2].OverviewImageName))
                                //        files[index + 2].OverviewImageType = GetItemImageByType(files[index + 2].OverviewImagePath);
                                //}
                                //else
                                //{
                                //    files[index + 2].OverviewImageType = "/TrainingLibrary/" + files[index + 2].OverviewImagePath;
                                //}

                                DataRow row = table.NewRow();
                                row["user_id"] = files[index].UserId.ToString();
                                row["category_id"] = files[index].CategoryId.ToString();
                                row["id1"] = files[index].Id.ToString();
                                row["id2"] = files[index + 1].Id.ToString();
                                row["id3"] = files[index + 2].Id.ToString();
                                //row["id4"] = "0";
                                row["course_description1"] = files[index].CourseDescription;
                                row["course_description2"] = files[index + 1].CourseDescription;
                                row["course_description3"] = files[index + 2].CourseDescription;
                                //row["course_description4"] = "";
                                row["overview_text1"] = files[index].OverviewText;
                                row["overview_text2"] = files[index + 1].OverviewText;
                                row["overview_text3"] = files[index + 2].OverviewText;
                                //row["overview_text4"] = "-";
                                row["overview_image_name1"] = files[index].OverviewImageName;
                                row["overview_image_name2"] = files[index + 1].OverviewImageName;
                                row["overview_image_name3"] = files[index + 2].OverviewImageName;
                                //row["overview_image_name4"] = "-";
                                row["overview_image_path1"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index].OverviewImagePath;
                                row["overview_image_path2"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 1].OverviewImagePath;
                                row["overview_image_path3"] = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 2].OverviewImagePath : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewTrainingLibraryPath"].ToString() + "/" + files[index + 2].OverviewImagePath;
                                //row["overview_image_path4"] = "#";
                                //row["overview_image_type1"] = files[index].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type2"] = files[index + 1].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type3"] = files[index + 2].OverviewImageType.Replace("~", ""); ;
                                //row["overview_image_type4"] = "-";
                                //row["is_new1"] = files[index].IsNew.ToString();
                                //row["is_new2"] = files[index + 1].IsNew.ToString();
                                //row["is_new3"] = files[index + 2].IsNew.ToString();
                                //row["is_new4"] = "0";

                                table.Rows.Add(row);
                            }

                            rpt.Visible = true;
                            rpt.DataSource = table;
                            rpt.DataBind();
                        }
                        else
                        {
                            rpt.Visible = false;
                            control.Visible = true;
                            GlobalMethods.ShowMessageControlDA(control, "There are no courses", MessageTypes.Info, true, true, false, true, false);
                        }
                    }
                    else
                    {
                        rpt.Visible = false;
                        GlobalMethods.ShowMessageControlDA(control, "There are no courses", MessageTypes.Info, true, true, false, true, false);
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
        
        #endregion

        #region Grids

        protected void Rdg1_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;
                        if (row != null)
                        {
                            HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                            HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                            HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");

                            if (row["id1"].ToString() == "0")
                                div1.Visible = false;
                            if (row["id2"].ToString() == "0")
                                div2.Visible = false;
                            if (row["id3"].ToString() == "0")
                                div3.Visible = false;

                            if (div1.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo1");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo1");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName1");

                                HiddenField hdnId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "id1");
                                HiddenField hdnUserId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnUserId1");
                                HiddenField hHdnFileName1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName1");
                                HiddenField hdnFilePath1 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath1");
                                
                                hdnId1.Value = row["id1"].ToString();
                                hdnUserId1.Value = row["user_id"].ToString();
                                hHdnFileName1.Value = row["overview_image_name1"].ToString();
                                hdnFilePath1.Value = row["overview_image_path1"].ToString();
                                
                                ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(Convert.ToInt32(hdnId1.Value), session);

                                if (course != null)
                                {
                                    aCompanyLogo.HRef = "/TrainingLibrary/" + course.OverviewImagePath;
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;

                                    HtmlAnchor aBtnChoose1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose1");

                                    aBtnChoose1.HRef = ControlLoader.Dashboard(vSession.User, "") + hdnId1.Value + "/partner-training-course";
                                }
                            }

                            if (div2.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo2");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo2");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName2");

                                HiddenField hdnId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "id2");
                                HiddenField hdnUserId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnUserId2");
                                HiddenField hHdnFileName2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName2");
                                HiddenField hdnFilePath2 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath2");

                                hdnId2.Value = row["id2"].ToString();
                                hdnUserId2.Value = row["user_id"].ToString();
                                hHdnFileName2.Value = row["overview_image_name2"].ToString();
                                hdnFilePath2.Value = row["overview_image_path2"].ToString();

                                ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(Convert.ToInt32(hdnId2.Value), session);

                                if (course != null)
                                {
                                    aCompanyLogo.HRef = "/TrainingLibrary/" + course.OverviewImagePath;
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;

                                    HtmlAnchor aBtnChoose2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose2");

                                    aBtnChoose2.HRef = ControlLoader.Dashboard(vSession.User, "") + hdnId2.Value + "/partner-training-course";
                                }
                            }

                            if (div3.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo3");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo3");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName3");

                                HiddenField hdnId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "id3");
                                HiddenField hdnUserId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnUserId3");
                                HiddenField hHdnFileName3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFileName3");
                                HiddenField hdnFilePath3 = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnFilePath3");

                                hdnId3.Value = row["id3"].ToString();
                                hdnUserId3.Value = row["user_id"].ToString();
                                hHdnFileName3.Value = row["overview_image_name3"].ToString();
                                hdnFilePath3.Value = row["overview_image_path3"].ToString();

                                ElioUsersTrainingCategoriesCourses course = Sql.GetTrainingCourseById(Convert.ToInt32(hdnId3.Value), session);

                                if (course != null)
                                {
                                    aCompanyLogo.HRef = "/TrainingLibrary/" + course.OverviewImagePath;
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;

                                    HtmlAnchor aBtnChoose3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose3");

                                    aBtnChoose3.HRef = ControlLoader.Dashboard(vSession.User, "") + hdnId3.Value + "/partner-training-course";
                                }
                            }
                        }
                    }
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

        protected void Rdg1_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    LoadGridData(Rdg1, UcRgd1);
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

        #region Buttons

        protected void aBtnChoose1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;

                var item = btn.Parent as RepeaterItem;
                if (item != null)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField hdnUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "user_id");

                        //ElioUsers user = Sql.GetUserById(Convert.ToInt32(hdnUserId.Value), session);
                        //if (user != null)
                        //{
                        //    string url = "/collaboration-library/" + user.GuId;

                        //    Response.Redirect(ControlLoader.Dashboard(vSession.User, url), false);
                        //}
                    }
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

        private void GetCollaborationVendors()
        {
            List<ElioUsers> vendors = SqlCollaboration.GetCollaborationVendorsByResellerUserId(vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), session);
            if (vendors.Count > 0)
            {
                divVendorsList.Visible = true;

                DrpVendors.Items.Clear();

                foreach (ElioUsers vendor in vendors)
                {
                    ListItem item = new ListItem();
                    item.Value = vendor.Id.ToString();
                    item.Text = vendor.CompanyName;

                    DrpVendors.Items.Add(item);
                }

                DrpVendors.Items.FindByValue(vendors[0].Id.ToString()).Selected = true;
                DrpVendors.SelectedItem.Value = vendors[0].Id.ToString();
                DrpVendors.SelectedItem.Text = vendors[0].CompanyName;

                DrpVendors.Enabled = (vendors.Count == 1) ? false : true;
            }
            else
            {
                divVendorsList.Visible = false;
            }
        }

        #region DropDownLists

        protected void DrpVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (DrpVendors.SelectedValue != "" && DrpVendors.SelectedValue != "0")
                {
                    LoadGridData(Rdg1, UcRgd1);
                }
                else
                {
                    Rdg1.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcRgd1, "You must select a vendor first", MessageTypes.Info, true, true, true, true, false);
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
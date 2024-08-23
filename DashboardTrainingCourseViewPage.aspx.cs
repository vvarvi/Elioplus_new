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
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class DashboardTrainingCourseViewPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page properties

        public enum Navigation
        {
            None,
            First,
            Next,
            Previous,
            Last,
            Pager,
            Sorting
        }

        public int NowViewing
        {
            get
            {
                object obj = ViewState["_NowViewing"];
                if (obj == null)
                    return 0;
                else
                    return (int)obj;
            }
            set
            {
                this.ViewState["_NowViewing"] = value;
            }
        }

        public int CourseId
        {
            get
            {
                if (ViewState["CourseId"] != null)
                    return Convert.ToInt32(ViewState["CourseId"]);
                else
                {
                    Uri path = HttpContext.Current.Request.Url;
                    var pathSegs = path.Segments;

                    if (pathSegs.Length == 6)
                    {
                        string id = pathSegs[pathSegs.Length - 2].TrimEnd('/');

                        if (id != "")
                        {
                            ViewState["CourseId"] = id;
                            return Convert.ToInt32(ViewState["CourseId"]);
                        }
                        else
                            return -1;
                    }
                    else
                        return -1;
                }
            }
            set
            {
                ViewState["CourseId"] = value;
            }
        }

        public int CurrentChapterId
        {
            get
            {
                if (ViewState["CurrentChapterId"] != null)
                    return Convert.ToInt32(ViewState["CurrentChapterId"]);
                else
                {
                    return -1;
                }
            }
            set
            {
                ViewState["CurrentChapterId"] = value;
            }
        }

        public string Key
        {
            get
            {
                if (ViewState["Key"] != null)
                    return ViewState["Key"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["Key"] = value;
            }
        }

        public bool ShowPartner
        {
            get
            {
                if (ViewState["ShowPartner"] != null)
                    return (bool)ViewState["ShowPartner"];
                else
                    return false;
            }
            set
            {
                ViewState["ShowPartner"] = value;
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

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    if (isError)
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTrainingAddNewPage", Actions.Add, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    if (!IsPostBack)
                    {
                        FixPage();
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

        #region Methods

        protected void LoadRptChapter(Navigation navigation, List<Chapter> chapters, Repeater rpt)
        {
            try
            {
                PagedDataSource objPds = new PagedDataSource();

                if (chapters.Count > 0)
                {
                    objPds.DataSource = chapters;

                    objPds.AllowPaging = true;

                    objPds.PageSize = 10;

                    switch (navigation)
                    {
                        case Navigation.First:
                            NowViewing = 1;
                            break;

                        case Navigation.Next:       //Increment NowViewing by 1
                            NowViewing++;
                            break;

                        case Navigation.Previous:   //Decrement NowViewing by 1
                            NowViewing--;
                            break;

                        default:                    //Default NowViewing set to 0
                            NowViewing = 0;
                            break;
                    }

                    objPds.CurrentPageIndex = NowViewing;
                    rpt.DataSource = objPds;
                    rpt.DataBind();
                }
                else
                {
                    rpt.DataSource = null;
                    rpt.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void FixPage()
        {
            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                if (CourseId > 0)
                {
                    ElioUsersTrainingCategoriesCourses course = Sql.GetPartnerTrainingCategoryCourseByCourseId(vSession.User.Id, CourseId, session);        //GetTrainingCategoryCourseByCourseId(CourseId, session);
                    if (course != null)
                    {
                        bool isPartner = SqlCollaboration.IsConfirmedResellerOfVendor(vSession.User.Id, course.UserId, session);
                        if (isPartner)
                        {
                            ImgCoursePreview.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;
                            LblCourseOverview.Text = course.CourseDescription;

                            List<ElioUsersTrainingCoursesChapters> chapters = Sql.GetPartnerTrainingCourseChapters(vSession.User.Id, course.CategoryId, course.Id, session);    //GetElioUserTrainingCourseChapters(course.UserId, course.CategoryId, course.Id, session);

                            if (chapters.Count > 0)
                            {
                                List<Chapter> chps = new List<Chapter>();
                                int count = 1;
                                foreach (ElioUsersTrainingCoursesChapters chapter in chapters)
                                {
                                    ListItem item = new ListItem();

                                    item.Value = chapter.Id.ToString();
                                    item.Text = chapter.ChapterTitle;

                                    item.Selected = true;

                                    CbxChaptersList.Items.Add(item);

                                    Chapter ch = new Chapter();
                                    ch.ChapterId = chapter.Id;
                                    ch.ChapterTitle = chapter.ChapterTitle;
                                    ch.ChapterText = chapter.ChapterText;
                                    ch.ChapterLink = chapter.ChapterLink;
                                    ch.CourseId = chapter.CourserId;
                                    ch.ChapterFileName = chapter.ChapterFileName;
                                    ch.ChapterFilePath = chapter.ChapterFilePath;
                                    ch.IsViewed = chapter.IsViewed;
                                    ch.ChapterStep = "CHAPTER " + count;

                                    chps.Add(ch);

                                    count++;
                                }

                                if (chps.Count > 0)
                                {
                                    LoadRptChapter(Navigation.None, chps, RdgChapter);
                                    //LoadRptChapter(Navigation.None, chps, RdgChapterSteps);
                                }                                
                            }

                            CurrentChapterId = -1;

                            divStep0.Visible = true;
                            divStep1.Visible = false;
                            BtnPrevious.Visible = false;
                        }
                    }
                }
            }
            else
            {
                //GetTiersByUser(vSession.User.Id);
                //LoadCountries();
            }

            vSession.Category = null;

            UpdateStrings();
            SetLinks();

            UploadMessageAlert.Visible = UcWizardAlertControl.Visible = false;
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {

        }

        private void GetPartnersList(string orderByClause, GlobalMethods.SearchCriteria criteria, int libraryGroupId)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, libraryGroupId, session);

                if (partners.Count > 0)
                {
                    //ImgBtnSendMsg.Visible = true;
                    //RptConnectionList.Visible = true;
                    //RptRetailorsList.Visible = true;
                    //RptEditRetailorsList.Visible = true;
                    //PnlCreateRetailorGroups.Enabled = true;

                    //UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    //RptConnectionList.DataSource = table;
                    //RptConnectionList.DataBind();

                    //RptRetailorsList.DataSource = table;
                    //RptRetailorsList.DataBind();

                    //RptEditRetailorsList.DataSource = table;
                    //RptEditRetailorsList.DataBind();
                }
                else
                {
                    //RptConnectionList.Visible = false;
                    //RptRetailorsList.Visible = false;
                    //RptEditRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers yet" : "You have no Vendors yet";
                    //GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false, false, false);
                    //GlobalMethods.ShowMessageControlDA(MessageControlRetailors, alert, MessageTypes.Info, true, true, false, false, false);

                    //ImgBtnSendMsg.Visible = false;
                    //PnlCreateRetailorGroups.Enabled = false;
                }
            }
        }

        #endregion

        #region Buttons

        protected void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divMStep0.Attributes["data-wizard-state"] = "pending";

                    ElioUsersTrainingCoursesChapters curChpt = Sql.GetTrainingNextChapter(vSession.User.Id, CourseId, CurrentChapterId, session);
                    if (curChpt != null)
                    {
                        if (CurrentChapterId > -1 && curChpt.IsViewed == 0)
                            Sql.SetTrainingChapterViewed(vSession.User.Id, CourseId, CurrentChapterId, session);

                        divStep1.Attributes["data-wizard-state"] = "current";
                        divStep1.Visible = true;
                        divStep0.Attributes["data-wizard-state"] = "pending";
                        divStep0.Visible = false;
                        BtnPrevious.Visible = true;

                        LblChapterTitle.Text = curChpt.ChapterTitle;
                        aChapterLink.HRef = curChpt.ChapterLink;
                        aChapterFilePath.HRef = curChpt.ChapterFilePath;
                        LblChapterFileName.Text = curChpt.ChapterFileName;
                        LblChapterText.Text = curChpt.ChapterText;

                        int previousChapterId = CurrentChapterId;
                        CurrentChapterId = curChpt.Id;

                        foreach (RepeaterItem item in RdgChapter.Items)
                        {
                            if (item != null)
                            {
                                HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                                if (hdnId != null)
                                {
                                    if (hdnId.Value == previousChapterId.ToString())
                                    {
                                        HiddenField hdnIsViewed = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsViewed");
                                        if (hdnIsViewed != null)
                                        {
                                            HtmlImage imgViewed = (HtmlImage)ControlFinder.FindControlRecursive(item, "imgViewed");
                                            if (imgViewed != null)
                                            {
                                                hdnIsViewed.Value = "1";
                                                imgViewed.Visible = true;
                                            }
                                        }
                                    }

                                    HtmlGenericControl divMStep1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMStep1");
                                    if (divMStep1 != null)
                                    {
                                        if (hdnId.Value == CurrentChapterId.ToString())
                                        {
                                            divMStep1.Attributes["data-wizard-state"] = "current";
                                        }
                                        else
                                        {
                                            divMStep1.Attributes["data-wizard-state"] = "pending";
                                        }
                                    }


                                }
                            }
                        }

                        bool isLast = Sql.IsTrainingChapterLast(vSession.User.Id, CourseId, CurrentChapterId, session);
                        if (isLast)
                        {
                            BtnNext.Visible = false;
                            BtnComplete.Visible = true;
                        }
                    }
                    else
                    {
                        BtnNext.Visible = false;
                        BtnPrevious.Visible = true;

                        foreach (RepeaterItem item in RdgChapter.Items)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                            if (hdnId != null)
                            {
                                HtmlGenericControl divMStep1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMStep1");
                                if (divMStep1 != null)
                                {
                                    if (hdnId.Value == CurrentChapterId.ToString())
                                    {
                                        divMStep1.Attributes["data-wizard-state"] = "current";
                                        break;
                                    }
                                    else
                                    {
                                        divMStep1.Attributes["data-wizard-state"] = "pending";
                                    }
                                }
                            }
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

        protected void BtnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divStep1.Visible = divStep0.Visible = false;
                    BtnPrevious.Visible = BtnNext.Visible = BtnComplete.Visible = false;

                    if (CourseId > 0)
                    {
                        Sql.SetTrainingCourseNotNew(vSession.User.Id, CourseId, session);
                        Sql.SetTrainingChaptersViewedByCourse(vSession.User.Id, CourseId, session);

                        foreach (RepeaterItem item in RdgChapter.Items)
                        {
                            if (item != null)
                            {
                                HiddenField hdnIsViewed = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsViewed");
                                if (hdnIsViewed != null)
                                {
                                    HtmlImage imgViewed = (HtmlImage)ControlFinder.FindControlRecursive(item, "imgViewed");
                                    if (imgViewed != null)
                                    {
                                        hdnIsViewed.Value = "1";
                                        imgViewed.Visible = true;
                                    }
                                }
                            }
                        }

                        GlobalMethods.ShowMessageControlDA(UcWizardAlertControl, "Congratulations! You completed  this training course successfully.", MessageTypes.Success, true, true, true, false, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ElioUsersTrainingCoursesChapters curChpt = Sql.GetTrainingPreviousChapter(vSession.User.Id, CourseId, CurrentChapterId, session);
                if (curChpt != null)
                {
                    divStep1.Attributes["data-wizard-state"] = "current";
                    divStep1.Visible = true;
                    divStep0.Attributes["data-wizard-state"] = "pending";
                    divStep0.Visible = false;
                    BtnPrevious.Visible = true;
                    BtnNext.Visible = true;
                    BtnComplete.Visible = false;

                    LblChapterTitle.Text = curChpt.ChapterTitle;
                    aChapterLink.HRef = curChpt.ChapterLink;
                    aChapterFilePath.HRef = curChpt.ChapterFilePath;
                    LblChapterFileName.Text = curChpt.ChapterFileName;
                    LblChapterText.Text = curChpt.ChapterText;

                    CurrentChapterId = curChpt.Id;

                    foreach (RepeaterItem item in RdgChapter.Items)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");
                        if (hdnId != null)
                        {
                            HtmlGenericControl divMStep1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMStep1");
                            if (divMStep1 != null)
                            {
                                if (hdnId.Value == CurrentChapterId.ToString())
                                {
                                    divMStep1.Attributes["data-wizard-state"] = "current";
                                }
                                else
                                {
                                    divMStep1.Attributes["data-wizard-state"] = "pending";
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (CourseId > 0)
                    {
                        CurrentChapterId = -1;

                        BtnNext.Visible = true;
                        BtnPrevious.Visible = false;
                        BtnComplete.Visible = false;

                        divStep0.Visible = true;
                        divStep1.Visible = false;

                        divMStep0.Attributes["data-wizard-state"] = divStep0.Attributes["data-wizard-state"] = "current";

                        ElioUsersTrainingCategoriesCourses course = Sql.GetPartnerTrainingCategoryCourseByCourseId(vSession.User.Id, CourseId, session);
                        if (course != null)
                        {
                            bool isPartner = SqlCollaboration.IsConfirmedResellerOfVendor(vSession.User.Id, course.UserId, session);
                            if (isPartner)
                            {
                                ImgCoursePreview.ImageUrl = "/TrainingLibrary/" + course.OverviewImagePath;
                                LblCourseOverview.Text = course.CourseDescription;

                                List<ElioUsersTrainingCoursesChapters> chapters = Sql.GetPartnerTrainingCourseChapters(vSession.User.Id, course.CategoryId, course.Id, session);

                                if (chapters.Count > 0)
                                {
                                    CbxChaptersList.Items.Clear();
                                    List<Chapter> chps = new List<Chapter>();
                                    int count = 1;
                                    foreach (ElioUsersTrainingCoursesChapters chapter in chapters)
                                    {
                                        ListItem item = new ListItem();

                                        item.Value = chapter.Id.ToString();
                                        item.Text = chapter.ChapterTitle;

                                        item.Selected = true;

                                        CbxChaptersList.Items.Add(item);

                                        Chapter ch = new Chapter();
                                        ch.ChapterId = chapter.Id;
                                        ch.ChapterTitle = chapter.ChapterTitle;
                                        ch.ChapterText = chapter.ChapterText;
                                        ch.ChapterLink = chapter.ChapterLink;
                                        ch.CourseId = chapter.CourserId;
                                        ch.ChapterFileName = chapter.ChapterFileName;
                                        ch.ChapterFilePath = chapter.ChapterFilePath;
                                        ch.ChapterStep = "CHAPTER " + count;

                                        chps.Add(ch);

                                        count++;
                                    }

                                    if (chps.Count > 0)
                                    {
                                        LoadRptChapter(Navigation.None, chps, RdgChapter);
                                        //LoadRptChapter(Navigation.None, chps, RdgChapterSteps);
                                    }
                                }
                            }
                        }

                        foreach (RepeaterItem item in RdgChapter.Items)
                        {
                            HtmlGenericControl divMStep1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMStep1");
                            if (divMStep1 != null)
                            {
                                divMStep1.Attributes["data-wizard-state"] = "pending";
                            }
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        #region Grids

        protected void RdgChapter_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        Chapter row = (Chapter)args.Item.DataItem;
                        if (row != null)
                        {
                            if (vSession.User != null)
                            {
                                HtmlImage imgViewed = (HtmlImage)ControlFinder.FindControlRecursive(item, "imgViewed");
                                imgViewed.Visible = row.IsViewed == 1;
                            }
                            else
                            {
                                Response.Redirect(ControlLoader.Login, false);
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

        #endregion

        #region Tabs

        #endregion

        #region DropDown Lists

        #endregion
    }
}
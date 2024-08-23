using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Enums;

namespace WdS.ElioPlus
{
    public partial class CriteriaSelection : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null && HttpContext.Current.Request.Url.AbsolutePath != null)
                {
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    string[] pathArray = path.Split('/');

                    if (pathArray.Length == 4)
                    {
                        string profile = pathArray[2];
                        int profileId = Convert.ToInt32(profile);
                        
                        session.OpenConnection();

                        if (vSession.User != null)
                        {
                            PhCriteriaSelection.Controls.Clear();
                            bool isMultiAccount = false;

                            if (profileId == vSession.User.Id)
                            {
                                ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.CriteriaSelection);
                            }
                            else
                            {
                                //ControlLoader.LoadElioControls(Page, PhCriteriaSelection, ControlLoader.MultiAccountsCriteriaSelection);
                                isMultiAccount = true;
                            }

                            FixPage(isMultiAccount);
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.Default(), false);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Default(), false);
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

        private void FixPage(bool isMultiAccountRegistration)
        {
            if (!IsPostBack)
            {
                UpdateStrings();
                SetLinks(isMultiAccountRegistration);
            }
        }

        private void SetLinks(bool isMultiAccountRegistration)
        {
            if (!isMultiAccountRegistration)
                aBtnSkip.HRef = ControlLoader.ThankYouPage;
            else
            {
                aBtnSkip.HRef = ControlLoader.Dashboard(vSession.User, "new-clients");
                aBtnSkip.Target = "_top";
            }
        }

        private void UpdateStrings()
        {
            LblAlgTitle.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Find Resellers" : "Find Vendors";
            LblAlgInfo.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Now you can go through our matching process " +
                                "in order to help us match you with the best resellers by selecting the following criteria about " +
                                "your company and your partner program." : "Now you can go through our matching process " +
                                "in order to help us match you with the best vendors by selecting the following criteria about " +
                                "your company and your needs.";            
            LblBtnSkip.Text = "Skip and do it later";
        }
    }
}
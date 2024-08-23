using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls.Categories.Vendors
{
    public partial class PopularVendor : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UpdateStrings();

                LoadData();
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

        private void LoadData()
        {
            ElioUsersIJViews user = Sql.GetPopularVendor(session);
            if (user != null)
            {
                vSession.PopularVendorId = user.Id;
                ImgBtnVendorLogo.ImageUrl = user.CompanyLogo;
                LblCompanyName.Text = user.CompanyName;
                RtbxSmallDescription.Text = user.Description;
                LblRegdate.Text = user.SysDate.ToString("dd/MM/yyyy");
                LblViews.Text = user.Views.ToString();
            }
        }

        private void UpdateStrings()
        {

        }

        #endregion

        #region Buttons

        protected void LnkBtnDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.Profile(vSession.User), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}
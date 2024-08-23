using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Controls.Collaboration.UserControls
{
    public partial class LibraryCategories : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    session.OpenConnection();

                    FillDropList();
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


        public void FillDropList()
        {
            List<ElioCollaborationUsersLibraryFilesCategories> categories = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategories(vSession.User.Id, session);

            Ddlcategory.Items.Clear();

            DropDownListItem item = new DropDownListItem();

            item.Value = "0";
            item.Text = "Choose category";

            Ddlcategory.Items.Add(item);

            foreach (ElioCollaborationUsersLibraryFilesCategories category in categories)
            {
                item = new DropDownListItem();

                item.Value = category.Id.ToString();
                item.Text = category.CategoryDescription;

                Ddlcategory.Items.Add(item);
            }
        }
    }
}
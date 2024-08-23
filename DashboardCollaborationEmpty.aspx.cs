using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.ImagesHelper;
using Telerik.Web.UI;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationEmpty : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public HtmlInputFile FileToUpload
        {
            get
            {
                //HtmlInputFile result = null;

                if (ViewState["FileToUpload"] != null)
                    return (HtmlInputFile)ViewState["FileToUpload"];
                else
                    return null;
            }
            set
            {
                ViewState["FileToUpload"] = value;
            }
        }

        public bool IsEditMode
        {
            get
            {
                if (ViewState["IsEditMode"] != null)
                    return (bool)ViewState["IsEditMode"];
                else
                    return false;
            }
            set
            {
                ViewState["IsEditMode"] = value;
            }
        }

        public int GroupId
        {
            get
            {
                if (ViewState["GroupId"] != null)
                    return Convert.ToInt32(ViewState["GroupId"]);
                else
                    return -1;
            }
            set
            {
                ViewState["GroupId"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs args)
        {
            MasterPageFile = "CollaborationToolMaster.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();
                                       
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

        private void FixPage()
        {
            if (!IsPostBack)
            {
               

            }
        }

        protected void Btn_OnClick(object sender, EventArgs args)
        {

        }
    }
}
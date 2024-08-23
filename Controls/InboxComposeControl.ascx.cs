using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using Telerik.Web.UI;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxComposeControl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //RacbxRecipient.Filter = (RadAutoCompleteFilter)Enum.Parse(typeof(RadAutoCompleteFilter), RadAutoCompleteFilter.StartsWith.ToString(), true);
                //RacbxRecipient.TextSettings.SelectionMode = (RadAutoCompleteSelectionMode)Enum.Parse(typeof(RadAutoCompleteSelectionMode), RadAutoCompleteSelectionMode.Multiple.ToString(), true);
                //RacbxRecipient.Delimiter = ";";
                //RacbxRecipient.DropDownPosition = (RadAutoCompleteDropDownPosition)Enum.Parse(typeof(RadAutoCompleteDropDownPosition), RadAutoCompleteDropDownPosition.Automatic.ToString(), true);
                //RacbxRecipient.AllowCustomEntry = true;
                //RacbxRecipient.TokensSettings.AllowTokenEditing = false;
                //RacbxRecipient.MinFilterLength = 1;
                //RacbxRecipient.MaxResultCount = 10;

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        //UpdateStrings();
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
        }

    }
}
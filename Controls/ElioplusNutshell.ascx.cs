using System;
using System.Linq;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Enums;


namespace WdS.ElioPlus.Controls
{
    public partial class ElioplusNutshell : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    UpdateStrings();

                    aLearnMore.HRef = ControlLoader.HowItWorks;
                }

                FixPaymentBtns();
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

        private void UpdateStrings()
        {
            LblElioplusNutsell.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "52")).Text;
            LblNutshell.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "53")).Text;
            LblMore.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "55")).Text;
            LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "54")).Text;
        }

        private void FixPaymentBtns()
        {
            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, false, ref showBtn, ref showModal, session);

            if (allowPayment)
            {
                BtnGetElioNow.Visible = false; // showBtn;
                aGetElioNow.Visible = true; // showModal;
            }
            else
            {
                BtnGetElioNow.Visible = false;
                aGetElioNow.Visible = false;
            }

            if (vSession.User == null)
            {
                LblGetElioNow.Text = BtnGetElioNow.Text = "Start Now Free";
                aGetElioNow.HRef = ControlLoader.Login;
            }
            else if (vSession.User != null && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                LblGetElioNow.Text = BtnGetElioNow.Text = "Start a new partnership!";
                aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");
            }
            else
            {
                LblGetElioNow.Text = BtnGetElioNow.Text = "Upgrade Now";
                aGetElioNow.HRef = ControlLoader.Pricing;
            }
        }
        
        #endregion

        #region Buttons

        protected void BtnSearchGoPremium_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}
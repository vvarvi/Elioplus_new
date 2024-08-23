using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using EnvDTE;
using static ServiceStack.Diagnostics.Events;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Services;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.SalesforceDC;
using System.Web;
using Stripe.Checkout;

namespace WdS.ElioPlus.pages
{
    public partial class PrmSoftwarePage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        //protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["ReCaptcha_Key"].ToString();
        //protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["ReCaptcha_Secret"].ToString();

        //[WebMethod]
        //public static string VerifyCaptcha(string response)
        //{
        //    string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
        //    return (new WebClient()).DownloadString(url);
        //}

        public string Trans
        {
            get
            {
                if (ViewState["Trans"] != null)
                {
                    return ViewState["Trans"].ToString();
                }

                return "";
            }
            set
            {
                ViewState["Trans"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    if (HttpContext.Current.Request.Url.AbsoluteUri.EndsWith("/"))
                        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.TrimEnd('/'), false);

                    LoadClients();
                    FixButtons();
                    SetLinks();
                    ClearMessageData();

                    Trans = GlobalMethods.GetTranslationsForPage();
                    UpdateStrings(Trans);

                    aTranslateUK.Visible = Trans != "";
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

        private void UpdateStrings(string pageLang)
        {
            Literal1.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "1")).Text;
            //Literal2.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "2")).Text;
            Literal3.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "3")).Text;
            Literal4.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "4")).Text;
            //Literal5.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "5")).Text;
            Literal6.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "6")).Text;
            Literal7.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "7")).Text;
            Literal8.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "8")).Text;

            //Literal9.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "button", "1")).Text;
            //Literal10.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "button", "2")).Text;

            //TbxFirstName.Attributes["placeholder"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "tooltip", "1")).Text;
            //TbxLastName.Attributes["placeholder"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "tooltip", "2")).Text;
            //TbxCompanyName.Attributes["placeholder"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "tooltip", "3")).Text;
            //TbxEmail.Attributes["placeholder"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "tooltip", "4")).Text;

            Label1.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "1")).Text;
            Label01.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "7")).Text;
            Label2.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "2")).Text;
            //Label3.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "3")).Text;
            //Label4.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "4")).Text;
            //Label5.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "5")).Text;

            Label6.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "2")).Text;
            Label06.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "16")).Text;
            Label7.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "3")).Text;
            //Label130.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "1")).Text;
            //Label111.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "4")).Text;
            Label14.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "4")).Text;
            Label17.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "4")).Text;
            Label131.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "4")).Text;
            Label9.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "9")).Text;
            Label10.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "10")).Text;
            Label11.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "4")).Text;
            Label12.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "12")).Text;
            Label13.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "13")).Text;

            //Label8.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "8")).Text;
            Label15.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "15")).Text;
            Label16.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "16")).Text;

            Label18.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "18")).Text;
            Label19.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "19")).Text;
            Label20.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "20")).Text;

            LblPrmFeatures1.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "21")).Text;

            Label21.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "23")).Text;
            Label22.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "24")).Text;
            Label23.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "25")).Text;
            Label24.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "26")).Text;
            Label25.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "27")).Text;
            Label26.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "28")).Text;
            Label27.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "29")).Text;
            Label28.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "30")).Text;
            Label29.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "31")).Text;
            Label30.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "32")).Text;
            //Label31.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "33")).Text;
            Label32.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "34")).Text;
            Label33.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "35")).Text;
            Label34.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "36")).Text;
            Label35.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "37")).Text;
            Label36.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "38")).Text;
            //Label37.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "39")).Text;
            //Label38.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "40")).Text;
            //Label39.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "41")).Text;
            //Label40.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "42")).Text;
            //Label41.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "43")).Text;
            //Label42.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "44")).Text;
            //Label43.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "45")).Text;
            //Label44.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "46")).Text;

            Label045.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "108")).Text;
            Label0045.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "109")).Text;
            Label45.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "5")).Text;
            //LblLearnMore.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "6")).Text;

            Label46.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "47")).Text;
            //Label47.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "48")).Text;
            //Label48.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "49")).Text;
            //LblSecurity1.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "50")).Text;
            //Label49.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "51")).Text;
            //Label50.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "52")).Text;
            //Label51.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "53")).Text;
            //Label52.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "54")).Text;
            //Label53.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "55")).Text;
            //Label54.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "56")).Text;
            //Label55.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "57")).Text;
            Label56.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "58")).Text.Replace("{count}", "5");
            Label57.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "59")).Text;
            Label58.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "60")).Text;
            Label59.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "61")).Text;
            Label60.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "12")).Text;
            Label61.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "27")).Text;
            Label62.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "63")).Text;
            Label63.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "64")).Text;

            //Label64.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "7")).Text;

            //Label65.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "65")).Text;
            //Label66.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "57")).Text;
            Label67.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "66")).Text.Replace("{count}", "100");
            Label68.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "67")).Text.Replace("{count}", "10");

            Label69.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "60")).Text;
            Label70.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "61")).Text;
            Label71.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "12")).Text;
            Label72.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "27")).Text;
            Label73.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "63")).Text;
            Label74.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "64")).Text;
            Label075.Text = Label75.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "62")).Text;
            Label076.Text = Label76.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "68")).Text;

            //Label77.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "7")).Text;

            //Label78.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "69")).Text;
            //Label79.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "57")).Text;
            //Label80.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "70")).Text;

            Label81.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "66")).Text.Replace("{count}", "250");
            Label82.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "67")).Text.Replace("{count}", "20");
            Label83.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "60")).Text;
            Label84.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "61")).Text;
            Label85.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "12")).Text;
            Label86.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "27")).Text;
            Label87.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "63")).Text;
            Label88.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "64")).Text;
            Label89.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "71")).Text;
            Label90.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "68")).Text;

            //Label91.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "7")).Text;

            //Label92.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "72")).Text;
            //Label092.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "112")).Text;

            //Label93.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "8")).Text;
            //BtnSend.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "9")).Text;

            Label94.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "73")).Text;
            Label95.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "74")).Text;
            Label96.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "75")).Text;
            Label97.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "76")).Text;
            Label98.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "77")).Text;
            Label99.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "78")).Text;
            Label100.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "79")).Text;
            Label101.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "80")).Text;
            Label102.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "81")).Text;
            Label103.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "82")).Text;
            Label104.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "83")).Text;
            Label105.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "84")).Text;
            //Label106.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "85")).Text;
            //Label107.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "86")).Text;
            //Label108.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "87")).Text;
            Label109.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "88")).Text;
            Label110.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "89")).Text;
            Label111.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "90")).Text;
            //Label112.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "91")).Text;
            //Label113.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "92")).Text;
            //Label114.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "93")).Text;
            //Label115.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "94")).Text;
            //Label116.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "95")).Text;
            //Label117.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "96")).Text;

            //Label118.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "10")).Text;

            //Label119.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "97")).Text;
            //Label120.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "98")).Text;
            //Label121.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "99")).Text;
            //Label122.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "100")).Text;
            //Label123.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "101")).Text;
            //Label124.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "102")).Text;
            //Label125.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "103")).Text;
            //Label126.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "104")).Text;
            //Label127.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "105")).Text;
            //Label128.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "106")).Text;
            //Label129.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "107")).Text;

            //Literal1.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "1")).Text;
            //Literal2.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "2")).Text;
            //Literal3.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "3")).Text;
            //Literal4.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "4")).Text;
            //Literal5.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "5")).Text;
            //Literal6.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "6")).Text;
            //Literal7.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "7")).Text;
            //Literal8.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "literal", "8")).Text;

            //Literal9.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "9")).Text;
            //Literal10.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "10")).Text;
            //Literal11.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "11")).Text;
            //Literal12.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "12")).Text;

            //Literal13.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "11")).Text;
            //Literal14.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "12")).Text;
            //Literal15.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "13")).Text;
            //Literal16.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "14")).Text;
            //Literal17.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "button", "15")).Text;

            //Label132.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "6")).Text;

            //Literal18.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "13")).Text;
            //Literal19.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "14")).Text;
            //Literal20.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "15")).Text;
            //Literal21.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "16")).Text;
            //Literal22.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "literal", "17")).Text;

            Label133.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "110")).Text;
            Label134.Text = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("content", "prmmaster", "label", "111")).Text;

            //HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            if (metaDescription != null)
                metaDescription.Attributes["content"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "label", "1")).Text;

            if (metaKeywords != null)
                metaKeywords.Attributes["content"] = Localizer.GetTranslationResults((pageLang != "") ? pageLang : vSession.Lang, new XElementInfo("header", "prmmaster", "label", "2")).Text;
        }

        private void FixButtons()
        {
            if (vSession.User == null)
                aGetStartedFree.Visible = aGetStartedFreeB.Visible = aFree.Visible = aStartup.Visible = aGrowth.Visible = true;
            else
            {
                if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    aGetStartedFree.Visible = aGetStartedFreeB.Visible = aFree.Visible = aStartup.Visible = aGrowth.Visible = false;

                    aChekoutStartup.Visible = aChekoutGrowth.Visible = true;
                }
                else
                {
                    aGetStartedFree.Visible = aGetStartedFreeB.Visible = aFree.Visible = aStartup.Visible = aGrowth.Visible = false;
                    aChekoutStartup.Visible = aChekoutGrowth.Visible = false;
                }
            }
        }

        private void SetLinks()
        {
            aFree.HRef = aStartup.HRef = aGrowth.HRef = ControlLoader.Login;
            aGetStartedFree.HRef = aGetStartedFreeB.HRef = ControlLoader.SignUpPrm;
            
            aPrmSoftwareFeatures.HRef = ControlLoader.PrmSoftwarePartnerPortal;

            aDeals.HRef = ControlLoader.PrmSoftwareDealRegistration;
            aLeads.HRef = ControlLoader.PrmSoftwareLeadDistribution;
            aIntegrations.HRef = ControlLoader.PrmSoftwareCrmIntegrations;
            aContentManagement.HRef = ControlLoader.PrmSoftwareContentManagement;

            aContactUs.HRef = ControlLoader.ContactUs;
            //aPrmSoftwareFeatures9.HRef = ControlLoader.PrmSoftwarePartnerActivation;
            //aPrmSoftwareFeatures10.HRef = ControlLoader.PrmSoftwarePartnerTierManagement;
            //aPrmSoftwareFeatures11.HRef = ControlLoader.PrmSoftwarePartnerTeamRoles;
            //aPrmSoftwareFeatures12.HRef = ControlLoader.PrmSoftwarePartnerManagement;
            //aImpartner.HRef = ControlLoader.ResourcesAlternativesImpartnerPage;
            //aPartnerStack.HRef = ControlLoader.ResourcesAlternativesPartnerStackPage;
            //aAllbound.HRef = ControlLoader.ResourcesAlternativesAllboundPage;
            //aZiftSolutions.HRef = ControlLoader.ResourcesAlternativesZiftSolutionsPage;
            //aSalesforcePRM.HRef = ControlLoader.ResourcesAlternativesSalesforceCommunitiesPage;
            //aMagentrix.HRef = ControlLoader.ResourcesAlternativesMindmatrixPage;
            //aMyprm.HRef = ControlLoader.ResourcesAlternativesMyPrmPage;            
            //aLearnMore.HRef = ControlLoader.PrmSoftwareCrmIntegrations;

            aIntegrationHubspot.HRef = ControlLoader.PrmSoftwareHubspotIntegration;
            aIntegrationSalesforce.HRef = ControlLoader.PrmSoftwareSalesforceIntegration;
            aIntegrationDynamics.HRef = ControlLoader.PrmSoftwareCrmIntegrations;

            //aFrPrmSoftware.HRef = ControlLoader.PrmSoftwareFR;
            //aEsPrmSoftware.HRef = ControlLoader.PrmSoftwareES;
            //aDePrmSoftware.HRef = ControlLoader.PrmSoftwareDE;
            //aPtPrmSoftware.HRef = ControlLoader.PrmSoftwarePT;
            //aArPrmSoftware.HRef = ControlLoader.PrmSoftwareAR;
        }

        private void LoadClients()
        {
            try
            {
                List<ElioUsers> clients = GlobalDBMethods.GetHomePageClientsProfiles("39966,45583,47524,52356,53141", session);
                if (clients.Count > 0 && clients.Count == 5)
                {
                    //ImgPartner1.ImageUrl = clients[5].CompanyLogo;
                    //ImgPartner1.AlternateText = clients[5].CompanyName;
                    //ImgPartner4.ImageUrl = clients[0].CompanyLogo;
                    //ImgPartner4.AlternateText = clients[0].CompanyName;
                    //ImgPartner5.ImageUrl = clients[4].CompanyLogo;
                    //ImgPartner5.AlternateText = clients[4].CompanyName;
                    //ImgPartner2.ImageUrl = clients[1].CompanyLogo;
                    //ImgPartner2.AlternateText = clients[1].CompanyName;
                    //ImgPartner3.ImageUrl = clients[2].CompanyLogo;
                    //ImgPartner3.AlternateText = clients[2].CompanyName;
                    //ImgPartner6.ImageUrl = clients[3].CompanyLogo;
                    //ImgPartner6.AlternateText = clients[3].CompanyName;

                    aClient1.HRef = "/profiles/" + clients[0].CompanyType.ToLower() + "/" + clients[0].Id + "/" + Regex.Replace(clients[0].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient4.HRef = "/profiles/" + clients[2].CompanyType.ToLower() + "/" + clients[2].Id + "/" + Regex.Replace(clients[2].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient5.HRef = "/profiles/" + clients[3].CompanyType.ToLower() + "/" + clients[3].Id + "/" + Regex.Replace(clients[3].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient2.HRef = "/profiles/" + clients[1].CompanyType.ToLower() + "/" + clients[1].Id + "/" + Regex.Replace(clients[1].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    //aClient3.HRef = "/profiles/" + clients[2].CompanyType.ToLower() + "/" + clients[2].Id + "/" + Regex.Replace(clients[2].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    aClient6.HRef = "/profiles/" + clients[4].CompanyType.ToLower() + "/" + clients[4].Id + "/" + Regex.Replace(clients[4].CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();

                    if (aClient1.HRef.EndsWith("-"))
                        aClient1.HRef = aClient1.HRef.Substring(0, aClient1.HRef.Length - 1);
                    if (aClient4.HRef.EndsWith("-"))
                        aClient4.HRef = aClient4.HRef.Substring(0, aClient4.HRef.Length - 1);
                    if (aClient5.HRef.EndsWith("-"))
                        aClient5.HRef = aClient5.HRef.Substring(0, aClient5.HRef.Length - 1);
                    if (aClient2.HRef.EndsWith("-"))
                        aClient2.HRef = aClient2.HRef.Substring(0, aClient2.HRef.Length - 1);
                    //if (aClient3.HRef.EndsWith("-"))
                    //    aClient3.HRef = aClient3.HRef.Substring(0, aClient3.HRef.Length - 1);
                    if (aClient6.HRef.EndsWith("-"))
                        aClient6.HRef = aClient6.HRef.Substring(0, aClient6.HRef.Length - 1);
                }
                else
                {
                    //ImgPartner1.ImageUrl = "/Images/Logos/a0cff7cb-37b8-46ed-b56d-61cddbee7a09/iri-logo-anniversary-2018-750px-white-bg_8_1_2018_12345.png";
                    //ImgPartner2.ImageUrl = "/Images/Logos/d3f7053c-ee32-4053-9f73-9b90fe953cb4/logo-contentverse-by-computhink_10_4_2017_12345.png";
                    //ImgPartner3.ImageUrl = "/Images/Logos/cf85f912-83b3-4890-b60d-90319f5eb7b8/mon_elioplus_logo_27_1_2017_12345.png";
                    //ImgPartner4.ImageUrl = "/Images/Logos/d24fc38f-a7a3-4633-85a4-43b64de87649/csslogo-400_25_6_2016_12345.jpg";
                    //ImgPartner5.ImageUrl = "/Images/Logos/4d65eb5d-a68b-4508-a63f-f4d1fed5f426/IglooLogo_Green_16_1_2017_12345.png";
                    //ImgPartner6.ImageUrl = "/Images/Logos/95a126cb-3ffc-4c8a-9a13-56e9c288666d/spinbackup-logo%20(1).jpg_15_6_2016_12345.jpeg";
                    aClient1.HRef = "/profiles/vendors/2226/iri-the-cosort-company";
                    aClient2.HRef = "/profiles/vendors/4297/computhink-inc-";
                    //aClient3.HRef = "profiles/vendors/5898/monitis";
                    aClient4.HRef = "/profiles/vendors/2747/christiansteven-software";
                    aClient5.HRef = "profiles/vendors/5708/igloo-software";
                    aClient6.HRef = "/profiles/vendors/2386/spinbackup";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool CheckData()
        {
            bool isError = false;

            //if (string.IsNullOrEmpty(TbxFirstName.Text))
            //{
            //    LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "1")).Text;
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxLastName.Text))
            //{
            //    LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "2")).Text;
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxCompanyName.Text))
            //{
            //    LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "3")).Text;
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}

            //if (string.IsNullOrEmpty(TbxEmail.Text))
            //{
            //    LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "4")).Text;
            //    divWarningMessage.Visible = true;
            //    return isError = true;
            //}
            //else
            //{
            //    if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
            //    {
            //        LblWarningMessage.Text = "Access denied";
            //        divWarningMessage.Visible = true;
            //        BtnSend.Enabled = false;
            //        return isError = true;
            //    }

            //    if (!Validations.IsEmail(TbxEmail.Text))
            //    {
            //        LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "5")).Text;
            //        divWarningMessage.Visible = true;
            //        return isError = true;
            //    }
            //}

            return isError;
        }

        private void ClearMessageData()
        {
            //divWarningMessage.Visible = false;
            //divSuccessMessage.Visible = false;

            //TbxFirstName.Text = "";
            //TbxLastName.Text = "";
            //TbxCompanyName.Text = "";
            //TbxEmail.Text = "";
        }

        private void CheckOut(int packId, DBSession session)
        {
            ElioUsers user = vSession.User;

            ElioPackets packet = Sql.GetPacketById(packId, session);
            if (packet != null)
            {
                bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                if (!setEpiredSess)
                {
                    Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                    return;
                }

                Session chSession = null;

                ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCouponByUserPlanId(user.Id, packet.stripePlanId, session);

                if (planCoupon != null)
                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerWithDiscountApi(packet.stripePlanId, planCoupon.CouponId, user);
                else
                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi(packet.stripePlanId, user, false);

                if (chSession != null)
                {
                    StripeUsersCheckoutSessions uSession = new StripeUsersCheckoutSessions();

                    uSession.UserId = user.Id;
                    uSession.StripePlanId = packet.stripePlanId;
                    uSession.CheckoutSessionId = chSession.Id;
                    uSession.CheckoutUrl = chSession.Url;
                    uSession.DateCreated = DateTime.Now;
                    uSession.LastUpdate = DateTime.Now;

                    DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
                    loader.Insert(uSession);

                    Response.Redirect(chSession.Url, false);
                }
            }
        }

        #endregion

        #region Buttons

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                bool isError = CheckData();

                if (isError) return;

                try
                {
                    //EmailSenderLib.ContactElioplusForDemoRequest(TbxFirstName.Text, TbxLastName.Text, TbxCompanyName.Text, TbxEmail.Text, "", vSession.Lang, session);
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                    //divWarningMessage.Visible = true;
                    //LblWarningMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "6")).Text;
                    return;
                }

                //ClearMessageData();

                //LblSuccessMessage.Text = Localizer.GetTranslationResults((Trans != "") ? Trans : vSession.Lang, new XElementInfo("content", "prmmaster", "message", "7")).Text;
                //divSuccessMessage.Visible = true;
                //BtnSend.Enabled = false;
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

        protected void aChekoutGrowth_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(9, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutStartup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(8, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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
    }
}
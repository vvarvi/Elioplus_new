using System;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls
{
    public partial class FeatureCompanies : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();                

                LoadFeaturedCompanies();

                UpdateStrings();
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
            LblPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "featurecompanies", "label", "1")).Text;
        }

        private void LoadFeaturedCompanies()
        {
            List<ElioUsers> fCompanies = Sql.GetFeaturedCompanies(session);
            if(fCompanies.Count==6)
            {
                //Img1.ImageUrl = fCompanies[0].CompanyLogo;
                //HdnCompany1.Value = fCompanies[0].Id.ToString();
                Img1.ImageUrl = "~/images/1mashape_logo_260px.png";
                //Img2.ImageUrl = fCompanies[1].CompanyLogo;
                //HdnCompany2.Value = fCompanies[1].Id.ToString();
                Img2.ImageUrl = "~/images/1movethechannel.jpg";
                Img3.ImageUrl = fCompanies[2].CompanyLogo;
                HdnCompany3.Value = fCompanies[2].Id.ToString();
                Img4.ImageUrl = fCompanies[3].CompanyLogo;
                HdnCompany4.Value = fCompanies[3].Id.ToString();
                Img5.ImageUrl = fCompanies[4].CompanyLogo;
                HdnCompany5.Value = fCompanies[4].Id.ToString();
                Img6.ImageUrl = fCompanies[5].CompanyLogo;
                HdnCompany6.Value = fCompanies[5].Id.ToString();
            }
        }

        private void LoadVendorDetails(int companyId)
        {
            vSession.ElioCompanyDetailsView = Sql.GetUserById(companyId, session);
            if (vSession.ElioCompanyDetailsView != null)
            {
                if (vSession.User.Id != vSession.ElioCompanyDetailsView.Id)
                {
                    GlobalDBMethods.AddCompanyViews(vSession.User, vSession.ElioCompanyDetailsView, vSession.Lang, session);
                }

                Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
            }
        }

        #endregion

        #region Buttons

        protected void ImgBtn1_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany1.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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

        protected void ImgBtn2_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany2.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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

        protected void ImgBtn3_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany3.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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

        protected void ImgBtn4_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany4.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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

        protected void ImgBtn5_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany5.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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

        protected void ImgBtn6_Onclick(object sedern, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadVendorDetails(Convert.ToInt32(HdnCompany6.Value));
                }
                else
                {
                    RadWindowManager1.RadAlert("Sign up to see more details of this and unlimited other company profiles. It`s completely Free and it takes less than 30 seconds.", 560, 180, "Create your profile now", null);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using Telerik.Web.UI;

namespace WdS.ElioPlus
{
    public partial class DashboardAlgorithm : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    session.OpenConnection();

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.PageDash404, false);
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

        # region methods

        private void FixPage()
        {
            BtnRunAlgorithm.Text = "Run algorithm";
            
        }

        protected DataTable GetUserDataTable()
        {
            DataTable userDt = new DataTable();

            try
            {
                session.OpenConnection();

                # region Create DataTable columns

                userDt.Columns.Add("id");
                userDt.Columns.Add("company_name");
                userDt.Columns.Add("company_type");
                userDt.Columns.Add("subcategories");
                userDt.Columns.Add("billing_type");
                userDt.Columns.Add("fee");
                userDt.Columns.Add("revenues");
                userDt.Columns.Add("support");
                userDt.Columns.Add("company_years");
                userDt.Columns.Add("program_years");
                userDt.Columns.Add("partners");
                userDt.Columns.Add("tiers");
                userDt.Columns.Add("training");
                userDt.Columns.Add("free_training");
                userDt.Columns.Add("material");
                userDt.Columns.Add("certification");
                userDt.Columns.Add("localization");
                userDt.Columns.Add("mdf");
                userDt.Columns.Add("portal");
                userDt.Columns.Add("country");

                # endregion

                # region Get User Info, Subcategories and Criteria

                # region Get All Users from Matches

                List<ElioAlgorithmMatches> uniqueUsers = Sql.GetUniqueMatches(session);

                # endregion

                foreach (ElioAlgorithmMatches uniqueUser in uniqueUsers)
                {
                    # region Get User Info

                    ElioUsers userInfo = Sql.GetUserById(uniqueUser.UserId, session);

                    # endregion

                    # region Get User Subcategories

                    List<ElioAlgSubcatIJGroupItems> userAlgSubcat = Sql.GetSubcatDescriptionByUserId(uniqueUser.UserId, session);


                    string userSubcat = "";

                    foreach (ElioAlgSubcatIJGroupItems sub in userAlgSubcat)
                    {
                        userSubcat += sub.Description + ", ";
                    }

                    if (userAlgSubcat.Count > 0)
                    {
                        userSubcat = userSubcat.Substring(0, userSubcat.Length - 2);
                    }

                    # endregion

                    # region Create Criteria strings

                    string fee = "-";
                    string revenues = "-";
                    string support = "";
                    string companyYears = "-";
                    string programYears = "-";
                    string partners = "-";
                    string tiers = "-";
                    string training = "-";
                    string freeTraining = "-";
                    string material = "-";
                    string certification = "-";
                    string localization = "-";
                    string mdf = "-";
                    string portal = "-";
                    string country = "";

                    # endregion

                    # region Get User Criteria

                    List<ElioUsersCriteria> userCriteria = Sql.GetUserCriteria(uniqueUser.UserId, session);

                    # endregion

                    # region Check User Criteria

                    foreach (ElioUsersCriteria userCrit in userCriteria)
                    {
                        switch (userCrit.CriteriaId)
                        {
                            case 1:
                                {
                                    companyYears = userCrit.CriteriaValue;
                                    break;
                                }
                            case 2:
                                {
                                    programYears = userCrit.CriteriaValue;
                                    break;
                                }
                            case 3:
                                {
                                    partners = userCrit.CriteriaValue;
                                    break;
                                }
                            case 4:
                                {
                                    tiers = userCrit.CriteriaValue;
                                    break;
                                }
                            case 5:
                                {
                                    training = userCrit.CriteriaValue;
                                    break;
                                }
                            case 6:
                                {
                                    freeTraining = userCrit.CriteriaValue;
                                    break;
                                }
                            case 7:
                                {
                                    material = userCrit.CriteriaValue;
                                    break;
                                }
                            case 8:
                                {
                                    certification = userCrit.CriteriaValue;
                                    break;
                                }
                            case 9:
                                {
                                    localization = userCrit.CriteriaValue;
                                    break;
                                }
                            case 10:
                                {
                                    mdf = userCrit.CriteriaValue;
                                    break;
                                }
                            case 11:
                                {
                                    portal = userCrit.CriteriaValue;
                                    break;
                                }
                            case 12:
                                {
                                    fee = userCrit.CriteriaValue;
                                    break;
                                }
                            case 13:
                                {
                                    revenues = userCrit.CriteriaValue;
                                    break;
                                }
                            case 14:
                                {
                                    support += userCrit.CriteriaValue + ", ";
                                    break;
                                }
                            case 15:
                                {
                                    country += userCrit.CriteriaValue + ", ";
                                    break;
                                }
                        }
                    }

                    if (userCriteria.Count > 0)
                    {
                        support = support.Substring(0, support.Length - 2);
                    }

                    if (string.IsNullOrEmpty(country))
                    {
                        country = userInfo.Country;
                    }
                    else
                    {
                        country = country.Substring(0, country.Length - 2);
                    }

                    # endregion

                    # region Fill Table rows

                    userDt.Rows.Add(userInfo.Id, userInfo.CompanyName, userInfo.CompanyType, userSubcat, userInfo.BillingType, fee, revenues, support, companyYears, programYears, partners, tiers, training, freeTraining, material, certification, localization, mdf, portal, country);

                    # endregion
                }

                # endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }

            return userDt;
        }

        protected DataTable GetMatchDataTable(int matchUserId)
        {
            DataTable matchesDt = new DataTable();

            try
            {
                session.OpenConnection();

                # region Create DataTable columns

                matchesDt.Columns.Add("m_id");
                matchesDt.Columns.Add("m_company_name");
                matchesDt.Columns.Add("m_company_type");
                matchesDt.Columns.Add("m_subcategories");
                matchesDt.Columns.Add("m_billing_type");
                matchesDt.Columns.Add("m_fee");
                matchesDt.Columns.Add("m_revenues");
                matchesDt.Columns.Add("m_support");
                matchesDt.Columns.Add("m_company_years");
                matchesDt.Columns.Add("m_program_years");
                matchesDt.Columns.Add("m_partners");
                matchesDt.Columns.Add("m_tiers");
                matchesDt.Columns.Add("m_training");
                matchesDt.Columns.Add("m_free_training");
                matchesDt.Columns.Add("m_material");
                matchesDt.Columns.Add("m_certification");
                matchesDt.Columns.Add("m_localization");
                matchesDt.Columns.Add("m_mdf");
                matchesDt.Columns.Add("m_portal");
                matchesDt.Columns.Add("m_country");

                # endregion

                # region Get Match Info, Subcategories and Criteria

                # region Get All Matches of User

                List<ElioAlgorithmMatches> matchUsers = Sql.GetUniqueMatchesById(matchUserId, session);

                # endregion

                foreach (ElioAlgorithmMatches matchUser in matchUsers)
                {
                    # region Get Match Info

                    ElioUsers matchInfo = Sql.GetUserById(matchUser.MatchId, session);

                    # endregion

                    # region Get Match Subcategories

                    List<ElioSubIndustriesGroupItems> matchProfSubcat = Sql.GetUserSubIndustries(matchUser.MatchId, session);
                    List<ElioAlgSubcatIJGroupItems> matchAlgSubcat = Sql.GetSubcatDescriptionByUserId(matchUser.MatchId, session);

                    string matchSubcat = "";

                    if (matchInfo.CompanyType == "Vendors")
                    {
                        foreach (ElioAlgSubcatIJGroupItems sub in matchAlgSubcat)
                        {
                            matchSubcat += sub.Description + ", ";
                        }

                        if (matchAlgSubcat.Count > 0)
                        {
                            matchSubcat = matchSubcat.Substring(0, matchSubcat.Length - 2);
                        }
                    }
                    else
                    {
                        foreach (ElioSubIndustriesGroupItems sub in matchProfSubcat)
                        {
                            matchSubcat += sub.Description + ", ";
                        }

                        if (matchAlgSubcat.Count > 0)
                        {
                            matchSubcat = matchSubcat.Substring(0, matchSubcat.Length - 2);
                        }
                    }

                    # endregion

                    # region Create Criteria strings

                    string mFee = "-";
                    string mRevenues = "-";
                    string mSupport = "";
                    string mCompanyYears = "-";
                    string mProgramYears = "-";
                    string mPartners = "-";
                    string mTiers = "-";
                    string mTraining = "-";
                    string mFreeTraining = "-";
                    string mMaterial = "-";
                    string mCertification = "-";
                    string mLocalization = "-";
                    string mMdf = "-";
                    string mPortal = "-";
                    string mCountry = "";

                    # endregion

                    # region Get Match Criteria

                    List<ElioUsersCriteria> matchCriteria = Sql.GetUserCriteria(matchUser.MatchId, session);

                    # endregion

                    # region Check Match Criteria

                    foreach (ElioUsersCriteria matchCrit in matchCriteria)
                    {
                        switch (matchCrit.CriteriaId)
                        {
                            case 1:
                                {
                                    mCompanyYears = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 2:
                                {
                                    mProgramYears = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 3:
                                {
                                    mPartners = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 4:
                                {
                                    mTiers = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 5:
                                {
                                    mTraining = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 6:
                                {
                                    mFreeTraining = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 7:
                                {
                                    mMaterial = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 8:
                                {
                                    mCertification = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 9:
                                {
                                    mLocalization = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 10:
                                {
                                    mMdf = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 11:
                                {
                                    mPortal = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 12:
                                {
                                    mFee = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 13:
                                {
                                    mRevenues = matchCrit.CriteriaValue;
                                    break;
                                }
                            case 14:
                                {
                                    mSupport += matchCrit.CriteriaValue + ", ";
                                    break;
                                }
                            case 15:
                                {
                                    mCountry += matchCrit.CriteriaValue + ", ";
                                    break;
                                }
                        }
                    }

                    if (matchCriteria.Count > 0)
                    {
                        mSupport = mSupport.Substring(0, mSupport.Length - 2);
                    }

                    if (string.IsNullOrEmpty(mCountry))
                    {
                        mCountry = matchInfo.Country;
                    }
                    else
                    {
                        mCountry = mCountry.Substring(0, mCountry.Length - 2);
                    }

                    # endregion

                    # region Fill Table rows

                    matchesDt.Rows.Add(matchInfo.Id, matchInfo.CompanyName, matchInfo.CompanyType, matchSubcat, matchInfo.BillingType, mFee, mRevenues, mSupport, mCompanyYears, mProgramYears, mPartners, mTiers, mTraining, mFreeTraining, mMaterial, mCertification, mLocalization, mMdf, mPortal, mCountry);

                    # endregion
                }

                # endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }

            return matchesDt;
        }

        # endregion

        # region Buttons

        protected void BtnRunAlgorithm_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LblMatchInfo.Visible = false;

                ElioAlgorithmMatches algorithmMatch = new ElioAlgorithmMatches();
                DataLoader<ElioAlgorithmMatches> algMatchLoader = new DataLoader<ElioAlgorithmMatches>(session);

                # region Get All Algorithm Users

                List<ElioUsersAlgorithmSubcategories> uniqueAlgorithmUsers = Sql.GetUniqueUsersAlgorithmSubcategories(session);

                # endregion

                foreach (ElioUsersAlgorithmSubcategories currentUser in uniqueAlgorithmUsers)
                {
                    # region Get User Info, Subcategories and Criteria

                    ElioUsers currentUserInfo = Sql.GetUserById(currentUser.UserId, session);

                    if (currentUserInfo != null)
                    {
                        List<ElioUsersAlgorithmSubcategories> currentUserAlgSub = Sql.GetUserAlgorithmSubcategoriesById(currentUser.UserId, session);
                        List<ElioUsersCriteria> currentUserCriteria = Sql.GetUserCriteria(currentUser.UserId, session);

                        # region Create list with User algorithm Subcategories

                        var curUserAlgSubList = new List<int>();
                        foreach (ElioUsersAlgorithmSubcategories sub in currentUserAlgSub)
                        {
                            curUserAlgSubList.Add(sub.SubcategoryId);
                        }

                        # endregion

                        # region Create string with User possible partners

                        string currentUserPossiblePartners = "";

                        foreach (ElioUsersAlgorithmSubcategories possiblePartner in uniqueAlgorithmUsers)
                        {
                            currentUserPossiblePartners += possiblePartner.UserId + ",";
                        }

                        if (uniqueAlgorithmUsers.Count > 0)
                        {
                            currentUserPossiblePartners = currentUserPossiblePartners.Substring(0, currentUserPossiblePartners.Length - 1);
                        }

                        # endregion

                        # region Create string with User opposite company type

                        string oppositeCompanyType = "";

                        oppositeCompanyType = currentUserInfo.CompanyType == Types.Vendors.ToString() ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();

                        # endregion

                        # region Get User candidate partners

                        List<ElioUsers> candidatePartners = Sql.GetOpportunitiesByCompanyType(currentUserPossiblePartners, oppositeCompanyType, session);

                        # endregion

                        foreach (ElioUsers possibleMatch in candidatePartners)
                        {
                            # region Get possible Match Criteria

                            List<ElioUsersCriteria> possibleMatchCriteria = Sql.GetUserCriteria(possibleMatch.Id, session);

                            # endregion

                            # region Get possible Match Subcategories in list

                            var posMatchSubList = new List<int>();

                            if (possibleMatch.CompanyType == "Resellers")
                            {
                                List<ElioUsersSubIndustriesGroupItems> possibleMatchProfSub = Sql.GetUserProfileSubcategoriesId(possibleMatch.Id, session);

                                foreach (ElioUsersSubIndustriesGroupItems sub in possibleMatchProfSub)
                                {
                                    posMatchSubList.Add(sub.SubIndustryGroupItemId);
                                }
                            }
                            else
                            {
                                List<ElioUsersAlgorithmSubcategories> possibleMatchAlgSub = Sql.GetUserAlgorithmSubcategoriesById(possibleMatch.Id, session);

                                foreach (ElioUsersAlgorithmSubcategories sub in possibleMatchAlgSub)
                                {
                                    posMatchSubList.Add(sub.SubcategoryId);
                                }
                            }

                            # endregion

                            # region Check User and possible Match common Subcategories

                            bool hasCommonSubcategory = curUserAlgSubList.Intersect(posMatchSubList).Any();

                            bool match = hasCommonSubcategory ? true : false;

                            # endregion

                            # region Check User and possible Match common Criteria

                            if (hasCommonSubcategory)
                            {
                                foreach (ElioUsersCriteria curUserCrit in currentUserCriteria)
                                {
                                    if (!match)
                                    {
                                        break;
                                    }

                                    switch (curUserCrit.CriteriaId)
                                    {
                                        case 1:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 1)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "1 - 5")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "6 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "6 +")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "1 - 5" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 2)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "1 - 3")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "4 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "4 +")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "1 - 3" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 3)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "< 50")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "50 - 100" || posMatchCrit.CriteriaValue == "101 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "50 - 100")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "< 50" || posMatchCrit.CriteriaValue == "101 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "101 +")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "< 50" || posMatchCrit.CriteriaValue == "50 - 100" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 4:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 4)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "1")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "2" || posMatchCrit.CriteriaValue == "3 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "2")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "1" || posMatchCrit.CriteriaValue == "3 +" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "3 +")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "1" || posMatchCrit.CriteriaValue == "2" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 5:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 5)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 6:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 6)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 7:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 7)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 8:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 8)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 9:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 9)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 10:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 10)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 11:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 11)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 12:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 12)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Yes")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "No" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "No")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Yes" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        case 13:
                                            {
                                                foreach (ElioUsersCriteria posMatchCrit in possibleMatchCriteria)
                                                {
                                                    if (posMatchCrit.CriteriaId == 13)
                                                    {
                                                        if (curUserCrit.CriteriaValue == "Commission")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Annual fee" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else if (curUserCrit.CriteriaValue == "Annual fee")
                                                        {
                                                            match = posMatchCrit.CriteriaValue == "Commission" ? false : true;
                                                            if (!match)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                    }
                                }

                                if (match)
                                {
                                    var curUserSupCri = new List<string>();
                                    foreach (ElioUsersCriteria curUserCri in currentUserCriteria)
                                    {
                                        if (curUserCri.CriteriaId == 14)
                                        {
                                            curUserSupCri.Add(curUserCri.CriteriaValue);
                                        }
                                    }

                                    var posMatchSupCri = new List<string>();
                                    foreach (ElioUsersCriteria posMatchCri in possibleMatchCriteria)
                                    {
                                        if (posMatchCri.CriteriaId == 14)
                                        {
                                            posMatchSupCri.Add(posMatchCri.CriteriaValue);
                                        }
                                    }

                                    if (curUserSupCri.Contains("Indifferent"))
                                    {
                                        match = true;
                                    }
                                    else if (posMatchSupCri.Contains("Indifferent"))
                                    {
                                        match = true;
                                    }
                                    else if (curUserSupCri.Intersect(posMatchSupCri).Any())
                                    {
                                        match = true;
                                    }
                                    else
                                    {
                                        match = false;
                                    }
                                }

                                if (match)
                                {
                                    if (currentUserInfo.CompanyType == "Resellers")
                                    {
                                        ElioCountries curUserRegion = Sql.GetRegionByCountryName(currentUserInfo.Country, session);
                                        var posMatchCountryCri = new List<string>();
                                        foreach (ElioUsersCriteria posMatchCri in possibleMatchCriteria)
                                        {
                                            if (posMatchCri.CriteriaId == 15)
                                            {
                                                posMatchCountryCri.Add(posMatchCri.CriteriaValue);
                                            }
                                        }

                                        if (posMatchCountryCri.Contains("Indifferent"))
                                        {
                                            match = true;
                                        }
                                        else if (posMatchCountryCri.Contains("Select All"))
                                        {
                                            match = true;
                                        }
                                        else if (posMatchCountryCri.Contains(curUserRegion.Region))
                                        {
                                            match = true;
                                        }
                                        else if (posMatchCountryCri.Contains(currentUserInfo.Country))
                                        {
                                            match = true;
                                        }
                                        else
                                        {
                                            match = false;
                                        }
                                    }
                                    else if (currentUserInfo.CompanyType == "Vendors")
                                    {
                                        ElioCountries posMatchRegion = Sql.GetRegionByCountryName(possibleMatch.Country, session);
                                        var curUserCountryCri = new List<string>();
                                        foreach (ElioUsersCriteria curUserCri in currentUserCriteria)
                                        {
                                            if (curUserCri.CriteriaId == 15)
                                            {
                                                curUserCountryCri.Add(curUserCri.CriteriaValue);
                                            }
                                        }

                                        if (curUserCountryCri.Contains("Indifferent"))
                                        {
                                            match = true;
                                        }
                                        else if (curUserCountryCri.Contains("Select All"))
                                        {
                                            match = true;
                                        }
                                        else if (curUserCountryCri.Contains(posMatchRegion.Region))
                                        {
                                            match = true;
                                        }
                                        else if (curUserCountryCri.Contains(possibleMatch.Country))
                                        {
                                            match = true;
                                        }
                                        else
                                        {
                                            match = false;
                                        }
                                    }
                                }
                            }

                            # endregion

                            # region Update database

                            bool exactMatch = match;

                            if (exactMatch)
                            {
                                bool existMatch = Sql.ExistMatch(currentUserInfo.Id, possibleMatch.Id, session);

                                if (!existMatch)
                                {
                                    algorithmMatch.UserId = currentUserInfo.Id;
                                    algorithmMatch.MatchId = possibleMatch.Id;
                                    algorithmMatch.SysDate = DateTime.Now;
                                    algorithmMatch.LastUpdated = DateTime.Now;

                                    algMatchLoader.Insert(algorithmMatch);
                                }
                                else
                                {
                                    ElioAlgorithmMatches existingMatch = Sql.GetMatchByUserAndMatchId(currentUserInfo.Id, possibleMatch.Id, session);
                                    existingMatch.LastUpdated = DateTime.Now;

                                    algMatchLoader.Update(existingMatch);
                                }
                            }
                            else
                            {
                                bool existMatch = Sql.ExistMatch(currentUserInfo.Id, possibleMatch.Id, session);

                                if (existMatch)
                                {
                                    Sql.DeleteMatch(currentUserInfo.Id, possibleMatch.Id, session);
                                }
                            }

                            # endregion
                        }
                    }

                    # endregion
                }

                LblMatchInfo.Text = "Successfull matching";
                LblMatchInfo.Visible = true;
                RdgConnections.Rebind();
            }
            catch (Exception ex)
            {
                LblMatchInfo.Text = "Something went wrong";
                LblMatchInfo.Visible = true;
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnAddCon_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Button btn = (Button)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                Label userID = (Label)ControlFinder.FindControlBackWards(item, "LblUID");
                Label matchID = (Label)ControlFinder.FindControlRecursive(item, "LblMID");
                Label userConnections = (Label)ControlFinder.FindControlBackWards(item, "LblConnections");
                Label lblConErrorMsg = (Label)ControlFinder.FindControlBackWards(item, "LblConErrorMessage");
                Button btnAddConnection = (Button)ControlFinder.FindControlRecursive(item, "BtnAddCon");

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(userID.Text), session);

                if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    ElioUserPacketStatus userStatus = Sql.GetUserPacketStatusFeatures(Convert.ToInt32(userID.Text), session);
                    DataLoader<ElioUserPacketStatus> userStatusLoader = new DataLoader<ElioUserPacketStatus>(session);

                    if (userStatus.AvailableConnectionsCount > 0)
                    {
                        ElioUsersConnections connection = new ElioUsersConnections();
                        DataLoader<ElioUsersConnections> connectionLoader = new DataLoader<ElioUsersConnections>(session);

                        bool existConnection = Sql.ExistConnection(Convert.ToInt32(userID.Text), Convert.ToInt32(matchID.Text), session);

                        if (!existConnection)
                        {
                            connection.UserId = Convert.ToInt32(userID.Text);
                            connection.ConnectionId = Convert.ToInt32(matchID.Text);
                            connection.SysDate = DateTime.Now;
                            connection.LastUpdated = DateTime.Now;
                            connection.CanBeViewed = 1;

                            connectionLoader.Insert(connection);

                            userStatus.AvailableConnectionsCount -= 1;

                            userStatusLoader.Update(userStatus);

                            btnAddConnection.Text = "Is Connection";

                            ElioUsers connectionUser = Sql.GetUserById(Convert.ToInt32(matchID.Text), session);

                            userConnections.Text += ", " + connectionUser.CompanyName;
                        }
                        else
                        {
                            lblConErrorMsg.Text = "This connection already exists";
                            lblConErrorMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblConErrorMsg.Text = "This user has no available connections";
                        lblConErrorMsg.Visible = true;
                    }
                }
                else
                {
                    lblConErrorMsg.Text = "This user is not premium";
                    lblConErrorMsg.Visible = true;
                }

                btnAddConnection.Focus();
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

        # endregion

        # region Grids

        protected void RdgConnections_OnNeedDataSource(object sender, EventArgs args)
        {
            RdgConnections.DataSource = GetUserDataTable();
        }

        protected void RdgConnections_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = e.Item as GridDataItem;

                    RadGrid rdg = item.FindControl("RdgMatches") as RadGrid;
                    if (rdg != null)
                    {
                        Label lblUserId = (Label)ControlFinder.FindControlRecursive(RdgConnections, "LblUserId");
                        if (lblUserId != null)
                        {
                            Label lblConnections = (Label)ControlFinder.FindControlRecursive(item, "LblConnections");
                            if (lblConnections != null)
                            {
                                if (!string.IsNullOrEmpty(lblUserId.Text))
                                {
                                    List<ElioUsersConnections> userConnections = Sql.GetUserConnections(Convert.ToInt32(lblUserId.Text), session);

                                    foreach (ElioUsersConnections con in userConnections)
                                    {
                                        ElioUsers user = Sql.GetUserById(con.ConnectionId, session);

                                        lblConnections.Text += user.CompanyName + ", ";
                                    }

                                    if (userConnections.Count > 0)
                                    {
                                        lblConnections.Text = lblConnections.Text.Substring(0, lblConnections.Text.Length - 2);
                                    }

                                    rdg.DataSource = GetMatchDataTable(Convert.ToInt32(lblUserId.Text));
                                }
                                else
                                {
                                    //error
                                }
                            }
                            else
                            {
                                //error
                            }
                        }
                        else
                        {
                            //error
                        }
                    }
                    else
                    {
                        rdg.DataSource = null;
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

        protected void RdgMatches_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Button btnAddCon = (Button)ControlFinder.FindControlRecursive(item, "BtnAddCon");
                    Label userID = (Label)ControlFinder.FindControlBackWards(item, "LblUID");
                    Label matchID = (Label)ControlFinder.FindControlRecursive(item, "LblMID");

                    bool existConnection = Sql.ExistConnection(Convert.ToInt32(userID.Text), Convert.ToInt32(matchID.Text), session);

                    btnAddCon.Text = existConnection ? "Is Connection" : "Add Connection";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion
    }
}
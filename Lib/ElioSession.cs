using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.LoadControls;
using System.Data;

namespace WdS.ElioPlus.Lib
{
    public class ElioSession
    {
        //public bool IsConnectionOpen
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["IsConnectionOpen"] == null)
        //            HttpContext.Current.Session["IsConnectionOpen"] = false;
        //        return (bool)HttpContext.Current.Session["IsConnectionOpen"];
        //    }
        //    set { HttpContext.Current.Session["IsConnectionOpen"] = value; }
        //}

        public List<ConnectionSearchList> ConnectionsList
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["ConnectionsList"] == null)
                    HttpContext.Current.Session["ConnectionsList"] = null;

                return (List<ConnectionSearchList>)HttpContext.Current.Session["ConnectionsList"];
            }
            set { HttpContext.Current.Session["ConnectionsList"] = value; }
        }

        public List<ElioUsersSearchInfo> UsersSearchInfoList
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["UsersSearchInfoList"] == null)
                    HttpContext.Current.Session["UsersSearchInfoList"] = null;

                return (List<ElioUsersSearchInfo>)HttpContext.Current.Session["UsersSearchInfoList"];
            }
            set { HttpContext.Current.Session["UsersSearchInfoList"] = value; }
        }

        public TrainingCategory Category
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["Category"] == null)
                    HttpContext.Current.Session["Category"] = null;

                return (TrainingCategory)HttpContext.Current.Session["Category"];
            }
            set { HttpContext.Current.Session["Category"] = value; }
        }

        public int IntentSignalsSelectedTab
        {
            get
            {
                if (HttpContext.Current.Session["IntentSignalsSelectedTab"] == null)
                    HttpContext.Current.Session["IntentSignalsSelectedTab"] = 1;
                return (int)HttpContext.Current.Session["IntentSignalsSelectedTab"];
            }
            set { HttpContext.Current.Session["IntentSignalsSelectedTab"] = value; }
        }

        public string SubAccountEmailLogin
        {
            get
            {
                if (HttpContext.Current.Session["SubAccountEmailLogin"] == null)
                    HttpContext.Current.Session["SubAccountEmailLogin"] = "";
                return HttpContext.Current.Session["SubAccountEmailLogin"].ToString();
            }
            set { HttpContext.Current.Session["SubAccountEmailLogin"] = value; }
        }
        
        public int LoggedInSubAccountRoleID
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInSubAccountRoleID"] == null)
                    HttpContext.Current.Session["LoggedInSubAccountRoleID"] = 0;
                return (int)HttpContext.Current.Session["LoggedInSubAccountRoleID"];
            }
            set { HttpContext.Current.Session["LoggedInSubAccountRoleID"] = value; }
        }

        public bool IsAdminRole
        {
            get
            {
                if (HttpContext.Current.Session["IsAdminRole"] == null)
                    HttpContext.Current.Session["IsAdminRole"] = false;
                return (bool)HttpContext.Current.Session["IsAdminRole"];
            }
            set { HttpContext.Current.Session["IsAdminRole"] = value; }
        }

        public ElioUsers User
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["User"] == null)
                    HttpContext.Current.Session["User"] = null;

                return (ElioUsers)HttpContext.Current.Session["User"];
            }
            set { HttpContext.Current.Session["User"] = value; }
        }

        public List<string> RelatedCategoriesRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesRndm"] = value; }
        }

        public List<string> RelatedCategoriesLondonRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesLondonRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesLondonRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesLondonRndm"] = value; }
        }

        public List<string> RelatedCategoriesAtlantaRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesAtlantaRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesAtlantaRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesAtlantaRndm"] = value; }
        }

        public List<string> RelatedCategoriesAustinRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesAustinRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesAustinRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesAustinRndm"] = value; }
        }

        public List<string> RelatedCategoriesBaltimoreRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesBaltimoreRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesBaltimoreRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesBaltimoreRndm"] = value; }
        }

        public List<string> RelatedCategoriesBostonRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesBostonRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesBostonRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesBostonRndm"] = value; }
        }

        public List<string> RelatedCategoriesCharlotteRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesCharlotteRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesCharlotteRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesCharlotteRndm"] = value; }
        }

        public List<string> RelatedCategoriesChicagoRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesChicagoRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesChicagoRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesChicagoRndm"] = value; }
        }

        public List<string> RelatedCategoriesDallasRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesDallasRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesDallasRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesDallasRndm"] = value; }
        }

        public List<string> RelatedCategoriesDenverRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesDenverRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesDenverRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesDenverRndm"] = value; }
        }

        public List<string> RelatedCategoriesHoustonRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesHoustonRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesHoustonRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesHoustonRndm"] = value; }
        }

        public List<string> RelatedCategoriesLosAngelesRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesLosAngelesRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesLosAngelesRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesLosAngelesRndm"] = value; }
        }

        public List<string> RelatedCategoriesMiamiRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesMiamiRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesMiamiRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesMiamiRndm"] = value; }
        }

        public List<string> RelatedCategoriesNewJerseyRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesNewJerseyRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesNewJerseyRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesNewJerseyRndm"] = value; }
        }

        public List<string> RelatedCategoriesNewYorkRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesNewYorkRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesNewYorkRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesNewYorkRndm"] = value; }
        }

        public List<string> RelatedCategoriesPhiladelphiaRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesPhiladelphiaRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesPhiladelphiaRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesPhiladelphiaRndm"] = value; }
        }

        public List<string> RelatedCategoriesPhoenixRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesPhoenixRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesPhoenixRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesPhoenixRndm"] = value; }
        }

        public List<string> RelatedCategoriesSanFranciscoRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSanFranciscoRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSanFranciscoRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSanFranciscoRndm"] = value; }
        }

        public List<string> RelatedCategoriesSanJoseRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSanJoseRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSanJoseRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSanJoseRndm"] = value; }
        }

        public List<string> RelatedCategoriesSeattleRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSeattleRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSeattleRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSeattleRndm"] = value; }
        }

        public List<string> RelatedCategoriesWashingtonRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesWashingtonRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesWashingtonRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesWashingtonRndm"] = value; }
        }

        public List<string> RelatedCategoriesCincinnatiRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesCincinnatiRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesCincinnatiRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesCincinnatiRndm"] = value; }
        }

        public List<string> RelatedCategoriesClevelandRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesClevelandRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesClevelandRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesClevelandRndm"] = value; }
        }

        public List<string> RelatedCategoriesColumbusRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesColumbusRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesColumbusRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesColumbusRndm"] = value; }
        }

        public List<string> RelatedCategoriesDetroitRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesDetroitRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesDetroitRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesDetroitRndm"] = value; }
        }

        public List<string> RelatedCategoriesHartfordRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesHartfordRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesHartfordRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesHartfordRndm"] = value; }
        }

        public List<string> RelatedCategoriesIndianapolisRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesIndianapolisRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesIndianapolisRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesIndianapolisRndm"] = value; }
        }

        public List<string> RelatedCategoriesKansasCityRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesKansasCityRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesKansasCityRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesKansasCityRndm"] = value; }
        }

        public List<string> RelatedCategoriesMilwuakeeRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesMilwuakeeRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesMilwuakeeRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesMilwuakeeRndm"] = value; }
        }

        public List<string> RelatedCategoriesMinneapolisRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesMinneapolisRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesMinneapolisRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesMinneapolisRndm"] = value; }
        }

        public List<string> RelatedCategoriesOaklandRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesOaklandRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesOaklandRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesOaklandRndm"] = value; }
        }

        public List<string> RelatedCategoriesOrlandoRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesOrlandoRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesOrlandoRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesOrlandoRndm"] = value; }
        }

        public List<string> RelatedCategoriesPittsburghRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesPittsburghRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesPittsburghRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesPittsburghRndm"] = value; }
        }

        public List<string> RelatedCategoriesPortlandRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesPortlandRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesPortlandRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesPortlandRndm"] = value; }
        }

        public List<string> RelatedCategoriesRaleighRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesRaleighRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesRaleighRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesRaleighRndm"] = value; }
        }

        public List<string> RelatedCategoriesSacramentoRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSacramentoRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSacramentoRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSacramentoRndm"] = value; }
        }

        public List<string> RelatedCategoriesSaltLakeCityRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSaltLakeCityRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSaltLakeCityRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSaltLakeCityRndm"] = value; }
        }

        public List<string> RelatedCategoriesSanDiegoRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesSanDiegoRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesSanDiegoRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesSanDiegoRndm"] = value; }
        }

        public List<string> RelatedCategoriesTampaRndm
        {
            get
            {
                if (HttpContext.Current.Session["RelatedCategoriesTampaRndm"] == null)
                    return null;
                else
                    return (List<string>)HttpContext.Current.Session["RelatedCategoriesTampaRndm"];
            }
            set { HttpContext.Current.Session["RelatedCategoriesTampaRndm"] = value; }
        }
        
        public string CategoryAreaValue
        {
            get
            {
                if (HttpContext.Current.Session["CategoryAreaValue"] == null)
                    HttpContext.Current.Session["CategoryAreaValue"] = "";
                return HttpContext.Current.Session["CategoryAreaValue"].ToString();
            }
            set { HttpContext.Current.Session["CategoryAreaValue"] = value; }
        }

        public Dictionary<string, List<string>> SearchCategoriesArea
        {
            get
            {
                if (HttpContext.Current.Session["SearchCategoriesArea"] == null)
                    HttpContext.Current.Session["SearchCategoriesArea"] = new Dictionary<string, List<string>>();
                return HttpContext.Current.Session["SearchCategoriesArea"] as Dictionary<string, List<string>>;
            }
            set { HttpContext.Current.Session["SearchCategoriesArea"] = value; }
        }

        public string Lang
        {
            get
            {
                if (HttpContext.Current.Session["Lang"] == null)
                    HttpContext.Current.Session["Lang"] = "en";
                return HttpContext.Current.Session["Lang"].ToString();
            }
            set { HttpContext.Current.Session["Lang"] = value; }
        }

        public string Page
        {
            get
            {
                if (HttpContext.Current.Session["Page"] == null)
                    HttpContext.Current.Session["Page"] = ControlLoader.Default();
                return HttpContext.Current.Session["Page"].ToString();
            }
            set { HttpContext.Current.Session["Page"] = value; }
        }

        public bool ShowResultsPanel
        {
            get
            {
                if (HttpContext.Current.Session["ShowResultsPanel"] == null)
                    HttpContext.Current.Session["ShowResultsPanel"] = false;
                return (bool)HttpContext.Current.Session["ShowResultsPanel"];
            }
            set { HttpContext.Current.Session["ShowResultsPanel"] = value; }
        }

        public bool HasToUpdateNewConnections
        {
            get
            {
                if (HttpContext.Current.Session["HasToUpdateNewConnections"] == null)
                    HttpContext.Current.Session["HasToUpdateNewConnections"] = false;
                return (bool)HttpContext.Current.Session["HasToUpdateNewConnections"];
            }
            set { HttpContext.Current.Session["HasToUpdateNewConnections"] = value; }
        }

        public bool HasToUpdateNewInvitationRequest
        {
            get
            {
                if (HttpContext.Current.Session["HasToUpdateNewInvitationRequest"] == null)
                    HttpContext.Current.Session["HasToUpdateNewInvitationRequest"] = false;
                return (bool)HttpContext.Current.Session["HasToUpdateNewInvitationRequest"];
            }
            set { HttpContext.Current.Session["HasToUpdateNewInvitationRequest"] = value; }
        }

        public bool UserHasExpiredOrder
        {
            get
            {
                if (HttpContext.Current.Session["UserHasExpiredOrder"] == null)
                    HttpContext.Current.Session["UserHasExpiredOrder"] = false;
                return (bool)HttpContext.Current.Session["UserHasExpiredOrder"];
            }
            set { HttpContext.Current.Session["UserHasExpiredOrder"] = value; }
        }

        public bool HasOpenSelfServicePopUp
        {
            get
            {
                if (HttpContext.Current.Session["HasOpenSelfServicePopUp"] == null)
                    HttpContext.Current.Session["HasOpenSelfServicePopUp"] = false;
                return (bool)HttpContext.Current.Session["HasOpenSelfServicePopUp"];
            }
            set { HttpContext.Current.Session["HasOpenSelfServicePopUp"] = value; }
        }

        public bool SuccessfullFileUpload
        {
            get
            {
                if (HttpContext.Current.Session["SuccessfullFileUpload"] == null)
                    HttpContext.Current.Session["SuccessfullFileUpload"] = false;
                return (bool)HttpContext.Current.Session["SuccessfullFileUpload"];
            }
            set { HttpContext.Current.Session["SuccessfullFileUpload"] = value; }
        }

        public bool SuccessfullPersonalImageUpload
        {
            get
            {
                if (HttpContext.Current.Session["SuccessfullPersonalImageUpload"] == null)
                    HttpContext.Current.Session["SuccessfullPersonalImageUpload"] = false;
                return (bool)HttpContext.Current.Session["SuccessfullPersonalImageUpload"];
            }
            set { HttpContext.Current.Session["SuccessfullPersonalImageUpload"] = value; }
        }

        public bool SuccessfullSubAccountPersonalImageUpload
        {
            get
            {
                if (HttpContext.Current.Session["SuccessfullSubAccountPersonalImageUpload"] == null)
                    HttpContext.Current.Session["SuccessfullSubAccountPersonalImageUpload"] = false;
                return (bool)HttpContext.Current.Session["SuccessfullSubAccountPersonalImageUpload"];
            }
            set { HttpContext.Current.Session["SuccessfullSubAccountPersonalImageUpload"] = value; }
        }

        public bool HasSelectedPersonalImageToUpload
        {
            get
            {
                if (HttpContext.Current.Session["HasSelectedPersonalImageToUpload"] == null)
                    HttpContext.Current.Session["HasSelectedPersonalImageToUpload"] = false;
                return (bool)HttpContext.Current.Session["HasSelectedPersonalImageToUpload"];
            }
            set { HttpContext.Current.Session["HasSelectedPersonalImageToUpload"] = value; }
        }

        public bool ShowRegistrationRwnd
        {
            get
            {
                if (HttpContext.Current.Session["ShowRegistrationRwnd"] == null)
                    HttpContext.Current.Session["ShowRegistrationRwnd"] = false;
                return (bool)HttpContext.Current.Session["ShowRegistrationRwnd"];
            }
            set { HttpContext.Current.Session["ShowRegistrationRwnd"] = value; }
        }

        public string LoadedDashboardControl
        {
            get
            {
                if (HttpContext.Current.Session["LoadedDashboardControl"] == null)
                    HttpContext.Current.Session["LoadedDashboardControl"] = ControlLoader.Messages;
                return HttpContext.Current.Session["LoadedDashboardControl"].ToString();
            }
            set { HttpContext.Current.Session["LoadedDashboardControl"] = value; }
        }

        public string LoadedCommunityControl
        {
            get
            {
                if (HttpContext.Current.Session["LoadedCommunityControl"] == null)
                    HttpContext.Current.Session["LoadedCommunityControl"] = ControlLoader.UcRdgPosts;
                return HttpContext.Current.Session["LoadedCommunityControl"].ToString();
            }
            set { HttpContext.Current.Session["LoadedCommunityControl"] = value; }
        }

        public int LoggedInUsersCount
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInUsersCount"] == null)
                    HttpContext.Current.Session["LoggedInUsersCount"] = 0;
                return Convert.ToInt32(HttpContext.Current.Session["LoggedInUsersCount"].ToString());
            }
            set { HttpContext.Current.Session["LoggedInUsersCount"] = value; }
        }

        public string TypeOfUser
        {
            get
            {
                if (HttpContext.Current.Session["TypeOfUser"] == null || HttpContext.Current.Session["TypeOfUser"].ToString() == string.Empty)
                    HttpContext.Current.Session["TypeOfUser"] = "0";
                return HttpContext.Current.Session["TypeOfUser"].ToString();
            }
            set { HttpContext.Current.Session["TypeOfUser"] = value; }
        }

        public string CommunityPostsStrQueryAppend
        {
            get
            {
                if (HttpContext.Current.Session["CommunityPostsStrQueryAppend"] == null)
                    HttpContext.Current.Session["CommunityPostsStrQueryAppend"] = "order by Elio_community_posts.sysdate desc";
                return HttpContext.Current.Session["CommunityPostsStrQueryAppend"].ToString();
            }
            set { HttpContext.Current.Session["CommunityPostsStrQueryAppend"] = value; }
        }

        public string LoadedDashboardCompanyEditControl
        {
            get
            {
                if (HttpContext.Current.Session["LoadedDashboardCompanyEditControl"] == null)
                    HttpContext.Current.Session["LoadedDashboardCompanyEditControl"] = ControlLoader.CompanyDataViewMode;
                return HttpContext.Current.Session["LoadedDashboardCompanyEditControl"].ToString();
            }
            set { HttpContext.Current.Session["LoadedDashboardCompanyEditControl"] = value; }
        }

        public string LoadedDashboardProfileEditControl
        {
            get
            {
                if (HttpContext.Current.Session["LoadedDashboardProfileEditControl"] == null)
                    HttpContext.Current.Session["LoadedDashboardProfileEditControl"] = ControlLoader.ProfileDataViewMode;
                return HttpContext.Current.Session["LoadedDashboardProfileEditControl"].ToString();
            }
            set { HttpContext.Current.Session["LoadedDashboardProfileEditControl"] = value; }
        }

        public string SearchQueryString
        {
            get
            {
                if (HttpContext.Current.Session["SearchQueryString"] == null)
                    HttpContext.Current.Session["SearchQueryString"] = string.Empty;
                return HttpContext.Current.Session["SearchQueryString"].ToString();
            }
            set { HttpContext.Current.Session["SearchQueryString"] = value; }
        }

        public string SelectedCompanyToComposeMessage
        {
            get
            {
                if (HttpContext.Current.Session["SelectedCompanyToComposeMessage"] == null)
                    HttpContext.Current.Session["SelectedCompanyToComposeMessage"] = string.Empty;
                return HttpContext.Current.Session["SelectedCompanyToComposeMessage"].ToString();
            }
            set { HttpContext.Current.Session["SelectedCompanyToComposeMessage"] = value; }
        }

        public string CategoryViewState
        {
            get
            {
                if (HttpContext.Current.Session["CategoryViewState"] == null)
                    HttpContext.Current.Session["CategoryViewState"] = "1";
                return HttpContext.Current.Session["CategoryViewState"].ToString();
            }
            set { HttpContext.Current.Session["CategoryViewState"] = value; }
        }

        public string IndustryViewState
        {
            get
            {
                if (HttpContext.Current.Session["IndustryViewState"] == null)
                    HttpContext.Current.Session["IndustryViewState"] = "0";
                return HttpContext.Current.Session["IndustryViewState"].ToString();
            }
            set { HttpContext.Current.Session["IndustryViewState"] = value; }
        }

        public string VerticalViewState
        {
            get
            {
                if (HttpContext.Current.Session["VerticalViewState"] == null)
                    HttpContext.Current.Session["VerticalViewState"] = "0";
                return HttpContext.Current.Session["VerticalViewState"].ToString();
            }
            set { HttpContext.Current.Session["VerticalViewState"] = value; }
        }

        public string PartnerViewState
        {
            get
            {
                if (HttpContext.Current.Session["PartnerViewState"] == null)
                    HttpContext.Current.Session["PartnerViewState"] = "0";
                return HttpContext.Current.Session["PartnerViewState"].ToString();
            }
            set { HttpContext.Current.Session["PartnerViewState"] = value; }
        }

        public string MarketViewState
        {
            get
            {
                if (HttpContext.Current.Session["MarketViewState"] == null)
                    HttpContext.Current.Session["MarketViewState"] = "0";
                return HttpContext.Current.Session["MarketViewState"].ToString();
            }
            set { HttpContext.Current.Session["MarketViewState"] = value; }
        }

        public string ApiViewState
        {
            get
            {
                if (HttpContext.Current.Session["ApiViewState"] == null)
                    HttpContext.Current.Session["ApiViewState"] = "0";
                return HttpContext.Current.Session["ApiViewState"].ToString();
            }
            set { HttpContext.Current.Session["ApiViewState"] = value; }
        }

        public string CountryViewState
        {
            get
            {
                if (HttpContext.Current.Session["CountryViewState"] == null)
                    HttpContext.Current.Session["CountryViewState"] = "0";
                return HttpContext.Current.Session["CountryViewState"].ToString();
            }
            set { HttpContext.Current.Session["CountryViewState"] = value; }
        }

        public string CompanyNameViewState
        {
            get
            {
                if (HttpContext.Current.Session["CompanyNameViewState"] == null)
                    HttpContext.Current.Session["CompanyNameViewState"] = string.Empty;
                return HttpContext.Current.Session["CompanyNameViewState"].ToString();
            }
            set { HttpContext.Current.Session["CompanyNameViewState"] = value; }
        }

        public string TechnologyCategory
        {
            get
            {
                if (HttpContext.Current.Session["TechnologyCategory"] == null)
                    HttpContext.Current.Session["TechnologyCategory"] = string.Empty;
                return HttpContext.Current.Session["TechnologyCategory"].ToString();
            }
            set { HttpContext.Current.Session["TechnologyCategory"] = value; }
        }

        public bool AdvancedViewState
        {
            get
            {
                if (HttpContext.Current.Session["AdvancedViewState"] == null)
                    HttpContext.Current.Session["AdvancedViewState"] = false;
                return (bool)HttpContext.Current.Session["AdvancedViewState"];
            }
            set { HttpContext.Current.Session["AdvancedViewState"] = value; }
        } 

        public ElioUsers ElioCompanyDetailsView
        {
            get
            {
                if (HttpContext.Current.Session["ElioCompanyDetailsView"] == null)
                    HttpContext.Current.Session["ElioCompanyDetailsView"] = null;
                return (ElioUsers)HttpContext.Current.Session["ElioCompanyDetailsView"];
            }
            set { HttpContext.Current.Session["ElioCompanyDetailsView"] = value; }
        }

        public DataTable ViewStateDataStore
        {
            get
            {
                if (HttpContext.Current.Session["ViewStateDataStore"] == null)
                    HttpContext.Current.Session["ViewStateDataStore"] = new DataTable();
                return (DataTable)HttpContext.Current.Session["ViewStateDataStore"];
            }
            set { HttpContext.Current.Session["ViewStateDataStore"] = value; }
        }

        public List<ConnectionList> ViewStateDataStoreList
        {
            get
            {
                if (HttpContext.Current.Session["ViewStateDataStore"] == null)
                    HttpContext.Current.Session["ViewStateDataStore"] = new List<ConnectionList>();
                return (List<ConnectionList>)HttpContext.Current.Session["ViewStateDataStore"];
            }
            set { HttpContext.Current.Session["ViewStateDataStore"] = value; }
        }

        public DataTable ViewStateDataStoreForExport
        {
            get
            {
                if (HttpContext.Current.Session["ViewStateDataStoreForExport"] == null)
                    HttpContext.Current.Session["ViewStateDataStoreForExport"] = new DataTable();
                return (DataTable)HttpContext.Current.Session["ViewStateDataStoreForExport"];
            }
            set { HttpContext.Current.Session["ViewStateDataStoreForExport"] = value; }
        }

        public DataSet ViewStateDataStoreDS
        {
            get
            {
                if (HttpContext.Current.Session["ViewStateDataStore"] == null)
                    HttpContext.Current.Session["ViewStateDataStore"] = new DataSet();
                return (DataSet)HttpContext.Current.Session["ViewStateDataStore"];
            }
            set { HttpContext.Current.Session["ViewStateDataStore"] = value; }
        }

        public bool HasSearchCriteria
        {
            get
            {
                if (HttpContext.Current.Session["HasSearchCriteria"] == null)
                    HttpContext.Current.Session["HasSearchCriteria"] = false;
                return (bool)HttpContext.Current.Session["HasSearchCriteria"];
            }
            set { HttpContext.Current.Session["HasSearchCriteria"] = value; }
        }

        public bool ShowOpportunitySuccessMsgAlert
        {
            get
            {
                if (HttpContext.Current.Session["ShowOpportunitySuccessMsgAlert"] == null)
                    HttpContext.Current.Session["ShowOpportunitySuccessMsgAlert"] = false;
                return (bool)HttpContext.Current.Session["ShowOpportunitySuccessMsgAlert"];
            }
            set { HttpContext.Current.Session["ShowOpportunitySuccessMsgAlert"] = value; }
        }

        public ElioCommunityPostsIJUsers LoadedCommunityPostDetails
        {
            get
            {
                if (HttpContext.Current.Session["LoadedCommunityPostDetails"] == null)
                    HttpContext.Current.Session["LoadedCommunityPostDetails"] = null;
                return (ElioCommunityPostsIJUsers)HttpContext.Current.Session["LoadedCommunityPostDetails"];
            }
            set { HttpContext.Current.Session["LoadedCommunityPostDetails"] = value; }
        }

        public List<ElioUsersId> CompaniesResultsId
        {
            get
            {
                if (HttpContext.Current.Session["CompaniesResultsId"] == null)
                    HttpContext.Current.Session["CompaniesResultsId"] = new List<ElioUsersId>();
                return (List<ElioUsersId>)HttpContext.Current.Session["CompaniesResultsId"];
            }
            set { HttpContext.Current.Session["CompaniesResultsId"] = value; }
        }

        public List<ElioCollaborationVendorsResellers> VendorsResellersList
        {
            get
            {
                if (HttpContext.Current.Session["VendorsResellersList"] == null)
                    HttpContext.Current.Session["VendorsResellersList"] = new List<ElioCollaborationVendorsResellers>();
                return (List<ElioCollaborationVendorsResellers>)HttpContext.Current.Session["VendorsResellersList"];
            }
            set { HttpContext.Current.Session["VendorsResellersList"] = value; }
        }

        public int CurrentGridPageIndex
        {
            get
            {
                if (HttpContext.Current.Session["CurrentGridPageIndex"] == null)
                    HttpContext.Current.Session["CurrentGridPageIndex"] = 0;
                return (int)HttpContext.Current.Session["CurrentGridPageIndex"];
            }
            set { HttpContext.Current.Session["CurrentGridPageIndex"] = value; }
        }

        public int PopularVendorId
        {
            get
            {
                if (HttpContext.Current.Session["PopularVendorId"] == null)
                    HttpContext.Current.Session["PopularVendorId"] = 0;
                return (int)HttpContext.Current.Session["PopularVendorId"];
            }
            set { HttpContext.Current.Session["PopularVendorId"] = value; }
        }

        public int SelectedValueForUserRegistrationChart
        {
            get
            {
                if (HttpContext.Current.Session["SelectedValueForUserRegistrationChart"] == null)
                    HttpContext.Current.Session["SelectedValueForUserRegistrationChart"] = 0;
                return (int)HttpContext.Current.Session["SelectedValueForUserRegistrationChart"];
            }
            set { HttpContext.Current.Session["SelectedValueForUserRegistrationChart"] = value; }
        }

        public bool ChangeSelectedValueForUserRegistrationChart
        {
            get
            {
                if (HttpContext.Current.Session["ChangeSelectedValueForUserRegistrationChart"] == null)
                    HttpContext.Current.Session["ChangeSelectedValueForUserRegistrationChart"] = false;
                return Convert.ToBoolean(HttpContext.Current.Session["ChangeSelectedValueForUserRegistrationChart"]);
            }
            set { HttpContext.Current.Session["ChangeSelectedValueForUserRegistrationChart"] = value; }
        }

        //public int LoadedSearchControlId
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["LoadedSearchControlId"] == null)
        //            HttpContext.Current.Session["LoadedSearchControlId"] = ControlLoader.SearchVendors;
        //        return (int)HttpContext.Current.Session["LoadedSearchControlId"];
        //    }
        //    set { HttpContext.Current.Session["LoadedSearchControlId"] = value; }
        //}

        public string LoadedBenefitsControl
        {
            get
            {
                if (HttpContext.Current.Session["LoadedBenefitsControl"] == null)
                    HttpContext.Current.Session["LoadedBenefitsControl"] = string.Empty;
                return HttpContext.Current.Session["LoadedBenefitsControl"].ToString();
            }
            set { HttpContext.Current.Session["LoadedBenefitsControl"] = value; }
        }

        public bool HasChangeReceiverCompany
        {
            get
            {
                if (HttpContext.Current.Session["HasChangeReceiverCompany"] == null)
                    HttpContext.Current.Session["HasChangeReceiverCompany"] = false;
                return (bool)HttpContext.Current.Session["HasChangeReceiverCompany"];
            }
            set { HttpContext.Current.Session["HasChangeReceiverCompany"] = value; }
        }
        
        public bool HasChangeSelectedCountry
        {
            get
            {
                if (HttpContext.Current.Session["HasChangeSelectedCountry"] == null)
                    HttpContext.Current.Session["HasChangeSelectedCountry"] = false;
                return (bool)HttpContext.Current.Session["HasChangeSelectedCountry"];
            }
            set { HttpContext.Current.Session["HasChangeSelectedCountry"] = value; }
        }
    }
}
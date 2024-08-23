using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.SearchResults
{
    public class SearchLib
    {
        public static string FillProfilePageContent(int fromPageIndex, int toPageIndex,
            out string category, out string technologyCategory, out string tabTitle,
            out string pagePath, out string pageTitle, out string content1, out string content2,
            out string metaDescription, out string metaKeywords, out string disclaimerText,
            out string lnk1, out string lnk2, out string lnk3, out string lnk4, out string lnk5,
            out string lnk6, out string lnk7, out string lnk8, out string lnk9, out string strQueryIDs,
            out int resultsCount, out bool existInFile, DBSession session)
        {
            #region initialize element values

            string countryName = "";
            string cityName = "";
            string companyType = "";
            resultsCount = 0;
            string strQuery = "";

            category = "";
            technologyCategory = "";
            tabTitle = "";
            pagePath = "";
            pageTitle = "";
            content1 = "";
            content2 = "";
            metaDescription = "";
            metaKeywords = "";
            disclaimerText = "";
            lnk1 = lnk2 = lnk3 = lnk4 = lnk5 = lnk6 = lnk7 = lnk8 = lnk9 = "";
            strQueryIDs = "";
            existInFile = false;
            string technologyName = "";
            bool containsCountry = false;
            bool containsCity = false;

            bool getDefault = false;

            bool containsCityTrans = false;

            int pathIndex = 1;

            #endregion

            #region get elements from path

            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            if (path.Length > 0)
            {
                string technology = path[path.Length - 1].TrimEnd();
                if (technology == "")
                    return "";
                else
                {
                    #region fix technology name

                    if (technology.StartsWith("'"))
                        technology = technology.Substring(1, technology.Length);
                    if (technology.EndsWith("'"))
                        technology = technology.Substring(0, technology.Length - 1);

                    technologyName = technology.Replace("_", " ").Replace("and","&");       //technology.Replace("-", " ").Replace("'", "-");

                    technologyName = category = technologyCategory = GlobalMethods.CapitalizeFirstLetter(technologyName);

                    #endregion

                    #region get country / city

                    //bool containsCityTrans = false;
                    string cityTransName = "";

                    if (!path[1].TrimEnd('/').Contains("profile"))
                    {
                        if (path[pathIndex].Length > 0 && path[pathIndex].Length == 2)
                        {
                            #region url contains translation

                            cityTransName = path[pathIndex].TrimEnd('/');

                            if (ConfigurationManager.AppSettings["TranslationCities"] != null)
                            {
                                List<string> defaultCitiesTrans = ConfigurationManager.AppSettings["TranslationCities"].ToString().Split(',').ToList();
                                if (defaultCitiesTrans.Count > 0)
                                {
                                    foreach (string cityTrans in defaultCitiesTrans)
                                    {
                                        if (cityTrans != "" && cityTrans.Contains(cityTransName))
                                        {
                                            containsCityTrans = true;
                                            break;
                                        }
                                    }
                                }

                                if (containsCityTrans)
                                    pathIndex = pathIndex + 1;
                            }

                            #endregion
                        }

                        string part1 = path[pathIndex].TrimEnd('/').Replace("'", "");

                        containsCountry = Sql.ExistCountryByNameLike(part1, session);

                        if (!containsCountry)
                        {
                            containsCity = Sql.ExistCityInUsersByCityNameLike(part1, session);

                            if (containsCity)
                            {
                                string[] cityWords = path[pathIndex].Split('-');
                                foreach (var word in cityWords)
                                {
                                    cityName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                }

                                cityName = cityName.TrimEnd();
                            }
                            else
                            {
                                //not correct url
                            }
                        }
                        else
                        {
                            string[] countryWords = path[pathIndex].Split('-');
                            foreach (var word in countryWords)
                            {
                                countryName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            countryName = countryName.TrimEnd();

                            if (!path[pathIndex + 1].Contains("channel-partners") && !path[2].Contains("vendors"))
                            {
                                string part2 = path[pathIndex + 1].TrimEnd('/');

                                containsCity = Sql.ExistCityInUsersByCityNameLike(part2, session);

                                if (containsCity)
                                {
                                    string[] cityWords = path[pathIndex + 1].Split('-');
                                    foreach (var word in cityWords)
                                    {
                                        cityName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                    }

                                    cityName = cityName.TrimEnd();
                                }
                                else
                                {
                                    //not correct url
                                }
                            }
                        }
                    }

                    #endregion

                    #region url contains country OLD

                    //containsCountry = (!path[1].Contains("profile")) ? true : false;

                    //if (containsCountry)
                    //{
                    //    string[] countryWords = path[1].Split('-');
                    //    foreach (var word in countryWords)
                    //    {
                    //        country += char.ToUpper(word[0]) + word.Substring(1) + " ";
                    //    }

                    //    country = country.TrimEnd();
                    //}

                    #endregion

                    #region url contains city OLD

                    //containsCity = (path[2].Contains("channel-partners") || path[2].Contains("vendors")) ? false : true;

                    //if (containsCity)
                    //{
                    //    string[] countryWords = path[2].Split('-');
                    //    foreach (var word in countryWords)
                    //    {
                    //        city += char.ToUpper(word[0]) + word.Substring(1) + " ";
                    //    }

                    //    city = city.TrimEnd();
                    //}

                    #endregion

                    #region url company type

                    companyType = GlobalMethods.FixSearchDescriptionBack(path[path.Length - 2]);
                    if (companyType.StartsWith("vendors"))
                        companyType = Types.Vendors.ToString().ToLower();
                    else
                    {
                        companyType = EnumHelper.GetDescription(Types.Resellers).ToString();
                        //companyType = companyType.Replace(" ", "-").ToLower();
                    }

                    #endregion

                    #region Exception

                    if (technology == "sugar-crm")
                    {
                        technologyName = "sugarcrm";
                    }
                    else if (technology == "alt-n")
                    {
                        technologyName = "Alt-n";
                    }
                    else if (technology == "8x8")
                    {
                        technologyName = "8x8";
                    }
                    else if (technology == "dat-em")
                    {
                        technologyName = "Dat-em";
                    }
                    else if (technology == "Maitre-d")
                    {
                        technologyName = "Maitre-D";
                    }
                    else if (technology == "o-neil" || technology == "Ο-neil")
                    {
                        technologyName = "O-Neil";
                    }

                    #endregion
                }

                #region xml file root path

                string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
                string rootTemplatePath = string.Empty;

                if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                {
                    rootXmlPath = "D:/Projects/WdS.ElioPlus/";
                    rootTemplatePath = "WdS.ElioPlus\\";
                }

                #endregion

                #region read xml file

                XmlDocument xmlEmailsDoc = new XmlDocument();

                if (containsCountry && !containsCity)
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex].ToLower() + ".xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex].ToLower() + ".xml");
                    else
                    {
                        if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results-default.xml"))
                        {
                            xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results-default.xml");
                            getDefault = true;
                        }
                        else
                            return "";
                    }
                }
                else if (containsCountry && containsCity)
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex + 1].ToLower() + "_" + path[pathIndex].ToLower() + ".xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex + 1].ToLower() + "_" + path[pathIndex].ToLower() + ".xml");
                    else
                        return "";
                }
                else
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results_channel-partners.xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results_channel-partners.xml");
                    else
                        return "";
                }

                #endregion

                if (!getDefault)
                {
                    #region get technology element name

                    XmlNode xmlTechnology = xmlEmailsDoc.SelectSingleNode(@"descendant::technology[@name=""" + technologyName + @"""]");

                    if (xmlTechnology != null)
                    {
                        existInFile = true;

                        #region read xml node elements

                        XmlNode xmlCategory = xmlTechnology.SelectSingleNode("child::category");
                        if (xmlCategory != null)
                            category = xmlCategory.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlTechnologyCategory = xmlTechnology.SelectSingleNode("child::technologyCategory");
                        if (xmlTechnologyCategory != null)
                            technologyCategory = xmlTechnologyCategory.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlTabTitle = xmlTechnology.SelectSingleNode("child::tabTitle");
                        if (xmlTabTitle != null)
                            tabTitle = xmlTabTitle.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlPagePath = xmlTechnology.SelectSingleNode("child::pagePath");
                        if (xmlPagePath != null)
                            pagePath = xmlPagePath.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlPageTitle = xmlTechnology.SelectSingleNode("child::pageTitle");
                        if (xmlPageTitle != null)
                            pageTitle = xmlPageTitle.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlContent1 = xmlTechnology.SelectSingleNode("child::content1");
                        if (xmlContent1 != null)
                            content1 = xmlContent1.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlContent2 = xmlTechnology.SelectSingleNode("child::content2");
                        if (xmlContent2 != null)
                            content2 = xmlContent2.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlMetaDescription = xmlTechnology.SelectSingleNode("child::metaDescription");
                        if (xmlMetaDescription != null)
                            metaDescription = xmlMetaDescription.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlMetaKeywords = xmlTechnology.SelectSingleNode("child::metaKeywords");
                        if (xmlMetaKeywords != null)
                            metaKeywords = xmlMetaKeywords.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlDisclaimerText = xmlTechnology.SelectSingleNode("child::disclaimerText");
                        if (xmlDisclaimerText != null)
                            disclaimerText = xmlDisclaimerText.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink1 = xmlTechnology.SelectSingleNode("child::link1");
                        if (xmlLink1 != null)
                            lnk1 = xmlLink1.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink2 = xmlTechnology.SelectSingleNode("child::link2");
                        if (xmlLink2 != null)
                            lnk2 = xmlLink2.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink3 = xmlTechnology.SelectSingleNode("child::link3");
                        if (xmlLink3 != null)
                            lnk3 = xmlLink3.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink4 = xmlTechnology.SelectSingleNode("child::link4");
                        if (xmlLink4 != null)
                            lnk4 = xmlLink4.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink5 = xmlTechnology.SelectSingleNode("child::link5");
                        if (xmlLink5 != null)
                            lnk5 = xmlLink5.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink6 = xmlTechnology.SelectSingleNode("child::link6");
                        if (xmlLink6 != null)
                            lnk6 = xmlLink6.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink7 = xmlTechnology.SelectSingleNode("child::link7");
                        if (xmlLink7 != null)
                            lnk7 = xmlLink7.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink8 = xmlTechnology.SelectSingleNode("child::link8");
                        if (xmlLink8 != null)
                            lnk8 = xmlLink8.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlLink9 = xmlTechnology.SelectSingleNode("child::link9");
                        if (xmlLink9 != null)
                            lnk9 = xmlLink9.InnerText.Replace("\n", "").Replace("\r", "").Trim();

                        XmlNode xmlStrQueryIDs = xmlTechnology.SelectSingleNode("child::strQueryIDs");
                        if (xmlStrQueryIDs != null)
                        {
                            strQueryIDs = xmlStrQueryIDs.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                            resultsCount = strQueryIDs.Split(',').Length;
                        }

                        #endregion
                    }
                    else
                        category = technologyCategory = technologyName;

                    #endregion
                }
                else
                {
                    #region get default content

                    XmlNode xmlTechnology = xmlEmailsDoc.SelectSingleNode(@"descendant::technology[@name=""default""]");

                    if (xmlTechnology != null)
                    {
                        existInFile = true;

                        #region read xml node elements

                        XmlNode xmlTabTitle = xmlTechnology.SelectSingleNode("child::tabTitle");
                        if (xmlTabTitle != null)
                            tabTitle = xmlTabTitle.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        XmlNode xmlPageTitle = xmlTechnology.SelectSingleNode("child::pageTitle");
                        if (xmlPageTitle != null)
                            pageTitle = xmlPageTitle.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        XmlNode xmlContent1 = xmlTechnology.SelectSingleNode("child::content1");
                        if (xmlContent1 != null)
                            content1 = xmlContent1.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        XmlNode xmlMetaDescription = xmlTechnology.SelectSingleNode("child::metaDescription");
                        if (xmlMetaDescription != null)
                            metaDescription = xmlMetaDescription.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        XmlNode xmlMetaKeywords = xmlTechnology.SelectSingleNode("child::metaKeywords");
                        if (xmlMetaKeywords != null)
                            metaKeywords = xmlMetaKeywords.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        XmlNode xmlDisclaimerText = xmlTechnology.SelectSingleNode("child::disclaimerText");
                        if (xmlDisclaimerText != null)
                            disclaimerText = xmlDisclaimerText.InnerText.Replace("\n", "").Replace("\r", "").Trim().Replace("{Product Name}", technologyName).Replace("{Country}", countryName);

                        #endregion
                    }

                    category = technologyCategory = technologyName;

                    #endregion
                }

                if (xmlEmailsDoc.ChildNodes.Count > 0 && xmlEmailsDoc.HasChildNodes)
                {
                    #region query

                    strQuery = @";WITH MyDataSet
                                 AS
                                 (";
                    strQuery += @"Select ROW_NUMBER() over (order by billing_type desc) as row_index
                        , u.id
                        --, round(avg(cast(rate as float)),2) as r
                        , company_name
                        , address
                        , city
                        , billing_type 
                        , company_type
                        , u.sysdate
                        from Elio_users u
                        --left join Elio_user_partner_program_rating on Elio_user_partner_program_rating.company_id=Elio_users.id 
                        inner join Elio_users_registration_products urp
                            on urp.user_id = u.id
                        inner join Elio_registration_products rp
                            on rp.id = urp.reg_products_id " + Environment.NewLine;
                    if (countryName != "")
                    {
                        strQuery += "inner join Elio_countries on Elio_countries.country_name = u.country ";
                    }

                    strQuery += "where 1 = 1 and u.is_public = 1 ";
                    strQuery += (companyType != "") ? "and company_type like '" + companyType + "' " : " ";
                    strQuery += (countryName != "") ? "and country='" + countryName + "' " : " ";
                    strQuery += (cityName != "") ? "and city='" + cityName + "' " : " ";
                    //strQuery += "and u.id in (" + strQueryIDs + ") ";

                    if (!getDefault)
                        strQuery += (category != "") ? (category == "dat-em" || category == "alt-n" || category == "maitre-d" || category == "o-neil") ? "and rp.description ='" + category + "'" : "and rp.description ='" + technologyName.Replace("_", " ") + "'" : "and rp.description ='" + technologyName.Replace("_", " ") + "' ";       //.Replace("-", " ")
                    else
                        strQuery += (technologyName != "") ? "and rp.description ='" + technologyName.Replace("_", " ") + "'" : " ";    //.Replace("-", " ")

                    strQuery += " GROUP BY u.id, company_name, billing_type, address, city, company_type, u.sysdate) ";

                    strQuery += @" select id,row_index
                            --,r,billing_type
                            --,isnull(totalViews,0) as totalViews 
                            ,address
                            ,city
                            ,billing_type
                            ,company_type
                            from MyDataSet  
                            --left join total_views on total_views.company_id = MyDataSet.id
                           where row_index between " + fromPageIndex + " and " + toPageIndex + " " +
                                    " ORDER BY sysdate DESC";

                    #endregion

                    #region results count

                    string strCount = @"Select count(u.id) as count
                        from Elio_users u
                        inner join Elio_users_registration_products urp
                            on urp.user_id = u.id
                        inner join Elio_registration_products rp
                            on rp.id = urp.reg_products_id " + Environment.NewLine;

                    if (countryName != "")
                    {
                        strCount += "inner join Elio_countries on Elio_countries.country_name = u.country ";
                    }

                    strCount += "where 1 = 1 ";
                    strCount += (companyType != "") ? "and company_type like '" + companyType + "' " : " ";
                    strCount += (countryName != "") ? "and country='" + countryName + "' " : " ";
                    strCount += (cityName != "") ? "and city='" + cityName + "' " : " ";

                    if (!getDefault)
                        strCount += (category != "") ? (category == "dat-em" || category == "alt-n" || category == "maitre-d" || category == "o-neil") ? "and rp.description ='" + category + "'" : "and rp.description ='" + category.Replace("_", " ") + "'" : "and rp.description ='" + technologyName.Replace("_", " ") + "' ";        //.Replace("-", " ")
                    else
                        strCount += (technologyName != "") ? "and rp.description ='" + technologyName.Replace("_", " ") + "'" : " ";        //.Replace("-", " ")

                    if (session.Connection.State == ConnectionState.Closed)
                        session.OpenConnection();

                    DataTable dt = session.GetDataTable(strCount);
                    if (dt != null && dt.Rows.Count > 0)
                        resultsCount = Convert.ToInt32(dt.Rows[0]["count"]);

                    #endregion
                }
            }

            #endregion

            return strQuery;
        }

        public static string GetTabTitleFromFile(DBSession session)
        {
            #region initialize element values
                        
            string companyType = "";            
            string technologyName = "";
            bool containsCountry = false;
            bool containsCity = false;
            //string countryName = "";
            //string cityName = "";
            bool containsCityTrans = false;
            string cityTransName = "";

            int pathIndex = 1;

            #endregion

            #region get elements from path

            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            if (path.Length > 0)
            {
                string technology = path[path.Length - 1].TrimEnd();
                if (technology == "")
                    return "";
                else
                {
                    #region fix technology name

                    if (technology.StartsWith("'"))
                        technology = technology.Substring(1, technology.Length);
                    if (technology.EndsWith("'"))
                        technology = technology.Substring(0, technology.Length - 1);

                    technologyName = technology.Replace("_", " ").Replace("and", "&");

                    technologyName = GlobalMethods.CapitalizeFirstLetter(technologyName);

                    #endregion

                    #region get country / city

                    if (session.Connection.State == ConnectionState.Closed)
                        session.OpenConnection();

                    if (!path[pathIndex].TrimEnd('/').Contains("profile"))
                    {
                        if (path[pathIndex].Length > 0 && path[pathIndex].Length == 2)
                        {
                            #region url contains translation

                            cityTransName = path[pathIndex].TrimEnd('/');

                            if (ConfigurationManager.AppSettings["TranslationCities"] != null)
                            {
                                List<string> defaultCitiesTrans = ConfigurationManager.AppSettings["TranslationCities"].ToString().Split(',').ToList();
                                if (defaultCitiesTrans.Count > 0)
                                {
                                    foreach (string cityTrans in defaultCitiesTrans)
                                    {
                                        if (cityTrans != "" && cityTrans.Contains(cityTransName))
                                        {
                                            containsCityTrans = true;
                                            break;
                                        }
                                    }
                                }

                                if (containsCityTrans)
                                    pathIndex = pathIndex + 1;
                            }

                            #endregion
                        }

                        string part1 = path[pathIndex].TrimEnd('/').Replace("'", "");

                        containsCountry = Sql.ExistCountryByNameLike(part1, session);

                        if (!containsCountry)
                        {
                            containsCity = Sql.ExistCityInUsersByCityNameLike(part1, session);
                        }
                        else
                        {
                            if (!path[pathIndex + 1].Contains("channel-partners") && !path[pathIndex + 1].Contains("vendors"))
                            {
                                string part2 = path[pathIndex + 1].TrimEnd('/');

                                containsCity = Sql.ExistCityInUsersByCityNameLike(part2, session);
                            }
                        }
                    }

                    #endregion

                    #region url contains country - OLD

                    //containsCountry = (!path[1].Contains("profile")) ? true : false;

                    //if (containsCountry)
                    //{
                    //    string[] countryWords = path[1].Split('-');
                    //    foreach (var word in countryWords)
                    //    {
                    //        country += char.ToUpper(word[0]) + word.Substring(1) + " ";
                    //    }

                    //    country = country.TrimEnd();
                    //}

                    #endregion

                    #region url contains city - OLD

                    //containsCity = (path[2].Contains("channel-partners") || path[2].Contains("vendors")) ? false : true;

                    //if (containsCity)
                    //{
                    //    string[] countryWords = path[2].Split('-');
                    //    foreach (var word in countryWords)
                    //    {
                    //        city += char.ToUpper(word[0]) + word.Substring(1) + " ";
                    //    }

                    //    city = city.TrimEnd();
                    //}

                    #endregion

                    #region url company type

                    companyType = GlobalMethods.FixSearchDescriptionBack(path[path.Length - 2]);
                    if (companyType.StartsWith("vendors"))
                        companyType = Types.Vendors.ToString().ToLower();
                    else
                    {
                        companyType = EnumHelper.GetDescription(Types.Resellers).ToString();
                        //companyType = companyType.Replace(" ", "-").ToLower();
                    }

                    #endregion

                    #region Exception

                    if (technology == "sugar-crm")
                    {
                        technology = "sugarcrm";
                    }

                    #endregion
                }

                #region xml file root path

                string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
                string rootTemplatePath = string.Empty;

                if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                {
                    rootXmlPath = "D:/Projects/WdS.ElioPlus/";
                    rootTemplatePath = "WdS.ElioPlus\\";
                }

                #endregion

                #region read xml file

                XmlDocument xmlEmailsDoc = new XmlDocument();

                if (containsCountry && !containsCity)
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex].ToLower() + ".xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex].ToLower() + ".xml");
                    else
                        return "";
                }
                else if (containsCountry && containsCity)
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex + 1].ToLower() + "_" + path[pathIndex].ToLower() + ".xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/Countries/search-results_" + path[pathIndex + 1].ToLower() + "_" + path[pathIndex].ToLower() + ".xml");
                    else
                        return "";
                }
                else
                {
                    if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results_channel-partners.xml"))
                        xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/search-results_channel-partners.xml");
                    else
                        return "";
                }

                #endregion

                #region get technology element name

                XmlNode xmlTechnology = xmlEmailsDoc.SelectSingleNode(@"descendant::technology[@name=""" + technologyName + @"""]");

                if (xmlTechnology != null)
                {
                    #region read xml node elements

                    XmlNode xmlTabTitle = xmlTechnology.SelectSingleNode("child::tabTitle");
                    if (xmlTabTitle != null)
                        return xmlTabTitle.InnerText.Replace("\n", "").Replace("\r", "").Trim();
                    else
                        return "";

                    #endregion
                }
                else
                    return "";

                #endregion
            }
            else
                return "";

            #endregion
        }

        public static List<string> GetChannelPartnersSearchTechnologies()
        {
            List<string> tech = new List<string>();

            #region xml file root path

            string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
            string rootTemplatePath = string.Empty;

            if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                rootXmlPath = "D:/NewProject/ElioPlus/";
                rootTemplatePath = "ElioPlus\\";
            }

            #endregion

            #region read xml file

            XmlDocument xmlEmailsDoc = new XmlDocument();

            if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/ChannelPartners/channelPartners-technologies.xml"))
                xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/ChannelPartners/channelPartners-technologies.xml");
            else
                return tech;
            
            #endregion

            #region get technologies element

            XmlNode xmlTechnologies = xmlEmailsDoc.SelectSingleNode("child::Technologies");

            if (xmlTechnologies != null)
            {
                #region read xml node technologies
                
                tech = xmlTechnologies.InnerText.Replace("\n", "").Replace("\r", "").Trim().Split(',').ToList();

                #endregion
            }
            else
                return tech;

            #endregion

            return tech;
        }

        public static List<string> GetChannelPartnersSearchTechnologiesByStartLetter(string startLetter)
        {
            List<string> tech = new List<string>();

            #region xml file root path

            string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
            string rootTemplatePath = string.Empty;

            if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                rootXmlPath = "D:/NewProject/ElioPlus/";
                rootTemplatePath = "ElioPlus\\";
            }

            #endregion

            #region read xml file

            XmlDocument xmlEmailsDoc = new XmlDocument();

            if (File.Exists(rootXmlPath + "Lib/SearchResults/XMLLocalizer/ChannelPartners/channelPartners-technologies_" + startLetter + ".xml"))
                xmlEmailsDoc.Load(rootXmlPath + "Lib/SearchResults/XMLLocalizer/ChannelPartners/channelPartners-technologies_" + startLetter + ".xml");
            else
                return tech;

            #endregion

            #region get technologies element

            XmlNode xmlTechnologies = xmlEmailsDoc.SelectSingleNode("child::Technologies");

            if (xmlTechnologies != null)
            {
                #region read xml node technologies

                tech = xmlTechnologies.InnerText.Replace("\n", "").Replace("\r", "").Trim().Split(',').ToList();

                #endregion
            }
            else
                return tech;

            #endregion

            return tech;
        }
    }
}
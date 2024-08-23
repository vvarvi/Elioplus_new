using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Utils
{
    public class WriteXML
    {
        private DBSession _session;
        public WriteXML(DBSession session)
        {
            _session = session;
        }

        public delegate void LogMessageEventHandler(string logType, string logMessage);
        //static public event LogMessageEventHandler LogMessage;

        static private object Locker = new object();
        static public string LogSiteMapPath = System.Configuration.ConfigurationManager.AppSettings["LogSiteMapPath"];

        static public string GetFileName(string logPath, int scenario, string fileNamePath)
        {
            if (string.IsNullOrEmpty(logPath))
                return null;
            string path = FileHelper.AddRootToPath(logPath);
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = fileNamePath + "-" + scenario.ToString();

            if (File.Exists(path + "sitemap" + fileName + "-new.xml"))
                return GetFileName(logPath, scenario, fileName);
            else
                return path + "sitemap" + fileName + "-new.xml";
        }

        static public void WriteSiteMapFile(int scenario, int id, DBSession session)
        {
            string path = GetFileName(LogSiteMapPath, scenario, "");
            if (string.IsNullOrEmpty(path))
                return;

            System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);

            try
            {
                string xml = @"<?xml version=\""1.0\"" encoding=\""utf-8\""?><urlset xmlns=\""http://www.sitemaps.org/schemas/sitemap/0.9\"" 
                            xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xsi:schemaLocation=\""http://www.google.com/schemas/sitemap-news/0.9 http://www.google.com/schemas/sitemap-news/0.9/sitemap.xsd\"">";

                //List<ElioPages> pages = Sql.GetAllPages(session);
                
                string mainURL = "";
                int count = 0;

                lock (Locker)
                {
                    w.WriteLine(xml);
                    //w.WriteLine("<urlset>");

                    switch (scenario)
                    {
                        case 1: //All public profiles
                            {
                                List<ElioUsers> users = Sql.GetFullRegisteredPublicUsers(id, session);

                                foreach (ElioUsers user in users)
                                {
                                    mainURL = "https://www.elioplus.com" + ControlLoader.Profile(user);

                                    w.WriteLine("   <url>");
                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    w.WriteLine("       <changefreq>yearly</changefreq>");
                                    w.WriteLine("       <priority>0.5</priority>");
                                    w.WriteLine("   </url>");
                                    count++;

                                    if (count >= 50000)
                                    {
                                        WriteSiteMapFile(scenario, user.Id, session);
                                        break;
                                    }
                                }

                                break;
                            }

                        case 2: //Vendors verticals
                            {
                                if (id == 0)    //Only the first time in first file xml
                                {
                                    string[] staticPages = { "https://elioplus.com/prm-software", "https://elioplus.com/channel-partner-recruitment", "https://elioplus.com/intent-signals", "https://elioplus.com/referral-software", "https://elioplus.com/pricing", "https://elioplus.com/search", "https://elioplus.com/case-studies", "https://elioplus.com/login", "https://elioplus.com/free-sign-up", "https://elioplus.com/channel-partners", "https://elioplus.com/partner-portals", "https://elioplus.com/saas-partner-programs", "https://elioplus.com/search/channel-partners", "https://elioplus.com/about-us", "https://elioplus.com/contact-us", "https://elioplus.com/faq", "https://elioplus.com/resources-page", "https://elioplus.com/partner-to-partner-deals", "https://elioplus.com/prm-free-sign-up", "https://elioplus.com/prm-software/crm-integrations", "https://elioplus.com/prm-software/partner-portal", "https://elioplus.com/prm-software/partner-directory", "https://elioplus.com/prm-software/partner-onboarding", "https://elioplus.com/prm-software/deal-registration", "https://elioplus.com/prm-software/lead-distribution", "https://elioplus.com/prm-software/collaboration", "https://elioplus.com/prm-software/channel-analytics", "https://elioplus.com/prm-software/partner-locator", "https://elioplus.com/prm-software/partner-2-partner", "https://elioplus.com/prm-software/partner-activation", "https://elioplus.com/prm-software/tier-management", "https://elioplus.com/prm-software/team-roles", "https://elioplus.com/prm-software/partner-management", "https://elioplus.com/partner-relationship-management-system", "https://elioplus.com/manage-channel-partners", "https://elioplus.com/partner-management", "https://elioplus.com/channel-partner-recruitment-process", "https://elioplus.com/partnering-examples", "https://elioplus.com/networks/comptia", "https://elioplus.com/networks/spiceworks", "https://elioplus.com/alternatives/allbound", "https://elioplus.com/alternatives/impact-company", "https://elioplus.com/alternatives/impartner", "https://elioplus.com/alternatives/mindmatrix", "https://elioplus.com/alternatives/model-n", "https://elioplus.com/alternatives/partnerize", "https://elioplus.com/alternatives/salesforce-communities", "https://elioplus.com/alternatives/zift-solutions", "https://elioplus.com/alternatives/partnerstack", "https://elioplus.com/alternatives/myprm", "https://elioplus.com/alternatives/channeltivity", "https://elioplus.com/intent-signals/australia", "https://elioplus.com/intent-signals/canada", "https://elioplus.com/intent-signals/hong-kong", "https://elioplus.com/intent-signals/south-africa", "https://elioplus.com/intent-signals/india", "https://elioplus.com/intent-signals/singapore", "https://elioplus.com/intent-signals/united-states", "https://elioplus.com/intent-signals/united-kingdom", "https://elioplus.com/intent-signals/united-arab-emirates", "https://elioplus.com/intent-signals/sap", "https://elioplus.com/intent-signals/ibm", "https://elioplus.com/intent-signals/zoho", "https://elioplus.com/intent-signals/kofax", "https://elioplus.com/intent-signals/ringcentral", "https://elioplus.com/intent-signals/salesforce", "https://elioplus.com/intent-signals/microsoft", "https://elioplus.com/intent-signals/lenovo", "https://elioplus.com/intent-signals/symantec", "https://elioplus.com/intent-signals/cloudflare", "https://elioplus.com/intent-signals/mcafee", "https://elioplus.com/intent-signals/crowdstrike", "https://elioplus.com/intent-signals/fortinet", "https://elioplus.com/intent-signals/crm", "https://elioplus.com/intent-signals/meraki", "https://elioplus.com/intent-signals/cisco", "https://elioplus.com/intent-signals/sophos", "https://elioplus.com/intent-signals/mimecast", "https://elioplus.com/intent-signals/erp", "https://elioplus.com/intent-signals/dell", "https://elioplus.com/intent-signals/avaya", "https://elioplus.com/intent-signals/sage", "https://elioplus.com/intent-signals/netsuite", "https://elioplus.com/intent-signals/arista", "https://elioplus.com/intent-signals/netapp", "https://elioplus.com/intent-signals/oracle", "https://elioplus.com/intent-signals/pos", "https://elioplus.com/intent-signals/google", "https://elioplus.com/white-label-partner-programs", "https://elioplus.com/msps-partner-programs", "https://elioplus.com/systems-integrators-partner-programs", "https://elioplus.com/afghanistan/channel-partners", "https://elioplus.com/albania/channel-partners", "https://elioplus.com/algeria/channel-partners", "https://elioplus.com/angola/channel-partners", "https://elioplus.com/armenia/channel-partners", "https://elioplus.com/azerbaijan/channel-partners", "https://elioplus.com/argentina/channel-partners", "https://elioplus.com/australia/channel-partners", "https://elioplus.com/austria/channel-partners", "https://elioplus.com/bahrain/channel-partners", "https://elioplus.com/bangladesh/channel-partners", "https://elioplus.com/barbados/channel-partners", "https://elioplus.com/belarus/channel-partners", "https://elioplus.com/benin/channel-partners", "https://elioplus.com/bermuda/channel-partners", "https://elioplus.com/bolivia/channel-partners", "https://elioplus.com/bosnia-and-herzegovina/channel-partners", "https://elioplus.com/botswana/channel-partners", "https://elioplus.com/bulgaria/channel-partners", "https://elioplus.com/belgium/channel-partners", "https://elioplus.com/brazil/channel-partners", "https://elioplus.com/cambodia/channel-partners", "https://elioplus.com/cameroon/channel-partners", "https://elioplus.com/cape-verde/channel-partners", "https://elioplus.com/chad/channel-partners", "https://elioplus.com/costa-rica/channel-partners", "https://elioplus.com/croatia/channel-partners", "https://elioplus.com/cyprus/channel-partners", "https://elioplus.com/canada/channel-partners", "https://elioplus.com/chile/channel-partners", "https://elioplus.com/colombia/channel-partners", "https://elioplus.com/czech-republic/channel-partners", "https://elioplus.com/denmark/channel-partners", "https://elioplus.com/dominican-republic/channel-partners", "https://elioplus.com/ecuador/channel-partners", "https://elioplus.com/egypt/channel-partners", "https://elioplus.com/el-salvador/channel-partners", "https://elioplus.com/estonia/channel-partners", "https://elioplus.com/ethiopia/channel-partners", "https://elioplus.com/fiji/channel-partners", "https://elioplus.com/finland/channel-partners", "https://elioplus.com/france/channel-partners", "https://elioplus.com/gabon/channel-partners", "https://elioplus.com/georgia/channel-partners", "https://elioplus.com/ghana/channel-partners", "https://elioplus.com/greece/channel-partners", "https://elioplus.com/guatemala/channel-partners", "https://elioplus.com/germany/channel-partners", "https://elioplus.com/honduras/channel-partners", "https://elioplus.com/hungary/channel-partners", "https://elioplus.com/hong-kong/channel-partners", "https://elioplus.com/iceland/channel-partners", "https://elioplus.com/iran/channel-partners", "https://elioplus.com/iraq/channel-partners", "https://elioplus.com/india/channel-partners", "https://elioplus.com/indonesia/channel-partners", "https://elioplus.com/ireland/channel-partners", "https://elioplus.com/israel/channel-partners", "https://elioplus.com/italy/channel-partners", "https://elioplus.com/jamaica/channel-partners", "https://elioplus.com/jordan/channel-partners", "https://elioplus.com/japan/channel-partners", "https://elioplus.com/kazakhstan/channel-partners", "https://elioplus.com/kenya/channel-partners", "https://elioplus.com/kuwait/channel-partners", "https://elioplus.com/kyrgyzstan/channel-partners", "https://elioplus.com/laos/channel-partners", "https://elioplus.com/latvia/channel-partners", "https://elioplus.com/lebanon/channel-partners", "https://elioplus.com/liberia/channel-partners", "https://elioplus.com/libya/channel-partners", "https://elioplus.com/lithuania/channel-partners", "https://elioplus.com/luxembourg/channel-partners", "https://elioplus.com/macedonia/channel-partners", "https://elioplus.com/madagascar/channel-partners", "https://elioplus.com/Malawi/channel-partners", "https://elioplus.com/maldives/channel-partners", "https://elioplus.com/mali/channel-partners", "https://elioplus.com/malta/channel-partners", "https://elioplus.com/mauritania/channel-partners", "https://elioplus.com/monaco/channel-partners", "https://elioplus.com/mongolia/channel-partners", "https://elioplus.com/montenegro/channel-partners", "https://elioplus.com/morocco/channel-partners", "https://elioplus.com/mozambique/channel-partners", "https://elioplus.com/malaysia/channel-partners", "https://elioplus.com/mexico/channel-partners", "https://elioplus.com/namibia/channel-partners", "https://elioplus.com/nepal/channel-partners", "https://elioplus.com/nicaragua/channel-partners", "https://elioplus.com/nigeria/channel-partners", "https://elioplus.com/netherlands/channel-partners", "https://elioplus.com/new-zealand/channel-partners", "https://elioplus.com/norway/channel-partners", "https://elioplus.com/oman/channel-partners", "https://elioplus.com/pakistan/channel-partners", "https://elioplus.com/panama/channel-partners", "https://elioplus.com/papua-new-guinea/channel-partners", "https://elioplus.com/paraguay/channel-partners", "https://elioplus.com/peru/channel-partners", "https://elioplus.com/philippines/channel-partners", "https://elioplus.com/puerto-rico/channel-partners", "https://elioplus.com/poland/channel-partners", "https://elioplus.com/portugal/channel-partners", "https://elioplus.com/qatar/channel-partners", "https://elioplus.com/romania/channel-partners", "https://elioplus.com/rwanda/channel-partners", "https://elioplus.com/russia/channel-partners", "https://elioplus.com/san-marino/channel-partners", "https://elioplus.com/saudi-arabia/channel-partners", "https://elioplus.com/senegal/channel-partners", "https://elioplus.com/serbia/channel-partners", "https://elioplus.com/sierra-leone/channel-partners", "https://elioplus.com/slovakia/channel-partners", "https://elioplus.com/slovenia/channel-partners", "https://elioplus.com/somalia/channel-partners", "https://elioplus.com/sri-lanka/channel-partners", "https://elioplus.com/sudan/channel-partners", "https://elioplus.com/suriname/channel-partners", "https://elioplus.com/syria/channel-partners", "https://elioplus.com/singapore/channel-partners", "https://elioplus.com/south-africa/channel-partners", "https://elioplus.com/spain/channel-partners", "https://elioplus.com/sweden/channel-partners", "https://elioplus.com/switzerland/channel-partners", "https://elioplus.com/tajikistan/channel-partners", "https://elioplus.com/tanzania/channel-partners", "https://elioplus.com/togo/channel-partners", "https://elioplus.com/trinidad-and-tobago/channel-partners", "https://elioplus.com/tunisia/channel-partners", "https://elioplus.com/turkmenistan/channel-partners", "https://elioplus.com/thailand/channel-partners", "https://elioplus.com/turkey/channel-partners", "https://elioplus.com/united-arab-emirates/channel-partners", "https://elioplus.com/united-kingdom/channel-partners", "https://elioplus.com/united-states/channel-partners", "https://elioplus.com/uganda/channel-partners", "https://elioplus.com/ukraine/channel-partners", "https://elioplus.com/uruguay/channel-partners", "https://elioplus.com/venezuela/channel-partners", "https://elioplus.com/vietnam/channel-partners", "https://elioplus.com/yemen/channel-partners", "https://elioplus.com/zambia/channel-partners", "https://elioplus.com/zimbabwe/channel-partners" };

                                    foreach (string page in staticPages)
                                    {
                                        mainURL = page;

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                List<ElioSubIndustriesGroupItems> verticals = Sql.GetVerticalsOverId(id, session);

                                foreach (ElioSubIndustriesGroupItems vertical in verticals)
                                {
                                    string linkUrl = vertical.Description.Replace("&", "and").Replace(" ", "_");
                                    mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles(Types.Vendors.ToString(), linkUrl.ToLower());

                                    w.WriteLine("   <url>");
                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                    w.WriteLine("       <priority>1.0</priority>");
                                    w.WriteLine("   </url>");
                                    count++;

                                    if (count >= 50000)
                                    {
                                        WriteSiteMapFile(scenario, vertical.Id, session);
                                        break;
                                    }

                                    #region To Delete

                                    //string strQuery = @"Select u.*
                                    //                    from Elio_users u 
                                    //                    inner join Elio_users_sub_industries_group_items 
                                    //                    on u.id = Elio_users_sub_industries_group_items.user_id 
                                    //                    where u.is_public = 1 
                                    //                    and company_type = 'Vendors'
                                    //                    and Elio_users_sub_industries_group_items.sub_industry_group_item_id = @sub_industry_group_item_id";

                                    //DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                                    //List<ElioUsers> users = loader.Load(strQuery
                                    //                                , DatabaseHelper.CreateIntParameter("@sub_industry_group_item_id", vertical.Id));

                                    //foreach (ElioUsers user in users)
                                    //{
                                    //    mainURL = "https://www.elioplus.com" + ControlLoader.Profile(user);

                                    //    w.WriteLine("   <url>");
                                    //    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    //    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    //    w.WriteLine("       <changefreq>yearly</changefreq>");
                                    //    w.WriteLine("       <priority>0.5</priority>");
                                    //    w.WriteLine("   </url>");
                                    //    count++;
                                    //}

                                    #endregion
                                }

                                break;
                            }

                        case 3: //Vendors White Label
                            {
                                List<ElioSubIndustriesGroup> groups = Sql.GetAllVerticalCategories(session);
                                foreach (ElioSubIndustriesGroup group in groups)
                                {
                                    List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                                    foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                                    {
                                        mainURL = "https://www.elioplus.com/white-label/vendors/" + industryGroupItem.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 4: //Vendors MSP
                            {
                                List<ElioSubIndustriesGroup> groups = Sql.GetAllVerticalCategories(session);
                                foreach (ElioSubIndustriesGroup group in groups)
                                {
                                    List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                                    foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                                    {
                                        mainURL = "https://www.elioplus.com/msps/vendors/" + industryGroupItem.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 5: //Vendors System Integrators
                            {
                                List<ElioSubIndustriesGroup> groups = Sql.GetAllVerticalCategories(session);
                                foreach (ElioSubIndustriesGroup group in groups)
                                {
                                    List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                                    foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                                    {
                                        mainURL = "https://www.elioplus.com/system-integrators/vendors/" + industryGroupItem.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 6: //Channel Partners verticals WW
                            {
                                List<ElioSubIndustriesGroupItems> verticals = Sql.GetAllVerticals(session);

                                foreach (ElioSubIndustriesGroupItems vertical in verticals)
                                {
                                    string linkUrl = vertical.Description.Replace("&", "and").Replace(" ", "_");
                                    mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles("channel-partners", linkUrl.ToLower());

                                    w.WriteLine("   <url>");
                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                    w.WriteLine("       <priority>1.0</priority>");
                                    w.WriteLine("   </url>");
                                    count++;
                                }

                                break;
                            }

                        case 7: //Channel Partners products WW
                            {
                                List<ElioRegistrationProducts> products = Sql.GetRegistrationProducts(session);

                                foreach (ElioRegistrationProducts product in products)
                                {
                                    string linkUrl = product.Description.Replace("&", "and").Replace(" ", "_").Replace("_\n\n", "").Replace("_\n", "").TrimStart().TrimEnd();
                                    mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles("channel-partners", linkUrl.ToLower());

                                    w.WriteLine("   <url>");
                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                    w.WriteLine("       <priority>1.0</priority>");
                                    w.WriteLine("   </url>");
                                    count++;
                                }

                                break;
                            }

                        case 81: //Channel Partners verticals by regions
                            {
                                DataTable regionsTbl = Sql.GetRegionsFromCountries(session);
                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        DataTable regionVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        if (regionVerts.Rows.Count > 0)
                                        {
                                            //write xml

                                            foreach (DataRow item in regionVerts.Rows)
                                            {
                                                mainURL = "https://www.elioplus.com" + item["link"];

                                                w.WriteLine("   <url>");
                                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                                w.WriteLine("       <priority>1.0</priority>");
                                                w.WriteLine("   </url>");
                                                count++;
                                            }
                                        }

                                        DataTable countriesTbl = Sql.GetCountriesByRegionTbl(rowReg["region"].ToString(), session);
                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityVerts.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityVerts.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 810: //Channel Partners verticals by regions no translations europe
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region = 'Europe'
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionVerts.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionVerts.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region = 'Europe'
                                                                                        and country_name in
                                                                                        (
                                                                                        'France','Spain','Germany','Portugal','Austria','Italy'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityVerts.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityVerts.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 8100: //Channel Partners verticals by regions no translations North America, South America
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region in ('North America','South America')
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionVerts.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionVerts.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region in ('North America','South America')
                                                                                        and country_name in
                                                                                        (
                                                                                        'Costa Rica','Dominican Republic','El Salvador','Guatemala','Honduras','Mexico','Panama','Puerto Rico',
                                                                                        'Brazil','Argentina','Bolivia','Chile','Colombia','Ecuador','Paraguay','Peru','Uruguay','Venezuela'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityVerts = Sql.GetVerticalsWithUsersCountForSearchResults(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityVerts.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityVerts.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryVerts.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryVerts.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesVerts = Sql.GetVerticalsWithUsersCountForSearchResultsWithNoTrans(false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesVerts.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesVerts.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 82: //Channel Partners products by regions -- first 50%
                            {
                                DataTable regionsTbl = Sql.GetRegionsFromCountries(session);
                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        if (regionProds.Rows.Count > 0)
                                        {
                                            //write xml

                                            foreach (DataRow item in regionProds.Rows)
                                            {
                                                mainURL = "https://www.elioplus.com" + item["link"];

                                                w.WriteLine("   <url>");
                                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                                w.WriteLine("       <priority>1.0</priority>");
                                                w.WriteLine("   </url>");
                                                count++;
                                            }
                                        }

                                        DataTable countriesTbl = Sql.GetCountriesByRegionTbl(rowReg["region"].ToString(), session);
                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 820: //Channel Partners products by regions no translations europe -- first 50%
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region = 'Europe'
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionProds.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionProds.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region = 'Europe'
                                                                                        and country_name in
                                                                                        (
                                                                                        'France','Spain','Germany','Portugal','Austria','Italy'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 8200: //Channel Partners products by regions no translations North America, South America -- first 50%
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region in ('North America','South America')
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionProds.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionProds.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region in ('North America','South America')
                                                                                        and country_name in
                                                                                        (
                                                                                        'Costa Rica','Dominican Republic','El Salvador','Guatemala','Honduras','Mexico','Panama','Puerto Rico',
                                                                                        'Brazil','Argentina','Bolivia','Chile','Colombia','Ecuador','Paraguay','Peru','Uruguay','Venezuela'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, true, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 83: //Channel Partners products by regions -- last 50%
                            {
                                DataTable regionsTbl = Sql.GetRegionsFromCountries(session);
                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        if (regionProds.Rows.Count > 0)
                                        {
                                            //write xml

                                            foreach (DataRow item in regionProds.Rows)
                                            {
                                                mainURL = "https://www.elioplus.com" + item["link"];

                                                w.WriteLine("   <url>");
                                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                                w.WriteLine("       <priority>1.0</priority>");
                                                w.WriteLine("   </url>");
                                                count++;
                                            }
                                        }

                                        DataTable countriesTbl = Sql.GetCountriesByRegionTbl(rowReg["region"].ToString(), session);
                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 830: //Channel Partners products by regions no translations europe -- last 50%
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region = 'Europe'
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionProds.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionProds.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region = 'Europe'
                                                                                        and country_name in
                                                                                        (
                                                                                        'France','Spain','Germany','Portugal','Austria','Italy'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                        case 8300: //Channel Partners products by regions no translations North America, South America -- last 50%
                            {
                                DataTable regionsTbl = session.GetDataTable(@"SELECT distinct region, + REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and') + '/channel-partners' as link
                                                                                FROM Elio_countries
                                                                                where is_public = 1
                                                                                and region in ('North America','South America')
                                                                                order by region");

                                if (regionsTbl != null && regionsTbl.Rows.Count > 0)
                                {
                                    foreach (DataRow rowReg in regionsTbl.Rows)
                                    {
                                        //DataTable regionProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, "channel-partners", rowReg["region"].ToString(), "", "", "", session);

                                        //if (regionProds.Rows.Count > 0)
                                        //{
                                        //    //write xml

                                        //    foreach (DataRow item in regionProds.Rows)
                                        //    {
                                        //        mainURL = "https://www.elioplus.com" + item["link"];

                                        //        w.WriteLine("   <url>");
                                        //        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        //        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        //        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        //        w.WriteLine("       <priority>1.0</priority>");
                                        //        w.WriteLine("   </url>");
                                        //        count++;
                                        //    }
                                        //}

                                        DataTable countriesTbl = session.GetDataTable(@"SELECT distinct id
                                                ,                                           country_name, + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(region), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/' + RTRIM(LTRIM(REPLACE(REPLACE(REPLACE(REPLACE(Lower(country_name), ' ', '-'), '&', 'and'), '.', ''), ',',''))) + '/channel-partners' as link
                                                                                        FROM Elio_countries
                                                                                        where is_public = 1
                                                                                        and region in ('North America','South America')
                                                                                        and country_name in
                                                                                        (
                                                                                        'Costa Rica','Dominican Republic','El Salvador','Guatemala','Honduras','Mexico','Panama','Puerto Rico',
                                                                                        'Brazil','Argentina','Bolivia','Chile','Colombia','Ecuador','Paraguay','Peru','Uruguay','Venezuela'
                                                                                        )
                                                                                        order by country_name");

                                        if (countriesTbl != null && countriesTbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow rowCountry in countriesTbl.Rows)
                                            {
                                                if (rowCountry["country_name"].ToString() == "United States" || rowCountry["country_name"].ToString() == "United Kingdom" || rowCountry["country_name"].ToString() == "India" || rowCountry["country_name"].ToString() == "Australia")
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable statesTbl = Sql.GetStatesByCountryTbl(rowCountry["country_name"].ToString(), session);
                                                    if (statesTbl != null && statesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowSate in statesTbl.Rows)
                                                        {
                                                            DataTable regionCountryStateProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), "", session);

                                                            if (regionCountryStateProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryStateProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }

                                                            DataTable citiesByRegionCountryStateTbl = Sql.GetCitiesByRegionCountryStateTbl(rowCountry["country_name"].ToString(), rowSate["state"].ToString(), session);
                                                            if (citiesByRegionCountryStateTbl != null && citiesByRegionCountryStateTbl.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow rowCity in citiesByRegionCountryStateTbl.Rows)
                                                                {
                                                                    DataTable regionCountryStateCityProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), rowSate["state"].ToString(), rowCity["city"].ToString(), session);

                                                                    if (regionCountryStateCityProds.Rows.Count > 0)
                                                                    {
                                                                        //write xml

                                                                        foreach (DataRow item in regionCountryStateCityProds.Rows)
                                                                        {
                                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                                            w.WriteLine("   <url>");
                                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                            w.WriteLine("       <priority>1.0</priority>");
                                                                            w.WriteLine("   </url>");
                                                                            count++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable regionCountryProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", "", session);

                                                    if (regionCountryProds.Rows.Count > 0)
                                                    {
                                                        //write xml

                                                        foreach (DataRow item in regionCountryProds.Rows)
                                                        {
                                                            mainURL = "https://www.elioplus.com" + item["link"];

                                                            w.WriteLine("   <url>");
                                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                                            w.WriteLine("       <priority>1.0</priority>");
                                                            w.WriteLine("   </url>");
                                                            count++;
                                                        }
                                                    }

                                                    DataTable citiesTbl = Sql.GetCitiesByRegionCountryTbl(rowReg["region"].ToString(), rowCountry["country_name"].ToString(), session);
                                                    if (citiesTbl != null && citiesTbl.Rows.Count > 0)
                                                    {
                                                        foreach (DataRow rowCity in citiesTbl.Rows)
                                                        {
                                                            DataTable regionCountryCitiesProds = Sql.GetTechnologiesWithUsersCountForSearchResultsByTopNoTrans(false, false, "channel-partners", rowReg["region"].ToString(), rowCountry["country_name"].ToString(), "", rowCity["city"].ToString(), session);

                                                            if (regionCountryCitiesProds.Rows.Count > 0)
                                                            {
                                                                //write xml

                                                                foreach (DataRow item in regionCountryCitiesProds.Rows)
                                                                {
                                                                    mainURL = "https://www.elioplus.com" + item["link"];

                                                                    w.WriteLine("   <url>");
                                                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                                                    w.WriteLine("       <priority>1.0</priority>");
                                                                    w.WriteLine("   </url>");
                                                                    count++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                            #region Old cases

                            /*
                        case 8: //Channel Partners verticals by country
                            {
                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };
                                List<ElioCountries> countries = Sql.GetPublicCountries(session);

                                foreach (ElioCountries country in countries)
                                {
                                    List<ElioSubIndustriesGroupItems> IndustryGroupItem = Sql.GetSubIndustriesGroupItemsByCountry(country.CountryName, session);

                                    string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                    string region = country.Region.Replace(" ", "-").ToLower();

                                    foreach (var item in IndustryGroupItem)
                                    {
                                        string linkUrl = "/" + region + "/" + countryName + "/channel-partners/" + item.Description.Replace("&", "and").Replace(" ", "_").ToLower();
                                        mainURL = "https://www.elioplus.com" + linkUrl;

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 9: //Channel Partners products by country
                            {
                                DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };
                                List<ElioCountries> countries = Sql.GetPublicCountries(session);

                                foreach (ElioCountries country in countries)
                                {
                                    List<ElioRegistrationProducts> products = loader.Load(@"SELECT rp.id, rp.description
	                                                                                    FROM Elio_registration_products rp
	                                                                                    inner join Elio_users_registration_products urp
		                                                                                    on rp.id = urp.reg_products_id
	                                                                                    inner join elio_users u
		                                                                                    on urp.user_id = u.id
	                                                                                    where rp.is_public = 1
	                                                                                    and u.company_type = 'Channel Partners'
	                                                                                    and country = @country
                                                                                        group by rp.id, rp.description"
                                                                    , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                                    string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                    string region = country.Region.Replace(" ", "-").ToLower();

                                    foreach (ElioRegistrationProducts product in products)
                                    {
                                        string linkUrl = "/" + region + "/" + countryName + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();
                                        mainURL = "https://www.elioplus.com" + linkUrl;

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 10: //Channel Partners verticals by city
                            {
                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };
                                List<ElioCountries> countries = Sql.GetPublicCountries(session);

                                foreach (ElioCountries country in countries)
                                {
                                    List<ElioUsers> cities = loader.Load(@"select eu.city
                                                                            from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                            inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                            where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                            and eu.country = @country
                                                                            and eu.city != ''
                                                                            group by city
                                                                            "
                                                                , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                                    DataLoader<ElioSubIndustriesGroupItems> loaderVert = new DataLoader<ElioSubIndustriesGroupItems>(session);

                                    foreach (ElioUsers user in cities)
                                    {
                                        List<ElioSubIndustriesGroupItems> verticalsByCity = loaderVert.Load(@"select es.sub_industies_group_id, es.description
                                                                                                                from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                                                                inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                                                                where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                                                                and eu.country = @country
                                                                                                                and eu.city = @city
                                                                                                                group by es.sub_industies_group_id, es.description"
                                                                , DatabaseHelper.CreateStringParameter("@country", country.CountryName)
                                                                , DatabaseHelper.CreateStringParameter("@city", user.City));

                                        string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                        string cityName = user.City.Replace(" ", "-").ToLower();
                                        string region = country.Region.Replace(" ", "-").ToLower();

                                        foreach (ElioSubIndustriesGroupItems vertical in verticalsByCity)
                                        {
                                            string linkUrl = "/" + region + "/" + countryName + "/" + cityName + "/channel-partners/" + vertical.Description.Replace("&", "and").Replace(" ", "_").ToLower();
                                            mainURL = "https://www.elioplus.com" + linkUrl;

                                            w.WriteLine("   <url>");
                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                            w.WriteLine("       <priority>1.0</priority>");
                                            w.WriteLine("   </url>");
                                            count++;
                                        }
                                    }
                                }

                                break;
                            }

                        case 11: //Channel Partners products by city
                            {
                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };
                                List<ElioCountries> countries = Sql.GetPublicCountries(session);

                                foreach (ElioCountries country in countries)
                                {
                                    List<ElioUsers> cities = loader.Load(@"SELECT u.city
	                                                                        FROM Elio_registration_products rp
	                                                                        inner join Elio_users_registration_products urp
		                                                                        on rp.id = urp.reg_products_id
	                                                                        inner join elio_users u
		                                                                        on urp.user_id = u.id
	                                                                        where rp.is_public = 1
	                                                                        and u.company_type = 'Channel Partners'
                                                                            and u.country = @country
                                                                            and u.city != ''
                                                                            group by city"
                                                                , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                                    DataLoader<ElioRegistrationProducts> loaderProd = new DataLoader<ElioRegistrationProducts>(session);

                                    foreach (ElioUsers user in cities)
                                    {
                                        List<ElioRegistrationProducts> productsByCity = loaderProd.Load(@"SELECT rp.id, rp.description
	                                                                                                        FROM Elio_registration_products rp
	                                                                                                        inner join Elio_users_registration_products urp
		                                                                                                        on rp.id = urp.reg_products_id
	                                                                                                        inner join elio_users u
		                                                                                                        on urp.user_id = u.id
                                                                                                            where u.company_type = 'Channel Partners' and u.is_public = 1
                                                                                                            and u.country = @country
                                                                                                            and u.city = @city
                                                                                                            group by rp.id, rp.description"
                                                                , DatabaseHelper.CreateStringParameter("@country", country.CountryName)
                                                                , DatabaseHelper.CreateStringParameter("@city", user.City));

                                        string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                        string cityName = user.City.Replace(" ", "-").ToLower();
                                        string region = country.Region.Replace(" ", "-").ToLower();

                                        foreach (ElioRegistrationProducts product in productsByCity)
                                        {
                                            string linkUrl = "/" + region + "/" + countryName + "/" + cityName + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();
                                            mainURL = "https://www.elioplus.com" + linkUrl;

                                            w.WriteLine("   <url>");
                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                            w.WriteLine("       <priority>1.0</priority>");
                                            w.WriteLine("   </url>");
                                            count++;
                                        }
                                    }
                                }

                                break;
                            }

                        case 12: //Channel Partners verticals by country translate
                            {
                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                                foreach (string country in SelectedCountries)
                                {
                                    List<ElioSubIndustriesGroupItems> IndustryGroupItem = Sql.GetSubIndustriesGroupItemsByCountry(country, session);

                                    string countryName = country.Replace(" ", "-").ToLower();

                                    foreach (var item in IndustryGroupItem)
                                    {
                                        string linkUrl = "/" + countryName + "/channel-partners/" + item.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                        if (country == "Austria")
                                        {
                                            linkUrl = "/at" + linkUrl;
                                        }
                                        else if (country == "Brazil" || country == "Portugal")
                                        {
                                            linkUrl = "/pt" + linkUrl;
                                        }
                                        else if (country == "Germany")
                                        {
                                            linkUrl = "/de" + linkUrl;
                                        }
                                        else if (country == "Italy")
                                        {
                                            linkUrl = "/it" + linkUrl;
                                        }
                                        else if (country == "Netherlands")
                                        {
                                            linkUrl = "/nl" + linkUrl;
                                        }
                                        else if (country == "Poland")
                                        {
                                            linkUrl = "/pl" + linkUrl;
                                        }
                                        else if (country == "Spain")
                                        {
                                            linkUrl = "/es" + linkUrl;
                                        }
                                        else
                                            linkUrl = "/la" + linkUrl;

                                        mainURL = "https://www.elioplus.com" + linkUrl;

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 13: //Channel Partners products by country translate
                            {
                                DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                                foreach (string country in SelectedCountries)
                                {
                                    List<ElioRegistrationProducts> products = loader.Load(@"SELECT rp.id, rp.description
	                                                                                    FROM Elio_registration_products rp
	                                                                                    inner join Elio_users_registration_products urp
		                                                                                    on rp.id = urp.reg_products_id
	                                                                                    inner join elio_users u
		                                                                                    on urp.user_id = u.id
	                                                                                    where rp.is_public = 1
	                                                                                    and u.company_type = 'Channel Partners'
	                                                                                    and country = @country
                                                                                        group by rp.id, rp.description"
                                                                    , DatabaseHelper.CreateStringParameter("@country", country));

                                    string countryName = country.Replace(" ", "-").ToLower();
                                    foreach (ElioRegistrationProducts product in products)
                                    {
                                        string linkUrl = "/" + countryName + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                        if (country == "Austria")
                                        {
                                            linkUrl = "/at" + linkUrl;
                                        }
                                        else if (country == "Brazil" || country == "Portugal")
                                        {
                                            linkUrl = "/pt" + linkUrl;
                                        }
                                        else if (country == "Germany")
                                        {
                                            linkUrl = "/de" + linkUrl;
                                        }
                                        else if (country == "Italy")
                                        {
                                            linkUrl = "/it" + linkUrl;
                                        }
                                        else if (country == "Netherlands")
                                        {
                                            linkUrl = "/nl" + linkUrl;
                                        }
                                        else if (country == "Poland")
                                        {
                                            linkUrl = "/pl" + linkUrl;
                                        }
                                        else if (country == "Spain")
                                        {
                                            linkUrl = "/es" + linkUrl;
                                        }
                                        else
                                            linkUrl = "/la" + linkUrl;

                                        mainURL = "https://www.elioplus.com" + linkUrl;

                                        w.WriteLine("   <url>");
                                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                        w.WriteLine("       <changefreq>weekly</changefreq>");
                                        w.WriteLine("       <priority>1.0</priority>");
                                        w.WriteLine("   </url>");
                                        count++;
                                    }
                                }

                                break;
                            }

                        case 14: //Channel Partners verticals by city translate
                            {
                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                                foreach (string country in SelectedCountries)
                                {
                                    List<ElioUsers> cities = loader.Load(@"select eu.city
                                                                            from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                            inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                            where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                            and eu.country = @country
                                                                            and eu.city != ''
                                                                            group by city
                                                                            "
                                                                   , DatabaseHelper.CreateStringParameter("@country", country));

                                    DataLoader<ElioSubIndustriesGroupItems> loaderVert = new DataLoader<ElioSubIndustriesGroupItems>(session);

                                    foreach (ElioUsers user in cities)
                                    {
                                        List<ElioSubIndustriesGroupItems> verticalsByCity = loaderVert.Load(@"select es.sub_industies_group_id, es.description
                                                                                                                from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                                                                inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                                                                where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                                                                and eu.country = @country
                                                                                                                and eu.city = @city
                                                                                                                group by es.sub_industies_group_id, es.description"
                                                                , DatabaseHelper.CreateStringParameter("@country", country)
                                                                , DatabaseHelper.CreateStringParameter("@city", user.City));

                                        string countryName = country.Replace(" ", "-").ToLower();
                                        string cityName = user.City.Replace(" ", "-").ToLower();

                                        foreach (ElioSubIndustriesGroupItems vertical in verticalsByCity)
                                        {
                                            string linkUrl = "/" + cityName + "/channel-partners/" + vertical.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                            if (country == "Austria")
                                            {
                                                linkUrl = "/at" + linkUrl;
                                            }
                                            else if (country == "Brazil" || country == "Portugal")
                                            {
                                                linkUrl = "/pt" + linkUrl;
                                            }
                                            else if (country == "Germany")
                                            {
                                                linkUrl = "/de" + linkUrl;
                                            }
                                            else if (country == "Italy")
                                            {
                                                linkUrl = "/it" + linkUrl;
                                            }
                                            else if (country == "Netherlands")
                                            {
                                                linkUrl = "/nl" + linkUrl;
                                            }
                                            else if (country == "Poland")
                                            {
                                                linkUrl = "/pl" + linkUrl;
                                            }
                                            else if (country == "Spain")
                                            {
                                                linkUrl = "/es" + linkUrl;
                                            }
                                            else
                                                linkUrl = "/la" + linkUrl;

                                            mainURL = "https://www.elioplus.com" + linkUrl;

                                            w.WriteLine("   <url>");
                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                            w.WriteLine("       <priority>1.0</priority>");
                                            w.WriteLine("   </url>");
                                            count++;
                                        }
                                    }
                                }

                                break;
                            }

                        case 15: //Channel Partners products by city translate
                            {
                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                                //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                                string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                                foreach (string country in SelectedCountries)
                                {
                                    List<ElioUsers> cities = loader.Load(@"SELECT u.city
	                                                                        FROM Elio_registration_products rp
	                                                                        inner join Elio_users_registration_products urp
		                                                                        on rp.id = urp.reg_products_id
	                                                                        inner join elio_users u
		                                                                        on urp.user_id = u.id
	                                                                        where rp.is_public = 1
	                                                                        and u.company_type = 'Channel Partners'
                                                                            and u.country = @country
                                                                            and u.city != ''
                                                                            group by city"
                                                                , DatabaseHelper.CreateStringParameter("@country", country));

                                    DataLoader<ElioRegistrationProducts> loaderProd = new DataLoader<ElioRegistrationProducts>(session);

                                    foreach (ElioUsers user in cities)
                                    {
                                        List<ElioRegistrationProducts> productsByCity = loaderProd.Load(@"SELECT rp.id, rp.description
	                                                                                                        FROM Elio_registration_products rp
	                                                                                                        inner join Elio_users_registration_products urp
		                                                                                                        on rp.id = urp.reg_products_id
	                                                                                                        inner join elio_users u
		                                                                                                        on urp.user_id = u.id
                                                                                                            where u.company_type = 'Channel Partners' and u.is_public = 1
                                                                                                            and u.country = @country
                                                                                                            and u.city = @city
                                                                                                            group by rp.id, rp.description"
                                                                , DatabaseHelper.CreateStringParameter("@country", country)
                                                                , DatabaseHelper.CreateStringParameter("@city", user.City));

                                        string countryName = country.Replace(" ", "-").ToLower();
                                        string cityName = user.City.Replace(" ", "-").ToLower();

                                        foreach (ElioRegistrationProducts product in productsByCity)
                                        {
                                            string linkUrl = "/" + cityName + "/channel-partners/" + product.Description.Replace("&", "and").Replace(" ", "_").ToLower();

                                            if (country == "Austria")
                                            {
                                                linkUrl = "/at" + linkUrl;
                                            }
                                            else if (country == "Brazil" || country == "Portugal")
                                            {
                                                linkUrl = "/pt" + linkUrl;
                                            }
                                            else if (country == "Germany")
                                            {
                                                linkUrl = "/de" + linkUrl;
                                            }
                                            else if (country == "Italy")
                                            {
                                                linkUrl = "/it" + linkUrl;
                                            }
                                            else if (country == "Netherlands")
                                            {
                                                linkUrl = "/nl" + linkUrl;
                                            }
                                            else if (country == "Poland")
                                            {
                                                linkUrl = "/pl" + linkUrl;
                                            }
                                            else if (country == "Spain")
                                            {
                                                linkUrl = "/es" + linkUrl;
                                            }
                                            else
                                                linkUrl = "/la" + linkUrl;

                                            mainURL = "https://www.elioplus.com" + linkUrl;

                                            w.WriteLine("   <url>");
                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                            w.WriteLine("       <priority>1.0</priority>");
                                            w.WriteLine("   </url>");
                                            count++;
                                        }
                                    }
                                }

                                break;
                            }

                            */

                            #endregion
                    }

                    w.WriteLine("</urlset>");
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                w.Close();
                w.Dispose();
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        static public void WriteAllSiteMapFiles(int scenario, int id)
        {
            DBSession session = new DBSession();

            string path = GetFileName(LogSiteMapPath, scenario, "");
            if (string.IsNullOrEmpty(path))
                return;
            
            System.IO.StreamWriter w = new System.IO.StreamWriter(path, true, System.Text.Encoding.Default);

            try
            {
                string xml = @"<?xml version=\""1.0\"" encoding=\""utf-8\""?><urlset xmlns=\""http://www.sitemaps.org/schemas/sitemap/0.9\"" 
                            xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xsi:schemaLocation=\""http://www.google.com/schemas/sitemap-news/0.9 http://www.google.com/schemas/sitemap-news/0.9/sitemap.xsd\"">";

                //List<ElioPages> pages = Sql.GetAllPages(session);

                string mainURL = "";
                int count = 0;

                //lock (Locker)
                //{
                w.WriteLine(xml);
                //w.WriteLine("<urlset>");


                if (scenario == 1) //All public profiles
                {
                    try
                    {
                        if (id == 0)    //Only the first time in first file xml
                        {
                            string[] staticPages = { "https://elioplus.com/prm-software", "https://elioplus.com/channel-partner-recruitment", "https://elioplus.com/intent-signals", "https://elioplus.com/referral-software", "https://elioplus.com/pricing", "https://elioplus.com/search", "https://elioplus.com/case-studies", "https://elioplus.com/login", "https://elioplus.com/free-sign-up", "https://elioplus.com/channel-partners", "https://elioplus.com/partner-portals", "https://elioplus.com/saas-partner-programs", "https://elioplus.com/search/channel-partners", "https://elioplus.com/about-us", "https://elioplus.com/contact-us", "https://elioplus.com/faq", "https://elioplus.com/resources-page", "https://elioplus.com/partner-to-partner-deals", "https://elioplus.com/prm-free-sign-up", "https://elioplus.com/prm-software/crm-integrations", "https://elioplus.com/prm-software/partner-portal", "https://elioplus.com/prm-software/partner-directory", "https://elioplus.com/prm-software/partner-onboarding", "https://elioplus.com/prm-software/deal-registration", "https://elioplus.com/prm-software/lead-distribution", "https://elioplus.com/prm-software/collaboration", "https://elioplus.com/prm-software/channel-analytics", "https://elioplus.com/prm-software/partner-locator", "https://elioplus.com/prm-software/partner-2-partner", "https://elioplus.com/prm-software/partner-activation", "https://elioplus.com/prm-software/tier-management", "https://elioplus.com/prm-software/team-roles", "https://elioplus.com/prm-software/partner-management", "https://elioplus.com/partner-relationship-management-system", "https://elioplus.com/manage-channel-partners", "https://elioplus.com/partner-management", "https://elioplus.com/channel-partner-recruitment-process", "https://elioplus.com/partnering-examples", "https://elioplus.com/networks/comptia", "https://elioplus.com/networks/spiceworks", "https://elioplus.com/alternatives/allbound", "https://elioplus.com/alternatives/impact-company", "https://elioplus.com/alternatives/impartner", "https://elioplus.com/alternatives/mindmatrix", "https://elioplus.com/alternatives/model-n", "https://elioplus.com/alternatives/partnerize", "https://elioplus.com/alternatives/salesforce-communities", "https://elioplus.com/alternatives/zift-solutions", "https://elioplus.com/alternatives/partnerstack", "https://elioplus.com/alternatives/myprm", "https://elioplus.com/alternatives/channeltivity", "https://elioplus.com/intent-signals/australia", "https://elioplus.com/intent-signals/canada", "https://elioplus.com/intent-signals/hong-kong", "https://elioplus.com/intent-signals/south-africa", "https://elioplus.com/intent-signals/india", "https://elioplus.com/intent-signals/singapore", "https://elioplus.com/intent-signals/united-states", "https://elioplus.com/intent-signals/united-kingdom", "https://elioplus.com/intent-signals/united-arab-emirates", "https://elioplus.com/intent-signals/sap", "https://elioplus.com/intent-signals/ibm", "https://elioplus.com/intent-signals/zoho", "https://elioplus.com/intent-signals/kofax", "https://elioplus.com/intent-signals/ringcentral", "https://elioplus.com/intent-signals/salesforce", "https://elioplus.com/intent-signals/microsoft", "https://elioplus.com/intent-signals/lenovo", "https://elioplus.com/intent-signals/symantec", "https://elioplus.com/intent-signals/cloudflare", "https://elioplus.com/intent-signals/mcafee", "https://elioplus.com/intent-signals/crowdstrike", "https://elioplus.com/intent-signals/fortinet", "https://elioplus.com/intent-signals/crm", "https://elioplus.com/intent-signals/meraki", "https://elioplus.com/intent-signals/cisco", "https://elioplus.com/intent-signals/sophos", "https://elioplus.com/intent-signals/mimecast", "https://elioplus.com/intent-signals/erp", "https://elioplus.com/intent-signals/dell", "https://elioplus.com/intent-signals/avaya", "https://elioplus.com/intent-signals/sage", "https://elioplus.com/intent-signals/netsuite", "https://elioplus.com/intent-signals/arista", "https://elioplus.com/intent-signals/netapp", "https://elioplus.com/intent-signals/oracle", "https://elioplus.com/intent-signals/pos", "https://elioplus.com/intent-signals/google", "https://elioplus.com/white-label-partner-programs", "https://elioplus.com/msps-partner-programs", "https://elioplus.com/systems-integrators-partner-programs", "https://elioplus.com/afghanistan/channel-partners", "https://elioplus.com/albania/channel-partners", "https://elioplus.com/algeria/channel-partners", "https://elioplus.com/angola/channel-partners", "https://elioplus.com/armenia/channel-partners", "https://elioplus.com/azerbaijan/channel-partners", "https://elioplus.com/argentina/channel-partners", "https://elioplus.com/australia/channel-partners", "https://elioplus.com/austria/channel-partners", "https://elioplus.com/bahrain/channel-partners", "https://elioplus.com/bangladesh/channel-partners", "https://elioplus.com/barbados/channel-partners", "https://elioplus.com/belarus/channel-partners", "https://elioplus.com/benin/channel-partners", "https://elioplus.com/bermuda/channel-partners", "https://elioplus.com/bolivia/channel-partners", "https://elioplus.com/bosnia-and-herzegovina/channel-partners", "https://elioplus.com/botswana/channel-partners", "https://elioplus.com/bulgaria/channel-partners", "https://elioplus.com/belgium/channel-partners", "https://elioplus.com/brazil/channel-partners", "https://elioplus.com/cambodia/channel-partners", "https://elioplus.com/cameroon/channel-partners", "https://elioplus.com/cape-verde/channel-partners", "https://elioplus.com/chad/channel-partners", "https://elioplus.com/costa-rica/channel-partners", "https://elioplus.com/croatia/channel-partners", "https://elioplus.com/cyprus/channel-partners", "https://elioplus.com/canada/channel-partners", "https://elioplus.com/chile/channel-partners", "https://elioplus.com/colombia/channel-partners", "https://elioplus.com/czech-republic/channel-partners", "https://elioplus.com/denmark/channel-partners", "https://elioplus.com/dominican-republic/channel-partners", "https://elioplus.com/ecuador/channel-partners", "https://elioplus.com/egypt/channel-partners", "https://elioplus.com/el-salvador/channel-partners", "https://elioplus.com/estonia/channel-partners", "https://elioplus.com/ethiopia/channel-partners", "https://elioplus.com/fiji/channel-partners", "https://elioplus.com/finland/channel-partners", "https://elioplus.com/france/channel-partners", "https://elioplus.com/gabon/channel-partners", "https://elioplus.com/georgia/channel-partners", "https://elioplus.com/ghana/channel-partners", "https://elioplus.com/greece/channel-partners", "https://elioplus.com/guatemala/channel-partners", "https://elioplus.com/germany/channel-partners", "https://elioplus.com/honduras/channel-partners", "https://elioplus.com/hungary/channel-partners", "https://elioplus.com/hong-kong/channel-partners", "https://elioplus.com/iceland/channel-partners", "https://elioplus.com/iran/channel-partners", "https://elioplus.com/iraq/channel-partners", "https://elioplus.com/india/channel-partners", "https://elioplus.com/indonesia/channel-partners", "https://elioplus.com/ireland/channel-partners", "https://elioplus.com/israel/channel-partners", "https://elioplus.com/italy/channel-partners", "https://elioplus.com/jamaica/channel-partners", "https://elioplus.com/jordan/channel-partners", "https://elioplus.com/japan/channel-partners", "https://elioplus.com/kazakhstan/channel-partners", "https://elioplus.com/kenya/channel-partners", "https://elioplus.com/kuwait/channel-partners", "https://elioplus.com/kyrgyzstan/channel-partners", "https://elioplus.com/laos/channel-partners", "https://elioplus.com/latvia/channel-partners", "https://elioplus.com/lebanon/channel-partners", "https://elioplus.com/liberia/channel-partners", "https://elioplus.com/libya/channel-partners", "https://elioplus.com/lithuania/channel-partners", "https://elioplus.com/luxembourg/channel-partners", "https://elioplus.com/macedonia/channel-partners", "https://elioplus.com/madagascar/channel-partners", "https://elioplus.com/Malawi/channel-partners", "https://elioplus.com/maldives/channel-partners", "https://elioplus.com/mali/channel-partners", "https://elioplus.com/malta/channel-partners", "https://elioplus.com/mauritania/channel-partners", "https://elioplus.com/monaco/channel-partners", "https://elioplus.com/mongolia/channel-partners", "https://elioplus.com/montenegro/channel-partners", "https://elioplus.com/morocco/channel-partners", "https://elioplus.com/mozambique/channel-partners", "https://elioplus.com/malaysia/channel-partners", "https://elioplus.com/mexico/channel-partners", "https://elioplus.com/namibia/channel-partners", "https://elioplus.com/nepal/channel-partners", "https://elioplus.com/nicaragua/channel-partners", "https://elioplus.com/nigeria/channel-partners", "https://elioplus.com/netherlands/channel-partners", "https://elioplus.com/new-zealand/channel-partners", "https://elioplus.com/norway/channel-partners", "https://elioplus.com/oman/channel-partners", "https://elioplus.com/pakistan/channel-partners", "https://elioplus.com/panama/channel-partners", "https://elioplus.com/papua-new-guinea/channel-partners", "https://elioplus.com/paraguay/channel-partners", "https://elioplus.com/peru/channel-partners", "https://elioplus.com/philippines/channel-partners", "https://elioplus.com/puerto-rico/channel-partners", "https://elioplus.com/poland/channel-partners", "https://elioplus.com/portugal/channel-partners", "https://elioplus.com/qatar/channel-partners", "https://elioplus.com/romania/channel-partners", "https://elioplus.com/rwanda/channel-partners", "https://elioplus.com/russia/channel-partners", "https://elioplus.com/san-marino/channel-partners", "https://elioplus.com/saudi-arabia/channel-partners", "https://elioplus.com/senegal/channel-partners", "https://elioplus.com/serbia/channel-partners", "https://elioplus.com/sierra-leone/channel-partners", "https://elioplus.com/slovakia/channel-partners", "https://elioplus.com/slovenia/channel-partners", "https://elioplus.com/somalia/channel-partners", "https://elioplus.com/sri-lanka/channel-partners", "https://elioplus.com/sudan/channel-partners", "https://elioplus.com/suriname/channel-partners", "https://elioplus.com/syria/channel-partners", "https://elioplus.com/singapore/channel-partners", "https://elioplus.com/south-africa/channel-partners", "https://elioplus.com/spain/channel-partners", "https://elioplus.com/sweden/channel-partners", "https://elioplus.com/switzerland/channel-partners", "https://elioplus.com/tajikistan/channel-partners", "https://elioplus.com/tanzania/channel-partners", "https://elioplus.com/togo/channel-partners", "https://elioplus.com/trinidad-and-tobago/channel-partners", "https://elioplus.com/tunisia/channel-partners", "https://elioplus.com/turkmenistan/channel-partners", "https://elioplus.com/thailand/channel-partners", "https://elioplus.com/turkey/channel-partners", "https://elioplus.com/united-arab-emirates/channel-partners", "https://elioplus.com/united-kingdom/channel-partners", "https://elioplus.com/united-states/channel-partners", "https://elioplus.com/uganda/channel-partners", "https://elioplus.com/ukraine/channel-partners", "https://elioplus.com/uruguay/channel-partners", "https://elioplus.com/venezuela/channel-partners", "https://elioplus.com/vietnam/channel-partners", "https://elioplus.com/yemen/channel-partners", "https://elioplus.com/zambia/channel-partners", "https://elioplus.com/zimbabwe/channel-partners" };

                            foreach (string page in staticPages)
                            {
                                mainURL = page;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }
                        }

                        session.OpenConnection();
                        List<ElioUsers> users = Sql.GetFullRegisteredPublicUsers(id, session);
                        session.CloseConnection();

                        foreach (ElioUsers user in users)
                        {
                            mainURL = "https://www.elioplus.com" + ControlLoader.Profile(user);

                            w.WriteLine("   <url>");
                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                            w.WriteLine("       <changefreq>yearly</changefreq>");
                            w.WriteLine("       <priority>0.5</priority>");
                            w.WriteLine("   </url>");
                            count++;

                            //if (count >= 50000)
                            //{
                            //    w.WriteLine("</urlset>");
                            //    w.Close();
                            //    w.Dispose();
                            //    WriteAllSiteMapFiles(scenario, user.Id);
                            //}
                        }

                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();

                        id = 0;
                        //scenario++;
                        count = 0;
                        return;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 2) //Vendors verticals
                {
                    try
                    {
                        session.OpenConnection();
                        List<ElioSubIndustriesGroupItems> verticals = Sql.GetVerticalsOverId(id, session);
                        session.CloseConnection();

                        foreach (ElioSubIndustriesGroupItems vertical in verticals)
                        {
                            string linkUrl = vertical.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_");
                            mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles(Types.Vendors.ToString(), linkUrl.ToLower());

                            w.WriteLine("   <url>");
                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                            w.WriteLine("       <changefreq>weekly</changefreq>");
                            w.WriteLine("       <priority>1.0</priority>");
                            w.WriteLine("   </url>");
                            count++;

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, vertical.Id);
                            }
                        }

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 3) //Vendors White Label
                {
                    try
                    {
                        session.OpenConnection();
                        List<ElioSubIndustriesGroup> groups = Sql.GetVerticalCategoriesOverId(id, session);

                        foreach (ElioSubIndustriesGroup group in groups)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                            foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                            {
                                mainURL = "https://www.elioplus.com/white-label/vendors/" + industryGroupItem.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, group.Id);
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 4) //Vendors MSP
                {
                    try
                    {
                        session.OpenConnection();
                        List<ElioSubIndustriesGroup> groups = Sql.GetVerticalCategoriesOverId(id, session);
                        foreach (ElioSubIndustriesGroup group in groups)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                            foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                            {
                                mainURL = "https://www.elioplus.com/msps/vendors/" + industryGroupItem.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, group.Id);
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 5) //Vendors System Integrators
                {
                    try
                    {
                        session.OpenConnection();
                        List<ElioSubIndustriesGroup> groups = Sql.GetVerticalCategoriesOverId(id, session);
                        foreach (ElioSubIndustriesGroup group in groups)
                        {
                            List<ElioSubIndustriesGroupItems> industryGroupItems = Sql.GetSubIndustriesGroupItemsByIndustryGroupIdOrderById(group.Id, session);

                            foreach (ElioSubIndustriesGroupItems industryGroupItem in industryGroupItems)
                            {
                                mainURL = "https://www.elioplus.com/system-integrators/vendors/" + industryGroupItem.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, group.Id);
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 6) //Channel Partners verticals WW
                {
                    try
                    {
                        session.OpenConnection();
                        List<ElioSubIndustriesGroupItems> verticals = Sql.GetVerticalsOverId(id, session);
                        session.CloseConnection();

                        foreach (ElioSubIndustriesGroupItems vertical in verticals)
                        {
                            string linkUrl = vertical.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_");
                            mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles("channel-partners", linkUrl.ToLower());

                            w.WriteLine("   <url>");
                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                            w.WriteLine("       <changefreq>weekly</changefreq>");
                            w.WriteLine("       <priority>1.0</priority>");
                            w.WriteLine("   </url>");
                            count++;

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, vertical.Id);
                            }
                        }

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 7) //Channel Partners products WW
                {
                    session.OpenConnection();
                    List<ElioRegistrationProducts> products = Sql.GetRegistrationProductsOverId(id, session);
                    session.CloseConnection();

                    foreach (ElioRegistrationProducts product in products)
                    {
                        string linkUrl = product.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").Replace("_\n\n", "").Replace("_\n", "").TrimStart().TrimEnd();
                        mainURL = "https://www.elioplus.com" + ControlLoader.SubIndustryProfiles("channel-partners", linkUrl.ToLower());

                        w.WriteLine("   <url>");
                        w.WriteLine("       <loc>" + mainURL + "</loc>");
                        w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                        w.WriteLine("       <changefreq>weekly</changefreq>");
                        w.WriteLine("       <priority>1.0</priority>");
                        w.WriteLine("   </url>");
                        count++;

                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, product.Id);
                        }
                    }

                    id = 0;
                    scenario++;
                }

                if (scenario == 8) //Channel Partners verticals by country
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };

                        session.OpenConnection();
                        List<ElioCountries> countries = Sql.GetPublicCountriesOverId(id, session);

                        foreach (ElioCountries country in countries)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioSubIndustriesGroupItems> IndustryGroupItem = Sql.GetSubIndustriesGroupItemsByCountry(country.CountryName, session);

                            string countryName = country.CountryName.Replace(" ", "-").ToLower();
                            foreach (var item in IndustryGroupItem)
                            {
                                string linkUrl = "/" + countryName + "/channel-partners/" + item.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();
                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, country.Id);
                            }
                        }

                        session.CloseConnection();
                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 9) //Channel Partners products by country
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };

                        session.OpenConnection();
                        List<ElioCountries> countries = Sql.GetPublicCountriesOverId(id, session);

                        DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

                        foreach (ElioCountries country in countries)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioRegistrationProducts> products = loader.Load(@"SELECT rp.id, rp.description
	                                                                                    FROM Elio_registration_products rp
	                                                                                    inner join Elio_users_registration_products urp
		                                                                                    on rp.id = urp.reg_products_id
	                                                                                    inner join elio_users u
		                                                                                    on urp.user_id = u.id
	                                                                                    where rp.is_public = 1
	                                                                                    and u.company_type = 'Channel Partners'
	                                                                                    and country = @country
                                                                                        group by rp.id, rp.description"
                                                            , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                            string countryName = country.CountryName.Replace(" ", "-").ToLower();
                            foreach (ElioRegistrationProducts product in products)
                            {
                                string linkUrl = "/" + countryName + "/channel-partners/" + product.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();
                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, country.Id);
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 10) //Channel Partners verticals by city
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };

                        session.OpenConnection();
                        List<ElioCountries> countries = Sql.GetPublicCountriesOverId(id, session);

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                        foreach (ElioCountries country in countries)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioUsers> cities = loader.Load(@"select eu.city
                                                                            from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                            inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                            where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                            and eu.country = @country
                                                                            and eu.city != ''
                                                                            group by city"
                                                        , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                            DataLoader<ElioSubIndustriesGroupItems> loaderVert = new DataLoader<ElioSubIndustriesGroupItems>(session);

                            foreach (ElioUsers user in cities)
                            {
                                if (session.Connection.State == System.Data.ConnectionState.Closed)
                                    session.OpenConnection();

                                List<ElioSubIndustriesGroupItems> verticalsByCity = loaderVert.Load(@"select es.sub_industies_group_id, es.description
                                                                                                                from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                                                                inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                                                                where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                                                                and eu.country = @country
                                                                                                                and eu.city = @city
                                                                                                                group by es.sub_industies_group_id, es.description"
                                                        , DatabaseHelper.CreateStringParameter("@country", country.CountryName)
                                                        , DatabaseHelper.CreateStringParameter("@city", user.City));

                                string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                string cityName = user.City.Replace(" ", "-").ToLower();

                                foreach (ElioSubIndustriesGroupItems vertical in verticalsByCity)
                                {
                                    string linkUrl = "/" + cityName + "/channel-partners/" + vertical.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();
                                    mainURL = "https://www.elioplus.com" + linkUrl;

                                    w.WriteLine("   <url>");
                                    w.WriteLine("       <loc>" + mainURL + "</loc>");
                                    w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                    w.WriteLine("       <changefreq>weekly</changefreq>");
                                    w.WriteLine("       <priority>1.0</priority>");
                                    w.WriteLine("   </url>");
                                    count++;
                                }
                            }

                            if (count >= 50000)
                            {
                                w.WriteLine("</urlset>");
                                w.Close();
                                w.Dispose();
                                WriteAllSiteMapFiles(scenario, country.Id);
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 11) //Channel Partners products by city
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Afghanistan", "Albania", "Algeria", "Angola", "Armenia", "Azerbaijan", "Argentina", "Australia", "Austria", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Benin", "Bermuda", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bulgaria", "Belgium", "Brazil", "Cambodia", "Cameroon", "Cape Verde", "Chad", "China, People s Republic of", "China, Republic of (Taiwan)", "Congo, (Congo Â– Kinshasa)", "Costa Rica", "Cote d Ivoire (Ivory Coast)", "Croatia", "Cyprus", "Canada", "Chile", "Colombia", "Czech Republic", "Denmark", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Estonia", "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia, The", "Georgia", "Ghana", "Greece", "Guatemala", "Germany", "Honduras", "Hungary", "Hong Kong", "Iceland", "Iran", "Iraq", "India", "Indonesia", "Ireland", "Israel", "Italy", "Jamaica", "Jordan", "Japan", "Kazakhstan", "Kenya", "Korea, South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Maldives", "Mali", "Malta", "Mauritania", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar (Burma)", "Malaysia", "Mexico", "Namibia", "Nepal", "Nicaragua", "Nigeria", "Netherlands", "New Zealand", "Norway", "Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Puerto Rico", "Poland", "Portugal", "Qatar", "Romania", "Rwanda", "Russia", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Sierra Leone", "Slovakia", "Slovenia", "Somalia", "Sri Lanka", "Sudan", "Suriname", "Syria", "Singapore", "South Africa", "Spain", "Sweden", "Switzerland", "Tajikistan", "Tanzania", "Togo", "Trinidad and Tobago", "Tunisia", "Turkmenistan", "Thailand", "Turkey", "United Arab Emirates", "United Kingdom", "United States", "Uganda", "Ukraine", "Uruguay", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };

                        session.OpenConnection();
                        List<ElioCountries> countries = Sql.GetPublicCountriesOverId(id, session);

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                        DataLoader<ElioRegistrationProducts> loaderProd = new DataLoader<ElioRegistrationProducts>(session);

                        foreach (ElioCountries country in countries)
                        {
                            try
                            {
                                if (session.Connection.State == System.Data.ConnectionState.Closed)
                                    session.OpenConnection();

                                List<ElioUsers> cities = loader.Load(@"SELECT u.city
	                                                                        FROM Elio_registration_products rp
	                                                                        inner join Elio_users_registration_products urp
		                                                                        on rp.id = urp.reg_products_id
	                                                                        inner join elio_users u
		                                                                        on urp.user_id = u.id
	                                                                        where rp.is_public = 1
	                                                                        and u.company_type = 'Channel Partners'
                                                                            and u.country = @country
                                                                            and u.city != ''
                                                                            group by city
                                                                            order by city"
                                                            , DatabaseHelper.CreateStringParameter("@country", country.CountryName));

                                Logger.Debug("Country ID:" + country.Id + " with cities count:" + cities.Count);

                                foreach (ElioUsers user in cities)
                                {
                                    try
                                    {
                                        if (session.Connection.State == System.Data.ConnectionState.Closed)
                                            session.OpenConnection();

                                        List<ElioRegistrationProducts> productsByCity = loaderProd.Load(@"SELECT rp.id, rp.description
	                                                                                                        FROM Elio_registration_products rp
	                                                                                                        inner join Elio_users_registration_products urp
		                                                                                                        on rp.id = urp.reg_products_id
	                                                                                                        inner join elio_users u
		                                                                                                        on urp.user_id = u.id
                                                                                                            where u.company_type = 'Channel Partners' and u.is_public = 1
                                                                                                            and u.country = @country
                                                                                                            and u.city = @city
                                                                                                            group by rp.id, rp.description"
                                                                , DatabaseHelper.CreateStringParameter("@country", country.CountryName)
                                                                , DatabaseHelper.CreateStringParameter("@city", user.City));

                                        string countryName = country.CountryName.Replace(" ", "-").ToLower();
                                        string cityName = user.City.Replace(" ", "-").ToLower();

                                        Logger.Debug("City:" + user.City + " with products count:" + productsByCity.Count);

                                        foreach (ElioRegistrationProducts product in productsByCity)
                                        {
                                            string linkUrl = "/" + cityName + "/channel-partners/" + product.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();
                                            mainURL = "https://www.elioplus.com" + linkUrl;

                                            w.WriteLine("   <url>");
                                            w.WriteLine("       <loc>" + mainURL + "</loc>");
                                            w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                            w.WriteLine("       <changefreq>weekly</changefreq>");
                                            w.WriteLine("       <priority>1.0</priority>");
                                            w.WriteLine("   </url>");
                                            count++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(ex.Message.ToString(), "City: " + user.City + " for Country ID:" + country.Id.ToString() + ex.StackTrace.ToString());
                                    }
                                }

                                if (count >= 50000)
                                {
                                    w.WriteLine("</urlset>");
                                    w.Close();
                                    w.Dispose();
                                    WriteAllSiteMapFiles(scenario, country.Id);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(ex.Message.ToString(), "Country ID: " + country.Id.ToString() + ex.StackTrace.ToString());
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 12) //Channel Partners verticals by country translate
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                        session.OpenConnection();

                        foreach (string country in SelectedCountries)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioSubIndustriesGroupItems> IndustryGroupItem = Sql.GetSubIndustriesGroupItemsByCountry(country, session);

                            string countryName = country.Replace(" ", "-").ToLower();
                            foreach (var item in IndustryGroupItem)
                            {
                                string linkUrl = "/" + countryName + "/channel-partners/" + item.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                if (country == "Austria")
                                {
                                    linkUrl = "/at" + linkUrl;
                                }
                                else if (country == "Brazil" || country == "Portugal")
                                {
                                    linkUrl = "/pt" + linkUrl;
                                }
                                else if (country == "Germany")
                                {
                                    linkUrl = "/de" + linkUrl;
                                }
                                else if (country == "Italy")
                                {
                                    linkUrl = "/it" + linkUrl;
                                }
                                else if (country == "Netherlands")
                                {
                                    linkUrl = "/nl" + linkUrl;
                                }
                                else if (country == "Poland")
                                {
                                    linkUrl = "/pl" + linkUrl;
                                }
                                else if (country == "Spain")
                                {
                                    linkUrl = "/es" + linkUrl;
                                }
                                else
                                    linkUrl = "/la" + linkUrl;

                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 13) //Channel Partners products by country translate
                {
                    try
                    {
                        if (count >= 50000)
                        {
                            w.WriteLine("</urlset>");
                            w.Close();
                            w.Dispose();
                            WriteAllSiteMapFiles(scenario, id);
                        }

                        //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                        string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                        session.OpenConnection();

                        DataLoader<ElioRegistrationProducts> loader = new DataLoader<ElioRegistrationProducts>(session);

                        foreach (string country in SelectedCountries)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioRegistrationProducts> products = loader.Load(@"SELECT rp.id, rp.description
	                                                                                    FROM Elio_registration_products rp
	                                                                                    inner join Elio_users_registration_products urp
		                                                                                    on rp.id = urp.reg_products_id
	                                                                                    inner join elio_users u
		                                                                                    on urp.user_id = u.id
	                                                                                    where rp.is_public = 1
	                                                                                    and u.company_type = 'Channel Partners'
	                                                                                    and country = @country
                                                                                        group by rp.id, rp.description"
                                                            , DatabaseHelper.CreateStringParameter("@country", country));

                            string countryName = country.Replace(" ", "-").ToLower();
                            foreach (ElioRegistrationProducts product in products)
                            {
                                string linkUrl = "/" + countryName + "/channel-partners/" + product.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                if (country == "Austria")
                                {
                                    linkUrl = "/at" + linkUrl;
                                }
                                else if (country == "Brazil" || country == "Portugal")
                                {
                                    linkUrl = "/pt" + linkUrl;
                                }
                                else if (country == "Germany")
                                {
                                    linkUrl = "/de" + linkUrl;
                                }
                                else if (country == "Italy")
                                {
                                    linkUrl = "/it" + linkUrl;
                                }
                                else if (country == "Netherlands")
                                {
                                    linkUrl = "/nl" + linkUrl;
                                }
                                else if (country == "Poland")
                                {
                                    linkUrl = "/pl" + linkUrl;
                                }
                                else if (country == "Spain")
                                {
                                    linkUrl = "/es" + linkUrl;
                                }
                                else
                                    linkUrl = "/la" + linkUrl;

                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }
                        }

                        session.CloseConnection();

                        id = 0;
                        scenario++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                if (scenario == 14) //Channel Partners verticals by city translate
                {
                    if (count >= 50000)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        WriteAllSiteMapFiles(scenario, id);
                    }

                    //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                    string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                    session.OpenConnection();

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                    foreach (string country in SelectedCountries)
                    {
                        if (session.Connection.State == System.Data.ConnectionState.Closed)
                            session.OpenConnection();

                        List<ElioUsers> cities = loader.Load(@"select eu.city
                                                                            from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                            inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                            where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                            and eu.country = @country
                                                                            and eu.city != ''
                                                                            group by city
                                                                            order by city"
                                                       , DatabaseHelper.CreateStringParameter("@country", country));

                        DataLoader<ElioSubIndustriesGroupItems> loaderVert = new DataLoader<ElioSubIndustriesGroupItems>(session);

                        foreach (ElioUsers user in cities)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioSubIndustriesGroupItems> verticalsByCity = loaderVert.Load(@"select es.sub_industies_group_id, es.description
                                                                                                                from elio_users eu inner join Elio_users_sub_industries_group_items eus on eu.id = eus.user_id
                                                                                                                inner join Elio_sub_industries_group_items es on es.id = eus.sub_industry_group_item_id
                                                                                                                where eu.company_type = 'Channel Partners' and eu.is_public = 1
                                                                                                                and eu.country = @country
                                                                                                                and eu.city = @city
                                                                                                                group by es.sub_industies_group_id, es.description"
                                                    , DatabaseHelper.CreateStringParameter("@country", country)
                                                    , DatabaseHelper.CreateStringParameter("@city", user.City));

                            string countryName = country.Replace(" ", "-").ToLower();
                            string cityName = user.City.Replace(" ", "-").ToLower();

                            foreach (ElioSubIndustriesGroupItems vertical in verticalsByCity)
                            {
                                string linkUrl = "/" + cityName + "/channel-partners/" + vertical.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                if (country == "Austria")
                                {
                                    linkUrl = "/at" + linkUrl;
                                }
                                else if (country == "Brazil" || country == "Portugal")
                                {
                                    linkUrl = "/pt" + linkUrl;
                                }
                                else if (country == "Germany")
                                {
                                    linkUrl = "/de" + linkUrl;
                                }
                                else if (country == "Italy")
                                {
                                    linkUrl = "/it" + linkUrl;
                                }
                                else if (country == "Netherlands")
                                {
                                    linkUrl = "/nl" + linkUrl;
                                }
                                else if (country == "Poland")
                                {
                                    linkUrl = "/pl" + linkUrl;
                                }
                                else if (country == "Spain")
                                {
                                    linkUrl = "/es" + linkUrl;
                                }
                                else
                                    linkUrl = "/la" + linkUrl;

                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }
                        }
                    }

                    session.CloseConnection();

                    id = 0;
                    scenario++;
                }

                if (scenario == 15) //Channel Partners products by city translate
                {
                    if (count >= 50000)
                    {
                        w.WriteLine("</urlset>");
                        w.Close();
                        w.Dispose();
                        WriteAllSiteMapFiles(scenario, id);
                    }

                    //string[] SelectedCountries = { "Algeria", "Angola", "Australia", "Canada", "Egypt", "Ethiopia", "Ghana", "Hong Kong", "Ireland", "Kenya", "Morocco", "New Zealand", "Nigeria", "Singapore", "South Africa", "Tunisia", "United Kingdom", "India", "United States" };
                    string[] SelectedCountries = { "Austria", "Brazil", "Portugal", "Germany", "Italy", "Netherlands", "Poland", "Spain",
                                                               "Argentina", "Bolivia", "Chile", "Colombia", "Costa Rica", "Dominican Republic", "Ecuador",
                                                               "El Salvador", "Guatemala", "Honduras", "Mexico", "Panama", "Paraguay", "Peru", "Puerto Rico",
                                                               "Uruguay", "Venezuela" };

                    session.OpenConnection();

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                    foreach (string country in SelectedCountries)
                    {
                        if (session.Connection.State == System.Data.ConnectionState.Closed)
                            session.OpenConnection();

                        List<ElioUsers> cities = loader.Load(@"SELECT u.city
	                                                                        FROM Elio_registration_products rp
	                                                                        inner join Elio_users_registration_products urp
		                                                                        on rp.id = urp.reg_products_id
	                                                                        inner join elio_users u
		                                                                        on urp.user_id = u.id
	                                                                        where rp.is_public = 1
	                                                                        and u.company_type = 'Channel Partners'
                                                                            and u.country = @country
                                                                            and u.city != ''
                                                                            group by city
                                                                            order by city"
                                                    , DatabaseHelper.CreateStringParameter("@country", country));

                        DataLoader<ElioRegistrationProducts> loaderProd = new DataLoader<ElioRegistrationProducts>(session);

                        foreach (ElioUsers user in cities)
                        {
                            if (session.Connection.State == System.Data.ConnectionState.Closed)
                                session.OpenConnection();

                            List<ElioRegistrationProducts> productsByCity = loaderProd.Load(@"SELECT rp.id, rp.description
	                                                                                                        FROM Elio_registration_products rp
	                                                                                                        inner join Elio_users_registration_products urp
		                                                                                                        on rp.id = urp.reg_products_id
	                                                                                                        inner join elio_users u
		                                                                                                        on urp.user_id = u.id
                                                                                                            where u.company_type = 'Channel Partners' and u.is_public = 1
                                                                                                            and u.country = @country
                                                                                                            and u.city = @city
                                                                                                            group by rp.id, rp.description"
                                                    , DatabaseHelper.CreateStringParameter("@country", country)
                                                    , DatabaseHelper.CreateStringParameter("@city", user.City));

                            string countryName = country.Replace(" ", "-").ToLower();
                            string cityName = user.City.Replace(" ", "-").ToLower();

                            foreach (ElioRegistrationProducts product in productsByCity)
                            {
                                string linkUrl = "/" + cityName + "/channel-partners/" + product.Description.Replace(" ", " ").Replace("&", "and").Replace(" ", "_").ToLower();

                                if (country == "Austria")
                                {
                                    linkUrl = "/at" + linkUrl;
                                }
                                else if (country == "Brazil" || country == "Portugal")
                                {
                                    linkUrl = "/pt" + linkUrl;
                                }
                                else if (country == "Germany")
                                {
                                    linkUrl = "/de" + linkUrl;
                                }
                                else if (country == "Italy")
                                {
                                    linkUrl = "/it" + linkUrl;
                                }
                                else if (country == "Netherlands")
                                {
                                    linkUrl = "/nl" + linkUrl;
                                }
                                else if (country == "Poland")
                                {
                                    linkUrl = "/pl" + linkUrl;
                                }
                                else if (country == "Spain")
                                {
                                    linkUrl = "/es" + linkUrl;
                                }
                                else
                                    linkUrl = "/la" + linkUrl;

                                mainURL = "https://www.elioplus.com" + linkUrl;

                                w.WriteLine("   <url>");
                                w.WriteLine("       <loc>" + mainURL + "</loc>");
                                w.WriteLine("       <lastmod>" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "</lastmod>");
                                w.WriteLine("       <changefreq>weekly</changefreq>");
                                w.WriteLine("       <priority>1.0</priority>");
                                w.WriteLine("   </url>");
                                count++;
                            }
                        }
                    }

                    session.CloseConnection();

                    scenario++;
                }

                w.WriteLine("</urlset>");
                w.Close();
                //}
            }
            catch (Exception ex)
            {
                w.WriteLine("</urlset>");
                w.Close();
                w.Dispose();
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        static public void GenerateXML()
        {
            try
            {
                //string fileName = "sitemap.xml";

                //string DOMAIN = "http://www.sohel-elite.com";
                //string LAST_MODIFY = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                //string CHANGE_FREQ = "monthly";
                //string TOP_PRIORITY = "0.5";
                //string MEDIUM_PRIORITY = "0.8";

                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";

                //XDocument Start
                //XDocument xDoc = new XDocument(
                //    new XDeclaration("1.0", "UTF-8", "no"),
                //    new XElement(ns + "urlset",
                //    new XAttribute(XNamespace.Xmlns + "xsi", xsiNs),
                //    new XAttribute(xsiNs + "schemaLocation",
                //        "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                //    new XElement(ns + "url",

                //        //Root Element
                //        new XElement(ns + "loc", DOMAIN),
                //        new XElement(ns + "lastmod", LAST_MODIFY),
                //        new XElement(ns + "changefreq", "weekly"),
                //        new XElement(ns + "priority", TOP_PRIORITY)),

                //        ////Level0 Menu
                //        //from level0 in GetParentCMSMenu()
                //        //select new XElement(ns + "url",
                //        //new XElement(ns + "loc", String.Concat(DOMAIN, WebsiteHelpers.GetMenuRouteURL(Util.Parse<string>(level0.MENU_ALLIAS), Util.Parse<string>((level0.Level1 == null) ? string.Empty : level0.Level1), Util.Parse<int>(level0.APPLICATION_ID)))),
                //        //new XElement(ns + "lastmod", LAST_MODIFY),
                //        //new XElement(ns + "changefreq", CHANGE_FREQ),
                //        //new XElement(ns + "priority", MEDIUM_PRIORITY)
                //    ),

                //        ////Level1 Menu
                //        //from level0 in GetParentCMSMenu()
                //        //from level1 in GetLevel1Menu(Util.Parse<int>(level0.MENU_ID))
                //        //select new XElement(ns + "url",
                //        //new XElement(ns + "loc", String.Concat(DOMAIN, WebsiteHelpers.GetMenuRouteURL(Util.Parse<string>(level1.Level1), Util.Parse<string>((level1.MENU_ALLIAS == null) ? string.Empty : level1.MENU_ALLIAS), Util.Parse<int>(level1.APPLICATION_ID)))),
                //        //new XElement(ns + "lastmod", LAST_MODIFY),
                //        //new XElement(ns + "changefreq", CHANGE_FREQ),
                //        //new XElement(ns + "priority", MEDIUM_PRIORITY)
                //    ),

                //        ////Level2 Menu
                //        //from level0 in GetParentCMSMenu()
                //        //from level1 in GetLevel1Menu(Util.Parse<int>(level0.MENU_ID))
                //        //from level2 in GetLevel2Menu(Util.Parse<int>(level1.MENU_ID))
                //        //select new
                //        //XElement(ns + "url",
                //        //new XElement(ns + "loc", String.Concat(DOMAIN, WebsiteHelpers.GetMenuRouteURL(Util.Parse<string>(level2.Menu), Util.Parse<string>(level2.Level1), Util.Parse<int>(level2.AppID), Util.Parse<string>(level2.Level2)))),
                //        //new XElement(ns + "lastmod", LAST_MODIFY),
                //        //new XElement(ns + "changefreq", CHANGE_FREQ),
                //        //new XElement(ns + "priority", MEDIUM_PRIORITY)
                //    )

                //));
                //XDocument End

                //xDoc.Save(Server.MapPath("~/") + fileName);

                //this.MessageHolder.Visible = true;
                //this.MessageHolder.Attributes.Add("class", "success");
                //this.MessageHolder.InnerHtml = "Sitemap.xml created successfully";

            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                //this.MessageHolder.Visible = true;
                //this.MessageHolder.Attributes.Add("class", "error");
                //this.MessageHolder.InnerHtml = Constants.ERROR_LONG_MESSAGE + "<br/>" + ex.ToString();
            }
        }
    }
}
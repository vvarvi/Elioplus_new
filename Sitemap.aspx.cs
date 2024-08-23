using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class Sitemap : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            Response.ContentType = "text/xml";

            using (XmlTextWriter writer = new XmlTextWriter(Server.MapPath("sitemap.xml"), Encoding.UTF8))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi:schemaLocation", "http://www.google.com/schemas/sitemap-news/0.9 http://www.google.com/schemas/sitemap-news/0.9/sitemap.xsd");

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/home");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/how-it-works");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/pricing");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/search");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();                

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/login");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/free-sign-up");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/contact-us");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/reset-password");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/faq");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/about-us");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/latest-posts");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/most-voted-posts");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/most-discussed-posts");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/must-read-posts");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/login");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/free-sign-up");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "https://www.elioplus.com/community/reset-password");
                writer.WriteElementString("changefreq", "daily");
                writer.WriteElementString("priority", "1.0");
                writer.WriteEndElement();


                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);

                con.Open();

                SqlDataAdapter adp1 = new SqlDataAdapter("select id, company_name, company_type from elio_users where account_status=1 and is_public=1 order by company_type desc, id desc", con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                foreach (DataRow row in dt1.Rows)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", "https://www.elioplus.com/profiles/" + row["company_type"].ToString().ToLower() + "/" + row["id"].ToString() + "/" + Regex.Replace(row["company_name"].ToString(), @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "");
                    writer.WriteElementString("changefreq", "daily");
                    writer.WriteElementString("priority", "0.80");
                    writer.WriteEndElement();
                }


                SqlDataAdapter adp2 = new SqlDataAdapter("select id, topic from Elio_community_posts order by id desc", con);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);

                foreach (DataRow row in dt2.Rows)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", "https://www.elioplus.com/community/posts/" + row["id"].ToString() + "/" + Regex.Replace(row["topic"].ToString(), @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "");
                    writer.WriteElementString("changefreq", "daily");
                    writer.WriteElementString("priority", "0.80");
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.Flush();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Telerik.Windows.Documents.Spreadsheet.Model;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Utils
{
    public static class ExcelLib
    {
        internal class MyContact
        {
            public string email { get; set; }
            public string prod1 { get; set; }
            public string prod2 { get; set; }
            public string prod3 { get; set; }
        }
        public static void ReadFile()
        {
            try
            {
                var fileName = string.Format("{0}\\ChannelPartnersProducts.xlsx", Directory.GetCurrentDirectory());
                fileName = "E:\\Projects\\Elioplus\\WdS.ElioPlus\\Lib\\Utils\\ChannelPartnersProducts.xlsx";
                if (fileName != "")
                {
                    var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 10.0 Xml;HDR=YES;IMEX=1;\"", fileName);

                    var adapter = new OleDbDataAdapter("SELECT * FROM IRI", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "table");

                    //DataTable data = ds.Tables["anyNameHere"];
                    var data = ds.Tables["table"].AsEnumerable();

                    var query = data.Where(x => x.Field<string>("email") != string.Empty).Select(x =>
                    new MyContact
                    {
                        email = x.Field<string>("email"),
                        prod1 = x.Field<string>("First Name"),
                        prod2 = x.Field<string>("Last Name"),
                        prod3 = x.Field<string>("Phone Number"),
                    }); ;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadFile", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB(int from, int to, DBSession session)
        {
            try
            {
                session.OpenConnection();
                //session.BeginTransaction();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Projects\\Elioplus\\WdS.ElioPlus\\Lib\\Utils\\Database1.mdb";
                string strSQL = "SELECT * FROM IRI where id between " + from + " and " + to + "";
                // Create a connection  
                //using (OleDbConnection connection = new OleDbConnection(connectionString))
                //{
                // Create a command and set its connection  
                //OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.  
                try
                {
                    // Open connecton  
                    //connection.Open();

                    DataTable table = new DataTable();

                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            //Logger.Debug("Row ID " + row["ID"].ToString());

                            string email = row["email"].ToString();
                            if (email != "")
                            {
                                //Logger.Debug("row ID: " + row["ID"].ToString() + " and email: " + row["email"].ToString());

                                ElioUsers user = Sql.GetUserByEmailTop1(email, session);
                                if (user != null)
                                {
                                    Logger.Debug("user id: " + user.Id + " with email: " + row["email"].ToString());

                                    foreach (DataColumn column in table.Columns)
                                    {
                                        string columnName = column.ColumnName;
                                        if (columnName != "" && columnName != "email" && columnName != "ID")
                                        {
                                            if (columnName.StartsWith("Field"))
                                            {
                                                string itemDescription = table.Rows[index][columnName].ToString();
                                                if (itemDescription == "" || string.IsNullOrEmpty(itemDescription) || itemDescription == "NULL")
                                                {
                                                    break;
                                                }

                                                if (itemDescription != "" && !string.IsNullOrEmpty(itemDescription) && itemDescription != "NULL" && !itemDescription.StartsWith("Field"))
                                                {
                                                    //Logger.Debug("item description is: " + itemDescription);

                                                    ElioRegistrationProducts product = Sql.GetRegistrationProductsIDByDescription(itemDescription, session);
                                                    if (product == null)
                                                    {
                                                        //Logger.Debug("itemDescription " + itemDescription + "not exist and is going to be inserted");

                                                        product = new ElioRegistrationProducts();
                                                        product.Description = itemDescription;
                                                        product.IsPublic = 1;
                                                        product.Sysdate = DateTime.Now;

                                                        DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                                                        insertLoader.Insert(product);
                                                        Logger.Debug("Inserted product " + itemDescription);
                                                    }

                                                    if (product != null)
                                                    {
                                                        bool exist = Sql.ExistUserRegistrationProduct(user.Id, product.Id, session);
                                                        if (!exist)
                                                        {
                                                            ElioUsersRegistrationProducts userProducts = new ElioUsersRegistrationProducts();
                                                            userProducts.UserId = user.Id;
                                                            userProducts.RegProductsId = product.Id;

                                                            DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);
                                                            loader.Insert(userProducts);

                                                            Logger.Debug("Inserted productID:" + product.Id + " for userID: " + user.Id);
                                                        }
                                                        //else
                                                        //{
                                                        //    Logger.Debug("Exist productID:" + product.Id + " for userID: " + user.Id);
                                                        //}
                                                    }
                                                }
                                                //else
                                                //{
                                                //    Logger.Debug("itemDescription is emty");
                                                //}
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.Debug("User not found for email: " + row["email"].ToString());
                                }
                            }

                            index++;
                        }

                        Logger.Debug("Finish proccess successfully");
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }

                    // Execute command  
                    //using (OleDbDataReader reader = command.ExecuteReader())
                    //{

                    //    Console.WriteLine("------------Original data----------------");
                    //    while (reader.Read())
                    //    {
                    //        string email = reader["email"].ToString();
                    //        if (email != "")
                    //        {
                    //            try
                    //            {

                    //            }
                    //            catch (Exception ex)
                    //            {

                    //            }
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB", ex.Message.ToString(), ex.StackTrace.ToString());
                }
                //}
            }
            catch (Exception ex)
            {
                //session.RollBackTransaction();
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB", ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                //session.CloseConnection();
                //session.CommitTransaction();
            }
        }

        public static void ReadAccessDB_v2(int from, int to, bool isAccessDB, DBSession session)
        {
            try
            {
                if (isAccessDB)
                {
                    #region AccessDB

                    DataTable table = new DataTable();

                    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                    if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                        connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                    string strSQL = "SELECT user_id, city, email FROM Cities_7 where id between " + from + " and " + to + "";

                    try
                    {
                        using (OleDbConnection conn = new OleDbConnection(connectionString))
                        {
                            OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                            conn.Open();

                            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                            adapter.Fill(table);
                        }

                        if (table != null && table.Rows.Count > 0)
                        {
                            Logger.Debug("Start proccess successfully");

                            int index = 0;
                            foreach (DataRow row in table.Rows)
                            {
                                string email = row["email"].ToString();
                                if (email != "")
                                {
                                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(row["user_id"]), session);
                                    if (user != null)
                                    {
                                        if (user.Email == row["email"].ToString() && string.IsNullOrEmpty(user.City))
                                        {
                                            if (user.AccountStatus == (int)AccountStatus.Completed)
                                            {
                                                //Logger.Debug("user id: " + user.Id + " with email: " + row["email"].ToString());
                                                //int columnsCount = table.Columns.Count;
                                                //foreach (DataColumn column in table.Columns)
                                                //{
                                                string city = row["city"].ToString();

                                                //string columnName = table.Columns[2].ColumnName;
                                                //if (columnName != "" && columnName != "email" && columnName != "ID" && columnName.StartsWith("address"))
                                                //{
                                                //string address = table.Rows[index][columnName].ToString();
                                                if (city == "" || string.IsNullOrEmpty(city) || city == "NULL")
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    user.City = city;
                                                    //int userId = user.Id;
                                                    Logger.Debug(user.Id.ToString());

                                                    //try
                                                    //{
                                                    //    String addressToCheck = user.Address;
                                                    //    String CountryCodeToCheck = user.Country != "" ? user.Country : "";

                                                    //    IGeolocationProvider multipleProvider = new MultipleGeolocationProvider();

                                                    //    XDocument doc = new XDocument();

                                                    //    XDocument XmultipleProvider = multipleProvider.resolveAddress(addressToCheck, CountryCodeToCheck, user.Country == "", false);

                                                    //    String status = "";

                                                    //    //String provider = "";

                                                    //    String VenueResolvedCity = "";
                                                    //    //String VenueResolvedCountry = "";

                                                    //    try
                                                    //    {
                                                    //        status = XmultipleProvider.Element("NewDataSet").Element("QueryStatus").Value;

                                                    //        if (status == "connection_error")
                                                    //        {
                                                    //            Logger.DetailedError("ExcelLib.cs", "Connection Error");
                                                    //        }
                                                    //    }
                                                    //    catch (Exception ex)
                                                    //    {
                                                    //        Logger.DetailedError("ExcelLib.cs", "XmultipleProvider.Element(\"NewDataSet\").Element(\"QueryStatus\").Value", ex.StackTrace.ToString(), ex.Message.ToString());
                                                    //    }

                                                    //    if (status == "ok")
                                                    //    {
                                                    //        XElement NewDataSet = new XElement("NewDataSet");
                                                    //        if (XmultipleProvider.Element("NewDataSet") != null)
                                                    //        {
                                                    //            NewDataSet = XmultipleProvider.Element("NewDataSet");
                                                    //            XElement Table = new XElement("Table");
                                                    //            if (XmultipleProvider.Element("NewDataSet").Element("Table") != null)
                                                    //            {
                                                    //                Table = NewDataSet.Element("Table");

                                                    //                //provider = Table.Element("PROVIDER").Value;
                                                    //                VenueResolvedCity = Table.Element("CITY").Value;
                                                    //                //VenueResolvedCountry = Table.Element("COUNTRY_CODE").Value;
                                                    //            }
                                                    //        }

                                                    //        if (VenueResolvedCity != "")
                                                    //            user.City = VenueResolvedCity;
                                                    //    }
                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    Logger.DetailedError("ExcelLib.cs", "IGeolocationProvider Error", ex.StackTrace.ToString(), ex.Message.ToString());
                                                    //}

                                                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                                                    loader.Update(user);
                                                }
                                            }
                                            //break;
                                            //}
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        Logger.Debug("User not found for email: " + row["email"].ToString());
                                    }
                                }

                                index++;
                                Logger.Debug("Index: " + index.ToString());
                            }

                            Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                        }
                        else
                        {
                            Logger.Debug("DataTable is empty");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB", ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    #endregion
                }
                else
                {
                    #region Elio Users

                    if (session.Connection.State == ConnectionState.Closed)
                        session.OpenConnection();

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                    List<ElioUsers> users = loader.Load(@"SELECT *
                                                        FROM elio_users
                                                        where account_status = 1
                                                        and address != ''
                                                        and city is null
                                                        and id between " + from + " and " + to + "" +
                                                        "order by id");

                    if (users.Count > 0)
                    {
                        foreach (ElioUsers user in users)
                        {
                            if (user.AccountStatus == (int)AccountStatus.Completed)
                            {
                                Logger.Debug(user.Id.ToString());

                                try
                                {
                                    String addressToCheck = user.Address;
                                    String CountryCodeToCheck = user.Country != "" ? user.Country : "";

                                    IGeolocationProvider multipleProvider = new MultipleGeolocationProvider();

                                    XDocument doc = new XDocument();

                                    XDocument XmultipleProvider = multipleProvider.resolveAddress(addressToCheck, CountryCodeToCheck, user.Country == "", false);

                                    String status = "";

                                    String VenueResolvedCity = "";

                                    try
                                    {
                                        status = XmultipleProvider.Element("NewDataSet").Element("QueryStatus").Value;

                                        if (status == "connection_error")
                                        {
                                            Logger.DetailedError("ExcelLib.cs", "Connection Error");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError("ExcelLib.cs", "XmultipleProvider.Element(\"NewDataSet\").Element(\"QueryStatus\").Value", ex.StackTrace.ToString(), ex.Message.ToString());
                                    }

                                    if (status == "ok")
                                    {
                                        XElement NewDataSet = new XElement("NewDataSet");
                                        if (XmultipleProvider.Element("NewDataSet") != null)
                                        {
                                            NewDataSet = XmultipleProvider.Element("NewDataSet");
                                            XElement Table = new XElement("Table");
                                            if (XmultipleProvider.Element("NewDataSet").Element("Table") != null)
                                            {
                                                Table = NewDataSet.Element("Table");

                                                VenueResolvedCity = Table.Element("CITY").Value;
                                            }
                                        }

                                        if (VenueResolvedCity != "")
                                        {
                                            user.City = VenueResolvedCity;

                                            loader.Update(user);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError("ExcelLib.cs", "IGeolocationProvider Error", ex.StackTrace.ToString(), ex.Message.ToString());
                                }
                            }
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB_updateMetropolitanCities(int from, int to, string country, DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string strSQL = "SELECT id, city, metropolitan FROM US_metropolitan where id between " + from + " and " + to + "";

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            string city = row["city"].ToString();
                            string metropolitan = row["metropolitan"].ToString();

                            if (city != "" && metropolitan != "")
                            {
                                DataTable tbl = session.GetDataTable(@"select convert(nvarchar(100), [id]) + ','
                                                                        from elio_users
                                                                        where country = @country
                                                                        and isnull(city, '') != ''
                                                                        and is_public = 1
                                                                        and company_type = 'Channel Partners'
                                                                        and account_status = 1
                                                                        and city = @city and city != '" + metropolitan + "'"
                                                        , DatabaseHelper.CreateStringParameter("@country", country)
                                                        , DatabaseHelper.CreateStringParameter("@city", city));

                                if (tbl != null && tbl.Rows.Count > 0)
                                {
                                    string userIDs = tbl.Rows[0].ToString();

                                    if (userIDs != "")
                                    {
                                        if (userIDs.Length > 0 && userIDs.EndsWith(","))
                                            userIDs = userIDs.Substring(0, userIDs.Length - 1);

                                        try
                                        {
                                            session.BeginTransaction();

                                            session.ExecuteQuery(@"update elio_users
                                                                set city = " + metropolitan + "" +
                                                                    "where id in (" + userIDs + ")");

                                            session.CommitTransaction();
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();
                                            Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities UPDATE ERROR FOR USER IDs: " + userIDs, ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.Debug("Users for city: " + row["city"].ToString() + " and metropolitan area: " + row["metropolitan"].ToString() + " not updated");
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB_updateMetropolitanCities_v2(int from, int to, string country, DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string tableName = country.Replace(" ", "_") + "_metropolitan";

                if (country == "United Kingdom")
                    tableName = "UK_metropolitan";
                else if (country == "New Zealand")
                    tableName = "New_Zealand_metropolitan";
                else if (country == "United Arab Emirates")
                    tableName = "United_Arab_Emirates_metropolitan";
                else if (country == "Czech Republic")
                    tableName = "Czech_Republic_metropolitan";

                string strSQL = "SELECT distinct metropolitan FROM " + tableName;

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        //DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            //string city = row["city"].ToString();
                            string metropolitan = row["metropolitan"].ToString();

                            if (metropolitan != "")
                            {
                                strSQL = @"SELECT city FROM " + tableName + " WHERE metropolitan = '" + metropolitan + "'";

                                DataTable tbl = new DataTable();

                                //Logger.Debug("For metropolitan: " + metropolitan);
                                using (OleDbConnection conn = new OleDbConnection(connectionString))
                                {
                                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                                    conn.Open();

                                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                                    adapter.Fill(tbl);
                                }

                                //DataTable tbl = session.GetDataTable(@"SELECT city FROM US_metropolitan WHERE metropolitan = @metropolitan"
                                //                        , DatabaseHelper.CreateStringParameter("@metropolitan", metropolitan));

                                if (tbl != null && tbl.Rows.Count > 0)
                                {
                                    string cities = "";

                                    foreach (DataRow cityRow in tbl.Rows)
                                    {
                                        if (cityRow["city"].ToString().Contains("'"))
                                            cityRow["city"] = cityRow["city"].ToString().Replace("'", "");

                                        cities += "'" + cityRow["city"] + "',";
                                    }

                                    if (cities != "")
                                    {
                                        if (cities.Length > 0 && cities.EndsWith(","))
                                            cities = cities.Substring(0, cities.Length - 1);

                                        Logger.Debug("For metropolitan: " + metropolitan + " found these cities: " + cities);

                                        try
                                        {
                                            session.BeginTransaction();

                                            session.ExecuteQuery(@"update elio_users
                                                                    set city = @metropolitan
                                                                    where country = @country " +
                                                                    "and isnull(city, '') != '' " +
                                                                    "and is_public = 1 " +
                                                                    "and company_type = 'Channel Partners' " +
                                                                    "and account_status = 1 " +
                                                                    "and city != @metropolitan " +
                                                                    "and city in (" + cities + ")"
                                                                , DatabaseHelper.CreateStringParameter("@metropolitan", metropolitan)
                                                                , DatabaseHelper.CreateStringParameter("@country", country));

                                            session.CommitTransaction();

                                            Logger.Debug("Metropolitan: " + metropolitan + " updated successfully");
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();
                                            Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities UPDATE ERROR FOR USER CITIES: " + cities, ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.Debug("Cities not found for metropolitan area: " + row["metropolitan"].ToString() + " so not updated");
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateMetropolitanCities", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB_updateCitiesStates(int from, int to, string country, DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string tableName = country + "_city_states";

                if (country == "United Kingdom")
                    tableName = "UK_city_states";
                else if (country == "New Zealand")
                    tableName = "New_Zealand_city_states";
                else if (country == "United Arab Emirates")
                    tableName = "United_Arab_Emirates_city_states";

                string strSQL = "SELECT ID, city, state FROM " + tableName;

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            string city = row["city"].ToString();
                            string state = row["state"].ToString();

                            if (city != "" && state != "")
                            {
                                //Logger.Debug("For city: " + city + " and state: " + state);

                                try
                                {
                                    session.BeginTransaction();

                                    session.ExecuteQuery(@"update elio_users
                                                            set state = @state
                                                            where country = @country " +
                                                            "and isnull(city, '') != '' " +
                                                            "and is_public = 1 " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and account_status = 1 " +
                                                            "and city = @city"
                                                        , DatabaseHelper.CreateStringParameter("@state", state)
                                                        , DatabaseHelper.CreateStringParameter("@country", country)
                                                        , DatabaseHelper.CreateStringParameter("@city", city));

                                    session.CommitTransaction();

                                    Logger.Debug("State: " + state + " updated successfully for city: " + city);
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateCitiesStates UPDATE ERROR FOR USER CITY: " + city + " AND STATE: " + state, ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB_updateWrongCities(int from, int to, string country, DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string strSQL = "SELECT user_id, country, email, city FROM Wrong_cities";

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            int userId = Convert.ToInt32(row["user_id"]);
                            string city = row["city"].ToString();
                            string email = row["email"].ToString();
                            string countryName = row["country"].ToString();

                            if (userId > 0 && email != "" && city != "" && countryName != "")
                            {
                                //Logger.Debug("For city: " + city + " and state: " + state);

                                try
                                {
                                    session.BeginTransaction();

                                    session.ExecuteQuery(@"update elio_users
                                                            set city = @city
                                                            where country = @country " +
                                                            "and id = @user_id " +
                                                            "and is_public = 1 " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and account_status = 1 " +
                                                            "and email = @email"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@country", countryName)
                                                        , DatabaseHelper.CreateStringParameter("@city", city));

                                    session.CommitTransaction();

                                    Logger.Debug("User ID: " + userId + " in country: " + countryName + " updated successfully for city: " + city);
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateWrongCities UPDATE ERROR FOR USER CITY: " + city + " AND USER ID: " + userId, ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateWrongCities", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void ReadAccessDB_updateEmptyCitiesStates(int from, int to, string country, DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string strSQL = "SELECT user_id, country, email, state, city FROM Empty_cities WHERE country IS NOT NULL";

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            int userId = Convert.ToInt32(row["user_id"]);
                            string city = row["city"].ToString();
                            string email = row["email"].ToString();
                            string countryName = row["country"].ToString();
                            string state = row["state"].ToString();

                            if (userId > 0 && email != "" && countryName != "")
                            {
                                //Logger.Debug("For city: " + city + " and state: " + state);

                                try
                                {
                                    if (!string.IsNullOrEmpty(city) && city != "NULL" && !string.IsNullOrEmpty(state) && state != "NULL")
                                    {
                                        session.BeginTransaction();

                                        #region update city & state

                                        session.ExecuteQuery(@"update elio_users
                                                            set city = @city,
                                                                state = @state
                                                            where country = @country " +
                                                            "and id = @user_id " +
                                                            "and is_public = 1 " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and account_status = 1 " +
                                                            "and email = @email"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@country", countryName)
                                                        , DatabaseHelper.CreateStringParameter("@city", city)
                                                        , DatabaseHelper.CreateStringParameter("@state", state));

                                        Logger.Debug("User ID: " + userId + " in country: " + countryName + " updated successfully for city: " + city + " and state: " + state);

                                        #endregion

                                        session.CommitTransaction();
                                    }
                                    else if (!string.IsNullOrEmpty(city) && city != "NULL" && (string.IsNullOrEmpty(state) || state == "NULL"))
                                    {
                                        session.BeginTransaction();

                                        #region update city

                                        session.ExecuteQuery(@"update elio_users
                                                                set city = @city
                                                            where country = @country " +
                                                            "and id = @user_id " +
                                                            "and is_public = 1 " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and account_status = 1 " +
                                                            "and email = @email"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@country", countryName)
                                                        , DatabaseHelper.CreateStringParameter("@city", city));

                                        Logger.Debug("User ID: " + userId + " in country: " + countryName + " updated successfully for city: " + city);

                                        #endregion

                                        session.CommitTransaction();
                                    }
                                    else if (!string.IsNullOrEmpty(state) && state != "NULL" && (string.IsNullOrEmpty(city) || city == "NULL"))
                                    {
                                        session.BeginTransaction();

                                        #region update state

                                        session.ExecuteQuery(@"update elio_users
                                                                set state = @state
                                                            where country = @country " +
                                                            "and id = @user_id " +
                                                            "and is_public = 1 " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and account_status = 1 " +
                                                            "and email = @email"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@country", countryName)
                                                        , DatabaseHelper.CreateStringParameter("@state", state));

                                        Logger.Debug("User ID: " + userId + " in country: " + countryName + " updated successfully for state: " + state);

                                        #endregion

                                        session.CommitTransaction();
                                    }
                                    else
                                    {
                                        Logger.Debug("EMPTY --> User ID: " + userId + " in country: " + countryName + " did not updated because city and state were empty");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates UPDATE ERROR FOR USER CITY: " + city + " AND USER ID: " + userId, ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void UpdateEmptyCitiesToElioUsers(DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string strSQL = "SELECT ID, user_id, email, city FROM Elio_users_cities WHERE city IS NOT NULL";

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            int userId = Convert.ToInt32(row["user_id"]);
                            string city = row["city"].ToString();
                            string email = row["email"].ToString();

                            if (userId > 0 && email != "" && city != "")
                            {
                                //Logger.Debug("For city: " + city + " and state: " + state);

                                try
                                {
                                    if (!string.IsNullOrEmpty(city) && city != "NULL")
                                    {
                                        session.BeginTransaction();

                                        #region update city & state

                                        session.ExecuteQuery(@"update elio_users
                                                            set city = @city
                                                            where id = @user_id " +
                                                            "and company_type = 'Channel Partners' " +                                                            
                                                            "and email = @email"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@city", city));

                                        Logger.Debug("User ID: " + userId + " with email: " + email + " updated successfully for city: " + city);

                                        #endregion

                                        session.CommitTransaction();
                                    }
                                    else
                                    {
                                        Logger.Debug("EMPTY --> User ID: " + userId + " with email: " + email + " did not updated because city was empty");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates UPDATE ERROR FOR USER CITY: " + city + " AND USER ID: " + userId, ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void UpdateEmptyStatesCitiesToElioUsers(DBSession session)
        {
            try
            {
                #region AccessDB

                DataTable table = new DataTable();

                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\WdS.ElioPlus\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\inetpub\wwwroot\elioplus.com\httpdocs\Lib\Utils\Database1.accdb;Persist Security Info=False;";

                string strSQL = "SELECT ID, user_id, country, email, state, city, company_region FROM Elio_users_states_cities order by user_id";

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                        adapter.Fill(table);
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        Logger.Debug("Start proccess successfully");

                        int index = 0;
                        foreach (DataRow row in table.Rows)
                        {
                            int userId = Convert.ToInt32(row["user_id"]);
                            string state = row["state"].ToString();
                            string city = row["city"].ToString();
                            string companyRegion = row["company_region"].ToString();
                            string email = row["email"].ToString();

                            if (userId > 0 && email != "" && city != "" && state !="" && companyRegion != "")
                            {
                                //Logger.Debug("For city: " + city + " and state: " + state);

                                try
                                {
                                    if (!string.IsNullOrEmpty(state) && state != "NULL" && !string.IsNullOrEmpty(city) && city != "NULL" && !string.IsNullOrEmpty(companyRegion) && companyRegion != "NULL")
                                    {
                                        session.BeginTransaction();

                                        #region update city & state

                                        session.ExecuteQuery(@"update elio_users
                                                            set city = @city,
                                                                state = @state
                                                            where id = @user_id " +
                                                            "and company_type = 'Channel Partners' " +
                                                            "and email = @email " +
                                                            "and company_region = @company_region"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateStringParameter("@email", email)
                                                        , DatabaseHelper.CreateStringParameter("@city", city)
                                                        , DatabaseHelper.CreateStringParameter("@state", state)
                                                        , DatabaseHelper.CreateStringParameter("@company_region", companyRegion));

                                        Logger.Debug("User ID: " + userId + " with email: " + email + " updated successfully for city: " + city + " and state: " + state);

                                        #endregion

                                        session.CommitTransaction();
                                    }
                                    else
                                    {
                                        Logger.Debug("EMPTY --> User ID: " + userId + " with email: " + email + " did not updated because city was empty or state was empty");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates UPDATE ERROR FOR USER CITY: " + city + " AND USER ID: " + userId, ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            index++;
                            Logger.Debug("Index: " + index.ToString());
                        }

                        Logger.Debug("Finish proccess successfully at index: " + (index - 1).ToString());
                    }
                    else
                    {
                        Logger.Debug("DataTable is empty");
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Lib.Utils.ExceLib.ReadAccessDB_updateEmptyCitiesStates", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}
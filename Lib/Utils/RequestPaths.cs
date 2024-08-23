using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using System.Configuration;

namespace WdS.ElioPlus.Lib.Utils
{
    public class RequestPaths
    {
        public const string defaultErrorPage = ControlLoader.DefaultErrorPage;
        public const string errorPage = ControlLoader.Page404;
        public const string DashErrorPage = ControlLoader.PageDash404;

        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        private string _page;
        public string Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }

        private string _companyType;
        public string CompanyType
        {
            get
            {
                return _companyType;
            }
            set
            {
                _companyType = value;
            }
        }

        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        private string _subCategory;
        public string SubCategory
        {
            get
            {
                return _subCategory;
            }
            set
            {
                _subCategory = value;
            }
        }

        private string _key;
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public RequestPaths(string path, ref ElioUsers user, ref bool isError, ref string errorPage, bool isProfile, DBSession session)
        {
            if (path != null)
            {
                #region Path Not Empty

                string[] originalPathElements = path.Split('/');

                #region Fix Path Array Elements

                List<string> pathElements = new List<string>();
                int elementsCount = 0;
                int elementsMaxCount = 4;


                foreach (string item in originalPathElements)
                {
                    if (item != string.Empty)
                    {
                        pathElements.Add(item);
                        elementsCount++;

                        if (item == "messages" || item == "view" || item == "reply")
                        {
                            elementsMaxCount++;
                        }
                    }

                    //if (originalPathElements.Length < 6)
                    //{
                    //    if (elementsCount == elementsMaxCount)
                    //        break;
                    //}
                    //else
                    //{
                    //    elementsMaxCount = 6;
                    //}
                }

                #endregion

                _type = pathElements[0];

                if (!string.IsNullOrEmpty(_type))
                {
                    #region Type Exist

                    int number;

                    if (_type == "profiles" && !isProfile)
                    {
                        #region Profiles Page

                        _userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                        _companyType = pathElements[1];

                        if (pathElements[3].EndsWith("connection-profile"))
                        {
                            int outLength = "-connection-profile".Length;

                            _companyName = pathElements[3].Substring(0, pathElements[3].Length - outLength);
                        }
                        else
                            _companyName = pathElements[3];

                        user = Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            string companyNameDB = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                            if (companyNameDB.EndsWith("-"))
                                companyNameDB = companyNameDB.Substring(0, companyNameDB.Length - 1);

                            if (_companyName.ToLower() != companyNameDB || (_companyType != Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower()))
                            //if (_companyName != user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower())   // REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(company_name,'@"[^A-Za-z0-9]+"','-'),',','.'),' ','-'),'.',''),' ',''));
                            {
                                isError = true;
                            }
                            else
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic)))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else if (_type == "connection-profile")
                    {
                        #region Connection Profile Page

                        _userId = (int.TryParse(pathElements[1], out number)) ? number : 0;
                        _companyName = pathElements[2];

                        user = Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            string companyNameDB = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                            if (companyNameDB.EndsWith("-"))
                                companyNameDB = companyNameDB.Substring(0, companyNameDB.Length - 1);

                            if (_companyName != companyNameDB)
                            //if (_companyName != user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower())   // REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(company_name,'@"[^A-Za-z0-9]+"','-'),',','.'),' ','-'),'.',''),' ',''));
                            {
                                isError = true;
                            }
                            else
                            {
                                if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ((user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic))) && user.UserApplicationType == (int)UserApplicationType.Elioplus))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else if (_type == "dashboard")
                    {
                        #region Dashboard Page

                        _userId = (int.TryParse(pathElements[1], out number)) ? number : 0;
                        _companyName = pathElements[2];
                        _page = pathElements[3];

                        if (pathElements.Count == elementsMaxCount)
                        {
                            user = Sql.GetUserById(_userId, session);

                            if (user != null)
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                                {
                                    if (_companyName != Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                    {
                                        isError = true;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                                    {
                                        if (_companyName != Regex.Replace(user.FirstName + "-" + user.LastName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                    else
                                    {
                                        if (_companyName != Regex.Replace(user.Username, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isError = true;
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #endregion
                    }
                    else if (_type.EndsWith("profile") && isProfile)
                    {
                        string[] _companyDetails = new string[10];
                        #region Profiles By Category Page

                        if (elementsCount == 4)
                        {
                            _userId = (int.TryParse(pathElements[2], out number)) ? number : 0;

                            string[] geoType = pathElements[1].Split('-').ToArray();
                            if (geoType.Length > 1)
                            {
                                if (geoType[0].StartsWith("vendors"))
                                    _companyType = geoType[0];
                                else
                                    _companyType = "channel-partners";
                            }
                            else
                                _companyType = pathElements[1];

                            _category = pathElements[2];
                            _companyName = pathElements[3];
                            _companyDetails = _companyName.Split('-');
                        }
                        else
                        {
                            //to do
                            //_userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                            _companyType = pathElements[1];
                            _category = pathElements[2];
                            _subCategory = pathElements[3];
                            _companyName = pathElements[4];
                            _companyDetails = _companyName.Split('-');
                        }

                        if (_companyDetails.Length > 0)
                            if (Lib.Utils.Validations.IsNumber(_companyDetails[_companyDetails.Length - 1]))
                                _userId = Convert.ToInt32(_companyDetails[_companyDetails.Length - 1]);
                            else
                                _userId = 0;
                        else
                            _userId = 0;

                        if (_userId > 0)
                            user = Sql.GetUserById(_userId, session);

                        //user = Sql.GetUserByCompanyNameRegexForURLSearch(_companyName, session);  //Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            if (_userId != user.Id || (_companyType != Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower()))
                            {
                                isError = true;
                            }
                            //if (_companyName != user.CompanyName.Trim().ToLower() || (_companyType != user.CompanyType.ToLower()))
                            //{
                            //    isError = true;
                            //}
                            else
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic)))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region No Page/Error

                        isError = true;

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region Type Not Exist

                    isError = true;

                    #endregion
                }

                #region Error

                if (isError)
                {
                    errorPage = DashErrorPage;
                    return;
                }

                #endregion

                #endregion
            }
            else
            {
                #region Path Empty

                isError = true;

                #endregion
            }

            #region Error

            if (isError)
            {
                errorPage = defaultErrorPage;
                return;
            }

            #endregion
        }

        public RequestPaths(string path, ref ElioUsers user, ref bool isError, ref string errorPage, DBSession session)
        {
            if (path != null)
            {
                #region Path Not Empty

                string[] originalPathElements = path.Split('/');
                
                #region Fix Path Array Elements

                List<string> pathElements = new List<string>();
                int elementsCount = 0;
                int elementsMaxCount = 4;


                foreach (string item in originalPathElements)
                {
                    if (item != string.Empty)
                    {
                        pathElements.Add(item);
                        elementsCount++;

                        if (item == "messages" || item == "view" || item == "reply")
                        {
                            elementsMaxCount++;
                        }
                    }

                    //if (originalPathElements.Length < 6)
                    //{
                    //    if (elementsCount == elementsMaxCount)
                    //        break;
                    //}
                    //else
                    //{
                    //    elementsMaxCount = 6;
                    //}
                }                

                #endregion

                _type = pathElements[0];

                if (!string.IsNullOrEmpty(_type))
                {
                    #region Type Exist

                    int number;

                    if (_type == "profiles")
                    {
                        #region Profiles Page

                        _userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                        _companyType = pathElements[1];

                        if (pathElements[3].EndsWith("connection-profile"))
                        {
                            int outLength = "-connection-profile".Length;

                            _companyName = pathElements[3].Substring(0, pathElements[3].Length - outLength);
                        }
                        else
                            _companyName = pathElements[3];

                        user = Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            string companyNameDB = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                            if (companyNameDB.EndsWith("-"))
                                companyNameDB = companyNameDB.Substring(0, companyNameDB.Length - 1);

                            if (_companyName.ToLower() != companyNameDB || (_companyType != Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower()))
                            //if (_companyName != user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower())   // REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(company_name,'@"[^A-Za-z0-9]+"','-'),',','.'),' ','-'),'.',''),' ',''));
                            {
                                isError = true;
                            }
                            else
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic)))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else if (_type == "connection-profile")
                    {
                        #region Connection Profile Page

                        _userId = (int.TryParse(pathElements[1], out number)) ? number : 0;                        
                        _companyName = pathElements[2];

                        user = Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            string companyNameDB = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                            if (companyNameDB.EndsWith("-"))
                                companyNameDB = companyNameDB.Substring(0, companyNameDB.Length - 1);

                            if (_companyName != companyNameDB)
                            //if (_companyName != user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower())   // REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(company_name,'@"[^A-Za-z0-9]+"','-'),',','.'),' ','-'),'.',''),' ',''));
                            {
                                isError = true;
                            }
                            else
                            {
                                if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ((user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic))) && user.UserApplicationType == (int)UserApplicationType.Elioplus))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else if (_type == "dashboard")
                    {
                        #region Dashboard Page

                        _userId = (int.TryParse(pathElements[1], out number)) ? number : 0;
                        _companyName = pathElements[2];
                        _page = pathElements[3];

                        if (pathElements.Count == elementsMaxCount)
                        {
                            user = Sql.GetUserById(_userId, session);

                            if (user != null)
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                                {
                                    if (_companyName != Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                    {
                                        isError = true;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                                    {
                                        if (_companyName != Regex.Replace(user.FirstName + "-" + user.LastName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                    else
                                    {
                                        if (_companyName != Regex.Replace(user.Username, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isError = true;
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #endregion
                    }
                    else if (_type.EndsWith("profiles"))
                    {
                        string[] _companyDetails = new string[10];
                        #region Profiles By Category Page

                        if (elementsCount == 4)
                        {
                            _userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                            _companyType = pathElements[1];
                            _category = pathElements[2];
                            _companyName = pathElements[3];
                            _companyDetails = _companyName.Split('-');
                        }
                        else
                        {
                            //to do
                            //_userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                            _companyType = pathElements[1];
                            _category = pathElements[2];
                            _subCategory = pathElements[3];
                            _companyName = pathElements[4];
                            _companyDetails = _companyName.Split('-');
                        }

                        if (_companyDetails.Length > 0)
                            _userId = Convert.ToInt32(_companyDetails[_companyDetails.Length - 1]);

                        if (_userId > 0)
                            user = Sql.GetUserById(_userId, session);

                        //user = Sql.GetUserByCompanyNameRegexForURLSearch(_companyName, session);  //Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            if (_userId != user.Id || (_companyType != Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower()))
                            {
                                isError = true;
                            }
                            //if (_companyName != user.CompanyName.Trim().ToLower() || (_companyType != user.CompanyType.ToLower()))
                            //{
                            //    isError = true;
                            //}
                            else
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic)))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region No Page/Error

                        isError = true;

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region Type Not Exist

                    isError = true;

                    #endregion
                }

                #region Error

                if (isError)
                {
                    errorPage = DashErrorPage;
                    return;
                }

                #endregion

                #endregion
            }
            else
            {
                #region Path Empty

                isError = true;

                #endregion
            }

            #region Error

            if (isError)
            {
                errorPage = defaultErrorPage;
                return;
            }

            #endregion
        }

        public RequestPaths(string path, ref ElioUsers user, ref bool isError, ref string errorPage, out string key, DBSession session)
        {
            key = "";

            if (path != null)
            {
                #region Path Not Empty

                string[] originalPathElements = path.Split('/');

                #region Fix Path Array Elements

                List<string> pathElements = new List<string>();
                int elementsCount = 0;
                int elementsMaxCount = originalPathElements.Length - 1;

                foreach (string item in originalPathElements)
                {
                    if (item != string.Empty)
                    {
                        pathElements.Add(item);
                        elementsCount++;

                        if (item == "messages")
                        {
                            elementsMaxCount++;
                        }
                    }

                    if (elementsCount == elementsMaxCount)
                        break;
                }

                #endregion

                _type = pathElements[0];

                if (!string.IsNullOrEmpty(_type))
                {
                    #region Type Exist

                    int number;

                    if (_type == "profiles")
                    {
                        #region Profiles Page

                        _userId = (int.TryParse(pathElements[2], out number)) ? number : 0;
                        _companyType = pathElements[1];
                        _companyName = pathElements[3];

                        user = Sql.GetUserById(_userId, session);

                        if (user != null)
                        {
                            if (_companyName != Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower() || (_companyType != Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower()))
                            {
                                isError = true;
                            }
                            else
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) || (user.IsPublic == Convert.ToInt32(AccountStatus.NotPublic)))
                                {
                                    isError = true;
                                }
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #region Error

                        if (isError)
                        {
                            errorPage = defaultErrorPage;
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    else if (_type == "dashboard")
                    {
                        #region Dashboard Page

                        _userId = (int.TryParse(pathElements[1], out number)) ? number : 0;
                        _companyName = pathElements[2];
                        _page = pathElements[3];
                        key = (pathElements.Count == 5) ? pathElements[4] : "";

                        if (pathElements.Count == elementsMaxCount)
                        {
                            user = Sql.GetUserById(_userId, session);

                            if (user != null)
                            {
                                if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                                {
                                    if (_companyName != Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                    {
                                        isError = true;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                                    {
                                        if (_companyName != Regex.Replace(user.FirstName + "-" + user.LastName, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                    else
                                    {
                                        if (_companyName != Regex.Replace(user.Username, @"[^A-Za-z0-9]+", "-").Trim().ToLower())
                                        {
                                            isError = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isError = true;
                            }
                        }
                        else
                        {
                            isError = true;
                        }

                        #endregion
                    }
                    else
                    {
                        #region No Page/Error

                        isError = true;

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region Type Not Exist

                    isError = true;

                    #endregion
                }

                #region Error

                if (isError)
                {
                    errorPage = DashErrorPage;
                    return;
                }

                #endregion

                #endregion
            }
            else
            {
                #region Path Empty

                isError = true;

                #endregion
            }

            #region Error

            if (isError)
            {
                errorPage = defaultErrorPage;
                return;
            }

            #endregion
        }
    }
}
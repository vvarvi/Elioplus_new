using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class ClearbitSql
    {
        public static bool ExistsClearbitPerson(int userId, DBSession session)
        {
            return ExistsClearbitPerson(userId, "", session);
        }

        public static bool ExistsClearbitPerson(int userId, string clearbitPersonId, DBSession session)
        {
            DataTable table = new DataTable();

            if (clearbitPersonId != "")
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person with (nolock) 
                                                where user_id = @user_id
                                                and clearbit_person_id = @clearbit_person_id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateStringParameter("@clearbit_person_id", clearbitPersonId));
            }
            else
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person with (nolock) 
                                                where user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
            
            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static void SetClearbitPersonPublicActiveStatus(int userId, int isPublic, int isActive, DBSession session)
        {
            DataTable table = new DataTable();

            table = session.GetDataTable(@"UPDATE Elio_users_person SET is_public = @is_public, is_active = @is_active 
                                           WHERE user_id = @user_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                            , DatabaseHelper.CreateIntParameter("@is_active", isActive));
        }

        public static void SetClearbitPersonCompanyPublicActiveStatus(int userId, int isPublic, int isActive, DBSession session)
        {
            DataTable table = new DataTable();

            table = session.GetDataTable(@"UPDATE Elio_users_person_companies SET is_public = @is_public, is_active = @is_active 
                                           WHERE user_id = @user_id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@is_public", isPublic)
                                            , DatabaseHelper.CreateIntParameter("@is_active", isActive));
        }

        public static bool ExistsClearbitCompany(int userId, DBSession session)
        {
            return ExistsClearbitCompany(userId, "", session);
        }

        public static bool ExistsClearbitCompany(int userId, string companyId, DBSession session)
        {
            DataTable table = new DataTable();

            if (companyId != "")
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person_companies with (nolock) 
                                            where user_id = @user_id
                                            and clearbit_company_id = @clearbit_company_id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateStringParameter("@clearbit_company_id", companyId));
            }
            else
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person_companies with (nolock) 
                                            where user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool IsPersonProfileClaimed(int userId, DBSession session)
        {
            return IsPersonProfileClaimed(userId, "", session);
        }

        public static bool IsPersonProfileClaimed(int userId, string clearbitPersonId, DBSession session)
        {
            DataTable table = new DataTable();

            if (clearbitPersonId != "")
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person with (nolock) 
                                            where user_id = @user_id
                                            and clearbit_person_id = @clearbit_person_id
                                            and is_claimed = 1"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateStringParameter("@clearbit_person_id", clearbitPersonId));
            }
            else
            {
                table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person with (nolock) 
                                            where user_id = @user_id
                                            and is_claimed = 1"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasClearbitCompanyPhones(int userId, string companyId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person_company_phone_numbers with (nolock) 
                                                   where user_id = @user_id
                                                   and clearbit_company_id = @clearbit_company_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@clearbit_company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static bool HasClearbitCompanyTags(int userId, string companyId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"Select COUNT(id) as count from Elio_users_person_company_tags with (nolock) 
                                                   where user_id = @user_id
                                                   and clearbit_company_id = @clearbit_company_id"
                                                   , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                   , DatabaseHelper.CreateStringParameter("@clearbit_company_id", companyId));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }

        public static ElioUsersPerson GetPersonByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            return loader.LoadSingle("Select * from Elio_users_person with (nolock) where user_id = @user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPerson GetPersonByUserIdAndEmail(int userId, string email, DBSession session)
        {
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            return loader.LoadSingle("Select TOP 1 * from Elio_users_person with (nolock) where user_id = @user_id and email = @email"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateStringParameter("@email", email));
        }

        public static int GetPersonsCountByUserId(int userId, DBSession session)
        {
            DataTable table = new DataTable();

            table = session.GetDataTable("Select COUNT(*) AS count from Elio_users_person with (nolock) where user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]);
        }

        public static bool ExistPersonsMoreThanOneByUserId(int userId, DBSession session)
        {
            DataTable table = new DataTable();

            table = session.GetDataTable("Select COUNT(*) AS count from Elio_users_person with (nolock) where user_id = @user_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return Convert.ToInt32(table.Rows[0]["count"]) > 1 ? true : false;
        }

        public static List<ElioUsersPerson> GetAllPersonsByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            return loader.Load("Select * from Elio_users_person with (nolock) where user_id = @user_id", DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPerson GetPersonByUserIdAndClearbitPersonId(int userId, string clearbitPersonId, DBSession session)
        {
            DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
            return loader.LoadSingle("Select * from Elio_users_person with (nolock) where user_id = @user_id and clearbit_person_id = @clearbit_person_id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateStringParameter("@clearbit_person_id", clearbitPersonId));
        }

        public static ElioUsersPersonCompanies GetPersonCompanyByUserIdAndCompanyId(int userId, string clearbitCompanyId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
            return loader.LoadSingle(@"Select * from Elio_users_person_companies with (nolock) 
                                        where clearbit_company_id = @clearbit_company_id 
                                        and user_id = @user_id"
                                    , DatabaseHelper.CreateStringParameter("@clearbit_company_id", clearbitCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPersonCompanyTags GetPersonCompanyTagsByTagName(int userId, string clearbitCompanyId, string tagName, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyTags> loader = new DataLoader<ElioUsersPersonCompanyTags>(session);
            return loader.LoadSingle(@"Select * from Elio_users_person_company_tags with (nolock) 
                                        where clearbit_company_id = @clearbit_company_id 
                                        and user_id = @user_id 
                                        and tag_name = @tag_name"
                                    , DatabaseHelper.CreateStringParameter("@clearbit_company_id", clearbitCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateStringParameter("@tag_name", tagName));
        }

        public static List<ElioUsersPersonCompanyTags> GetPersonCompanyTagsByUserIdAndCompanyId(int userId, string clearbitCompanyId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyTags> loader = new DataLoader<ElioUsersPersonCompanyTags>(session);
            return loader.Load(@"Select * from Elio_users_person_company_tags with (nolock) 
                                        where clearbit_company_id = @clearbit_company_id 
                                        and user_id = @user_id"
                                    , DatabaseHelper.CreateStringParameter("@clearbit_company_id", clearbitCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersPersonCompanyPhoneNumbers> GetPersonCompanyPhonesByUserIdAndCompanyId(int userId, string clearbitCompanyId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyPhoneNumbers> loader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
            return loader.Load(@"Select * from Elio_users_person_company_phone_numbers with (nolock) 
                                        where clearbit_company_id = @clearbit_company_id 
                                        and user_id = @user_id"
                                    , DatabaseHelper.CreateStringParameter("@clearbit_company_id", clearbitCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPersonCompanyPhoneNumbers GetPersonCompanyPhoneByPhone(int userId, string clearbitCompanyId, string phoneNumber, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyPhoneNumbers> loader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
            return loader.LoadSingle(@"Select * from Elio_users_person_company_phone_numbers with (nolock) 
                                        where clearbit_company_id = @clearbit_company_id 
                                        and user_id = @user_id
                                        and phone_number = @phone_number"
                                    , DatabaseHelper.CreateStringParameter("@clearbit_company_id", clearbitCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateStringParameter("@phone_number", phoneNumber));
        }

        public static ElioUsersPersonCompanies GetPersonCompanyByPersonIdUserId(int elioPersonId, int userId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
            return loader.LoadSingle(@"Select * from Elio_users_person_companies with (nolock) 
                                        where elio_person_id = @elio_person_id 
                                        and user_id = @user_id
                                        and is_public = 1"
                                    , DatabaseHelper.CreateIntParameter("@elio_person_id", elioPersonId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static ElioUsersPersonCompanies GetPersonCompanyByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
            return loader.LoadSingle(@"Select * from Elio_users_person_companies with (nolock) 
                                        where 1 = 1 
                                        and user_id = @user_id
                                        and is_public = 1"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersPersonCompanyPhoneNumbers> GetPersonCompanyPhones(int elioPersonCompanyId, int userId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyPhoneNumbers> loader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
            return loader.Load(@"Select * from Elio_users_person_company_phone_numbers  
                                        where elio_person_company_id = @elio_person_company_id 
                                        and user_id = @user_id"
                                    , DatabaseHelper.CreateIntParameter("@elio_person_company_id", elioPersonCompanyId)
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersPersonCompanyTags> GetPersonCompanyTags1(int elioPersonCompanyId, DBSession session)
        {
            DataLoader<ElioUsersPersonCompanyTags> loader = new DataLoader<ElioUsersPersonCompanyTags>(session);
            return loader.Load(@"Select * from Elio_users_person_company_tags  
                                 where elio_person_company_id = @elio_person_company_id"
                                , DatabaseHelper.CreateIntParameter("@elio_person_company_id", elioPersonCompanyId));
        }

        public static string GetPersonCompanyTagsAsString(int elioPersonCompanyId, DBSession session)
        {
            DataTable table = new DataTable();
            table = session.GetDataTable(@"Select replace(tag_name, '&', 'and') + ', '
                                                from Elio_users_person_company_tags  
                                                where elio_person_company_id = @elio_person_company_id
                                                for xml path('')"
                                            , DatabaseHelper.CreateIntParameter("@elio_person_company_id", elioPersonCompanyId));

            return (table.Rows.Count > 0) ? (table.Rows[0][0].ToString().Length >= 2) ? table.Rows[0][0].ToString().Substring(0, table.Rows[0][0].ToString().Length - 2) : table.Rows[0][0].ToString() : "";
        }

        public static ElioUsersPersonIJPersonCompanies GetPersonCompaniesByUserId(int userId, DBSession session)
        {
            DataLoader<ElioUsersPersonIJPersonCompanies> loader = new DataLoader<ElioUsersPersonIJPersonCompanies>(session);
            return loader.LoadSingle(@"Select up.id
                                              ,up.user_id
                                              ,clearbit_person_id
                                              ,up.given_name
                                              ,up.family_name
                                              ,up.email
                                              ,up.phone
                                              ,up.location
                                              ,up.time_zone
                                              ,up.bio
                                              ,up.avatar
                                              ,up.title
                                              ,up.role
                                              ,up.seniority
                                              ,up.twitter_handle
                                              ,up.linkedin_handle
                                              ,up.about_me_handle
                                              ,up.date_inserted
                                              ,up.last_update
                                              ,up.is_public
                                              ,up.is_active
                                              ,up.is_claimed
                                              ,upc.elio_person_company_id as elio_person_company_id                                              
                                              ,upc.clearbit_company_id as clearbit_company_id
                                              ,upc.name
                                              ,upc.domain
                                              ,upc.sector
                                              ,upc.industry_group
                                              ,upc.industry
                                              ,upc.sub_industry
                                              ,upc.description
                                              ,upc.founded_year
                                              ,upc.location
                                              ,upc.fund_amount
                                              ,upc.employees_number
                                              ,upc.employees_range
                                              ,upc.annual_revenue
                                              ,upc.annual_revenue_range
                                              ,upc.facebook_handle
                                              ,upc.twitter_handle
                                              ,upc.crunchbase_handle
                                              ,upc.logo
                                              ,upc.type
                                        from Elio_users_person up
                                         inner join Elio_users_person_companies upc
	                                        on up.id = upc.elio_person_id
                                        where up.user_id = @user_id 
                                        and up.is_public = 1"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }
    }
}
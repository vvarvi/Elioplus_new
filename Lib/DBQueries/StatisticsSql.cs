using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class StatisticsSql
    {
        public static DataTable GetUsersRegistrationYears(DBSession session)
        {
            DataTable table = session.GetDataTable("select year(sysdate) as year from elio_users group by year(sysdate) order by year");

            return table;
        }

        public static DataTable GetCountOfUsersRegistrationMonthsByYear(string year, string accountStatus, DBSession session)
        {
            DataTable table = session.GetDataTable("select month(sysdate) as month,count(id) as count from elio_users " +
                                                   "where account_status in ( " + accountStatus + " ) and year(sysdate) in ('" + year + "') and user_application_type=1" +
                                                   "group by month(sysdate)");

            return table;
        }  

        public static DataTable GetCountOfUsersAndthirdPartyRegistrationMonthsByYear(string year, string accountStatus, int userApplicationType, DBSession session)
        {
            DataTable table = session.GetDataTable("select month(sysdate) as month,count(id) as count from elio_users " +
                                                   "where account_status in ( " + accountStatus + " ) and year(sysdate) in ('" + year + "') and user_application_type=@user_application_type " +
                                                   "group by month(sysdate)"
                                                   , DatabaseHelper.CreateIntParameter("@user_application_type", userApplicationType));

            return table;
        }

        public static DataTable GetCountOfUsersAndthirdPartyRegistrationMonthsByYearByType(string year, string accountStatus, string companyType, int userApplicationType, DBSession session)
        {
            DataTable table = session.GetDataTable("select month(sysdate) as month,count(id) as count from elio_users " +
                                                   "where account_status in ( " + accountStatus + " ) and year(sysdate) in ('" + year + "') and company_type in ('" + companyType + "') and user_application_type=@user_application_type " +
                                                   "group by month(sysdate)"
                                                   , DatabaseHelper.CreateIntParameter("@user_application_type", userApplicationType));

            return table;
        }    
    }
}
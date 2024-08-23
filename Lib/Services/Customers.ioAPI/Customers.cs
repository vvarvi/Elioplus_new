using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.Customers.ioAPI
{
    public class Customers
    {
        public static void AddUsersToCustomersAPI(ElioSession vSession, DBSession session)
        {
            bool mustClosed = false;
            if (session.Connection.State == System.Data.ConnectionState.Closed)
                session.OpenConnection();

            List<ElioUsers> users = null;
            //vSession.LoggedInUsersCount = 0;
            if (vSession.LoggedInUsersCount <= 100)
            {
                users = Sql.GetPublicFullRegisteredUsersJoinCustomersAPI(AccountPublicStatus.IsPublic, AccountStatus.Completed, session);
                if (users.Count > 0)
                {
                    foreach (ElioUsers user in users)
                    {
                        vSession.User = user;
                        if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                        {
                            ElioUsersCustomersCustom newInCustoners = new ElioUsersCustomersCustom();

                            newInCustoners.UserId = user.Id;
                            newInCustoners.ExistInCustomers = 1;
                            newInCustoners.Sysdate = DateTime.Now;

                            DataLoader<ElioUsersCustomersCustom> loader = new DataLoader<ElioUsersCustomersCustom>(session);
                            loader.Insert(newInCustoners);


                            break;
                        }
                    }
                }

                vSession.LoggedInUsersCount++;

                if (mustClosed)
                    session.CloseConnection();
            }
        }
    }
}
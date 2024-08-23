using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class Sql2
    {
        public static List<ElioUsersMessages> GetUsersMessages(DBLiveSession session)
        {
            DataLiveLoader<ElioUsersMessages> loader = new DataLiveLoader<ElioUsersMessages>(session);
            return loader.Load("Select * from elio_users_messages");
        }

        public static List<ElioUsersFavorites> GetUsersFavorites(DBLiveSession session)
        {
            DataLiveLoader<ElioUsersFavorites> loader = new DataLiveLoader<ElioUsersFavorites>(session);
            return loader.Load("Select * from elio_users_favorites");
        }
    }
}
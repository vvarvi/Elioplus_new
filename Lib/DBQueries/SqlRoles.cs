using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class SqlRoles
    {
        public static bool ExistRoleToSubAccount(int userId, int roleId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_users_sub_accounts
                                                        WHERE user_id = @user_id 
                                                            AND team_role_id = @team_role_id"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@team_role_id", roleId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static bool DeleteUserPermissionsRole(int userId, int roleId, DBSession session)
        {
            int row = session.ExecuteQuery(@"Delete from Elio_permissions_users_roles 
                                                where user_id = @user_id
                                                and id = @id"
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                            , DatabaseHelper.CreateIntParameter("@id", roleId));

            return row > -1 ? true : false;
        }

        public static bool DeleteUserPermissionsRolesFormAction(int userId, int roleId, DBSession session)
        {
            int row = session.ExecuteQuery(@"Delete from Elio_permissions_roles_forms_actions 
                                                where role_id = @role_id
                                                and user_id = @user_id"
                                            , DatabaseHelper.CreateIntParameter("@role_id", roleId)
                                            , DatabaseHelper.CreateIntParameter("@user_id", userId));

            return row > -1 ? true : false;
        }

        public static List<int> GetPermissionFormsIdsArray(DBSession session)
        {
            List<int> ids = new List<int>();
            DataTable table = session.GetDataTable(@"SELECT id FROM Elio_permissions_forms WHERE is_public = 1");

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (Convert.ToInt32(table.Rows[i]["id"]) > 0)
                        ids.Add(Convert.ToInt32(table.Rows[i]["id"]));
                }
            }

            return ids;
        }

        public static ElioPermissionsUsersRoles GetUserPermissionsRole(int userId, int roleId, DBSession session)
        {
            DataLoader<ElioPermissionsUsersRoles> loader = new DataLoader<ElioPermissionsUsersRoles>(session);

            return loader.LoadSingle(@"SELECT * 
                                        FROM Elio_permissions_users_roles 
                                        WHERE 1 = 1 
                                        and user_id = @user_id 
                                        and id = @id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@id", roleId));
        }

        public static ElioPermissionsRolesFormsActions GetUserPermissionRoleFormAction(int userId, int roleId, int formActionId, DBSession session)
        {
            DataLoader<ElioPermissionsRolesFormsActions> loader = new DataLoader<ElioPermissionsRolesFormsActions>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_permissions_roles_forms_actions 
                                    WHERE is_active = 1 
                                        and is_public = 1 
                                        and user_id = @user_id 
                                        and role_id = @role_id
                                        and form_action_id = @form_action_id"
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@role_id", roleId)
                                , DatabaseHelper.CreateIntParameter("@form_action_id", formActionId));
        }

        public static List<ElioTeamRoles> GetDefaultRoles(DBSession session)
        {
            DataLoader<ElioTeamRoles> loader = new DataLoader<ElioTeamRoles>(session);

            return loader.Load(@"SELECT [id]
                                       ,[role_description]
                                       ,[sysdate]
                                       ,[is_public]
                                  FROM Elio_team_roles
                                  WHERE is_public = 1");
        }

        public static bool ExistSystemRole(int userId, int roleId, string roleName, DBSession session)
        {
            DataLoader<ElioTeamRoles> loader = new DataLoader<ElioTeamRoles>(session);

            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_permissions_users_roles
                                                        WHERE user_id = @user_id 
                                                            AND role_name = @role_name 
                                                            AND is_system = 1 
                                                            AND (parent_role_id = @parent_role_id)
                                                            AND is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateStringParameter("@role_name", roleName)
                            , DatabaseHelper.CreateIntParameter("@parent_role_id", roleId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static int GetUserRolePermission(int userId, int roleId, string roleName, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT id
                                                        FROM Elio_permissions_users_roles
                                                        WHERE user_id = @user_id 
                                                            AND role_name = @role_name 
                                                            AND is_system = 1 
                                                            AND (parent_role_id = @parent_role_id)
                                                            AND is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateStringParameter("@role_name", roleName)
                            , DatabaseHelper.CreateIntParameter("@parent_role_id", roleId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : -1;
        }


        public static List<ElioPermissionsFormsActions> GetDefaultFormActions(DBSession session)
        {
            DataLoader<ElioPermissionsFormsActions> loader = new DataLoader<ElioPermissionsFormsActions>(session);

            return loader.Load(@"SELECT [id]
                                      ,[form_id]
                                      ,[action_id]
                                  FROM Elio_permissions_forms_actions
                                  WHERE is_public = 1");
        }

        public static bool ExistRoleFormAction(int userId, int roleId, int formActionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as count
                                                        FROM Elio_permissions_roles_forms_actions
                                                        WHERE user_id = @user_id 
                                                            AND form_action_id = @form_action_id                                                              
                                                            AND role_id = @role_id
                                                            AND is_active = 1
                                                            AND is_public = 1"
                            , DatabaseHelper.CreateIntParameter("@user_id", userId)
                            , DatabaseHelper.CreateIntParameter("@form_action_id", formActionId)
                            , DatabaseHelper.CreateIntParameter("@role_id", roleId));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static List<ElioPermissionsUsersRoles> GetUserPermissionRoles(int userId, string isSystem, DBSession session)
        {
            DataLoader<ElioPermissionsUsersRoles> loader = new DataLoader<ElioPermissionsUsersRoles>(session);

            if (isSystem == "0" || isSystem == "1")
            {
                return loader.Load(@"SELECT * FROM Elio_permissions_users_roles 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id and is_system = @is_system ORDER BY id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                    , DatabaseHelper.CreateIntParameter("@is_system", Convert.ToInt32(isSystem)));
            }
            else
            {
                return loader.Load(@"SELECT * FROM Elio_permissions_users_roles 
                                    WHERE is_active = 1 and is_public = 1 and user_id = @user_id ORDER BY is_system desc,id"
                                    , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
        }

        public static DataTable GetUserPermissionRolesTbl(int userId, string isSystem, DBSession session)
        {
            if (isSystem == "0" || isSystem == "1")
            {
                return session.GetDataTable(@"SELECT id,user_id,role_name,role_description,is_system,parent_role_id FROM Elio_permissions_users_roles 
                                                WHERE is_active = 1 and is_public = 1 and user_id = @user_id and is_system = @is_system ORDER BY id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateIntParameter("@is_system", Convert.ToInt32(isSystem)));
            }
            else
            {
                return session.GetDataTable(@"SELECT id,user_id,role_name,role_description,is_system,parent_role_id FROM Elio_permissions_users_roles 
                                                WHERE is_active = 1 and is_public = 1 and user_id = @user_id ORDER BY id"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }
        }

        public static int GetUserLastPermissionRoleIDTbl(int userId, string isSystem, DBSession session)
        {
            DataTable table = null;

            if (isSystem == "0" || isSystem == "1")
            {
                table = session.GetDataTable(@"SELECT top 1 id FROM Elio_permissions_users_roles 
                                                        WHERE is_active = 1 and is_public = 1 and user_id = @user_id and is_system = @is_system ORDER BY id desc"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                        , DatabaseHelper.CreateIntParameter("@is_system", Convert.ToInt32(isSystem)));
            }
            else
            {
                table = session.GetDataTable(@"SELECT top 1 id FROM Elio_permissions_users_roles 
                                                        WHERE is_active = 1 and is_public = 1 and user_id = @user_id ORDER BY id desc"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", userId));
            }

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : -1;
        }

        public static bool HasUserPermissionRoles(int userId, string isSystem, DBSession session)
        {
            string strQuery = @"SELECT count(id) as count 
                                FROM Elio_permissions_users_roles 
                                WHERE is_active = 1 
                                    and is_public = 1 
                                    and user_id = @user_id ";

            strQuery += (isSystem == "0" || isSystem == "1") ? " and is_system = @is_system" : "";

            DataTable table = session.GetDataTable(strQuery                                                            
                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                , DatabaseHelper.CreateIntParameter("@is_system", isSystem));

            return (table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["count"]) > 0 : false;
        }

        public static List<ElioPermissionsForms> GetPermissionsForms(DBSession session)
        {
            DataLoader<ElioPermissionsForms> loader = new DataLoader<ElioPermissionsForms>(session);

            return loader.Load(@"SELECT * FROM Elio_permissions_forms WHERE is_public = 1 ORDER BY id");
        }

        public static ElioPermissionsForms GetPermissionsFormById(string formId, DBSession session)
        {
            DataLoader<ElioPermissionsForms> loader = new DataLoader<ElioPermissionsForms>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_permissions_forms WHERE form_id = @form_id", DatabaseHelper.CreateStringParameter("@form_id", formId));
        }

        public static List<ElioPermissionsActions> GetPermissionsActions(DBSession session)
        {
            DataLoader<ElioPermissionsActions> loader = new DataLoader<ElioPermissionsActions>(session);

            return loader.Load(@"SELECT * FROM Elio_permissions_actions WHERE is_public = 1 ORDER BY id");
        }

        public static ElioPermissionsActions GetPermissionsActionById(int actionId, DBSession session)
        {
            DataLoader<ElioPermissionsActions> loader = new DataLoader<ElioPermissionsActions>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_permissions_actions WHERE id = @id", DatabaseHelper.CreateIntParameter("@id", actionId));
        }

        public static ElioPermissionsActions GetPermissionsActionByActonName(string actionName, DBSession session)
        {
            DataLoader<ElioPermissionsActions> loader = new DataLoader<ElioPermissionsActions>(session);

            return loader.LoadSingle(@"SELECT * FROM Elio_permissions_actions WHERE action_name = @action_name", DatabaseHelper.CreateStringParameter("@action_name", actionName));
        }

        public static bool ExistFormAction(int formId, int actionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as exist
                                          FROM Elio_permissions_forms_actions
                                          where form_id = @form_id and action_id = @action_id"
                                        , DatabaseHelper.CreateIntParameter("@form_id", formId)
                                        , DatabaseHelper.CreateIntParameter("@action_id", actionId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["exist"]) == 1 : false;
        }

        public static int GetPermissionsFormActionIdTbl(int formId, int actionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT id
                                          FROM Elio_permissions_forms_actions
                                          where form_id = @form_id and action_id = @action_id"
                                        , DatabaseHelper.CreateIntParameter("@form_id", formId)
                                        , DatabaseHelper.CreateIntParameter("@action_id", actionId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["id"]) : 0;
        }

        public static bool DeletePermissionsRolesFormAction(int userId, int roleId, int formActionId, DBSession session)
        {
            int row = session.ExecuteQuery(@"DELETE FROM Elio_permissions_roles_forms_actions
                                          where user_id = @user_id and role_id = @role_id and form_action_id = @form_action_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@role_id", roleId)
                                        , DatabaseHelper.CreateIntParameter("@form_action_id", formActionId));

            return row > 0;
        }

        public static bool ExistUserRolesFormAction(int userId, int roleId, int formId, int actionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as exist
                                                      FROM Elio_permissions_roles_forms_actions
                                                      where user_id = @user_id
                                                      and role_id = @role_id
                                                      and form_action_id in 
                                                      (
	                                                      SELECT id
	                                                      FROM Elio_permissions_forms_actions
	                                                      where form_id = @form_id
	                                                      and action_id = @action_id
                                                      )"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@role_id", roleId)
                                        , DatabaseHelper.CreateIntParameter("@form_id", formId)
                                        , DatabaseHelper.CreateIntParameter("@action_id", actionId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["exist"]) == 1 : false;
        }

        public static List<ElioUsersSubAccountsIJPermissionsRolesIJUsers> GetUserSubAccountsIJRolesIJUsers(int userId, int selectTop, int roleId, string subAccountEmail, DBSession session)
        {
            DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers>(session);

            string strQuery = "Select ";

            if (selectTop > 0)
                strQuery += " top " + selectTop + " ";

            strQuery += @" * from Elio_users_sub_accounts usa
                            inner join Elio_permissions_users_roles pur on pur.id = usa.team_role_id and pur.user_id = usa.user_id
                            inner join Elio_users u on u.id = usa.user_id
                            where usa.user_id = @user_id ";

            if (roleId > 0)
                strQuery += @" and pur.id = " + roleId + " ";

            if (subAccountEmail != "")
                strQuery += " and (usa.email like '" + subAccountEmail + "%' or usa.last_name like '" + subAccountEmail + "%' or usa.first_name like '" + subAccountEmail + "%')";

            return loader.Load(strQuery
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static List<ElioUsersSubAccountsIJPermissionsRolesIJUsers> GetUserSubAccountsConfirmedIJRolesIJUsers(int userId, int selectTop, int roleId, string subAccountEmail, DBSession session)
        {
            DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers>(session);

            string strQuery = "Select ";

            if (selectTop > 0)
                strQuery += " top " + selectTop + " ";

            strQuery += @" * from Elio_users_sub_accounts usa
                            inner join Elio_permissions_users_roles pur on pur.id = usa.team_role_id and pur.user_id = usa.user_id
                            inner join Elio_users u on u.id = usa.user_id
                            where usa.user_id = @user_id
                            and usa.is_confirmed = 1
                            and usa.is_active = 1 ";

            if (roleId > 0)
                strQuery += @" and pur.id = " + roleId + " ";

            if (subAccountEmail != "")
                strQuery += " and (usa.email like '" + subAccountEmail + "%' or usa.last_name like '" + subAccountEmail + "%' or usa.first_name like '" + subAccountEmail + "%')";

            return loader.Load(strQuery
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId));
        }

        public static DataTable GetUserSubAccountsMembersTbl(int userId, int selectTop, int isActive, int isConfirmed, DBSession session)
        {
            string strQuery = "Select ";

            if (selectTop > 0)
                strQuery += " top " + selectTop + " ";

            strQuery += @" pur.id as role_id,pur.role_name,usa.id as sub_account_id,usa.last_name,usa.first_name,usa.email,usa.position from Elio_users_sub_accounts usa
                            inner join Elio_permissions_users_roles pur on pur.id = usa.team_role_id and pur.user_id = usa.user_id
                            inner join Elio_users u on u.id = usa.user_id
                            where usa.user_id = @user_id
                            and usa.is_active = @is_active
                            and is_confirmed = @is_confirmed";

            return session.GetDataTable(strQuery
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                 , DatabaseHelper.CreateIntParameter("@is_confirmed", isConfirmed));
        }

        public static DataTable GetUserSubAccountsMembersFullNameAndRoleView(int userId, int selectTop, int isActive, int isConfirmed, DBSession session)
        {
            string strQuery = "Select ";

            if (selectTop > 0)
                strQuery += " top " + selectTop + " ";

            strQuery += @" pur.id as role_id,
                            case 
	                            when 
		                            usa.account_status = 1 
	                            then
		                            usa.last_name + ' ' + usa.first_name + ' - (' + pur.role_name + ')' 
	                            else
		                            usa.email + ' - (' + pur.role_name + ')' 
	                            end
                            as sub_fullname
                            ,pur.role_name
                            ,usa.last_name,usa.first_name
                            ,usa.id as sub_account_id,usa.email
                            ,usa.position 
                            ,u.company_logo
                            from Elio_users_sub_accounts usa
                            inner join Elio_permissions_users_roles pur on pur.id = usa.team_role_id and pur.user_id = usa.user_id
                            inner join Elio_users u on u.id = usa.user_id
                            where usa.user_id = @user_id
                            and usa.is_active = @is_active
                            and is_confirmed = @is_confirmed";

            return session.GetDataTable(strQuery
                                 , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                 , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                 , DatabaseHelper.CreateIntParameter("@is_confirmed", isConfirmed));
        }

        public static ElioUsersSubAccountsIJPermissionsRolesIJUsers GetSubAccountIJRolesIJUsersById(int id, DBSession session)
        {
            DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers> loader = new DataLoader<ElioUsersSubAccountsIJPermissionsRolesIJUsers>(session);

            return loader.LoadSingle(@"Select * from Elio_users_sub_accounts 
                                       inner join Elio_permissions_users_roles on Elio_permissions_users_roles.id=Elio_users_sub_accounts.team_role_id
                                       inner join Elio_users on Elio_users.id=Elio_users_sub_accounts.user_id
                                       where Elio_users_sub_accounts.id=@id"
                                       , DatabaseHelper.CreateIntParameter("@id", id));
        }

        public static bool HasRolesPermission(int userId, int roleId, int formActionId, DBSession session)
        {
            DataTable table = session.GetDataTable(@"SELECT count(id) as exist
                                                      FROM Elio_permissions_roles_forms_actions
                                                      where 1 = 1
                                                      and user_id = @user_id
                                                      and role_id = @role_id
                                                      and form_action_id = @form_action_id"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@role_id", roleId)
                                        , DatabaseHelper.CreateIntParameter("@form_action_id", formActionId));

            return (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["exist"]) == 1 : false;
        }

        public static bool HasSubAccountMembersByUserId(int userId, int isActive, int isConfirmed, DBSession session)
        {
            DataTable table = session.GetDataTable(@"select Count(id) as count 
                                                    from Elio_users_sub_accounts with (nolock) 
                                                    where user_id = @user_id
                                                    and is_active = @is_active
                                                    and is_confirmed = @is_confirmed"
                                                , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                                , DatabaseHelper.CreateIntParameter("@is_active", isActive)
                                                , DatabaseHelper.CreateIntParameter("@is_confirmed", isConfirmed));

            return Convert.ToInt32(table.Rows[0]["count"]) == 0 ? false : true;
        }
    }
}
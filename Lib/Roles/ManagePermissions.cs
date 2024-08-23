using System;
using System.Data;
using System.Web.UI;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Roles
{
    public class ManagePermissions
    {
        DBSession session = new DBSession();

        public static bool ManagePermissionsRights(int userId, int loggedInRoleId, bool isAdminRole, string formId, Actions action, DBSession session)
        {
            bool hasRight = false;
            bool mustClose = false;

            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                {
                    session.OpenConnection();
                    mustClose = true;
                }

                if (loggedInRoleId > 0 && !isAdminRole)
                {
                    ElioPermissionsForms form = SqlRoles.GetPermissionsFormById(formId, session);
                    if (form != null)
                    {
                        ElioPermissionsActions permissionAction = SqlRoles.GetPermissionsActionByActonName(action.ToString(), session);
                        if (permissionAction != null)
                        {
                            int formActionID = SqlRoles.GetPermissionsFormActionIdTbl(form.Id, permissionAction.Id, session);
                            if (formActionID > 0)
                            {
                                hasRight = SqlRoles.HasRolesPermission(userId, loggedInRoleId, formActionID, session);
                            }
                        }
                    }
                }
                else
                    hasRight = true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (mustClose)
                    session.CloseConnection();
            }

            return hasRight;
        }
    }
}
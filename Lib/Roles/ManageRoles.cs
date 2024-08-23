using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Roles
{
    public class ManageRoles
    {
        public static void InsertSystemRolePermissionsByUser(int userId, DBSession session)
        {
            try
            {
                session.BeginTransaction();

                List<ElioTeamRoles> systemRoles = SqlRoles.GetDefaultRoles(session);

                if (systemRoles.Count > 0)
                {
                    //List<ElioPermissionsUsersRoles> userListRoles = new List<ElioPermissionsUsersRoles>();
                    List<ElioPermissionsFormsActions> permissions = SqlRoles.GetDefaultFormActions(session);

                    foreach (ElioTeamRoles role in systemRoles)
                    {
                        bool exist = SqlRoles.ExistSystemRole(userId, role.Id, role.RoleDescription, session);
                        if (!exist)
                        {
                            ElioPermissionsUsersRoles userRole = new ElioPermissionsUsersRoles();

                            userRole.UserId = userId;
                            userRole.RoleName = role.RoleDescription;
                            userRole.RoleDescription = role.RoleDescription;
                            userRole.IsSystem = 1;
                            userRole.ParentRoleId = role.Id;
                            userRole.Sysdate = DateTime.Now;
                            userRole.LastUpdate = DateTime.Now;
                            userRole.IsActive = 1;
                            userRole.IsPublic = 1;

                            DataLoader<ElioPermissionsUsersRoles> loader = new DataLoader<ElioPermissionsUsersRoles>(session);
                            loader.Insert(userRole);

                            //userListRoles.Add(userRole);
                            if (permissions.Count > 0)
                            {
                                foreach (ElioPermissionsFormsActions permission in permissions)
                                {
                                    bool existFormAction = SqlRoles.ExistRoleFormAction(userId, userRole.Id, permission.Id, session);
                                    if (!existFormAction)
                                    {
                                        ElioPermissionsRolesFormsActions roleFA = new ElioPermissionsRolesFormsActions();

                                        roleFA.UserId = userId;
                                        roleFA.RoleId = userRole.Id;
                                        roleFA.FormActionId = permission.Id;
                                        roleFA.Sysdate = DateTime.Now;
                                        roleFA.LastUpdate = DateTime.Now;
                                        roleFA.IsActive = 1;
                                        roleFA.IsPublic = 1;

                                        DataLoader<ElioPermissionsRolesFormsActions> loaderRoleFA = new DataLoader<ElioPermissionsRolesFormsActions>(session);
                                        loaderRoleFA.Insert(roleFA);
                                    }
                                }
                            }
                        }
                        else
                        {
                            #region Case: For new forms and actions by default insert to each user

                            //if (permissions.Count > 0)
                            //{
                            //    int userRolePermissionId = SqlRoles.GetUserRolePermission(userId, role.Id, role.RoleDescription, session);
                            //    if (userRolePermissionId > 0)
                            //    {
                            //        foreach (ElioPermissionsFormsActions permission in permissions)
                            //        {
                            //            bool existFormAction = SqlRoles.ExistRoleFormAction(userId, userRolePermissionId, permission.Id, session);
                            //            if (!existFormAction)
                            //            {
                            //                ElioPermissionsRolesFormsActions roleFA = new ElioPermissionsRolesFormsActions();

                            //                roleFA.UserId = userId;
                            //                roleFA.RoleId = userRolePermissionId;
                            //                roleFA.FormActionId = permission.Id;
                            //                roleFA.Sysdate = DateTime.Now;
                            //                roleFA.LastUpdate = DateTime.Now;
                            //                roleFA.IsActive = 1;
                            //                roleFA.IsPublic = 1;

                            //                DataLoader<ElioPermissionsRolesFormsActions> loaderRoleFA = new DataLoader<ElioPermissionsRolesFormsActions>(session);
                            //                loaderRoleFA.Insert(roleFA);
                            //            }
                            //        }
                            //    }
                            //}

                            #endregion
                        }

                        #region Update Sub Account old team roles ID

                        int userRoleId = SqlRoles.GetUserRolePermission(userId, role.Id, role.RoleDescription, session);
                        if (userRoleId > 0 && userRoleId != role.Id)
                        {
                            List<ElioUsersSubAccounts> subAccounts = Sql.GetUserSubAccountsByRoleId(userId, role.Id, session);
                            if (subAccounts.Count > 0)
                            {
                                foreach (ElioUsersSubAccounts subAccount in subAccounts)
                                {
                                    if (subAccount.TeamRoleId != userRoleId)
                                    {
                                        subAccount.TeamRoleId = userRoleId;
                                        subAccount.LastUpdated = DateTime.Now;

                                        DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);
                                        loader.Update(subAccount);
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                throw ex;
            }
        }
    }
}
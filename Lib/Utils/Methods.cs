using System;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Lib.DB;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus.Lib.Utils
{
    public class Methods
    {
        #region To delete

        public static bool FixUserIndustriesByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
            bool hasSelectedItem = false;

        //    DataLoader<ElioUsersIndustries> loader = new DataLoader<ElioUsersIndustries>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            bool exist = false;

        //            exist = Sql.ExistUserIndustry(userId, Convert.ToInt32(item.Value), session);
        //            if (item.Selected)
        //            {
        //                hasSelectedItem = true;
        //                if (!exist)
        //                {
        //                    ElioUsersIndustries newIndustry = new ElioUsersIndustries();
        //                    newIndustry.UserId = userId;
        //                    newIndustry.IndustryId = Convert.ToInt32(item.Value);

        //                    loader.Insert(newIndustry);
        //                }
        //            }
        //            else
        //            {
        //                if (exist)
        //                {
        //                    //Delete user Industry
        //                    Sql.DeleteUserIndustry(userId, Convert.ToInt32(item.Value), session);
        //                }
        //            }
        //        }
        //    }
            return hasSelectedItem;
        }

        public static void InsertUserIndustriesByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
        //    DataLoader<ElioUsersIndustries> loader = new DataLoader<ElioUsersIndustries>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                ElioUsersIndustries newIndustry = new ElioUsersIndustries();
        //                newIndustry.UserId = userId;
        //                newIndustry.IndustryId = Convert.ToInt32(item.Value);

        //                loader.Insert(newIndustry);
        //            }
        //        }
        //    }
        }        

        public static bool FixUserPartnersByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
            bool hasSelectedItem = false;

        //    DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            bool exist = false;

        //            exist = Sql.ExistUserPartner(userId, Convert.ToInt32(item.Value), session);
        //            if (item.Selected)
        //            {
        //                hasSelectedItem = true;
        //                if (!exist)
        //                {
        //                    ElioUsersPartners newPartner = new ElioUsersPartners();
        //                    newPartner.UserId = userId;
        //                    newPartner.PartnerId = Convert.ToInt32(item.Value);

        //                    loader.Insert(newPartner);
        //                }
        //            }
        //            else
        //            {
        //                if (exist)
        //                {
        //                    //Delete user Partner
        //                    Sql.DeleteUserPartner(userId, Convert.ToInt32(item.Value), session);
        //                }
        //            }
        //        }
        //    }
            return hasSelectedItem;
        }

        public static void InsertPartnersByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
        //    DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                ElioUsersPartners newPartner = new ElioUsersPartners();
        //                newPartner.UserId = userId;
        //                newPartner.PartnerId = Convert.ToInt32(item.Value);

        //                loader.Insert(newPartner);
        //            }
        //        }
        //    }
        }

        public static bool FixUserMarketsByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
            bool hasSelectedItem = false;

        //    DataLoader<ElioUsersMarkets> loader = new DataLoader<ElioUsersMarkets>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            bool exist = false;

        //            exist = Sql.ExistUserMarket(userId, Convert.ToInt32(item.Value), session);
        //            if (item.Selected)
        //            {
        //                hasSelectedItem = true;
        //                if (!exist)
        //                {
        //                    ElioUsersMarkets newMarket = new ElioUsersMarkets();
        //                    newMarket.UserId = userId;
        //                    newMarket.MarketId = Convert.ToInt32(item.Value);

        //                    loader.Insert(newMarket);
        //                }
        //            }
        //            else
        //            {
        //                if (exist)
        //                {
        //                    //Delete user Market
        //                    Sql.DeleteUserMarket(userId, Convert.ToInt32(item.Value), session);
        //                }
        //            }
        //        }
        //    }
            return hasSelectedItem;
        }

        public static void InsertUserMarketsByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
        //    DataLoader<ElioUsersMarkets> loader = new DataLoader<ElioUsersMarkets>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                ElioUsersMarkets newMarket = new ElioUsersMarkets();
        //                newMarket.UserId = userId;
        //                newMarket.MarketId = Convert.ToInt32(item.Value);

        //                loader.Insert(newMarket);
        //            }
        //        }
        //    }
        }

        public static bool FixUserApiesByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
            bool hasSelectedItem = false;

        //    DataLoader<ElioUsersApies> loader = new DataLoader<ElioUsersApies>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            bool exist = false;

        //            exist = Sql.ExistUserApi(userId, Convert.ToInt32(item.Value), session);
        //            if (item.Selected)
        //            {
        //                hasSelectedItem = true;
        //                if (!exist)
        //                {
        //                    ElioUsersApies newApi = new ElioUsersApies();
        //                    newApi.UserId = userId;
        //                    newApi.ApiId = Convert.ToInt32(item.Value);

        //                    loader.Insert(newApi);
        //                }
        //            }
        //            else
        //            {
        //                if (exist)
        //                {
        //                    //Delete user Api
        //                    Sql.DeleteUserApi(userId, Convert.ToInt32(item.Value), session);
        //                }
        //            }
        //        }
        //    }

            return hasSelectedItem;
        }

        public static void InsertUserApiesByCheckBoxList(List<CheckBoxList> cbxList, int userId, DBSession session)
        {
        //    DataLoader<ElioUsersApies> loader = new DataLoader<ElioUsersApies>(session);

        //    foreach (CheckBoxList list in cbxList)
        //    {
        //        foreach (ListItem item in list.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                ElioUsersApies newApi = new ElioUsersApies();
        //                newApi.UserId = userId;
        //                newApi.ApiId = Convert.ToInt32(item.Value);

        //                loader.Insert(newApi);
        //            }
        //        }
        //    }
        }

        #endregion        
    }
}
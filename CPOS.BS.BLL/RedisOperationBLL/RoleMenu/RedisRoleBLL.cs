using JIT.CPOS.BS.Entity;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 角色菜单 缓存 操作
    /// </summary>
    public class RedisRoleBLL
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        public void SetRole(string customerID, string roleID, List<MenuModel> menuList)
        {
            try
            {
                RedisOpenAPI.Instance.CCRole().SetRole(new CC_Role
                {
                    CustomerID = customerID,
                    RoleID = roleID,
                    MenuList = menuList.Select(it => new CC_Menu
                    {
                        Create_Time = it.Create_Time,
                        Create_User_Id = it.Create_User_Id,
                        Display_Index = it.Display_Index,
                        Icon_Path = it.Icon_Path,
                        Menu_Code = it.Menu_Code,
                        Menu_Eng_Name = it.Menu_Eng_Name,
                        Menu_Id = it.Menu_Id,
                        Menu_Level = it.Menu_Level,
                        Menu_Name = it.Menu_Name,
                        Modify_Time = it.Modify_Time,
                        Modify_User_id = it.Modify_User_id,
                        Parent_Menu_Id = it.Parent_Menu_Id,
                        Reg_App_Id = it.Reg_App_Id,
                        Status = it.Status,
                        Url_Path = it.Url_Path,
                        User_Flag = it.User_Flag
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                throw new Exception("设置缓存失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        public List<MenuModel> GetRole(string customerID, string roleID)
        {
            try
            {
                var response = RedisOpenAPI.Instance.CCRole().GetRole(new CC_Role
                {
                    CustomerID = customerID,
                    RoleID = roleID
                });
                if (response.Code == ResponseCode.Success)
                {
                    return response.Result.MenuList.Select(it => new MenuModel
                    {
                        Create_Time = it.Create_Time,
                        Create_User_Id = it.Create_User_Id,
                        Display_Index = it.Display_Index,
                        Icon_Path = it.Icon_Path,
                        Menu_Code = it.Menu_Code,
                        Menu_Eng_Name = it.Menu_Eng_Name,
                        Menu_Id = it.Menu_Id,
                        Menu_Level = it.Menu_Level,
                        Menu_Name = it.Menu_Name,
                        Modify_Time = it.Modify_Time,
                        Modify_User_id = it.Modify_User_id,
                        Parent_Menu_Id = it.Parent_Menu_Id,
                        Reg_App_Id = it.Reg_App_Id,
                        Status = it.Status,
                        Url_Path = it.Url_Path,
                        User_Flag = it.User_Flag
                    }).ToList();
                }
                else
                {
                    return new List<MenuModel>();
                }
            }
            catch
            {
                return new List<MenuModel>();
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        public void DelRole(string customerID, string roleID)
        {
            try
            {
                RedisOpenAPI.Instance.CCRole().DelRole(new CC_Role
                {
                    CustomerID = customerID,
                    RoleID = roleID
                });
            }
            catch (Exception ex)
            {
                throw new Exception("删除缓存失败!" + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Module.Basic.Menu.Handler
{
    /// <summary>
    /// MenuHandler
    /// </summary>
    public class MenuHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "search_menu":
                    content = QueryMenuListData();
                    break;
                case "get_menu_by_id":
                    content = GetMenuInfoByIdData();
                    break;
                case "menu_save":
                    content = SaveMenuData();
                    break;
                case "menu_delete":
                    content = DeleteData();
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region QueryMenuListData
        /// <summary>
        /// 查询角色列表
        /// </summary>
        public string QueryMenuListData()
        {
            var form = Request("form").DeserializeJSONTo<MenuQueryEntity>();

            var appSysService = new AppSysService(CurrentUserInfo);
            IList<MenuModel> list = new List<MenuModel>();

            string content = string.Empty;
            string key = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            MenuModel queryEntity = new MenuModel();
            queryEntity.Reg_App_Id = form.app_sys_id;
            queryEntity.Parent_Menu_Id = FormatParamValue(Request("parent_menu_id"));
            queryEntity.Menu_Name = form.menu_name;
            queryEntity.Menu_Code = form.menu_code;
            list = appSysService.GetMenuList(queryEntity, pageIndex, PageSize);

            var jsonData = new JsonData();
            jsonData.totalCount = appSysService.GetMenuListCount(queryEntity).ToString();
            jsonData.data = list;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                list.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion


        #region GetMenuInfoByIdData
        /// <summary>
        /// 通过ID获取角色信息
        /// </summary>
        public string GetMenuInfoByIdData()
        {
            var service = new AppSysService(CurrentUserInfo);
            MenuModel data = null;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("menu_id") != null && Request("menu_id") != string.Empty)
            {
                key = Request("menu_id").ToString().Trim();
            }

            MenuModel queryEntity = new MenuModel();
            queryEntity.Menu_Id = key;
            var list = service.GetMenuList(queryEntity, 0, 1);
            if (list != null && list.Count > 0)
            {
                data = list[0];
            }

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveMenuData
        /// <summary>
        /// 保存角色
        /// </summary>
        public string SaveMenuData()
        {
            var menuService = new AppSysService(CurrentUserInfo);
            MenuModel obj = new MenuModel();
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string menu_id = string.Empty;
            if (Request("menu") != null && Request("menu") != string.Empty)
            {
                key = Request("menu").ToString().Trim();
            }
            if (Request("menu_id") != null && Request("menu_id") != string.Empty)
            {
                menu_id = Request("menu_id").ToString().Trim();
            }

            obj = key.DeserializeJSONTo<MenuModel>();

            //obj.Def_App_Id = "";
            //if (Request["app_sys_id"] != null && Request["app_sys_id"] != string.Empty)
            //{
            //    obj.Def_App_Id = Request["app_sys_id"].ToString().Trim();
            //}

            if (menu_id.Trim().Length == 0 || menu_id == "null" || menu_id == "undefined")
            {
                obj.Menu_Id = Utils.NewGuid();
                obj.Status = 1;
                obj.User_Flag = 1;
                obj.customer_id = CurrentUserInfo.CurrentUser.customer_id;
            }
            else
            {
                obj.Menu_Id = menu_id;
                obj.Status = 1;
                obj.User_Flag = 1;
            }

            obj.Menu_Level = 1;
            if (obj.Parent_Menu_Id != null && obj.Parent_Menu_Id.Trim().Length > 0)
            {
                var tmpParentObj = menuService.GetMenuList(new MenuModel() { Menu_Id = obj.Parent_Menu_Id }, 0, 1);
                if (tmpParentObj != null && tmpParentObj.Count > 0)
                {
                    obj.Menu_Level = tmpParentObj[0].Menu_Level + 1;
                }
            }
            else
            {
                obj.Parent_Menu_Id = "--";
            }

            if (obj.Menu_Code == null || obj.Menu_Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "编码不能为空";
                return responseData.ToJSON();
            }
            if (obj.Menu_Name == null || obj.Menu_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "名称不能为空";
                return responseData.ToJSON();
            }

            obj.Create_Time = Utils.GetNow();
            obj.Create_User_Id = CurrentUserInfo.CurrentUser.User_Id;
            obj.Modify_Time = Utils.GetNow();
            obj.Modify_User_id = CurrentUserInfo.CurrentUser.User_Id;

            menuService.SetMenuInfo(obj, true, "", out error);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 删除
        public string DeleteData()
        {
            var service = new AppSysService(CurrentUserInfo);
            string content = string.Empty;
            string error = "删除成功";
            var responseData = new ResponseData();
            string key = string.Empty;
            try
            {
                if (Request("ids") != null && Request("ids") != string.Empty)
                {
                    key = Request("ids").ToString().Trim();
                }
                if (key == null || key.Length == 0)
                {
                    responseData.success = false;
                    responseData.msg = "请选择菜单";
                    return responseData.ToJSON(); ;
                }

                var status = "-1";
                if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
                {
                    status = FormatParamValue(Request("status")).ToString().Trim();
                }
                if (!service.IsCheckMeauLast(key))
                {
                    responseData.success = false;
                    responseData.msg = "存在子菜单,请先删除子菜单";
                    return responseData.ToJSON(); ;

                }
                var idList = key.Split(',');
                foreach (var tmpId in idList)
                {
                    if (tmpId.Trim().Length > 0)
                    {
                        service.DeleteMenuById(tmpId.Trim());
                    }
                }
                responseData.success = true;
                responseData.msg = error;
            }
            catch (Exception ex)
            {
                responseData.success = false;
                responseData.msg = ex.Message;
            }
            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }
    #region QueryEntity
    public class MenuQueryEntity
    {
        public string app_sys_id;
        public string parent_menu_id;
        public string menu_name;
        public string menu_code;
    }
    #endregion

}
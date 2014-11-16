using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Web.SessionState;
using JIT.Utility.Web.ComponentModel;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// MenuSelectTreeHandler 的摘要说明
    /// </summary>
    public class MenuSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler,
        IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <param name="pParentNodeID">父节点ID</param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();

            string typeCode = "总部";
            AppSysService unitService = new AppSysService(new SessionManager().CurrentUserLoginInfo);
            IList<MenuModel> units;

            string key = string.Empty;
            string content = string.Empty;
            key = pParentNodeID;
            bool bl = false;
            if (!string.IsNullOrEmpty(key))
            {
                if (key.Substring(0, 1) == ",")
                {
                    bl = true;
                }
            }
            if (key == null || key == "-1" || key == "root" || key.Length == 0 || bl)
            {
                MenuModel queryEnity = new MenuModel();
                queryEnity.customer_id = CurrentUserInfo.CurrentUser.customer_id;

                queryEnity.Reg_App_Id = pParentNodeID;
                if (!string.IsNullOrEmpty(pParentNodeID))
                {
                    queryEnity.Reg_App_Id = pParentNodeID.Trim(',');
                }
                units = unitService.GetMenuList(queryEnity, 0, 1000);
            }
            else
            {
                MenuModel queryEnity = new MenuModel();
                queryEnity.Parent_Menu_Id = key;
                if (!string.IsNullOrEmpty(key))
                {
                    string[] str = key.Split(',');
                    if (str != null && str.Length > 1)
                    {
                        queryEnity.Parent_Menu_Id = str[0];
                        queryEnity.Reg_App_Id = str[1];
                    }
                }
                queryEnity.customer_id = CurrentUserInfo.CurrentUser.customer_id;
                units = unitService.GetMenuList(queryEnity, 0, 1000);
            }

            foreach (var item in units)
            {
                nodes.Add(item.Menu_Id, item.Menu_Name, item.Parent_Menu_Id, false);
            }
            return nodes;
        }


        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
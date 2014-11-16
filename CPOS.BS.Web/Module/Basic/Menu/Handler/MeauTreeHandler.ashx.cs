using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.BLL;
using JIT.Utility.Web.ComponentModel;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.Module.Basic.Menu.Handler
{
    /// <summary>
    /// MeauTreeHandler 的摘要说明
    /// </summary>
    public class MeauTreeHandler : JITCPOSTreeHandler
    {
        protected override Utility.Web.ComponentModel.TreeNodes GetNodes(string pParentNodeID)
        {
            string status = "";

            //var form = Request("form").DeserializeJSONTo<MenuQueryEntity>();

            if (Request("Status") != null && Request("Status") != "")
                status = Request("Status");
            //获取数据
            var appSysService = new AppSysService(CurrentUserInfo);
            MenuModel queryEntity = new MenuModel();
            string[] str = pParentNodeID.Split(',');
            if (str != null && str.Length > 1)
            {
                queryEntity.Reg_App_Id = str[1];
            }

            var list = appSysService.GetMenuList(queryEntity, 0, 1000);
            //组织数据
            TreeNodes nodes = new TreeNodes();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    TreeNode node = new TreeNode();
                    node.ID = item.Menu_Id;
                    //if (item.Menu_Level != 1)
                    //{
                    node.ParentID = item.Parent_Menu_Id;
                    //}

                    //node.Text = item.Menu_Name;
                    node.Text = "  <a class=\"pointer z_col_light_text\" onclick=\"fnView('" + item.Menu_Id + "')\">" + item.Menu_Name + "</a>";
                    node.IsLeaf = true;
                    //
                    nodes.Add(node);
                }
            }
            //
            return nodes;
        }
    }
}
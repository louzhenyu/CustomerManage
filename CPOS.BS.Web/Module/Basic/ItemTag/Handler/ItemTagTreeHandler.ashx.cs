using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.PageBase;
using JIT.Utility.Web.ComponentModel;

namespace JIT.CPOS.BS.Web.Module.Basic.ItemTag.Handler
{
    /// <summary>
    /// ItemTagTreeHandler1 的摘要说明
    /// </summary>
    public class ItemTagTreeHandler1 : JITCPOSTreeHandler
    {
        /// <summary>
        /// 获取节点数据
        /// </summary>
        /// <param name="pParentNodeID"></param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            //string status = "";
            //if (Request("Status") != null && Request("Status") != "")
            //    status = Request("Status");
            ////获取数据
            //var bll = new ItemCategoryService(this.CurrentUserInfo);
            //var list = bll.GetItemCagegoryList(status);
            ////组织数据
            //TreeNodes nodes = new TreeNodes();
            //if (list != null && list.Count > 0)
            //{
            //    foreach (var item in list)
            //    {
            //        TreeNode node = new TreeNode();
            //        node.ID = item.Item_Category_Id;
            //        if (string.IsNullOrWhiteSpace(item.Parent_Id) == false && item.Parent_Id != "-99")
            //        {
            //            node.ParentID = item.Parent_Id;
            //        }
            //        node.Text = item.Item_Category_Name;
            //        node.IsLeaf = true;
            //        //
            //        nodes.Add(node);
            //    }
            //}
            ////
            //return nodes;

            return null;
        }
    }
}
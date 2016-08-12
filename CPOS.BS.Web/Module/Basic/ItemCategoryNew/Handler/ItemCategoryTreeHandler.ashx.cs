using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.Web.ComponentModel;

namespace JIT.CPOS.BS.Web.Module2.BaseData.ItemCategory.Handler
{
    /// <summary>
    /// 获取商品分类树数据
    /// </summary>
    public class ItemCategoryTreeHandler : JITCPOSTreeHandler
    {
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            string status = "";
            if (Request("Status") != null && Request("Status") != "")
                status = Request("Status");
            string bat_id = "";
            if (Request("bat_id") != null && Request("bat_id") != "")
                bat_id = Request("bat_id");
            //获取数据
            var bll = new ItemCategoryService(this.CurrentUserInfo);
            var list = bll.GetItemCagegoryList(status, bat_id);
            //组织数据
            TreeNodes nodes = new TreeNodes();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    TreeNode node = new TreeNode();
                    node.ID = item.Item_Category_Id;
                    if (string.IsNullOrWhiteSpace(item.Parent_Id) == false && item.Parent_Id != "-99")
                    {
                        node.ParentID = item.Parent_Id;
                    }
                    node.Text = item.Item_Category_Name;
                    node.Status = item.Status;
                    node.IsLeaf = true;
                    node.NodeLevel = GetLevel(item.Parent_Id);
                    node.DisplayIndex = item.DisplayIndex == null ? 0 : (int)item.DisplayIndex;//排序字段,新加

                    //新添加的节点
                    node.create_time = string.IsNullOrEmpty(item.Create_Time) ? "" : Convert.ToDateTime(item.Create_Time).ToShortDateString();
                    node.ImageUrl = item.ImageUrl;
                    node.PromotionItemCount = item.PromotionItemCount;
                    nodes.Add(node);
                }
            }
            //
            return nodes;
        }

        public int GetLevel(string parentID)
        {
            var bll = new ItemCategoryService(this.CurrentUserInfo);

            if (string.IsNullOrEmpty(parentID) || parentID == "-99")
            {
                return 0;
            }
            else
            {
                var parent = bll.GetItemCategoryById(parentID);
                if (parentID != null)
                {
                    return GetLevel(parent.Parent_Id) + 1;
                }
            }
            return 0;
        }
    }
}
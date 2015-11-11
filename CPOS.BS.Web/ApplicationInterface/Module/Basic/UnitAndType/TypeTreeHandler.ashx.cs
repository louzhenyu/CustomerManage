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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.UnitAndType
{
    /// <summary>
    /// TypeTreeHandler 的摘要说明
    /// </summary>
    public class TypeTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler,
        IHttpHandler, IRequiresSessionState
    {

        protected override TreeNodes GetNodes(string pParentNodeID)
        {
           // int hasShop = 0;
            //if (Request("hasShop") != null && Request("hasShop") != "")
            //    hasShop = Convert.ToInt32(Request("hasShop"));
            var t_TypeBLL = new T_TypeBLL(this.CurrentUserInfo);
            var typeTable = t_TypeBLL.GetTypeTree(this.CurrentUserInfo.ClientID, this.CurrentUserInfo.UserID);
            var typeList = new List<T_TypeEntity>();
            if (typeTable != null && typeTable.Tables.Count != 0)
            {
                typeList = DataTableToObject.ConvertToList<T_TypeEntity>(typeTable.Tables[0]);//直接根据所需要的字段反序列化
            }
            //组织数据
            TreeNodes nodes = new TreeNodes();
            string parent_type_id = "-99";
            if (typeList != null && typeList.Count > 0)
            {
                foreach (var item in typeList)
                {
                    TreeNode node = new TreeNode();
                    node.ID = item.type_id;
                    //if (string.IsNullOrWhiteSpace(item.Parent_Id) == false && item.Parent_Id != "-99")
                    //{
                    node.ParentID = parent_type_id;
                    //  }
                    node.Text = item.type_name;
                    node.Status = item.status.ToString();
                    node.IsLeaf = false;
                    node.NodeLevel = (int)item.type_Level;//GetLevel(item.Parent_Id);
                    //成为下一级别的父节点*****
                    parent_type_id = item.type_id;
                    // node.DisplayIndex = item.DisplayIndex == null ? 0 : (int)item.DisplayIndex;//排序字段,新加

                    //新添加的节点
                    //  node.create_time = string.IsNullOrEmpty(item.c) ? "" : Convert.ToDateTime(item.Create_Time).ToShortDateString();
                    //node.ImageUrl = item.ImageUrl;
                    //node.PromotionItemCount = item.PromotionItemCount;
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
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

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Item
{
    /// <summary>
    /// ItemTreeHandler 的摘要说明
    /// </summary>
    public class ItemTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetCategoryTreeData":
                    content = GetCategoryTreeData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        public string GetCategoryTreeData()
        {
            string content = string.Empty;
            string strBat_id = "";
            if (Request("BatId") != null && Request("BatId") != "")
                strBat_id = Request("BatId");
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;//获取session数据        
   
            var itemService = new ItemService(loggingSessionInfo);
            T_Item_CategoryBLL bllCategory = new T_Item_CategoryBLL(loggingSessionInfo);

            var dsItem=itemService.GetItemTreeByCategoryType(strBat_id);
            var dsCategory = bllCategory.GetCategoryByCustomerId(loggingSessionInfo.ClientID, strBat_id);

            List<TreeNode> treeNode = new List<TreeNode>();
            if (dsCategory != null && dsCategory.Tables.Count != 0)
            {
                treeNode= DataTableToObject.ConvertToList<TreeNode>(dsCategory.Tables[0]);

            }
            var ItemList = new List<TreeNode>();
            if (dsItem != null && dsItem.Tables.Count != 0)
            {
                ItemList = DataTableToObject.ConvertToList<TreeNode>(dsItem.Tables[0]);
            }

            foreach (var node in treeNode)
             {
                foreach(var item in ItemList)
                {
                    if(node.id==item.ParentId)
                    {
                        if (node.children == null)
                            node.children = new List<TreeNode>();

                        node.children.Add(item);
                    }
                }
             }
            var jsonData = new JsonData();
            jsonData.totalCount = treeNode.Count.ToString();
            jsonData.data = treeNode;


            content = jsonData.ToJSON();
            return content;
        }
        public class TreeNode
        {
            /// <summary>
            /// 节点id
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 节点文本
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// 节点状态是否展开节点， open或者close 默认open  
            /// </summary>
            public string state { get; set; }
            /// <summary>
            /// 是否被选中
            /// </summary>
            public bool check { get; set; }
            public string attributes { get; set; }

            public IList<TreeNode> children { get; set; }
            public string ParentId { get; set; }
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
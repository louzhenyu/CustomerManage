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
using System.Data;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// ItemCategorySelectTreeHandler 的摘要说明
    /// </summary>
    public class ItemCategorySelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
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

            ItemCategoryService itemService = new ItemCategoryService(
                new SessionManager().CurrentUserLoginInfo);
            IList<ItemCategoryInfo> list = new List<ItemCategoryInfo>();

            string key = string.Empty;
            string content = string.Empty;
            if (Request("node") != null && Request("node") != string.Empty)
            {
                key = Request("node").ToString().Trim();
            }

            if (key == "root" || key.Length == 0)
            {
                key = "-99";
            }
            list = itemService.GetItemCategoryListByParentId(key);

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            foreach (var item in list)
            {
                if (item.Status == "1")
                {
                    nodes.Add(item.Item_Category_Id, item.Item_Category_Name, item.Parent_Id, false);
                }
            }
            DataSet ds =itemService.GetItemsBytype(key);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    nodes.Add(item["item_id"].ToString(), item["item_name"].ToString(), key, false);
                }
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
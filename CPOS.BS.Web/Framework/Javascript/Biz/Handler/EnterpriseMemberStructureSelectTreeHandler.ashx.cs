using System.Collections.Generic;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Web.ComponentModel;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// EnterpriseMemberStructureSelectTreeHandler 的摘要说明
    /// </summary>
    public class EnterpriseMemberStructureSelectTreeHandler : PageBase.JITCPOSTreeHandler, IHttpHandler
    {
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <param name="pParentNodeID">父节点ID</param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();

            EnterpriseMemberStructureBLL itemService = new EnterpriseMemberStructureBLL(new SessionManager().CurrentUserLoginInfo);

            string key = string.Empty;
            if (Request("node") != null && Request("node") != string.Empty)
            {
                key = Request("node").Trim();
            }

            if (key == "root" || key.Length == 0)
            {
                key = "";
            }
            IList<EnterpriseMemberStructureEntity> list = itemService.GetEnterpriseMemberStructureListByParentId(key);

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            foreach (var item in list)
            {
                nodes.Add(item.EnterpriseMemberStructureID.ToString(), item.StructureTitle, item.ParentID.ToString(), false);
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
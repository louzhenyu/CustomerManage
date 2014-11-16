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
    /// WMenuSelectTreeHandler 的摘要说明
    /// </summary>
    public class WMenuSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
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

            var service = new WMenuBLL(new SessionManager().CurrentUserLoginInfo);
            IList<WMenuEntity> data = new List<WMenuEntity>();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("node") != null && Request("node") != string.Empty)
            {
                key = Request("node").ToString().Trim();
            }
            if (key == "root")
            {
                key = "";
            }

            //if (key.Length == 2)
            //{
            //    data = service.GetCityListByProvince(key);
            //}
            //else if (key.Length == 4)
            //{
            //    data = service.GetAreaListByCity(key);
            //}
            //else if (key.Length == 0)
            //{
            //    data = service.GetProvinceList();
            //}

            var queryEntity = new WMenuEntity();
            queryEntity.ParentId = key;
            queryEntity.ApplicationId = Request("ApplicationId");

            data = service.GetWebWMenu(queryEntity, 0, 1000);

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            var parentCode = string.Empty;

            foreach (var item in data)
            {
                nodes.Add(item.ID, item.Name,
                    item.ParentId, 
                    item.Level == "3" ? true : false);
            }
            //
            //var root = new TreeNodes();
            //root.Add(new TreeNode() { ID = "1", IsLeaf = false, Text = "1" });
            //root.Add(new TreeNode() { ID = "1_1", ParentID = "1", IsLeaf = false, Text = "1_1" });
            //root.Add(new TreeNode() { ID = "1_1_1", ParentID = "1_1", IsLeaf = false, Text = "1_1_1`" });
            return nodes;
        }

        private string GetParentCode(string cityCode)
        {
            if (cityCode.Length == 2)
                return "root";
            if (cityCode.Length == 4)
                return cityCode.Substring(0, 2);
            if (cityCode.Length == 6)
                return cityCode.Substring(2, 2);
            return string.Empty;
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
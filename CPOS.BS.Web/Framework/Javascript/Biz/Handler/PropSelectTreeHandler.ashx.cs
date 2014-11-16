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
    /// PropSelectTreeHandler 的摘要说明
    /// </summary>
    public class PropSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler,
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

            var service = new PropService(new SessionManager().CurrentUserLoginInfo);
            IList<PropInfo> data = new List<PropInfo>();
            string content = string.Empty;

            string key = string.Empty;
            string domain = string.Empty;
            if (Request("node") != null && Request("node") != string.Empty)
            {
                key = Request("node").ToString().Trim();
            }
            if (key == "root" || key.Trim().Length == 0)
            {
                key = "-99";
            }
            if (Request("domain") != null && Request("domain") != string.Empty)
            {
                domain = Request("domain").ToString().Trim();
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

            var queryEntity = new PropInfo();
            queryEntity.Parent_Prop_id = key;
            queryEntity.Prop_Domain = domain;


            if (queryEntity.Parent_Prop_id != "-99")
            {
                var pr = new PropService(CurrentUserInfo);
                PropInfo parentObj = pr.GetPropInfoById(key);
                if (parentObj.Prop_Type != "2")
                {
                    queryEntity.CustomerId = this.CurrentUserInfo.ClientID;
                }

            }
            data = service.GetWebProp(queryEntity, 0, 1000);

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            var parentCode = string.Empty;

            foreach (var item in data)
            {
                nodes.Add(item.Prop_Id, item.Prop_Name,
                    item.Parent_Prop_id,
                    false);
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
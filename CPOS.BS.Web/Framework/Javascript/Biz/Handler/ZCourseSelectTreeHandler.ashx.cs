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
    /// ZCourseSelectTreeHandler 的摘要说明
    /// </summary>
    public class ZCourseSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
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

            var service = new ZCourseBLL(new SessionManager().CurrentUserLoginInfo);
            IList<ZCourseEntity> data = new List<ZCourseEntity>();
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

            var queryEntity = new ZCourseEntity();
            queryEntity.ParentId = key;
            //queryEntity.ApplicationId = Request("ApplicationId");
            queryEntity.OrderBy = "CourseName asc";

            data = service.GetCourses(queryEntity, 0, 1000);

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            var parentCode = string.Empty;

            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    nodes.Add(item.CourseId, item.CourseName,
                        item.ParentId,
                        item.CourseLevel == 2 ? true : false);
                }
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
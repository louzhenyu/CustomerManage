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
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// LEventSelectTreeHandler 的摘要说明
    /// </summary>
    public class LEventSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
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
            LEventsBLL service = new LEventsBLL(new SessionManager().CurrentUserLoginInfo);
            IList<LEventsEntity> units;

            string key = string.Empty;
            string content = string.Empty;
            key = pParentNodeID;

            var orderBy = new OrderBy[]{
                new OrderBy{ FieldName = "DisplayIndex", Direction=OrderByDirections.Asc }
            };
            if (key == null || key == "-1" || key == "root" || key.Length == 0)
            {
                units = service.QueryByEntity(new LEventsEntity() { 
                    IsSubEvent = 0,
                    CustomerId=this.CurrentUserInfo.CurrentUser.customer_id
                }, orderBy);
            }
            else
            {
                units = service.QueryByEntity(new LEventsEntity()
                {
                    ParentEventID = key,
                    CustomerId = this.CurrentUserInfo.CurrentUser.customer_id
                }, orderBy);
            }

            foreach (var item in units)
            {
                nodes.Add(item.EventID, item.Title, item.ParentEventID, false);
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
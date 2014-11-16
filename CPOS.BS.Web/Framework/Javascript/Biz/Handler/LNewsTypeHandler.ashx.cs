using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JIT.Utility.Web.ComponentModel;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// LNewsTypeHandler 的摘要说明
    /// </summary>
    public class LNewsTypeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler,
        IHttpHandler, IRequiresSessionState
    {

        protected override Utility.Web.ComponentModel.TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();
            LNewsTypeBLL service = new LNewsTypeBLL(new SessionManager().CurrentUserLoginInfo);
            IList<LNewsTypeEntity> units;

            string key = string.Empty;
            string content = string.Empty;
            key = pParentNodeID;

            var orderBy = new OrderBy[]{
                new OrderBy{ FieldName = "CreateTime", Direction=OrderByDirections.Desc }
            };
            if (key == null || key == "-1" || key == "root" || key.Length == 0)
            {
                units = service.QueryByEntity(new LNewsTypeEntity()
                {
                    IsDelete = 0,
                    CustomerId = this.CurrentUserInfo.CurrentUser.customer_id
                }, orderBy);
            }
            else
            {
                units = service.QueryByEntity(new LNewsTypeEntity()
                {
                    IsDelete = 0,
                    ParentTypeId = key,
                    CustomerId = this.CurrentUserInfo.CurrentUser.customer_id
                }, orderBy);
            }

            foreach (var item in units)
            {
                nodes.Add(item.NewsTypeId, item.NewsTypeName, item.ParentTypeId, false);
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
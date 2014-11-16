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
    /// ESalesVisitVipHandler 的摘要说明
    /// </summary>
    public class ESalesVisitVipHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
        IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="context"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request["method"])
            {
                case "get_list":
                default:
                    content = GetListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetListData
        /// <summary>
        /// 获取列表
        /// </summary>
        public string GetListData()
        {
            var service = new VipEnterpriseExpandBLL(new SessionManager().CurrentUserLoginInfo);
            var vipBLL = new VipBLL(new SessionManager().CurrentUserLoginInfo);
            IList<VipEnterpriseExpandEntity> data = new List<VipEnterpriseExpandEntity>();
            string content = string.Empty;

            string key = "";
            if (Request("pid") != null && Request("pid") != string.Empty)
            {
                key = Request("pid").ToString().Trim();
            }
            data = service.GetAll();

            if (data != null)
            {
                foreach (var item in data)
                {
                    item.Vip = vipBLL.GetByID(item.VipId);
                    item.VipName = item.Vip.VipName + "(" + item.Position + ")";
                }
            }

            var jsonData = new JsonData();
            jsonData.totalCount = data.Count.ToString();
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
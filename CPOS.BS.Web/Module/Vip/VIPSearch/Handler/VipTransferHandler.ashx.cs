using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.Module.Vip.VipSearch.Handler
{
    /// <summary>
    /// NaviSalesHandler
    /// </summary>
    public class VipTransferHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "data_query":      //活动查询
                    content = GetSaleFunnelData();
                    break;
           }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetData 获取数据

        /// <summary>
        /// 获取数据
        /// </summary>
        public string GetSaleFunnelData()
        {
            var vipBLL = new VipBLL(this.CurrentUserInfo);

            string content = string.Empty;

            var list = vipBLL.GetSaleFunnelData();

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                list.ToJSON(),
                list.Tables[0].Rows.Count);

            return content;
        }

        #endregion
    }

    #region QueryEntity

    public class EventsQueryEntity
    {
        public string VipShowId;
        public string VipName;
        public string Experience;
        public string ItemName;
        public string BeginTime;
    }

    #endregion
}
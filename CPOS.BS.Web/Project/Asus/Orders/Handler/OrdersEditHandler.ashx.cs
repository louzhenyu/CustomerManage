using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;

using JIT.Utility.Web;
using JIT.Utility.Reflection;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Project.Asus.Orders.Handler
{
    /// <summary>
    /// OrdersEditHandler 的摘要说明
    /// </summary>
    public class OrdersEditHandler : JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "GetOrdersDetail":
                    res = GetOrdersDetail(pContext.Request.Form);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetOrdersDetail
        /// <summary>
        /// 根据订单ID获取订单明细和订单操作流水数据
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string GetOrdersDetail(NameValueCollection rParams)
        {
            TInoutEntity entity = new TInoutEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity.OrderID = rParams["id"].ToString();
                DataSet ds = new DataSet();
                ds = new TInoutDetailBLL(CurrentUserInfo).HS_GetOrdersDetail(entity);
                return string.Format("{{\"orderDetail\":{0}}}", ds.Tables[0].ToJSON());
            }
            else
            {
                return "{{\"orderDetail\":\"\"}}";
            }
        }
        #endregion
    }
}
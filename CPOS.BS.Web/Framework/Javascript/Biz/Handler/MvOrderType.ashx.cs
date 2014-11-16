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
using System.Collections;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// MvOrderType 的摘要说明
    /// </summary>
    public class MvOrderType : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
        IHttpHandler, IRequiresSessionState
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request["method"])
            {
                case "order_type_mv_inout":
                    content = GetOrderTypeData("MV_INOUT");
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetOrderTypeData
        /// <summary>
        /// 单据类型
        /// </summary>
        public string GetOrderTypeData(string typeCode)
        {
            IList<Hashtable> list = new List<Hashtable>();

            string content = string.Empty;
            switch (typeCode)
            {
                case "MV_INOUT":
                    if (true)
                    {
                        Hashtable htObj = new Hashtable();
                        htObj["order_type_id"] = "AE6014B8F8194489A74B33C67526BF39";
                        htObj["order_type_name"] = "调拨出库";
                        list.Add(htObj);
                    }
                    if (true)
                    {
                        Hashtable htObj = new Hashtable();
                        htObj["order_type_id"] = "12C8C84572934D1F800D84EAFF74CB68";
                        htObj["order_type_name"] = "调拨入库";
                        list.Add(htObj);
                    }
                    break;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

     

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
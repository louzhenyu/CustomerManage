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
    /// OrderNo 的摘要说明
    /// </summary>
    public class OrderNo : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "new_order_no_po":
                    content = NewOrderNoData("PO");
                    break;
                case "new_order_no_ro":
                    content = NewOrderNoData("RO");
                    break;
                case "new_order_no_cc":
                    content = NewOrderNoData("CC");
                    break;
                case "new_order_no_so":
                    content = NewOrderNoData("SO");
                    break;
                case "new_order_no_do":
                    content = NewOrderNoData("DO");
                    break;
                case "new_order_no_rt":
                    content = NewOrderNoData("RT");
                    break;
                case "new_order_no_ws":
                    content = NewOrderNoData("WS");
                    break;
                case "new_order_no_aj":
                    content = NewOrderNoData("AJ");
                    break;
                case "new_order_no_mv_inout":
                    content = NewOrderNoData("MV");
                    break;
                case "CA":
                    content = NewOrderNoData("CA");
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region NewOrderNoData
        /// <summary>
        /// 生成单据号码
        /// </summary>
        public string NewOrderNoData(string typeCode)
        {
            var billService = new cBillService(CurrentUserInfo);
            string order_no;

            string content = string.Empty;
            order_no = billService.GetBillNextCode(typeCode);

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = order_no;

            //Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            //Jayrock.Json.Conversion.JsonConvert.Export(jsonData, writer);
            //content = writer.ToString();

            //return content;

            //var jsonData = new JsonData();
            //jsonData.totalCount = list.Count.ToString();
            //jsonData.data = list;

            content = jsonData.ToJSON();
            return content;

        }
        #endregion
    }
}
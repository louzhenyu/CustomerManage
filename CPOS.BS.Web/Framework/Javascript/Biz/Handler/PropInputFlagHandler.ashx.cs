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
    /// PropInputFlagHandler 的摘要说明
    /// </summary>
    public class PropInputFlagHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler, 
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
                default:
                    content = GetData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        public string GetData()
        {
            var billService = new cBillService(new SessionManager().CurrentUserLoginInfo);
            IList<BillStatusModel> list = new List<BillStatusModel>();
            //list.Add(new BillStatusModel() { Id = "1", Description = "无" });
            list.Add(new BillStatusModel() { Id = "2", Description = "文字" }); //label
            list.Add(new BillStatusModel() { Id = "3", Description = "输入文本" }); //text
            list.Add(new BillStatusModel() { Id = "10", Description = "输入文本(数字)" }); //text
            list.Add(new BillStatusModel() { Id = "7", Description = "输入文本(多行)" }); //textarea
            list.Add(new BillStatusModel() { Id = "4", Description = "下拉选项" }); //select
            list.Add(new BillStatusModel() { Id = "5", Description = "日期选项(yyyy-MM-dd)" }); //select-date-(yyyy-MM-dd)
            list.Add(new BillStatusModel() { Id = "6", Description = "日期选项(yyyy-MM)" }); // select-date-(yyyy-MM)
            list.Add(new BillStatusModel() { Id = "8", Description = "富文本" });
            list.Add(new BillStatusModel() { Id = "9", Description = "图片上传" });
            list.Add(new BillStatusModel() { Id = "11", Description = "键值对" });
            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

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
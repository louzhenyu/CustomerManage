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
    /// VipSourceHandler 的摘要说明
    /// </summary>
    public class VipSourceHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
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
                case "GetVipByPhone":
                    content = GetVipByPhone(pContext.Request["query"]);
                    break;
                case "VipSource":
                default:
                    content = GetVipSourceData(pContext.Request["vipSourceType"]);
                    break;

            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetVipSourceData
        /// <summary>
        /// 获取订单或者会员来源
        /// </summary>
        /// <param name="vipSourceType">0=会员；1=订单</param>
        /// <returns></returns>
        public string GetVipSourceData(string vipSourceType)
        {
            IList<BillStatusModel> list = new List<BillStatusModel>();
            SysVipSourceBLL service = new SysVipSourceBLL(new SessionManager().CurrentUserLoginInfo);
            SysVipSourceEntity[] dataList = null;
            if (!string.IsNullOrEmpty(vipSourceType))
                dataList = service.QueryByEntity(new SysVipSourceEntity() { VipSourceType = int.Parse(vipSourceType) }, null);
            else
                dataList = service.GetAll();
            foreach (var dataItem in dataList)
            {
                list.Add(new BillStatusModel()
                {
                    Id = dataItem.VipSourceID,
                    Description = dataItem.VipSourceName
                });
            }

            string content = string.Empty;

            var jsonData = new JsonData();
            jsonData.totalCount = list.Count.ToString();
            jsonData.data = list;

            content = jsonData.ToJSON();
            return content;
        }


        public string GetVipByPhone(string phone)
        {
            VipBLL vipBll = new VipBLL(new SessionManager().CurrentUserLoginInfo);

            if (string.IsNullOrWhiteSpace(phone))
            {
                return new List<VipEntity>().ToJSON();
            }

            List<VipEntity> viplist = vipBll.GetVipByPhone(phone.Trim());

            return viplist.ToJSON();
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
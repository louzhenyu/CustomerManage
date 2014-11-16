using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.Log;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.Module.VipCard.VipCardList.Handler
{
    /// <summary>
    /// VipCardListHandler 的摘要说明
    /// </summary>
    public class VipCardListHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("method:{0}", pContext.Request.QueryString["method"])
            });
            switch (pContext.Request.QueryString["method"])
            {
                case "Search": //查询
                    content = SearchVipCard();
                    break;
                case "vipCardUpdateStatus":     //修改卡状态
                    content = SetVipCardStatus();
                    break;
                case "templae_save": //修改保存
                   // content = SaveTemplate();
                    break;
                case "GetEventById":
                    //content = GetEventById();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region SearchVipCard
        /// <summary>
        /// 订单查询
        /// </summary>
        public string SearchVipCard()
        {
            var form = Request("form").DeserializeJSONTo<VipCardEntity>();

            VipCardBLL vipService = new VipCardBLL(CurrentUserInfo);

            VipCardEntity data = new VipCardEntity();
            string content = string.Empty;
            
            //string order_no = FormatParamValue(form.order_no);
            //string sales_unit_id = "";
            //string purchase_unit_id = "";
            //    purchase_unit_id = FormatParamValue(Request("purchase_unit_id"));

            //string order_status = FormatParamValue(form.order_status);
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));

            VipCardEntity searchInfo = new VipCardEntity();
            searchInfo.VipCardCode = form.VipCardCode;
            searchInfo.VipName = form.VipName;
            searchInfo.maxRowCount = maxRowCount;
            searchInfo.startRowIndex = startRowIndex;
            searchInfo.VipCardStatusId = form.VipCardStatusId;
            searchInfo.VipCardGradeID = form.VipCardGradeID;
            data = vipService.SearchVipCard(searchInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.VipCardInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region
        public string SetVipCardStatus()
        {
            string error = "";
            string content = string.Empty;
            var responseData = new ResponseData();
            #region 获取需要处理的会员卡标识集合
            string ids = string.Empty;
            string StatusIDNext = string.Empty;
            StatusIDNext = FormatParamValue(Request("StatusIDNext")).ToString().Trim();
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                ids = FormatParamValue(Request("ids")).ToString().Trim();
            }
            else {
                responseData.success = false;
                responseData.msg = "没有记录";
                content = responseData.ToJSON();
                return content;
            }
            #endregion
            VipCardStatusChangeLogBLL server = new VipCardStatusChangeLogBLL(this.CurrentUserInfo);
            bool bReturn = server.SetVipCardStatusChangeBatch(ids, Convert.ToInt32(StatusIDNext), out error);

            responseData.success = bReturn;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }
}
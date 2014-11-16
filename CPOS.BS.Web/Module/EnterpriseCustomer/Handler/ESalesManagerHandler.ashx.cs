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

namespace JIT.CPOS.BS.Web.Module.EnterpriseCustomer.Handler
{
    /// <summary>
    /// ESalesManagerHandler 的摘要说明
    /// </summary>
    public class ESalesManagerHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        #region 页面入口
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "ESales_query":      //销售线索查询
                    content = GetESalesQteryData();
                    break;
                case "ESales_delete":      //销售线索查询
                    content = GetESalesDelete();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        #endregion

        #region GetESalesQteryData 销售线索查询

        /// <summary>
        /// 查询活动列表
        /// </summary>
        public string GetESalesQteryData()
        {
            var server = new ESalesBLL(this.CurrentUserInfo);
            string content = string.Empty;
            #region 参数处理
            var form = Request("form").DeserializeJSONTo<ESalesQueryEntity>();
            string SalesName = FormatParamValue(form.SalesName);
            string EnterpriseCustomerId = FormatParamValue(Request("ECCustomerId"));
            string SalesProductId = FormatParamValue(form.SalesProductId);
            string SalesStage = FormatParamValue(form.SalesStage);
            string SalesVipId = FormatParamValue(form.SalesChargeVipId);
            string ParentEventID = FormatParamValue(Request("ParentEventID"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            ESalesEntity queryEntity = new ESalesEntity();
            queryEntity.SalesName = SalesName;
            queryEntity.EnterpriseCustomerId = EnterpriseCustomerId;
            queryEntity.SalesProductId = SalesProductId;
            queryEntity.StageId = SalesStage;
            queryEntity.SalesVipId = SalesVipId;
            #endregion
            int dataTotalCount = 0;
            var data = server.GetSalesList(queryEntity, pageIndex, PageSize, out dataTotalCount);
           

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }
        #region QueryEntity

        public class ESalesQueryEntity
        {
            public string SalesName;
            public string EnterpriseCustomerId;
            public string SalesProductId;
            public string SalesStage;
            public string SalesChargeVipId;
        }

        #endregion
        #endregion

        #region 删除
        public string GetESalesDelete()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            //int? status = int.Parse(Request("status"));
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "销售线索标识不能为空";
                return responseData.ToJSON();
            }

            new ESalesBLL(this.CurrentUserInfo).Delete(new ESalesEntity()
            {
                SalesId = key
                ,IsDelete = 1
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        } 
        #endregion
    }
}
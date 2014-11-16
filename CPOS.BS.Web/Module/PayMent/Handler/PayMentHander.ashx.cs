using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using System.Collections.Specialized;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess;
using JIT.CPOS.Common;
using System.Data;
using JIT.CPOS.BS.Web.PageBase;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.PayMent.Handler
{
    /// <summary>
    /// PayMentHander 的摘要说明
    /// </summary>
    public class PayMentHander : JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = string.Empty;
            switch (this.Method)
            {
                case "getPayMentTypePage":
                    content = GetPayMentTypePage(pContext.Request.Form);
                    break;
                case "getMapingbyPayMentTypeId":
                    content = GetMapingbyPayMentTypeId(pContext.Request);
                    break;
                case "disablePayment":
                    content = DisablePayment(pContext.Request.Form);
                    break;
                default:
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        #region GetPayMentTypePage
        public string GetPayMentTypePage(NameValueCollection rParams)
        {
            TPaymentTypeBLL service = new TPaymentTypeBLL(CurrentUserInfo);
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            ReqPayMentTypeEntity entity = rParams["form"].DeserializeJSONTo<ReqPayMentTypeEntity>();


            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && !string.IsNullOrEmpty(entity.Payment_Type_Name))
            {
                wheres.Add(new LikeCondition() { FieldName = "t.Payment_Type_Name", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.Payment_Type_Name });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Payment_Type_Code))
            {
                wheres.Add(new LikeCondition() { FieldName = "t.Payment_Type_Code", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.Payment_Type_Code });
            }

            PagedQueryResult<TPaymentTypeEntity> prlist = service.GetPaymentTypePage(wheres.ToArray(), startRowIndex, PageSize);
            return string.Format("{{\"totalCount\":{0},\"url\":\"{1}\",\"topics\":{2}}}", prlist.RowCount, ConfigurationManager.AppSettings["payMentUrl"], prlist.Entities.ToJSON());
        }

        #endregion

        #region GetMapingbyPayMentTypeId
        public string GetMapingbyPayMentTypeId(HttpRequest requst)
        {
            string paymentTypeId = requst.Form["paymentTypeId"];
            if (string.IsNullOrWhiteSpace(paymentTypeId))
            {
                paymentTypeId = requst.QueryString["paymentTypeId"].ToString();
            }


            TPaymentTypeCustomerMappingBLL server = new TPaymentTypeCustomerMappingBLL(this.CurrentUserInfo);
            TPaymentTypeCustomerMappingEntity entity = new TPaymentTypeCustomerMappingEntity();
            entity.IsDelete = 0;
            entity.PaymentTypeID = paymentTypeId;
            entity.CustomerId = this.CurrentUserInfo.ClientID;
            TPaymentTypeCustomerMappingEntity[] entitylist = server.QueryByEntity(entity, null);
            if (entitylist != null && entitylist.Length > 0)
            {
                entitylist[0].url = ConfigurationManager.AppSettings["payMentUrl"];
                return entitylist[0].ToJSON();
            }
            else
            {
                entity.url = ConfigurationManager.AppSettings["payMentUrl"];
                return entity.ToJSON();

            }
            return "";
        }
        #endregion
        #region DisablePayment
        /// <summary>
        /// 停用支付通道
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string DisablePayment(NameValueCollection rParams)
        {
            try
            {
                string paymentTypeId = rParams["paymentTypeId"].ToString();
                TPaymentTypeBLL server = new TPaymentTypeBLL(this.CurrentUserInfo);
                server.DisablePayment(paymentTypeId);
                return string.Format("{{\"ResultCode\":\"0\"}}");
            }
            catch (Exception)
            {

                return string.Format("{{\"ResultCode\":\"100\"}}");
            }


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
    #region 接受参数
    public class ReqPayMentTypeEntity
    {
        public string Payment_Type_Name { set; get; }
        public string Payment_Type_Code { set; get; }

    }
    #endregion


}
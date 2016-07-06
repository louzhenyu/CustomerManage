using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.PA;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Request;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Response;
using JIT.CPOS.DTO.Module.PA.Request;
using JIT.CPOS.DTO.Module.PA.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.PA.Order
{
    //public class SaveOrderAH : BaseActionHandler<SaveOrderInfoRP, SaveOrderRD>
    //{
    //    protected override SaveOrderRD ProcessRequest(DTO.Base.APIRequest<SaveOrderInfoRP> pRequest)
    //    {
    //        var rd = new SaveOrderRD();//返回值 
    //        var param = pRequest.Parameters;
    //        try
    //        {
    //            param.agentNo = "12";
    //            param.creditFlag = "Y";
    //            param.currency = "CNY";
    //            param.detailUrl = "12";
    //            param.merOrderNo = "12";
    //            param.merchantCode = "12";
    //            param.merchantId = "12";
    //            param.notityUrl = "12";
    //            param.openId = "12";
    //            param.orderAmount = "12";
    //            param.orderCategory = "12";
    //            param.agentNo = "12";
    //            param.creditFlag = "12";
    //            param.tradeType = "12";
    //            param.orderDetail = "12";
    //            param.orderPrepayExpireTime = "12";
    //            param.orderPrepayTime = "12";
    //            param.orderSubject = "12";
    //            param.orderStatus = "12";

    //            string sign = PAAppBLL.GetSecuritySign(param);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw;
    //        }

    //        return rd;
    //    }
    //}
}
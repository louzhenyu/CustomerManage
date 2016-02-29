using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.ApiServiceLog
{
    public class AddVipFeedbackInfoAH : BaseActionHandler<AddVipFeedbackInfoRP, EmptyResponseData>
    {
        /// <summary>
        /// 录入客户反馈信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<AddVipFeedbackInfoRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var VipFeedbackInfoBLL = new VipFeedbackInfoBLL(CurrentUserInfo);
            var AddData = new VipFeedbackInfoEntity()
            {
                Context = para.Context,
                ImageUrl = "",
                CustomerID = CurrentUserInfo.ClientID
            };
            for (int i = 0; i < para.ImageUrlArray.Length; i++)
            {
                AddData.ImageUrl += para.ImageUrlArray[i] + ",";
            }
            AddData.ImageUrl.TrimEnd(',');
            VipFeedbackInfoBLL.Create(AddData);
            return rd;
        }
    }
}
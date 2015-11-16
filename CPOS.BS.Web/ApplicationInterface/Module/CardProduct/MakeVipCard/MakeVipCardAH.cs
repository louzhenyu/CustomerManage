using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CardProduct.MakeVipCard
{
    /// <summary>
    /// 制卡接口
    /// </summary>
    public class MakeVipCardAH : BaseActionHandler<MakeVipCardRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<MakeVipCardRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBatchBLL = new VipCardBatchBLL(loggingSessionInfo);

            if (para != null)
            {
                VipCardBatchEntity Data = new VipCardBatchEntity()
                {
                    CardMedium = para.CardMedium,
                    RegionNumber = para.RegionNumber,
                    VipCardTypeCode = para.VipCardTypeCode,
                    CardPrefix = para.CardPrefix,
                    ImageUrl = para.ImageUrl,
                    Qty = para.Qty,
                    CostPrice = para.CostPrice,
                    BatchNo =para.BatchNo
                };
                //调用制卡方法
                VipCardBatchBLL.BatchMakeVipCard(Data);
            }



            return rd;
        }
    }
}
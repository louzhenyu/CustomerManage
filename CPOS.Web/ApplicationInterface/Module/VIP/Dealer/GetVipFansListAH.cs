using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Dealer.Request;
using JIT.CPOS.DTO.Module.VIP.Dealer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Dealer
{
    /// <summary>
    /// 获取当前会员粉丝列表
    /// </summary>
    public class GetVipFansListAH : BaseActionHandler<GetVipFansListRP, GetVipFansListRD>
    {
        protected override GetVipFansListRD ProcessRequest(DTO.Base.APIRequest<GetVipFansListRP> pRequest)
        {
            var rd = new GetVipFansListRD();
            var VipBLL = new VipBLL(CurrentUserInfo);
            var RetailTraderBLL = new RetailTraderBLL(CurrentUserInfo);

            //var RetailTrader = RetailTraderBLL.GetByID(pRequest.UserID);
            //string vipid = RetailTrader == null ? "" : RetailTrader.MultiLevelSalerFromVipId;

            int StarePage = pRequest.Parameters.PageIndex * pRequest.Parameters.PageSize;
            int EndPage = (pRequest.Parameters.PageIndex + 1) * pRequest.Parameters.PageSize;

            rd.VipFansList = VipBLL.GetVipFansList(pRequest.UserID, pRequest.Parameters.Code, pRequest.Parameters.VipName, StarePage, EndPage);
            rd.TotalCount = VipBLL.GetVipFansListCount(pRequest.UserID, pRequest.Parameters.Code, pRequest.Parameters.VipName);
            int PageSum = 0;
            //分页
            if (rd.TotalCount > 0)
            {
                PageSum = rd.TotalCount / pRequest.Parameters.PageSize;
                if (rd.TotalCount % pRequest.Parameters.PageSize != 0)
                    PageSum++;
            }
            rd.TotalPageCount = PageSum;
            return rd;
        }
    }
}
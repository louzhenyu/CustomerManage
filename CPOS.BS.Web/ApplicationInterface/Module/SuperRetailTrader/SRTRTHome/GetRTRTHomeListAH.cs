using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.SRTRTHome
{
    /// <summary>
    /// 获取分销商数据分析数据 {3个饼状图}
    /// </summary>
    public class GetRTRTHomeListAH : BaseActionHandler<GetRTRTHomeListRP, GetRTRTHomeListRD>
    {
        protected override GetRTRTHomeListRD ProcessRequest(DTO.Base.APIRequest<GetRTRTHomeListRP> pRequest)
        {
            var parameter = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var rd = new GetRTRTHomeListRD();
            R_SRT_RTHomeBLL bll = new R_SRT_RTHomeBLL(loggingSessionInfo);

            //按照时间获取最新一条有效记录
            List<OrderBy> orderCondition = new List<OrderBy>() { new OrderBy() { FieldName = "DateCode", Direction = OrderByDirections.Desc } };
            var model = bll.PagedQueryByEntity(new R_SRT_RTHomeEntity() { CustomerId = loggingSessionInfo.ClientID }, orderCondition.ToArray(), 1, 1);
            if (model.Entities != null && model.Entities.FirstOrDefault() != null)
            {
                rd.Day30ActiveRTCount = model.Entities.FirstOrDefault().Day30ActiveRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30ActiveRTCount;
                rd.Day30ExpandRTCount = model.Entities.FirstOrDefault().Day30ExpandRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30ExpandRTCount;
                rd.Day30JoinNoSalesRTCount = model.Entities.FirstOrDefault().Day30JoinNoSalesRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30JoinNoSalesRTCount;
                rd.Day30JoinSalesRTCount = model.Entities.FirstOrDefault().Day30JoinSalesRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30JoinSalesRTCount;
                rd.Day30NoActiveRTCount = model.Entities.FirstOrDefault().Day30NoActiveRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30NoActiveRTCount;
                rd.Day30SalesExpandRTCount = model.Entities.FirstOrDefault().Day30SalesExpandRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30SalesExpandRTCount;
                rd.Day30SalesNoExpandRTCount = model.Entities.FirstOrDefault().Day30SalesNoExpandRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30SalesNoExpandRTCount;
                rd.Day30SalesRTCount = model.Entities.FirstOrDefault().Day30SalesRTCount == null ? 0 : model.Entities.FirstOrDefault().Day30SalesRTCount;
            }
            return rd;
        }
    }
}
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.HomePageStats.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.HomePageStats
{
    public class GetHomePageStatsAH : BaseActionHandler<EmptyRequestParameter, GetHomePageStatsRD>
    {
        protected override GetHomePageStatsRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetHomePageStatsRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var homePageStatsBLL = new R_HomePageStatsBLL(loggingSessionInfo);
            var unitMonthlyAchievementRankingBLL = new R_UnitMonthlyAchievementRankingBLL(loggingSessionInfo);
            UnitService unitServer = new UnitService(CurrentUserInfo);

            //获取总部门店标识
            string unitId = unitServer.GetUnitByUnitTypeForWX("总部", null).Id;

            //统计数据
            var homePageStatsInfo = homePageStatsBLL.QueryByEntity(new R_HomePageStatsEntity() { CustomerId = loggingSessionInfo.ClientID, CurrentDay = DateTime.Now.Date }, null).FirstOrDefault();
            if (homePageStatsInfo != null)
            {
                rd.UnitCount = homePageStatsInfo.UnitCount;
                rd.UnitCurrentDayOrderAmount = homePageStatsInfo.UnitCurrentDayOrderAmount;
                rd.UnitCurrentDayOrderAmountDToD = homePageStatsInfo.UnitCurrentDayAvgOrderAmountDToD;
                rd.UnitMangerCount = homePageStatsInfo.UnitMangerCount;
                rd.UnitCurrentDayAvgOrderAmount = homePageStatsInfo.UnitCurrentDayAvgOrderAmount;
                rd.UnitCurrentDayAvgOrderAmountDToD = homePageStatsInfo.UnitCurrentDayAvgOrderAmountDToD;
                rd.UnitUserCount = homePageStatsInfo.UnitUserCount;
                rd.UserCurrentDayAvgOrderAmount = homePageStatsInfo.UserCurrentDayAvgOrderAmount;
                rd.UserCurrentDayAvgOrderAmountDToD = homePageStatsInfo.UserCurrentDayAvgOrderAmountDToD;
                rd.RetailTraderCount = homePageStatsInfo.RetailTraderCount;
                rd.CurrentDayRetailTraderOrderAmount = homePageStatsInfo.CurrentDayRetailTraderOrderAmount;
                rd.CurrentDayRetailTraderOrderAmountDToD = homePageStatsInfo.CurrentDayRetailTraderOrderAmountDToD;
                rd.VipCount = homePageStatsInfo.VipCount;
                rd.NewVipCount = homePageStatsInfo.NewVipCount;
                rd.NewVipDToD = homePageStatsInfo.NewVipDToD;
                rd.EventsCount = homePageStatsInfo.EventsCount;
                rd.EventJoinCount = homePageStatsInfo.EventJoinCount;
                rd.CurrentMonthSingleUnitAvgTranCount = homePageStatsInfo.CurrentMonthSingleUnitAvgTranCount;
                rd.VipContributePect = homePageStatsInfo.VipContributePect;
                rd.MonthArchivePect = homePageStatsInfo.MonthArchivePect;
                rd.CurrentMonthUnitAvgCustPrice = homePageStatsInfo.CurrentMonthUnitAvgCustPrice;
                rd.CurrentMonthSingleUnitAvgTranAmount = homePageStatsInfo.CurrentMonthSingleUnitAvgTranAmount;
                rd.CurrentMonthTranAmount = homePageStatsInfo.CurrentMonthTranAmount;
                rd.TranAmount = homePageStatsInfo.TranAmount;
                rd.VipTranAmount = homePageStatsInfo.VipTranAmount;
                rd.PreAuditOrder = homePageStatsInfo.PreAuditOrder;
                rd.PreSendOrder = homePageStatsInfo.PreSendOrder;
                rd.PreTakeOrder = homePageStatsInfo.PreTakeOrder;
                rd.PreRefund = homePageStatsInfo.PreRefund;
                rd.PreReturnCash = homePageStatsInfo.PreReturnCash;
            }

            //业绩排名top5和lower5
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            complexCondition.Add(new EqualsCondition() { FieldName = "DateCode", Value =  DateTime.Now.Date.ToString("yyyyMM") });

            //Top5 排序参数
            List<OrderBy> topOrder = new List<OrderBy> { };
            topOrder.Add(new OrderBy() { FieldName = "OrderPeopleTranAmount", Direction = OrderByDirections.Desc });
            topOrder.Add(new OrderBy() { FieldName = "UnitName", Direction = OrderByDirections.Asc });

            //Lower5 排序参数
            List<OrderBy> lowerOrder = new List<OrderBy> { };
            lowerOrder.Add(new OrderBy() { FieldName = "OrderPeopleTranAmount", Direction = OrderByDirections.Asc });
            lowerOrder.Add(new OrderBy() { FieldName = "UnitName", Direction = OrderByDirections.Asc });

            var performanceTop = unitMonthlyAchievementRankingBLL.PagedQuery(complexCondition.ToArray(), topOrder.ToArray(), 5, 1);
            var performanceLower = unitMonthlyAchievementRankingBLL.PagedQuery(complexCondition.ToArray(), lowerOrder.ToArray(), 5, 1);

            rd.PerformanceTop = performanceTop.Entities.Select(t => new PerformanceInfo()
            {
                UnitName = t.UnitName,
                OrderPeopleTranAmount = t.OrderPeopleTranAmount
            }).ToList();

            rd.PerformanceLower = performanceLower.Entities.Select(t => new PerformanceInfo()
            {
                UnitName = t.UnitName,
                OrderPeopleTranAmount = t.OrderPeopleTranAmount
            }).ToList();
            return rd;
        }
    }
}
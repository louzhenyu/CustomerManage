using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTAboutReport
{
    /// <summary>
    /// 超级分销_概览信息
    /// </summary>
    public class GetOverViewAH : BaseActionHandler<EmptyRequestParameter, GetOverViewRD>
    {
        protected override GetOverViewRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        
        {
            var rd = new GetOverViewRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //超级分销体系配置表 为判断是否存在分销体系使用
            var T_SuperRetailTraderConfigbll = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
            var R_SRT_Homebll = new R_SRT_HomeBLL(loggingSessionInfo);
            //获取分销体系信息
            var T_SuperRetailTraderConfigInfo = T_SuperRetailTraderConfigbll.QueryByEntity(new T_SuperRetailTraderConfigEntity() {IsDelete=0,CustomerId=loggingSessionInfo.CurrentUser.customer_id },null).FirstOrDefault();
            if (T_SuperRetailTraderConfigInfo != null)
            {
                rd.IsDataNull = 1;
            }
            else
            {
                rd.IsDataNull = 0; return rd;
            }
            //获取七天最新店员、会员相关数据
            var SevenDaysInfo = R_SRT_Homebll.GetSevenDaySalesAndPersonCount();
            if (SevenDaysInfo != null && SevenDaysInfo.Tables[0].Rows.Count > 0)
            {
                rd.DaySevenRTSalesList = DataTableToObject.ConvertToList<Day7RTSalesInfo>(SevenDaysInfo.Tables[0]);
                rd.DaySevenRTCountList = DataTableToObject.ConvertToList<Day7RTCountInfo>(SevenDaysInfo.Tables[0]);
            }
            //按统计日获取最新一条概览信息
            var R_SRT_HomeInfo = R_SRT_Homebll.QueryByEntity(new R_SRT_HomeEntity() { CustomerId = loggingSessionInfo.CurrentUser.customer_id }, new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
            if (R_SRT_HomeInfo != null)
            {
                //总销量、近七天新增销售额
                var RTTotalSalesInfo = new RTSalesInfo();
                RTTotalSalesInfo.RTTotalSalesAmount = R_SRT_HomeInfo.RTTotalSalesAmount;
                RTTotalSalesInfo.RTDay7AddSalesAmount = R_SRT_HomeInfo.RTDay7SalesAmount;
                rd.RTTotalSales = RTTotalSalesInfo;
                //分销商总数、近七天新增分销商总数
                var RTTotalCountInfo = new RTCountInfo();
                RTTotalCountInfo.RTTotalCount = R_SRT_HomeInfo.RTTotalCount;
                RTTotalCountInfo.Day7AddRTCount = R_SRT_HomeInfo.Day7AddRTCount;
                rd.RTTotalCount = RTTotalCountInfo;
                //近7日分销商订单信息
                var DaySevenRTOrderInfo = new DaysRTOrderInfo();
                DaySevenRTOrderInfo.DaysRTOrderCount = R_SRT_HomeInfo.Day7RTOrderCount;
                DaySevenRTOrderInfo.DaysRTOrderCountRate = R_SRT_HomeInfo.Day7RTOrderCountW2W;
                DaySevenRTOrderInfo.DaysTRTAvgAmount = R_SRT_HomeInfo.Day7RTAC;
                DaySevenRTOrderInfo.DaysTRTAvgAmountRate = R_SRT_HomeInfo.Day7RTACW2W;
                rd.DaySevenRTOrder = DaySevenRTOrderInfo;
                //近30日分销商订单信息
                var DayThirtyRTOrderInfo = new DaysRTOrderInfo();
                DayThirtyRTOrderInfo.DaysRTOrderCount = R_SRT_HomeInfo.Day30RTOrderCount;
                DayThirtyRTOrderInfo.DaysRTOrderCountRate = R_SRT_HomeInfo.Day30RTOrderCountM2M;
                DayThirtyRTOrderInfo.DaysTRTAvgAmount = R_SRT_HomeInfo.Day30RTAC;
                DayThirtyRTOrderInfo.DaysTRTAvgAmountRate = R_SRT_HomeInfo.Day30RTACM2M;
                rd.DayThirtyRTOrder = DayThirtyRTOrderInfo;
                //近7日活跃分销商
                var DaySevenActivityRTInfo = new DaysActivityRTInfo();
                DaySevenActivityRTInfo.DaysActiveRTCount = R_SRT_HomeInfo.Day7ActiveRTCount;
                DaySevenActivityRTInfo.DaysRTOrderCountRate = R_SRT_HomeInfo.Day7ActiveRTCountW2W;
                rd.DaySevenActivityRT = DaySevenActivityRTInfo;
                //近30日活跃分销商信息
                var DayThirtyActivityRTInfo = new DaysActivityRTInfo();
                DayThirtyActivityRTInfo.DaysActiveRTCount = R_SRT_HomeInfo.Day30ActiveRTCount;
                DayThirtyActivityRTInfo.DaysRTOrderCountRate = R_SRT_HomeInfo.Day30ActiveRTCountM2M;
                rd.DayThirtyActivityRT = DayThirtyActivityRTInfo;
                //近7日分享数据信息
                var DaySevenRTShareInfo = new DaysRTShareInfo();
                DaySevenRTShareInfo.DaysRTShareCount = R_SRT_HomeInfo.Day7RTShareCount;
                DaySevenRTShareInfo.DaysRTShareCountRate = R_SRT_HomeInfo.Day7RTShareCountW2W;
                DaySevenRTShareInfo.DaysAddRTCount = R_SRT_HomeInfo.Day7AddRTCount;
                DaySevenRTShareInfo.DaysAddRTCountRate = R_SRT_HomeInfo.Day7AddRTCountW2W;
                rd.DaySevenRTShare = DaySevenRTShareInfo;
                //近30日分享数据信息
                var DayThirtyRTShareInfo = new DaysRTShareInfo();
                DayThirtyRTShareInfo.DaysRTShareCount = R_SRT_HomeInfo.Day30RTShareCount;
                DayThirtyRTShareInfo.DaysRTShareCountRate = R_SRT_HomeInfo.Day30RTShareCountM2M;
                DayThirtyRTShareInfo.DaysAddRTCount = R_SRT_HomeInfo.Day30AddRTCount;
                DayThirtyRTShareInfo.DaysAddRTCountRate = R_SRT_HomeInfo.Day30AddRTCountM2M;
                rd.DayThirtyRTShare = DayThirtyRTShareInfo;
                rd.IsRTSalesOrder = 1;
            }
            else
            {
                rd.IsRTSalesOrder = 0;
            }
            return rd;
        }
    }
}
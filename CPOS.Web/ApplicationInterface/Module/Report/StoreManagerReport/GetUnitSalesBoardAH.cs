using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.Report;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Request;
using JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Report.StoreManagerReport
{
    public class GetUnitSalesBoardAH : BaseActionHandler<UnitSalesBoardRP, UnitSalesBoardRD>
    {
        protected override UnitSalesBoardRD ProcessRequest(DTO.Base.APIRequest<UnitSalesBoardRP> pRequest)
        {
            var rd = new UnitSalesBoardRD();
            var rp = pRequest.Parameters;
            var agg_UnitDailyBLL = new Agg_UnitDailyBLL(CurrentUserInfo);
            var daySalesInfo = default(Agg_UnitDailyEntity);
            var lst7 = default(Agg_UnitDailyEntity[]);

            #region 查询数据
            var tasks = new List<Task>();
            //查询日结报表
            tasks.Add(Task.Factory.StartNew(() =>
            {
                daySalesInfo = agg_UnitDailyBLL.GetEntity(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date));
            }));
            //查询七天销售额
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lst7 = agg_UnitDailyBLL.GetEntities(rp.CustomerID, rp.UnitID, Convert.ToDateTime(rp.Date).AddDays(-6), Convert.ToDateTime(rp.Date));
            }));
            //查询数据库
            Task.WaitAll(tasks.ToArray());
            #endregion 

            #region 日结报表
            if (daySalesInfo != null)
            {
                rd.UnitCurrentDaySalesAmount = daySalesInfo.SalesAmount;//销售额
                rd.UnitCurrentMonthSalesAmountPlan = daySalesInfo.SalesAmountPlan;//当月预定总销售额
                rd.UnitCurrentMonthSalesAchievementRate = daySalesInfo.AchievementRate;//当月业绩达成率
                rd.UnitCurrentMonthCompleteSalesAmount = daySalesInfo.CompleteSalesAmount; //本月已完成销售额
                rd.UnitCurrentMonthNoCompleteSalesAmount = daySalesInfo.NoCompleteSalesAmount;//本月未完成销售额
                rd.UnitCurrentMonthDaysRemaining = daySalesInfo.DaysRemaining;//本月仅剩天数
                rd.UnitCurrentMonthDayTargetSalesAmount = daySalesInfo.DayTargetSalesAmount; //剩余每天目标销售额
            }
            #endregion

            #region 七天销售额
            var unit7DaysSalesAmountList = lst7.Select(t => new UnitDaySalesAmount()
            {
                Date = t.DateCode.Value.ToString("yyyy-MM-dd"),//日期
                SalesAmount = (decimal)t.SalesAmount,//销售额
            }).ToList();

            ReportCommonBLL.Instance.FillReportDatasByTime(Convert.ToDateTime(rp.Date).AddDays(-6), lst7, ref unit7DaysSalesAmountList, (s, d) =>
            {
                return new UnitDaySalesAmount
                {
                    Date = s.DateCode.ToString(),
                    SalesAmount = s.SalesAmount
                };
            });

            rd.Unit7DaysSalesAmountList = unit7DaysSalesAmountList;
            #endregion

            return rd;
        }
    }
}
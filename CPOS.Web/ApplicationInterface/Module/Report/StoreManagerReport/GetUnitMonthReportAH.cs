using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.Report;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
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
    /// <summary>
    /// 月报表
    /// </summary>
    public class GetUnitMonthReportAH : BaseActionHandler<UnitMonthReportRP, UnitMonthReportRD>
    {
        protected override UnitMonthReportRD ProcessRequest(APIRequest<UnitMonthReportRP> pRequest)
        {
            //
            var rd = new UnitMonthReportRD();
            var rp = pRequest.Parameters;

            //
            var aggUnitMonthlyBLL = new Agg_UnitMonthlyBLL(CurrentUserInfo);
            var rUnitProductMonthSalesTopBLL = new R_UnitProductMonthSalesTopBLL(CurrentUserInfo);
            var aggUnitMonthlyEmplBLL = new Agg_UnitMonthly_EmplBLL(CurrentUserInfo);
            var aggUnitNowEmplBLL = new Agg_UnitNow_EmplBLL(CurrentUserInfo);

            //
            var aggUnitMonthlyEntity = default(Agg_UnitMonthlyEntity);   // 本月
            var lastMonthAggUnitMonthlyEntity = default(Agg_UnitMonthlyEntity);  //  上月
            var lastYearAggUnitMonthlyEntity = default(Agg_UnitMonthlyEntity);  //  去年同月
            var rUnitProductMonthSalesTopEntities = default(R_UnitProductMonthSalesTopEntity[]);  //  销量榜
            var aggUnitMonthlyEmplEntities = default(Agg_UnitMonthly_EmplEntity[]);   // 业绩榜
            var setoffAggUnitMonthlyEmplEntities = default(Agg_UnitMonthly_EmplEntity[]);  // 集客榜  
            var aggUnitNowEmplEntities = default(Agg_UnitNow_EmplEntity[]);   //  员工总数据

            //
            var tasks = new List<Task>();
            var date = Convert.ToDateTime(rp.Date);
            tasks.Add(Task.Factory.StartNew(() =>
            {
                aggUnitMonthlyEntity = aggUnitMonthlyBLL.QueryByEntity(new Agg_UnitMonthlyEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(date.Year + "-" + date.Month + "-01")
                }, null).FirstOrDefault();
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                var dateNew = date.AddMonths(-1);
                lastMonthAggUnitMonthlyEntity = aggUnitMonthlyBLL.QueryByEntity(new Agg_UnitMonthlyEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(dateNew.Year + "-" + dateNew.Month + "-01")
                }, null).FirstOrDefault();
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                var dateNew = date.AddYears(-1);
                lastYearAggUnitMonthlyEntity = aggUnitMonthlyBLL.QueryByEntity(new Agg_UnitMonthlyEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(dateNew.Year + "-" + dateNew.Month + "-01")
                }, null).FirstOrDefault();
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                rUnitProductMonthSalesTopEntities = rUnitProductMonthSalesTopBLL.PagedQueryByEntity(new R_UnitProductMonthSalesTopEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(date.Year + "-" + date.Month + "-01")
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="SalesQty",
                        Direction= OrderByDirections.Desc
                    }
                }, 10, 1).Entities;
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                aggUnitMonthlyEmplEntities = aggUnitMonthlyEmplBLL.PagedQueryByEntity(new Agg_UnitMonthly_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(date.Year + "-" + date.Month + "-01")
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="saleSamount",
                        Direction= OrderByDirections.Desc
                    },
                    new OrderBy
                    {
                        FieldName="EmplName",
                        Direction=OrderByDirections.Asc
                    }
                }, 10, 1).Entities;
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                setoffAggUnitMonthlyEmplEntities = aggUnitMonthlyEmplBLL.PagedQueryByEntity(new Agg_UnitMonthly_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(date.Year + "-" + date.Month + "-01")
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="setoffCount",
                        Direction= OrderByDirections.Desc
                    },
                    new OrderBy
                    {
                        FieldName="EmplName",
                        Direction=OrderByDirections.Asc
                    }

                }, 10, 1).Entities;
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                aggUnitNowEmplEntities = aggUnitNowEmplBLL.QueryByEntity(new Agg_UnitNow_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(rp.Date)
                }, null);
            }));
            Task.WaitAll(tasks.ToArray());

            // 本月
            if (aggUnitMonthlyEntity != null)
            {
                rd.UnitCurrentMonthSalesAmount = aggUnitMonthlyEntity.SalesAmount;
                rd.UnitCurrentMonthNewVipCount = aggUnitMonthlyEntity.NewVipCount;
                rd.UnitCurrentMonthOldVipBackCount = aggUnitMonthlyEntity.OldVipBackCount;
                rd.UnitCurrentMonthUseCouponCount = aggUnitMonthlyEntity.UseCouponCount;
            }

            // 上月
            if (lastMonthAggUnitMonthlyEntity != null)
            {
                rd.UnitLastMonthSalesAmount = lastMonthAggUnitMonthlyEntity.SalesAmount;
                rd.UnitLastMonthNewVipCount = lastMonthAggUnitMonthlyEntity.NewVipCount;
                rd.UnitLastMonthOldVipBackCount = lastMonthAggUnitMonthlyEntity.OldVipBackCount;
                rd.UnitLastMonthUseCouponCount = lastMonthAggUnitMonthlyEntity.UseCouponCount;
            }

            // 环比
            rd.UnitCurrentMonthSalesAmountMoM = ReportCommonBLL.Instance.CalcuDoD(lastMonthAggUnitMonthlyEntity == null ? null : lastMonthAggUnitMonthlyEntity.SalesAmount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.SalesAmount);
            rd.UnitCurrentMonthNewVipCountMoM = ReportCommonBLL.Instance.CalcuDoD(lastMonthAggUnitMonthlyEntity == null ? null : lastMonthAggUnitMonthlyEntity.NewVipCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.NewVipCount);
            rd.UnitCurrentMonthOldVipBackCountMoM = ReportCommonBLL.Instance.CalcuDoD(lastMonthAggUnitMonthlyEntity == null ? null : lastMonthAggUnitMonthlyEntity.OldVipBackCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.OldVipBackCount);
            rd.UnitCurrentMonthUseCouponCountMoM = ReportCommonBLL.Instance.CalcuDoD(lastMonthAggUnitMonthlyEntity == null ? null : lastMonthAggUnitMonthlyEntity.UseCouponCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.UseCouponCount);

            // 同比
            rd.UnitCurrentMonthSalesAmountYoY = ReportCommonBLL.Instance.CalcuDoD(lastYearAggUnitMonthlyEntity == null ? null : lastYearAggUnitMonthlyEntity.SalesAmount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.SalesAmount);
            rd.UnitCurrentMonthNewVipCountYoY = ReportCommonBLL.Instance.CalcuDoD(lastYearAggUnitMonthlyEntity == null ? null : lastYearAggUnitMonthlyEntity.NewVipCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.NewVipCount);
            rd.UnitCurrentMonthOldVipBackCountYoY = ReportCommonBLL.Instance.CalcuDoD(lastYearAggUnitMonthlyEntity == null ? null : lastYearAggUnitMonthlyEntity.OldVipBackCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.OldVipBackCount);
            rd.UnitCurrentMonthUseCouponCountYoY = ReportCommonBLL.Instance.CalcuDoD(lastYearAggUnitMonthlyEntity == null ? null : lastYearAggUnitMonthlyEntity.UseCouponCount, aggUnitMonthlyEntity == null ? null : aggUnitMonthlyEntity.UseCouponCount);

            // 销量榜
            rd.UnitCurrentMonthProductSalesTopList = new List<UnitProductSalesTop>();
            if (rUnitProductMonthSalesTopEntities != null && rUnitProductMonthSalesTopEntities.Length > 0)
            {
                var list = rUnitProductMonthSalesTopEntities.ToList();
                foreach (var item in list)
                {
                    rd.UnitCurrentMonthProductSalesTopList.Add(new UnitProductSalesTop
                    {
                        TopIndex = item.TopIndex,
                        ProductName = item.item_name,
                        ProductSKU = item.SkuName,
                        SalesAmount = item.SalesQty
                    });
                }
            }

            // 业绩榜
            rd.UnitCurrentMonthSalesAmountEmplTopList = new List<UnitSalesAmountEmplTop>();
            if (aggUnitMonthlyEmplEntities != null && aggUnitMonthlyEmplEntities.Length > 0)
            {
                var list = aggUnitMonthlyEmplEntities.ToList();
                var i = 1;
                foreach (var item in list)
                {
                    rd.UnitCurrentMonthSalesAmountEmplTopList.Add(new UnitSalesAmountEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        SalesAmount = item.SalesAmount
                    });
                    i++;
                }
            }

            // 集客榜
            rd.UnitCurrentMonthSetoffEmplTopList = new List<UnitMonthSetoffEmplTop>();
            if (setoffAggUnitMonthlyEmplEntities != null && setoffAggUnitMonthlyEmplEntities.Length > 0)
            {
                var list = setoffAggUnitMonthlyEmplEntities.ToList();
                var i = 1;
                foreach (var item in list)
                {
                    var empSum = aggUnitNowEmplEntities.FirstOrDefault(it => item.EmplID == item.EmplID);
                    rd.UnitCurrentMonthSetoffEmplTopList.Add(new UnitMonthSetoffEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        MonthSetoffCount = item.SetoffCount,
                        AllSetoffCount = empSum != null ? empSum.SetoffCount : 0
                    });
                    i++;
                }
            }

            //
            return rd;
        }
    }
}
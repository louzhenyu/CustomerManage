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
    /// 周报表
    /// </summary>
    public class GetUnitWeekReportAH : BaseActionHandler<UnitWeekReportRP, UnitWeekReportRD>
    {
        protected override UnitWeekReportRD ProcessRequest(APIRequest<UnitWeekReportRP> pRequest)
        {
            //
            var rd = new UnitWeekReportRD();
            var rp = pRequest.Parameters;

            //
            var aggUnitDailyBLL = new Agg_UnitDailyBLL(CurrentUserInfo);
            var aggUnitWeeklyEmplBLL = new Agg_UnitWeekly_EmplBLL(CurrentUserInfo);
            var aggUnitWeeklyBLL = new Agg_UnitWeeklyBLL(CurrentUserInfo);
            var rUnitProductWeekSalesTopBLL = new R_UnitProductWeekSalesTopBLL(CurrentUserInfo);
            var aggUnitMonthlyEmplBLL = new Agg_UnitMonthly_EmplBLL(CurrentUserInfo);

            //
            var aggUnitWeeklyEntity = default(Agg_UnitWeeklyEntity);  // 本周
            var lastAggUnitWeeklyEntity = default(Agg_UnitWeeklyEntity);  // 上周
            var aggUnitDailyEntities = default(Agg_UnitDailyEntity[]);  // 七天门店
            var lastAggUnitDailyEntities = default(Agg_UnitDailyEntity[]);  // 上七天门店
            var rUnitProductWeekSalesTopEntities = default(R_UnitProductWeekSalesTopEntity[]);  // 商品销量榜
            var salesAggUnitWeeklyEmplEntities = default(Agg_UnitWeekly_EmplEntity[]);  // 本周员工业绩
            var setoffAggUnitWeeklyEmplEntities = default(Agg_UnitWeekly_EmplEntity[]);  // 本周员工集客
            var aggUnitMonthlyEmplEntities = default(Agg_UnitMonthly_EmplEntity[]);   // 本月员工

            //
            var tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() =>
            {
                aggUnitWeeklyEntity = aggUnitWeeklyBLL.QueryByEntity(new Agg_UnitWeeklyEntity
                {
                    DateCode = Convert.ToDateTime(rp.Date),
                    UnitId = rp.UnitID,
                    CustomerId = rp.CustomerID
                }, null).FirstOrDefault();
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                lastAggUnitWeeklyEntity = aggUnitWeeklyBLL.QueryByEntity(new Agg_UnitWeeklyEntity
                {
                    DateCode = Convert.ToDateTime(rp.Date).AddDays(-7),
                    UnitId = rp.UnitID,
                    CustomerId = rp.CustomerID
                }, null).FirstOrDefault();
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                var wheres = new IWhereCondition[]
                {
                    new EqualsCondition
                    {
                        FieldName="CustomerId",
                        Value=rp.CustomerID
                    },
                    new EqualsCondition
                    {
                        FieldName="unitid",
                        Value=rp.UnitID
                    },
                    new DirectCondition("datecode>='" + Convert.ToDateTime(rp.Date).AddDays(-7).ToString("yyyy-MM-dd")+ "' "),
                    new DirectCondition("datecode<'" + Convert.ToDateTime(rp.Date).ToString("yyyy-MM-dd")+ "' ")
                };
                var orderbys = new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="datecode",
                        Direction= OrderByDirections.Asc
                    }
                };
                aggUnitDailyEntities = aggUnitDailyBLL.Query(wheres, orderbys);
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                var lastWheres = new IWhereCondition[]
                 {
                    new EqualsCondition
                    {
                        FieldName="CustomerId",
                        Value=rp.CustomerID
                    },
                    new EqualsCondition
                    {
                        FieldName="unitid",
                        Value=rp.UnitID
                    },
                    new DirectCondition("datecode>='" + Convert.ToDateTime(rp.Date).AddDays(-14).ToString("yyyy-MM-dd")+ "' "),
                    new DirectCondition("datecode<'" + Convert.ToDateTime(rp.Date).AddDays(-7).ToString("yyyy-MM-dd")+ "' ")
                 };
                var lastOrderBys = new OrderBy[]
                 {
                    new OrderBy
                    {
                        FieldName="datecode",
                        Direction= OrderByDirections.Asc
                    }
                 };
                lastAggUnitDailyEntities = aggUnitDailyBLL.Query(lastWheres, lastOrderBys);
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                rUnitProductWeekSalesTopEntities = rUnitProductWeekSalesTopBLL.PagedQueryByEntity(new R_UnitProductWeekSalesTopEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(rp.Date)
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                         FieldName="salesqty",
                         Direction= OrderByDirections.Desc
                    }
                }, 10, 1).Entities;
            }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                salesAggUnitWeeklyEmplEntities = aggUnitWeeklyEmplBLL.PagedQueryByEntity(new Agg_UnitWeekly_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(rp.Date)
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="SalesAmount",
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
                setoffAggUnitWeeklyEmplEntities = aggUnitWeeklyEmplBLL.PagedQueryByEntity(new Agg_UnitWeekly_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(rp.Date)
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="setoffcount",
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
                var date = Convert.ToDateTime(rp.Date);
                aggUnitMonthlyEmplEntities = aggUnitMonthlyEmplBLL.QueryByEntity(new Agg_UnitMonthly_EmplEntity
                {
                    CustomerId = rp.CustomerID,
                    UnitId = rp.UnitID,
                    DateCode = Convert.ToDateTime(date.Year.ToString() + "-" + date.Month.ToString() + "-01")
                }, null);
            }));
            Task.WaitAll(tasks.ToArray());

            // 本周
            if (aggUnitWeeklyEntity != null)
            {
                rd.UnitCurrentWeekSalesAmount = aggUnitWeeklyEntity.SalesAmount;
                rd.UnitCurrentWeekNewVipCount = aggUnitWeeklyEntity.NewVipCount;
                rd.UnitCurrentWeekOldVipBackCount = aggUnitWeeklyEntity.OldVipBackCount;
                rd.UnitCurrentWeekUseCouponCount = aggUnitWeeklyEntity.UseCouponCount;
            }

            // 上周
            if (lastAggUnitWeeklyEntity != null)
            {
                rd.UnitLastWeekSalesAmount = lastAggUnitWeeklyEntity.SalesAmount;
                rd.UnitLastWeekNewVipCount = lastAggUnitWeeklyEntity.NewVipCount;
                rd.UnitLastWeekOldVipBackCount = lastAggUnitWeeklyEntity.OldVipBackCount;
                rd.UnitLastWeekUseCouponCount = lastAggUnitWeeklyEntity.UseCouponCount;
            }

            // 增减
            rd.UnitCurrentWeekSalesAmountWoW = ReportCommonBLL.Instance.CalcuDoD(lastAggUnitWeeklyEntity == null ? null : lastAggUnitWeeklyEntity.SalesAmount, aggUnitWeeklyEntity == null ? null : aggUnitWeeklyEntity.SalesAmount);
            rd.UnitCurrentWeekNewVipCountWoW = ReportCommonBLL.Instance.CalcuDoD(lastAggUnitWeeklyEntity == null ? null : lastAggUnitWeeklyEntity.NewVipCount, aggUnitWeeklyEntity == null ? null : aggUnitWeeklyEntity.NewVipCount);
            rd.UnitCurrentWeekOldVipBackCountWoW = ReportCommonBLL.Instance.CalcuDoD(lastAggUnitWeeklyEntity == null ? null : lastAggUnitWeeklyEntity.OldVipBackCount, aggUnitWeeklyEntity == null ? null : aggUnitWeeklyEntity.OldVipBackCount);
            rd.UnitCurrentWeekUseCouponCountWoW = ReportCommonBLL.Instance.CalcuDoD(lastAggUnitWeeklyEntity == null ? null : lastAggUnitWeeklyEntity.UseCouponCount, aggUnitWeeklyEntity == null ? null : aggUnitWeeklyEntity.UseCouponCount);

            //             
            var startTime = Convert.ToDateTime(rp.Date).AddDays(-7);
            // 七天销售额
            var unitCurrentWeekSalesAmountList = new List<UnitDaySalesAmount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, aggUnitDailyEntities, ref unitCurrentWeekSalesAmountList, (s, d) =>
            {
                return new UnitDaySalesAmount
                {
                    Date = s.DateCode.ToString(),
                    SalesAmount = s.SalesAmount
                };
            });
            rd.UnitCurrentWeekSalesAmountList = unitCurrentWeekSalesAmountList;
            // 七天新增会员
            var unitCurrentWeekNewVipCountList = new List<UnitDayNewVipCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, aggUnitDailyEntities, ref unitCurrentWeekNewVipCountList, (s, d) =>
            {
                return new UnitDayNewVipCount
                {
                    Date = s.DateCode.ToString(),
                    NewVipCount = s.NewVipCount
                };
            });
            rd.UnitCurrentWeekNewVipCountList = unitCurrentWeekNewVipCountList;
            // 七天老会员回店数
            var unitCurrentWeekOldVipBackCountList = new List<UnitDayOldVipBackCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, aggUnitDailyEntities, ref unitCurrentWeekOldVipBackCountList, (s, d) =>
            {
                return new UnitDayOldVipBackCount
                {
                    Date = s.DateCode.ToString(),
                    OldVipBackCount = s.OldVipBackCount
                };
            });
            rd.UnitCurrentWeekOldVipBackCountList = unitCurrentWeekOldVipBackCountList;
            // 七天优惠券使用数
            var unitCurrentWeekUseCouponCountList = new List<UnitDayUseCouponCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, aggUnitDailyEntities, ref unitCurrentWeekUseCouponCountList, (s, d) =>
            {
                return new UnitDayUseCouponCount
                {
                    Date = s.DateCode.ToString(),
                    UseCouponCount = s.OldVipBackCount
                };
            });
            rd.UnitCurrentWeekUseCouponCountList = unitCurrentWeekUseCouponCountList;
            //      
            startTime = Convert.ToDateTime(rp.Date).AddDays(-14);
            // 上七天销售额
            var unitLastWeekSalesAmountList = new List<UnitDaySalesAmount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, lastAggUnitDailyEntities, ref unitLastWeekSalesAmountList, (s, d) =>
            {
                return new UnitDaySalesAmount
                {
                    Date = s.DateCode.ToString(),
                    SalesAmount = s.SalesAmount
                };
            });
            rd.UnitLastWeekSalesAmountList = unitLastWeekSalesAmountList;
            // 上七天新增会员
            var unitLastWeekNewVipCountList = new List<UnitDayNewVipCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, lastAggUnitDailyEntities, ref unitLastWeekNewVipCountList, (s, d) =>
            {
                return new UnitDayNewVipCount
                {
                    Date = s.DateCode.ToString(),
                    NewVipCount = s.NewVipCount
                };
            });
            rd.UnitLastWeekNewVipCountList = unitLastWeekNewVipCountList;
            // 上七天老会员回店数
            var unitLastWeekOldVipBackCountList = new List<UnitDayOldVipBackCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, lastAggUnitDailyEntities, ref unitLastWeekOldVipBackCountList, (s, d) =>
            {
                return new UnitDayOldVipBackCount
                {
                    Date = s.DateCode.ToString(),
                    OldVipBackCount = s.OldVipBackCount
                };
            });
            rd.UnitLastWeekOldVipBackCountList = unitLastWeekOldVipBackCountList;
            // 上七天优惠券使用数
            var unitLastWeekUseCouponCountList = new List<UnitDayUseCouponCount>();
            ReportCommonBLL.Instance.FillReportDatasByTime(startTime, lastAggUnitDailyEntities, ref unitLastWeekUseCouponCountList, (s, d) =>
            {
                return new UnitDayUseCouponCount
                {
                    Date = s.DateCode.ToString(),
                    UseCouponCount = s.OldVipBackCount
                };
            });
            rd.UnitLastWeekUseCouponCountList = unitLastWeekUseCouponCountList;


            // 商品销量榜
            rd.UnitCurrentWeekProductSalesTopList = new List<UnitProductSalesTop>();
            if (rUnitProductWeekSalesTopEntities != null && rUnitProductWeekSalesTopEntities.Length > 0)
            {
                var list = rUnitProductWeekSalesTopEntities.ToList();
                foreach (var item in list)
                {
                    rd.UnitCurrentWeekProductSalesTopList.Add(new UnitProductSalesTop
                    {
                        TopIndex = item.TopIndex,
                        ProductName = item.item_name,
                        ProductSKU = item.SkuName,
                        SalesAmount = item.SalesQty
                    });
                }
            }

            // 
            rd.UnitCurrentWeekSalesAmountEmplTopList = new List<UnitSalesAmountEmplTop>();  // 员工业绩榜
            if (salesAggUnitWeeklyEmplEntities != null && salesAggUnitWeeklyEmplEntities.Length > 0)
            {
                var list = salesAggUnitWeeklyEmplEntities.ToList();
                var i = 1;
                foreach (var item in salesAggUnitWeeklyEmplEntities)
                {
                    rd.UnitCurrentWeekSalesAmountEmplTopList.Add(new UnitSalesAmountEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        SalesAmount = item.SalesAmount
                    });
                    i++;
                }
            }

            // 
            rd.UnitCurrentWeekSetoffEmplTopList = new List<UnitWeekSetoffEmplTop>();  // 员工集客榜
            if (setoffAggUnitWeeklyEmplEntities != null && setoffAggUnitWeeklyEmplEntities.Length > 0)
            {
                var list = setoffAggUnitWeeklyEmplEntities.ToList();
                var i = 1;
                foreach (var item in list)
                {
                    var empMothData = aggUnitMonthlyEmplEntities.FirstOrDefault(it => it.EmplID == item.EmplID);
                    rd.UnitCurrentWeekSetoffEmplTopList.Add(new UnitWeekSetoffEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        WeekSetoffCount = item.SetoffCount,
                        MonthSetoffCount = empMothData != null ? empMothData.SetoffCount : 0
                    });
                    i++;
                }
            }

            //
            return rd;
        }
    }
}
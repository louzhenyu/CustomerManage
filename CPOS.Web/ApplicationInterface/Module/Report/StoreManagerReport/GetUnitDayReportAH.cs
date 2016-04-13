using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.Report;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
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
    /// 日报表
    /// </summary>
    public class GetUnitDayReportAH : BaseActionHandler<UnitDayReportRP, UnitDayReportRD>
    {
        protected override UnitDayReportRD ProcessRequest(APIRequest<UnitDayReportRP> pRequest)
        {
            //
            var rd = new UnitDayReportRD();
            var rp = pRequest.Parameters;

            //
            var aggUnitDailyEmplBLL = new Agg_UnitDaily_EmplBLL(CurrentUserInfo);
            var aggUnitDailyBLL = new Agg_UnitDailyBLL(CurrentUserInfo);
            var rUnitProductDaySalesTopBLL = new R_UnitProductDaySalesTopBLL(CurrentUserInfo);
            var t_UserBLL = new T_UserBLL(CurrentUserInfo);

            //
            var currentAggUnitDailyEntity = default(Agg_UnitDailyEntity);   // 当日
            var oldAggUnitDailyEntity = default(Agg_UnitDailyEntity);   // 昨日
            var rUnitProductDaySalesTopEntities = default(R_UnitProductDaySalesTopEntity[]);  //销量榜
            var aggUnitDailyEmplEntities = default(Agg_UnitDaily_EmplEntity[]);   // 业绩榜
            var aggUnitDailyEmplEntities2 = default(Agg_UnitDaily_EmplEntity[]);   // 集客榜
            var t_UserEntities = default(T_UserEntity[]);//门店员工

            //
            var tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(() =>
             {
                 currentAggUnitDailyEntity = aggUnitDailyBLL.QueryByEntity(new Agg_UnitDailyEntity
                 {
                     DateCode = Convert.ToDateTime(rp.Date),
                     UnitId = rp.UnitID,
                     CustomerId = rp.CustomerID
                 }, null).FirstOrDefault();
             }));
            tasks.Add(Task.Factory.StartNew(() =>
             {
                 oldAggUnitDailyEntity = aggUnitDailyBLL.QueryByEntity(new Agg_UnitDailyEntity
                 {
                     DateCode = Convert.ToDateTime(rp.Date).AddDays(-1),
                     UnitId = rp.UnitID,
                     CustomerId = rp.CustomerID
                 }, null).FirstOrDefault();
             }));
            tasks.Add(Task.Factory.StartNew(() =>
             {
                 rUnitProductDaySalesTopEntities = rUnitProductDaySalesTopBLL.PagedQueryByEntity(new R_UnitProductDaySalesTopEntity
                 {
                     DateCode = Convert.ToDateTime(rp.Date),
                     UnitId = rp.UnitID,
                     CustomerId = rp.CustomerID
                 }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="TopIndex",
                        Direction=OrderByDirections.Asc
                    } 
                }, 10, 1).Entities;
             }));
            tasks.Add(Task.Factory.StartNew(() =>
            {
                aggUnitDailyEmplEntities = aggUnitDailyEmplBLL.PagedQueryByEntity(new Agg_UnitDaily_EmplEntity
                {
                    DateCode = Convert.ToDateTime(rp.Date),
                    UnitId = rp.UnitID,
                    CustomerId = rp.CustomerID
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="SalesAmount",
                        Direction=OrderByDirections.Desc
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
                aggUnitDailyEmplEntities2 = aggUnitDailyEmplBLL.PagedQueryByEntity(new Agg_UnitDaily_EmplEntity
                {
                    DateCode = Convert.ToDateTime(rp.Date),
                    UnitId = rp.UnitID,
                    CustomerId = rp.CustomerID
                }, new OrderBy[]
                {
                    new OrderBy
                    {
                        FieldName="SetoffCount",
                        Direction=OrderByDirections.Desc
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
                t_UserEntities = t_UserBLL.GetEntitiesByCustomerIdUnitId(rp.CustomerID, rp.UnitID);
            }));
            Task.WaitAll(tasks.ToArray());

            // 当日            
            if (currentAggUnitDailyEntity != null)
            {
                rd.UnitCurrentDaySalesAmount = currentAggUnitDailyEntity.SalesAmount;
                rd.UnitCurrentDayNewVipCount = currentAggUnitDailyEntity.NewVipCount;
                rd.UnitCurrentDayOldVipBackCount = currentAggUnitDailyEntity.OldVipBackCount;
                rd.UnitCurrentDayUseCouponCount = currentAggUnitDailyEntity.UseCouponCount;
            }

            // 昨日            
            if (oldAggUnitDailyEntity != null)
            {
                rd.UnitYestodyDaySalesAmount = oldAggUnitDailyEntity.SalesAmount;
                rd.UnitYestodyDayNewVipCount = oldAggUnitDailyEntity.NewVipCount;
                rd.UnitYestodyDayOldVipBackCount = oldAggUnitDailyEntity.OldVipBackCount;
                rd.UnitYestodyDayUseCouponCount = oldAggUnitDailyEntity.UseCouponCount;
            }

            // 增减
            rd.UnitCurrentDaySalesAmountDoD = ReportCommonBLL.Instance.CalcuDoD(oldAggUnitDailyEntity == null ? null : oldAggUnitDailyEntity.SalesAmount, currentAggUnitDailyEntity == null ? null : currentAggUnitDailyEntity.SalesAmount);
            rd.UnitCurrentDayNewVipCountDoD = ReportCommonBLL.Instance.CalcuDoD(oldAggUnitDailyEntity == null ? null : oldAggUnitDailyEntity.NewVipCount, currentAggUnitDailyEntity == null ? null : currentAggUnitDailyEntity.NewVipCount);
            rd.UnitCurrentDayOldVipBackCountDoD = ReportCommonBLL.Instance.CalcuDoD(oldAggUnitDailyEntity == null ? null : oldAggUnitDailyEntity.OldVipBackCount, currentAggUnitDailyEntity == null ? null : currentAggUnitDailyEntity.OldVipBackCount);
            rd.UnitCurrentDayUseCouponCountDoD = ReportCommonBLL.Instance.CalcuDoD(oldAggUnitDailyEntity == null ? null : oldAggUnitDailyEntity.UseCouponCount, currentAggUnitDailyEntity == null ? null : currentAggUnitDailyEntity.UseCouponCount);

            // 销量榜
            rd.UnitCurrentDayProductSalesTopList = new List<UnitProductSalesTop>();
            if (rUnitProductDaySalesTopEntities != null && rUnitProductDaySalesTopEntities.Length > 0)
            {
                var list = rUnitProductDaySalesTopEntities.ToList();
                list.ForEach(it =>
                {
                    rd.UnitCurrentDayProductSalesTopList.Add(new UnitProductSalesTop
                    {
                        TopIndex = it.TopIndex,
                        ProductName = it.item_name,
                        ProductSKU = it.SkuName,
                        SalesAmount = it.SalesQty
                    });
                });
            }

            // 业绩榜
            rd.UnitCurrentDaySalesAmountEmplTopList = new List<UnitSalesAmountEmplTop>();
            if (aggUnitDailyEmplEntities != null && aggUnitDailyEmplEntities.Length > 0)
            {
                var list = aggUnitDailyEmplEntities.ToList();
                var i = 1;
                foreach (var item in list)
                {
                    var user = t_UserEntities.Where(p => p.user_id == item.EmplID).FirstOrDefault();
                    if(user == null)
                    {
                        continue;
                    }

                    rd.UnitCurrentDaySalesAmountEmplTopList.Add(new UnitSalesAmountEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        SalesAmount = item.SalesAmount
                    });
                    i++;
                }
            }

            // 集客榜
            rd.UnitCurrentDaySetoffEmplTopList = new List<UnitDaySetoffEmplTop>();
            if (aggUnitDailyEmplEntities2 != null && aggUnitDailyEmplEntities2.Length > 0)
            {
                var list = aggUnitDailyEmplEntities2.ToList();
                var i = 1;
                foreach (var item in aggUnitDailyEmplEntities2)
                {
                    var user = t_UserEntities.Where(p => p.user_id == item.EmplID).FirstOrDefault();
                    if (user == null)
                    {
                        continue;
                    }

                    rd.UnitCurrentDaySetoffEmplTopList.Add(new UnitDaySetoffEmplTop
                    {
                        TopIndex = i,
                        EmplName = item.EmplName,
                        SetoffCount = item.SetoffCount
                    });
                    i++;
                }
            }

            //
            return rd;
        }
    }
}
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitWeekReportRD : IAPIResponseData
    {
        /// <summary>
        /// 门店本周销售金额
        /// </summary>
        public decimal? UnitCurrentWeekSalesAmount { get; set; }

        /// <summary>
        /// 门店上周销售金额
        /// </summary>
        public decimal? UnitLastWeekSalesAmount { get; set; }

        /// <summary>
        /// 门店与上周销售增减比
        /// </summary>
        public decimal? UnitCurrentWeekSalesAmountWoW { get; set; }

        /// <summary>
        /// 门店七天每日销售额列表
        /// </summary>
        public List<UnitDaySalesAmount> UnitCurrentWeekSalesAmountList { get; set; }

        /// <summary>
        /// 门店上七天第日销售额列表
        /// </summary>
        public List<UnitDaySalesAmount> UnitLastWeekSalesAmountList { get; set; }

        /// <summary>
        /// 门店本周新增会员数
        /// </summary>
        public int? UnitCurrentWeekNewVipCount { get; set; }

        /// <summary>
        /// 门店上周新增会员数
        /// </summary>
        public int? UnitLastWeekNewVipCount { get; set; }

        /// <summary>
        /// 门店与上周新增会员增减比
        /// </summary>
        public decimal? UnitCurrentWeekNewVipCountWoW { get; set; }

        /// <summary>
        /// 门店七天新增会员数列表
        /// </summary>
        public List<UnitDayNewVipCount> UnitCurrentWeekNewVipCountList { get; set; }

        /// <summary>
        /// 门店上七天新增会员数列表
        /// </summary>
        public List<UnitDayNewVipCount> UnitLastWeekNewVipCountList { get; set; }

        /// <summary>
        /// 门店本周老会员回店数
        /// </summary>
        public int? UnitCurrentWeekOldVipBackCount { get; set; }

        /// <summary>
        /// 门店上周老会员回店数
        /// </summary>
        public int? UnitLastWeekOldVipBackCount { get; set; }

        /// <summary>
        /// 门店与上周老会员回店增减比
        /// </summary>
        public decimal? UnitCurrentWeekOldVipBackCountWoW { get; set; }

        /// <summary>
        /// 门店七天老会员回店数
        /// </summary>
        public List<UnitDayOldVipBackCount> UnitCurrentWeekOldVipBackCountList { get; set; }

        /// <summary>
        /// 门店上七天老会员回店数
        /// </summary>
        public List<UnitDayOldVipBackCount> UnitLastWeekOldVipBackCountList { get; set; }

        /// <summary>
        /// 门店本周优惠券使用数量
        /// </summary>
        public int? UnitCurrentWeekUseCouponCount { get; set; }

        /// <summary>
        /// 门店上周优惠券使用数量
        /// </summary>
        public int? UnitLastWeekUseCouponCount { get; set; }

        /// <summary>
        /// 门店与上周优惠券使用增减比
        /// </summary>
        public decimal? UnitCurrentWeekUseCouponCountWoW { get; set; }

        /// <summary>
        /// 门店七天优惠券使用数量
        /// </summary>
        public List<UnitDayUseCouponCount> UnitCurrentWeekUseCouponCountList { get; set; }

        /// <summary>
        /// 门店上七天优惠券使用数量
        /// </summary>
        public List<UnitDayUseCouponCount> UnitLastWeekUseCouponCountList { get; set; }

        /// <summary>
        /// 商品销量榜，最多10条
        /// </summary>
        public List<UnitProductSalesTop> UnitCurrentWeekProductSalesTopList { get; set; }

        /// <summary>
        /// 员工业绩榜，最多10条
        /// </summary>
        public List<UnitSalesAmountEmplTop> UnitCurrentWeekSalesAmountEmplTopList { get; set; }

        /// <summary>
        /// 员工集客榜，最多10条
        /// </summary>
        public List<UnitWeekSetoffEmplTop> UnitCurrentWeekSetoffEmplTopList { get; set; }
    }

    /// <summary>
    /// 门店日新增加会员数量
    /// </summary>
    public class UnitDayNewVipCount
    {
        /// <summary>
        /// 日期	yyyy-MM-dd
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 门店当日新增加会员数量	
        /// </summary>
        public int? NewVipCount { get; set; }
    }

    /// <summary>
    /// 门店日老会员回店数量
    /// </summary>
    public class UnitDayOldVipBackCount
    {
        /// <summary>
        /// 日期	yyyy-MM-dd
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 门店当日老会员回店数量	
        /// </summary>
        public int? OldVipBackCount { get; set; }
    }

    /// <summary>
    /// 门店日优惠券使用数量
    /// </summary>
    public class UnitDayUseCouponCount
    {
        /// <summary>
        /// 日期	yyyy-MM-dd
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 门店使用优惠券数量
        /// </summary>
        public int? UseCouponCount { get; set; }
    }

    /// <summary>
    /// 周员工集客榜
    /// </summary>
    public class UnitWeekSetoffEmplTop
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int? TopIndex { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmplName { get; set; }

        /// <summary>
        /// 周集客数量
        /// </summary>
        public int? WeekSetoffCount { get; set; }

        /// <summary>
        /// 月集客数量
        /// </summary>
        public int? MonthSetoffCount { get; set; }
    }
}

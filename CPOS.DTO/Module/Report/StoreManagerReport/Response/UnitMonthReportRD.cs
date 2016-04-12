using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitMonthReportRD : IAPIResponseData
    {
        /// <summary>
        /// 门店本月销售金额
        /// </summary>
        public decimal? UnitCurrentMonthSalesAmount { get; set; }

        /// <summary>
        /// 门店上月销售金额
        /// </summary>
        public decimal? UnitLastMonthSalesAmount { get; set; }

        /// <summary>
        /// 门店与上月销售增减比
        /// </summary>
        public decimal? UnitCurrentMonthSalesAmountMoM { get; set; }

        /// <summary>
        /// 门店与上年同期销售增减比
        /// </summary>
        public decimal? UnitCurrentMonthSalesAmountYoY { get; set; }

        /// <summary>
        /// 门店本月新增会员数
        /// </summary>
        public int? UnitCurrentMonthNewVipCount { get; set; }

        /// <summary>
        /// 门店上月新增会员数
        /// </summary>
        public int? UnitLastMonthNewVipCount { get; set; }

        /// <summary>
        /// 门店与上月新增会员增减比
        /// </summary>
        public decimal? UnitCurrentMonthNewVipCountMoM { get; set; }

        /// <summary>
        /// 门店与上年同期新增会员增减比
        /// </summary>
        public decimal? UnitCurrentMonthNewVipCountYoY { get; set; }

        /// <summary>
        /// 门店本月老会员回店数
        /// </summary>
        public int? UnitCurrentMonthOldVipBackCount { get; set; }

        /// <summary>
        /// 门店上月老会员回店数
        /// </summary>
        public int? UnitLastMonthOldVipBackCount { get; set; }

        /// <summary>
        /// 门店与上月老会员回店增减比
        /// </summary>
        public decimal? UnitCurrentMonthOldVipBackCountMoM { get; set; }

        /// <summary>
        /// 门店与上年同期老会员回店增减比
        /// </summary>
        public decimal? UnitCurrentMonthOldVipBackCountYoY { get; set; }

        /// <summary>
        /// 门店本月优惠券使用数量
        /// </summary>
        public int? UnitCurrentMonthUseCouponCount { get; set; }

        /// <summary>
        /// 门店上月优惠券使用数量
        /// </summary>
        public int? UnitLastMonthUseCouponCount { get; set; }

        /// <summary>
        /// 门店与上月优惠券使用增减比
        /// </summary>
        public decimal? UnitCurrentMonthUseCouponCountMoM { get; set; }

        /// <summary>
        /// 门店与上年同期优惠券使用增减比
        /// </summary>
        public decimal? UnitCurrentMonthUseCouponCountYoY { get; set; }

        /// <summary>
        /// 商品销量榜，最多10条
        /// </summary>
        public List<UnitProductSalesTop> UnitCurrentMonthProductSalesTopList { get; set; }

        /// <summary>
        /// 员工业绩榜，最多10条
        /// </summary>
        public List<UnitSalesAmountEmplTop> UnitCurrentMonthSalesAmountEmplTopList { get; set; }

        /// <summary>
        /// 员工集客榜，最多10条
        /// </summary>
        public List<UnitMonthSetoffEmplTop> UnitCurrentMonthSetoffEmplTopList { get; set; }
    }

    /// <summary>
    /// 员工集客榜
    /// </summary>
    public class UnitMonthSetoffEmplTop
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
        /// 月集客数量
        /// </summary>
        public int? MonthSetoffCount { get; set; }

        /// <summary>
        /// 全部集客数量
        /// </summary>
        public int? AllSetoffCount { get; set; }
    }
}

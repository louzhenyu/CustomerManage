using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitDayReportRD : IAPIResponseData
    {
        /// <summary>
        /// 门店当日销售金额
        /// </summary>
        public decimal? UnitCurrentDaySalesAmount { get; set; }

        /// <summary>
        /// 门店昨日销售金额
        /// </summary>
        public decimal? UnitYestodyDaySalesAmount { get; set; }

        /// <summary>
        /// 门店与昨日销售增减比
        /// </summary>
        public decimal? UnitCurrentDaySalesAmountDoD { get; set; }

        /// <summary>
        /// 门店当日新增会员数
        /// </summary>
        public int? UnitCurrentDayNewVipCount { get; set; }

        /// <summary>
        /// 门店昨日新增会员数
        /// </summary>
        public int? UnitYestodyDayNewVipCount { get; set; }

        /// <summary>
        /// 门店与昨日新增会员增减比
        /// </summary>
        public decimal? UnitCurrentDayNewVipCountDoD { get; set; }

        /// <summary>
        /// 门店当日老会员回店数
        /// </summary>
        public int? UnitCurrentDayOldVipBackCount { get; set; }

        /// <summary>
        /// 门店昨日老会员回店数
        /// </summary>
        public int? UnitYestodyDayOldVipBackCount { get; set; }

        /// <summary>
        /// 门店与昨日老会员回店增减比
        /// </summary>
        public decimal? UnitCurrentDayOldVipBackCountDoD { get; set; }

        /// <summary>
        /// 门店当日优惠券使用数量
        /// </summary>
        public int? UnitCurrentDayUseCouponCount { get; set; }

        /// <summary>
        /// 门店昨日优惠券使用数量
        /// </summary>
        public int? UnitYestodyDayUseCouponCount { get; set; }

        /// <summary>
        /// 门店与昨日优惠券使用增减比
        /// </summary>
        public decimal? UnitCurrentDayUseCouponCountDoD { get; set; }

        /// <summary>
        /// 商品销量榜，最多10条
        /// </summary>
        public List<UnitProductSalesTop> UnitCurrentDayProductSalesTopList { get; set; }

        /// <summary>
        /// 员工业绩榜，最多10条
        /// </summary>
        public List<UnitSalesAmountEmplTop> UnitCurrentDaySalesAmountEmplTopList { get; set; }

        /// <summary>
        /// 员工集客榜，最多10条
        /// </summary>
        public List<UnitDaySetoffEmplTop> UnitCurrentDaySetoffEmplTopList { get; set; }
    }

    public class UnitProductSalesTop
    {
        /// <summary>
        /// 排名	从1开始
        /// </summary>
        public int? TopIndex { get; set; }

        /// <summary>
        /// 商品名称	
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string ProductSKU { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public decimal? SalesAmount { get; set; }
    }

    public class UnitSalesAmountEmplTop
    {
        /// <summary>
        /// 排名	从1开始
        /// </summary>
        public int TopIndex { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmplName { get; set; }

        /// <summary>
        /// 销售业绩
        /// </summary>
        public decimal? SalesAmount { get; set; }
    }

    public class UnitDaySetoffEmplTop
    {
        /// <summary>
        /// 排名	从1开始
        /// </summary>
        public int TopIndex { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmplName { get; set; }

        /// <summary>
        /// 集客数量
        /// </summary>
        public int? SetoffCount { get; set; }
    }
}

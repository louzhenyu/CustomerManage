using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetOverViewRD : IAPIResponseData
    {

        /// <summary>
        /// 是否有分销体系  0=没有分销体系，1=有分销体系
        /// </summary>
        public int IsDataNull;
        /// <summary>
        /// 是否有经销的数据
        /// </summary>
        public int IsRTSalesOrder;
        /// <summary>
        /// 销售额信息
        /// </summary>
        public RTSalesInfo RTTotalSales;
        /// <summary>
        /// 分销商总数信息
        /// </summary>
        public RTCountInfo RTTotalCount;
        /// <summary>
        /// 近七天会员、店员销售额信息
        /// </summary>
        public List<Day7RTSalesInfo> DaySevenRTSalesList;
        /// <summary>
        /// 近七天会员、店员分销商数量
        /// </summary>
        public List<Day7RTCountInfo> DaySevenRTCountList;
        /// <summary>
        /// 近7日分销商品订单信息
        /// </summary>
        public DaysRTOrderInfo DaySevenRTOrder;
        /// <summary>
        /// 近30日分销商品订单信息
        /// </summary>
        public DaysRTOrderInfo DayThirtyRTOrder;
        /// <summary>
        /// 近7日活动分销商
        /// </summary>
        public DaysActivityRTInfo DaySevenActivityRT;
        /// <summary>
        /// 近30日活跃分销商
        /// </summary>
        public DaysActivityRTInfo DayThirtyActivityRT;
        /// <summary>
        /// 近7日分享信息
        /// </summary>
        public DaysRTShareInfo DaySevenRTShare;
        /// <summary>
        /// 近30日分享信息
        /// </summary>
        public DaysRTShareInfo DayThirtyRTShare;
        
    }

    /// <summary>
    /// 销售额信息
    /// </summary>
    public class RTSalesInfo
    {
        /// <summary>
        /// 总销量
        /// </summary>
        public decimal? RTTotalSalesAmount { get; set; }

        /// <summary>
        /// 近7天新增销售额
        /// </summary>
        public decimal? RTDay7AddSalesAmount { get; set; }
    }


    /// <summary>
    /// 分销商总数信息
    /// </summary>
    public class RTCountInfo
    {
        /// <summary>
        /// 分销商总人数
        /// </summary>
        public int? RTTotalCount { get; set; }
        /// <summary>
        /// 近7天新增分销商人数
        /// </summary>
        public int? Day7AddRTCount { get; set; }
    }


    /// <summary>
    /// 近七天会员、店员销售额信息
    /// </summary>
    public class Day7RTSalesInfo
    {
        /// <summary>
        /// 统计日
        /// </summary>
        public string DateStr { get; set; }
        /// <summary>
        /// 当天店员分销商销售额
        /// </summary>
        public decimal? DayUserRTSalesAmount { get; set; }
        /// <summary>
        /// 当天会员分销商销售额
        /// </summary>
        public decimal? DayVipRTSalesAmount { get; set; }
    }

    public class Day7RTCountInfo
    {
        /// <summary>
        /// 统计日
        /// </summary>
        public string DateStr { get; set; }
        /// <summary>
        /// 当天新增店员分销商数量
        /// </summary>
        public int? DayAddUserRTCount { get; set; }
        /// <summary>
        /// 当天新增会员分销商数量
        /// </summary>
        public int? DayAddVipRTCount { get; set; }
    }

    public class DaysRTOrderInfo
    {
        /// <summary>
        /// (近7、30日)分销商订单数量
        /// </summary>
        public int? DaysRTOrderCount { get; set; }
        /// <summary>
        /// (近7、30日)分销商订单数量增减比
        /// </summary>
        public decimal? DaysRTOrderCountRate { get; set; }
        /// <summary>
        /// (近7、30日)人均销量
        /// </summary>
        public decimal? DaysTRTAvgAmount { get; set; }
        /// <summary>
        /// (近7、30日)人均销量增减比
        /// </summary>
        public decimal? DaysTRTAvgAmountRate { get; set; }

    }
    /// <summary>
    /// 活跃相关信息
    /// </summary>
    public class DaysActivityRTInfo
    {
        /// <summary>
        /// (近7、30日) 活跃分销商数量
        /// </summary>
        public int? DaysActiveRTCount { get; set; }
        /// <summary>
        /// (近7、30日) 活跃分销商数量增减比
        /// </summary>
        public decimal? DaysRTOrderCountRate { get; set; }
    }

    /// <summary>
    /// 分享相关信息
    /// </summary>
    public class DaysRTShareInfo
    {
        /// <summary>
        /// (近7、30日) 分销商分享次数
        /// </summary>
        public int? DaysRTShareCount { get; set; }
        /// <summary>
        /// (近7、30日) 分销商分享次数增减比
        /// </summary>
        public decimal? DaysRTShareCountRate { get; set; }
        /// <summary>
        /// (近7、30日)新增分销商数量
        /// </summary>
        public int? DaysAddRTCount { get; set; }
        /// <summary>
        /// (近7、30日) 新增分销商数量减比
        /// </summary>
        public decimal? DaysAddRTCountRate { get; set; }
    }
}

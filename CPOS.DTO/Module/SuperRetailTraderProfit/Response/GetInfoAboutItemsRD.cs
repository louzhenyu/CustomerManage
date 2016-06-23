using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class GetInfoAboutItemsRD : IAPIResponseData
    {
        /// <summary>
        /// 近30天商品分享信息
        /// </summary>
        public SharedRTProductInfo SharedRTProduct { get; set; }
        /// <summary>
        /// 近30天商品销售信息
        /// </summary>
        public SalesRTProductInfo SalesRTProduct { get; set; }
        /// <summary>
        ///  近28天商品转化率   
        /// </summary>
        public RTProductCRateInfo RTProductCRate { get; set; }
        /// <summary>
        /// 分享次数最多信息
        /// </summary>
        public List<RTItemInfo> ShareMoreItemsList { get; set; }
        /// <summary>
        /// 销量最高的商品排行信息
        /// </summary>
        public List<RTItemInfo> SalesMoreItemesList { get; set; }
        /// <summary>
        /// 分享次数最少的排行信息
        /// </summary>
        public List<RTItemInfo> ShareLessItemsList { get; set; }
        /// <summary>
        /// 销量最少的商品排行信息
        /// </summary>
        public List<RTItemInfo> SalesLessItemesList { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string DateCode { get; set; }
    }
    /// <summary>
    /// 近30天商品分享信息
    /// </summary>
    public class SharedRTProductInfo 
    {
        /// <summary>
        /// 近30天有分享分销商品数量
        /// </summary>
        public int? Day30SharedRTProductCount { get; set; }
        /// <summary>
        /// 近30天未分享分销商品数量
        /// </summary>
        public int? Day30NoSharedRTProductCount { get; set; }
    }

    /// <summary>
    /// 近30天商品销售信息
    /// </summary>
    public class SalesRTProductInfo
    {
        /// <summary>
        /// 近30天有线上销售分销商品数量
        /// </summary>
        public int? Day30ShareSalesRTProductCount { get; set; }
        /// <summary>
        /// 近30天有线上销售分销商品数量
        /// </summary>
        public int? Day30F2FSalesRTProductCount { get; set; }
    }
    /// <summary>
    /// 近28天分销商品转化信息
    /// </summary>
    public class RTProductCRateInfo
    {
        /// <summary>
        /// 近7天分销商品转换率
        /// </summary>
        public decimal? Day7RTProductCRate { get; set; }
        /// <summary>
        /// 前7天分销商品转换率
        /// </summary>
        public decimal? LastDay7RTProductCRate { get; set; }
        /// <summary>
        /// 前前7天分销商品转换率
        /// </summary>
        public decimal? Last2Day7RTProductCRate { get; set; }
        /// <summary>
        /// 前前前7天分销商品转换率
        /// </summary>
        public decimal? Last3Day7RTProductCRate { get; set; }
    }

    /// <summary>
    /// 商品排行信息
    /// </summary>
    public class RTItemInfo
    {
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ItemImgUrl { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public int? ShareCount { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int? OrderCount { get; set; }
        /// <summary>
        /// 转化率
        /// </summary>
        public decimal? CRate { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public partial class vwPanicbuyingEventEntity
    {

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public vwPanicbuyingEventEntity()
        {
        }
        #endregion

        /// <summary>
        /// 状态描述
        /// </summary>
        public string EventStatusDesc { get; set; }

        public long DeadlineSecond { get; set; }


        public vwPanicbuyingItemInfo[] PanicbuyingItemList { get; set; }

    }
    public partial class vwPanicbuyingItemInfo
    {
        public string ItemId { get; set; }
        public Guid? EventId { get; set; }
        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlThumb { get; set; }
        public string ImageUrlMiddle { get; set; }
        public string ImageUrlBig { get; set; }
        public decimal Price { get; set; }
        public decimal SalesPrice { get; set; }
        public string DiscountRate { get; set; }
        public int? DisplayIndex { get; set; }
        public string ItemCategoryName { get; set; }
        public int? SalesPersonCount { get; set; }
        public string SkuId { get; set; }
        public string CreateDate { get; set; }
        public int? SalesCount { get; set; }
        public string DeadlineTime { get; set; }
        public long DeadlineSecond { get; set; }
        public string AddedTime { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int? Qty { get; set; }
        public int? OverQty { get; set; }
        public string StopReason { get; set; }
        public int Status { get; set; }
        #region 注释属性集
        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemCategoryID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemCode { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemName { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemNameEn { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemNameShort { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Pyzjm { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemRemark { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String CreateBy { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String CreateTime { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String LastUpdateTime { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String LastUpdateBy { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ImageUrl { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String CustomerID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Tel { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemUnit { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Guid? EventId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? EventTypeID { get; set; }

        //public int EventStatus { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? Qty { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? RemainingQty { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime? BeginTime { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime? EndTime { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? RemainingSec { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String UseInfo { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String BuyType { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String OffersTips { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String IsOnline { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Decimal? Price { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Decimal? SalesPrice { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Decimal? DiscountRate { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? DisplayIndex { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? IsFirst { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String StopReason { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? SalesPersonCount { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? DownloadPersonCount { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String BrandID { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String IsIAlumniItem { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String IsExchange { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String IntegralExchange { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Decimal? MonthSalesCount { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemCategoryName { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String SkuId { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Prop1Name { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Prop2Name { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemDisplayIndex { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemTypeDesc { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemSortDesc { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Int32? SalesQty { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Forpoints { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemIntroduce { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ItemParaIntroduce { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String ScanCodeIntegral { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String EdProp { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String FactoryName { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String GG { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public String Degree { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public DateTime? AddTime { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public int Status { get; set; }

        //public string deadlineTime
        //{
        //    get
        //    {
        //        TimeSpan span = TimeSpan.FromSeconds(this.RemainingSec.Value);
        //        return string.Format("{0}天{1}时{2}分{3}秒", span.Days, span.Hours, span.Minutes, span.Seconds);
        //    }
        //}
        #endregion
    }


}

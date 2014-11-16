using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    public partial class StoreDetailViewEntity : BaseEntity
    {
        #region 构造函数
        public StoreDetailViewEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 明细ID
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// 明细名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 价格，原价
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 促销价格
        /// </summary>
        public decimal? SalesPrice { get; set; }

        public string DiscountRate { get; set; }

        public Int64 DisplayIndex { get; set; }

        public string TypeID { get; set; }

        public string TypeCode { get; set; }

        /// <summary>
        /// 优惠券下载地址
        /// </summary>
        public string CouponURL { get; set; }

        /// <summary>
        /// 已预定人数
        /// </summary>
        public int? SalesPersonCount { get; set; }

        public string ItemCategoryName { get; set; }

        public string SkuID { get; set; }

        /// <summary>
        /// 是否已经加入购物车（1=已加入，0=未加入）
        /// </summary>
        public int IsShoppingCart { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }

        public string ItemTypeDesc { get; set; }

        public string ItemSortDesc { get; set; }

        public int? SalesQty { get; set; }

        public string Item_remark { get; set; }

        /// <summary>
        /// 是否积分兑换商品
        /// </summary>
        public string IsExchange { get; set; }

        /// <summary>
        /// 兑换所需积分
        /// </summary>
        public string IntegralExchange { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// 是否满房
        /// </summary>
        public int? IsFull { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public string VipLevelName { get; set; }
        #endregion
    }
}

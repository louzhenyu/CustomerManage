/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class HotelsOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public HotelsOrderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ReservationsVipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReservationsTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Contact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentOrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PaymentTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PaymentTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PaymentStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShopTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CheckDaysQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? RoomQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? StdTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RetailTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? DiscountRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RetailNeedTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PointsDAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CouponDAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OverDAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CashBackAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OrderSource { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ChannelSource { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderStatusDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
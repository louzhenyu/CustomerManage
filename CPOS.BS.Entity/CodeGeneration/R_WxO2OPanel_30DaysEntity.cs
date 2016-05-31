/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:54:11
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
    public partial class R_WxO2OPanel_30DaysEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_WxO2OPanel_30DaysEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxUV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxOrderVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxOrderVipPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxPV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxOrderPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WxOrderMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WxOrderPayMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WxOrderAVG { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxUV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxOrderVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxOrderVipPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxPV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxOrderCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastWxOrderPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastWxOrderMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastWxOrderPayMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? LastWxOrderAVG { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderVipCount_UV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderVipPayCount_OrderVipCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderVipPayCount_UV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_UV_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_PV_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderVipCount_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderCount_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderMoney_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderVipPayCount_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderPayCount_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderPayMoney_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Rate_OrderAVG_Last { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogIDs { get; set; }


        #endregion

    }
}
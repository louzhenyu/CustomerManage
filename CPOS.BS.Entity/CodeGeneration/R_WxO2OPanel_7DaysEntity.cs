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
    public partial class R_WxO2OPanel_7DaysEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_WxO2OPanel_7DaysEntity()
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
		public Int32? OfflineUV { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? WxOrderPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OfflineOrderPayCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WxOrderPayMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OfflineOrderPayMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WxOrderAVG { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OfflineOrderAVG { get; set; }

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
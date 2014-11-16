/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/14 10:15:25
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
    public partial class TPaymentOrderLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TPaymentOrderLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? PaymentOrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ChannelID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? AppOrderTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AppOrderAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppOrderDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Currency { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MobileNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ReturnUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DynamicID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DynamicIDType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ResultCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QrCodeUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}
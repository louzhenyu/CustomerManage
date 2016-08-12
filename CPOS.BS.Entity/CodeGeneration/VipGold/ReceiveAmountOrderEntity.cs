/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/13 18:00:26
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
    /// 实体： 10：支付成功   90：支付失败 
    /// </summary>
    public partial class ReceiveAmountOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ReceiveAmountOrderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServiceUnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServiceUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? VipDiscount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TransAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AmountFromPayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? CouponUsePay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AmountAcctPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayDatetTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TimeStamp { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}
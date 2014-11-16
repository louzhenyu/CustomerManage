/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/9 15:19:33
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
    public partial class CardDepositEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CardDepositEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? CardDepositId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CardNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CardPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SerialNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VerifyCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BatchId { get; set; }

		/// <summary>
		/// 门店Id
		/// </summary>
		public String UnitId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ChannelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DepositTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Bonus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ConsumedAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CouponQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CardStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UseStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

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


        #endregion

    }
}
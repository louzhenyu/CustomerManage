/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:10:21
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
    public partial class CustomerOrderPayEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomerOrderPayEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? OrderPayId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChannelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SerialPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PayAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? WithdrawalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReceivablesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderSource { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderPayStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PayTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ArriveTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? WithdrawalTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PlayMoneyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? WithdrawalId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FailureReason { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}
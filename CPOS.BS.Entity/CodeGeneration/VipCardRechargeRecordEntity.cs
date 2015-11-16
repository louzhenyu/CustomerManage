/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:27
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
    public partial class VipCardRechargeRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardRechargeRecordEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String RechargeRecordID { get; set; }

		/// <summary>
		/// 会员卡标识
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// 充值金额
		/// </summary>
		public Decimal? RechargeAmount { get; set; }

		/// <summary>
		/// 充值前卡内余额
		/// </summary>
		public Decimal? BalanceBeforeAmount { get; set; }

		/// <summary>
		/// 充值后卡内余额
		/// </summary>
		public Decimal? BalanceAfterAmount { get; set; }

		/// <summary>
		/// 充值小票号
		/// </summary>
		public String RechargeNo { get; set; }

		/// <summary>
		/// 充值方式
		/// </summary>
		public String PaymentTypeID { get; set; }

		/// <summary>
		/// 充值时间
		/// </summary>
		public DateTime? RechargeTime { get; set; }

		/// <summary>
		/// 充值操作员
		/// </summary>
		public String RechargeUserID { get; set; }

		/// <summary>
		/// 充值门店
		/// </summary>
		public String UnitID { get; set; }

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
		public String CustomerID { get; set; }


        #endregion

    }
}
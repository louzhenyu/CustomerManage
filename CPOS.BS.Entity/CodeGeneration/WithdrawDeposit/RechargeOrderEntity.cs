/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-19 17:43:07
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
    public partial class RechargeOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public RechargeOrderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? OrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// 会员卡号
		/// </summary>
		public String VipCardNo { get; set; }

		/// <summary>
		/// 门店编号
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 金额
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 实付金额
		/// </summary>
		public Decimal? ActuallyPaid { get; set; }

		/// <summary>
		/// 回馈金额
		/// </summary>
		public Decimal? ReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? PayPoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReceivePoints { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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


        #endregion

    }
}
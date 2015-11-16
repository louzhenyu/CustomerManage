/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:01
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
    public partial class VipCardBalanceChangeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardBalanceChangeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ChangeID { get; set; }

		/// <summary>
		/// 会员卡号
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 变动金额
		/// </summary>
		public Decimal? ChangeAmount { get; set; }

		/// <summary>
		/// 变动前余额
		/// </summary>
		public Decimal? ChangeBeforeBalance { get; set; }

		/// <summary>
		/// 变动后余额
		/// </summary>
		public Decimal? ChangeAfterBalance { get; set; }

		/// <summary>
		/// 单号
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 关联单号
		/// </summary>
		public String RelatedOrderNo { get; set; }

		/// <summary>
		/// 门店标识
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 门店编码
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 变动时间
		/// </summary>
		public DateTime? ChangeTime { get; set; }

		/// <summary>
		/// 变动原因
		/// </summary>
		public String ChangeReason { get; set; }

		/// <summary>
		/// 操作员
		/// </summary>
		public String OperUser { get; set; }

		/// <summary>
		/// 状态（1=正常，4=退款））
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark { get; set; }

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
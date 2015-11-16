/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    public partial class VipAmountDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipAmountDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? VipAmountDetailId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Amount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? UsedReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectiveDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DeadlineDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AmountSourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsValid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsWithdrawCash { get; set; }

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
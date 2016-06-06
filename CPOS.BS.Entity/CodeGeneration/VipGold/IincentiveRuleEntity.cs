/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 14:15:31
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
    public partial class IincentiveRuleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public IincentiveRuleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? IincentiveRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SetoffEventID { get; set; }

		/// <summary>
		/// 1 会员集客   2 员工集客
		/// </summary>
		public Int32? SetoffType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 1:现金 2：积分
		/// </summary>
		public Int32? SetoffRegAwardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SetoffRegPrize { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SetoffOrderPer { get; set; }

		/// <summary>
		/// 空或者0表示单单有效，>0表示具体限制次数
		/// </summary>
		public Int32? SetoffOrderTimers { get; set; }

		/// <summary>
		/// 10：使用中   90：失效
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

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
		/// 0 全部门店   1 指定门店 
		/// </summary>
		public Int32? ApplyUnit { get; set; }


        #endregion

    }
}
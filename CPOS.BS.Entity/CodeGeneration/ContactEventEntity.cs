/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/26 20:41:45
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
    public partial class ContactEventEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ContactEventEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ContactEventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ContactEventName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 1：积分，2：优惠券，3：参加活动次数
		/// </summary>
		public Int32? RewardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChanceCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

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


        #endregion

    }
}
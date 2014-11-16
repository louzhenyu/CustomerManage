/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/8 15:53:25
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
    public partial class WXAlarmNoticeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXAlarmNoticeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? AlarmNoticeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AlarmNoticeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AlarmNoticeRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AlarmNoticeDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AlarmNoticeStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Priority { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RequestBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FactBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProposalPlan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FactPlan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? RequestTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? FactTime { get; set; }

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
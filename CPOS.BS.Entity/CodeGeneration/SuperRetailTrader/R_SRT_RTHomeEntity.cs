/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
    public partial class R_SRT_RTHomeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_RTHomeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30ActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30NoActiveRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30SalesRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30SalesExpandRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30SalesNoExpandRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30ExpandRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30JoinSalesRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Day30JoinNoSalesRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }


        #endregion

    }
}
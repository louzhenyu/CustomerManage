/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/13 16:48:24
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
    public partial class TInOutStatusNodeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInOutStatusNodeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? NodeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NodeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NodeValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PreviousValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String NextValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayMethod { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
        public string DeliveryMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionDescEn { get; set; }

        #endregion

    }
}
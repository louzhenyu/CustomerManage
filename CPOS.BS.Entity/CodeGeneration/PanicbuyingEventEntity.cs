/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:19
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
    public partial class PanicbuyingEventEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PanicbuyingEventEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? EventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventRemark { get; set; }

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
		public Int32? EventStatus { get; set; }



        	/// <summary>
		/// 
		/// </summary>
        public Int32? IsCTW { get; set; }
        
		/// </summary>
		public Int32? PromotePersonCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BargainPersonCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemQty { get; set; }
        #endregion

    }
}
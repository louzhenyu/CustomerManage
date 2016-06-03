/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/14 16:14:12
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
    public partial class T_LEventsSharePersonLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_LEventsSharePersonLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? SharePersonLogId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BusTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShareVipID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShareOpenID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BeShareVipID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BeShareOpenID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ShareURL { get; set; }

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
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ShareVipType { get; set; }

        #endregion

    }
}
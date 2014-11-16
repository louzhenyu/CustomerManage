/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 18:49:15
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
    public partial class TOrderPushStrategyEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TOrderPushStrategyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OrderStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsIOSPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAndroidPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsWXPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsSMSPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsEmailPush { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PushInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PushObject { get; set; }

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


        #endregion

    }
}
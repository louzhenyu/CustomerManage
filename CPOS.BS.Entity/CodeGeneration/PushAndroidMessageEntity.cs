/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 13:28:20
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
    public partial class PushAndroidMessageEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PushAndroidMessageEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String AndroidMessageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ConnUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ChannelIDBaiDu { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserIDBaiDu { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? PushType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DeviceType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MessageKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MessageExpires { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TagName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BaiduPushAppID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FailureReason { get; set; }

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
		public Int32? MessageType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MessagePushType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}
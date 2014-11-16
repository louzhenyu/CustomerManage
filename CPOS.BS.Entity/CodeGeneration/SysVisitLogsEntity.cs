/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/31 16:47:21
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
    public partial class SysVisitLogsEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysVisitLogsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String LogsID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Plat { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Version { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IpAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ChannelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeviceToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SessionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Locale { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OSInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SpecialParams { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ResultCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ResultDescription { get; set; }

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
		public String WeiXinId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    /// 用户连接WiFi 
    /// </summary>
    public partial class WiFiUserVisitEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WiFiUserVisitEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 主键
		/// </summary>
		public Guid? VisitID { get; set; }

		/// <summary>
        /// 用户ID
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
        /// 门店ID
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
        /// 当前设备(当连接不同设备时，需要更新此字段)
		/// </summary>
		public Guid? CurrentDeviceID { get; set; }

		/// <summary>
        /// 日期
		/// </summary>
		public DateTime? CurrentDate { get; set; }

		/// <summary>
        /// 连接时间
		/// </summary>
		public DateTime? BeginTime { get; set; }

		/// <summary>
        /// 退出时间(当移动到另一位置时，需要将此字段置空)
		/// </summary>
		public DateTime? EndTime { get; set; }

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


        #endregion

    }
}
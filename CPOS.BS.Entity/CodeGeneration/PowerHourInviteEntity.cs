/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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
    public partial class PowerHourInviteEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PowerHourInviteEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 标识
		/// </summary>
		public Guid? PowerHourInviteID { get; set; }

		/// <summary>
		/// PowerHour(外键)
		/// </summary>
		public String PowerHourID { get; set; }

		/// <summary>
		/// 邀请人的UserID(外键)
		/// </summary>
		public String InvitorUserID { get; set; }

		/// <summary>
		/// 被邀请人的UserID(外键)
		/// </summary>
		public String StaffUserID { get; set; }

		/// <summary>
		/// 接受状态
		/// </summary>
		public Int32? AcceptState { get; set; }

		/// <summary>
		/// 出席状态
		/// </summary>
		public Int32? Attendence { get; set; }

		/// <summary>
		/// 客户ID
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
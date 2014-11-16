/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:22:22
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
    public partial class CustomerBackEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CustomerBackEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? CustomerBackId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ReceivingBank { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenBank { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BankAccount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ChannelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WithdrawalPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MD5Pwd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BackStatus { get; set; }

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
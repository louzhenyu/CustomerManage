/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/10 10:39:07
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
    public partial class WXRightOrdersEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WXRightOrdersEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? RightOrdersId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FeedBackId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Solution { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ExtInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TimeStamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MsgType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppSignature { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HandleBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HandlePlan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? HandleTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AssignBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AssignPlan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? AssignTime { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String ConfirmReason { get; set; }


        #endregion

    }
}
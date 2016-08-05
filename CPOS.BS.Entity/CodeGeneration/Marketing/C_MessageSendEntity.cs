/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/5 16:49:40
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
    public partial class C_MessageSendEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public C_MessageSendEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? SendID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? MessageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// SMS=短信   WeChat=微信   Email=邮件
		/// </summary>
		public String MessageType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Phone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ActualSendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Priority { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 0=未发送；   1=发送中；   2=已发送；   3=发送失败
		/// </summary>
		public Int32? IsSend { get; set; }

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
		/// 0=正常状态；1=删除状态
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DateDiffDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponCategory { get; set; }


        #endregion

    }
}
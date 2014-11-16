/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 13:31:25
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
    /// 订单状态信息记录  
    /// </summary>
    public partial class TInoutStatusEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutStatusEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// ID
		/// </summary>
		public Guid? InoutStatusID { get; set; }

		/// <summary>
        /// 订单ID
		/// </summary>
		public string OrderID { get; set; }

		/// <summary>
        /// 订单状态
		/// </summary>
		public int? OrderStatus { get; set; }

		/// <summary>
        /// 未审核理由
		/// </summary>
		public int? CheckResult { get; set; }

		/// <summary>
        /// 支付方式
		/// </summary>
		public int? PayMethod { get; set; }

		/// <summary>
        /// 配送公司
		/// </summary>
		public string DeliverCompanyID { get; set; }

		/// <summary>
        /// 配送单号
		/// </summary>
		public string DeliverOrder { get; set; }

		/// <summary>
        /// 图片
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
        /// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
        /// 客户ID
		/// </summary>
		public string CustomerID { get; set; }

		/// <summary>
        /// 创建人
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// 最后修改人
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// 最后修改时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// 是否删除
		/// </summary>
		public int? IsDelete { get; set; }

        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string StatusRemark { get; set; }

        #endregion

    }
}
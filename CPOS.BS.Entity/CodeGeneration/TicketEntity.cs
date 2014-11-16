/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/7 15:07:52
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
    public partial class TicketEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TicketEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 票务ID
		/// </summary>
		public Guid? TicketID { get; set; }

		/// <summary>
		/// 票务名称
		/// </summary>
		public string TicketName { get; set; }

		/// <summary>
		/// 票务备注
		/// </summary>
		public string TicketRemark { get; set; }

		/// <summary>
		/// 票务价格
		/// </summary>
		public decimal? TicketPrice { get; set; }

		/// <summary>
		/// 票务数量
		/// </summary>
		public int? TicketNum { get; set; }

		/// <summary>
		/// 票务排序
		/// </summary>
		public int? TicketSort { get; set; }

		/// <summary>
		/// 所属活动
		/// </summary>
		public string EventID { get; set; }

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
		/// 客户标识
		/// </summary>
		public string CustomerID { get; set; }


        #endregion

    }
}
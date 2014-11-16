/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/18 16:28:29
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
    /// 实体： 自定义拜访对象 
    /// </summary>
    public partial class VisitingObjectEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingObjectEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingObjectID { get; set; }

		/// <summary>
		/// 对象名称
		/// </summary>
		public string ObjectName { get; set; }

		/// <summary>
		/// 状态(0-未启用,1-启用)
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		/// 对象分组(0-门店拜访对象,1-门店检核对象 关联Options表)
		/// </summary>
		public int? ObjectGroup { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// 排版方式
		/// </summary>
		public int? LayoutType { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}
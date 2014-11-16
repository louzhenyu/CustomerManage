/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 15:25:07
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
    public partial class EclubIndustryEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EclubIndustryEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 主键ID
		/// </summary>
		public Guid? IndustryID { get; set; }

		/// <summary>
        /// 用户ID
		/// </summary>
		public String ParentID { get; set; }

		/// <summary>
        /// 行业等级，分别为1.第一级,2.第二级
		/// </summary>
		public Int32? IndustryType { get; set; }

		/// <summary>
        /// 行业名称
		/// </summary>
		public String IndustryName { get; set; }

		/// <summary>
        /// 行业排序
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
        /// 行业描述
		/// </summary>
		public String Description { get; set; }

		/// <summary>
        /// 客户ID
		/// </summary>
		public String CustomerId { get; set; }

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
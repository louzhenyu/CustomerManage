/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体： 终端/经销商分组定义 
    /// </summary>
    public partial class POPGroupEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public POPGroupEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public int? POPGroupID { get; set; }

		/// <summary>
		/// 终端类别(0-门店,1-经销商)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// 分组编号
		/// </summary>
		public string GroupNo { get; set; }

		/// <summary>
		/// 分组名称
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string GroupNameEn { get; set; }

		/// <summary>
		/// 分组条件
		/// </summary>
		public string GroupCondition { get; set; }

		/// <summary>
		/// sql语句
		/// </summary>
		public string SqlTemplate { get; set; }

		/// <summary>
		/// 是否需要根据条件自动填充
		/// </summary>
		public int? IsAutoFill { get; set; }

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
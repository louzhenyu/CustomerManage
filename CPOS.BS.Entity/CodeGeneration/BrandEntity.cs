/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:35:19
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
    /// 实体： 品牌 
    /// </summary>
    public partial class BrandEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BrandEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Int32? BrandID { get; set; }

		/// <summary>
		/// 品牌编号
		/// </summary>
		public String BrandNo { get; set; }

		/// <summary>
		/// 品牌名称
		/// </summary>
		public String BrandName { get; set; }

		/// <summary>
		/// 品牌名称(英文)
		/// </summary>
		public String BrandNameEn { get; set; }

		/// <summary>
		/// 是否自有品牌(0-否,1-是)
		/// </summary>
		public Int32? IsOwner { get; set; }

		/// <summary>
		/// 所属公司
		/// </summary>
		public String Firm { get; set; }

		/// <summary>
		/// 品牌等级
		/// </summary>
		public Int32? BrandLevel { get; set; }

		/// <summary>
		/// 是否叶子节点(0-否,1-是)
		/// </summary>
		public Int32? IsLeaf { get; set; }

		/// <summary>
		/// 上级品牌标识
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 客户标识
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// 客户经销商标识
		/// </summary>
		public Int32? ClientDistributorID { get; set; }

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
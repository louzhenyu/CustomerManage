/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/11/4 14:54:54
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
    public partial class APPUpgradeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APPUpgradeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? AppUpgradeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Plat { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IOSUpgradeUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AndroidUpgradeUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IOSUpgradeCon { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AndroidUpgradeCon { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Versions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AppName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsMandatoryUpdate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ServerUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
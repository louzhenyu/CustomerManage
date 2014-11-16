/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 16:29:54
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
    public partial class APPVersionManagerEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APPVersionManagerEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VersionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Channel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Plat { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PlatName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VersionNoUpdate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VersionNoLowest { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Notice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UserScope { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DownloadURL { get; set; }

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
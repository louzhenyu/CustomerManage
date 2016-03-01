/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/7 15:43:31
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
    public partial class SysPageEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysPageEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? PageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModuleName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsEntrance { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String URLTemplate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String JsonValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAuth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsRebuild { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Version { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Author { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DefaultHtml { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		public Int32? CustomerVisible { get; set; }


        #endregion

    }
}
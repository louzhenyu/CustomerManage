/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:19
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
    public partial class MarketSignInEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MarketSignInEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String SignInID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventID { get; set; }

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


        #endregion

    }
}
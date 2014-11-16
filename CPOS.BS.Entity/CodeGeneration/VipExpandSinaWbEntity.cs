/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/1 15:59:29
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
    public partial class VipExpandSinaWbEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipExpandSinaWbEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AccessToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Appkey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Scope { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateAt { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ExpireIn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ScreenName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LabelName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Province { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String City { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Location { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Url { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProfileImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ProfileUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Gender { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Weihao { get; set; }

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
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-10-18 9:39:00
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
    public partial class T_TypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_TypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_domain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? type_system_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? type_Level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }


        #endregion

    }
}
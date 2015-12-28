/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/7/6 16:12:43
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
    public partial class T_PropEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_PropEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String prop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_eng_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String parent_prop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? prop_level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_domain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_input_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? prop_max_lenth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_default_value { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? display_index { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }


        #endregion

    }
}
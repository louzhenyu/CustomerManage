/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-5-9 17:56:01
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
    public partial class T_RoleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_RoleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String role_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String def_app_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_eng_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? is_sys { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String table_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? org_level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_id { get; set; }


        #endregion

    }
}
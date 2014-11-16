/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
    public partial class t_customerEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_customerEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_post_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_contacter { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_fax { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_cell { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_memo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? customer_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? sys_modify_stamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_start_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String is_approve { get; set; }


        #endregion

    }
}